using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamblueTest.DatabaseModels;
using TeamblueTest.Models;


namespace TeamblueTest.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly TeamBlueContext db;
        private readonly ILogger<TextController> _logger;

        public TextController(TeamBlueContext db, ILogger<TextController> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<TextResponse> Index(TextRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Text))
            {
                _logger.LogError("Request does not have a Text");
                return BadRequest();
            }

            //Clean the text for punctuation and set it to lower case
            var cleanText = new string(request.Text.ToLower().Where(c => !Char.IsPunctuation(c)).ToArray());

            //Split the text into words
            char[] delimiterChars = { ' ', '\t', '\n', '\r' };
            var foundWords = cleanText.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

            //Find the distinct words
            var possibleNewWords = foundWords.Distinct().ToList().OrderBy(x=>x);

            //Find the words that are already in the database.
            var notDistinctWordsQuery = db.UniqueWords.Where(x => possibleNewWords.Contains(x.Word)).Select(x => x.Word);
            var notDistinctWords = notDistinctWordsQuery.ToList();

            //Find the words that aren't in the database
            var newDistinctWords = possibleNewWords.Except(notDistinctWords).ToList();
            int newDistinctWordsCount = newDistinctWords.Count();

            //Add the words to the database
            db.UniqueWords.AddRange(newDistinctWords.Select(x => new UniqueWord { Word = x }));

            //Find the words that are on the watch list
            var watchListWords = db.WatchListWords.Where(x => possibleNewWords.Contains(x.Word)).Select(x => x.Word).ToList();

            db.SaveChanges();

            return new TextResponse
            {
                DistinctUniqueWords = newDistinctWordsCount,
                WatchlistWords = watchListWords
            };
        }

        //Test function that will clear the table
        [HttpPost]
        [Route("Clear")]
        public void Clear()
        {
            //This can be done more efficient with some specific sql function, but this was faster to write
            db.UniqueWords.RemoveRange(db.UniqueWords);
            db.SaveChanges();
        }

        //Test function that will clear the watch list
        [HttpPost]
        [Route("ClearWatchList")]
        public void ClearWatchList()
        {
            //This can be done more efficient with some specific sql function, but this was faster to write
            db.WatchListWords.RemoveRange(db.WatchListWords);
            db.SaveChanges();
        }

        //Test function that will add a word to the watch list
        [HttpPost]
        [Route("AddToWatchList")]
        public void AddToWatchList(WatchListAdd request)
        {
            db.WatchListWords.Add(new WatchListWord { Word = request.Word });
            db.SaveChanges();
        }
    }
}