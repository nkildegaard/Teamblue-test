using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamblueTest.DatabaseModels;
using TeamblueTest.Models;

namespace TeamblueTest.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly TeamBlueContext db;

        public TextController(TeamBlueContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("")]
        public TextResponse Index(TextRequest request)
        {
            //Split the text into words. Perhaps we could do a more solid approach later, that will sort out punctuation and blank space etc. 
            var foundWords = request.Text.Split(" ");

            //Find the distinct words
            var possibleNewWords = foundWords.Distinct();

            //Find the words that are already in the database.
            var notDistinctWords = db.UniqueWords.Where(x => possibleNewWords.Contains(x.Word)).Select(x => x.Word).ToList();

            //Find the words that aren't in the database
            var newDistinctWords = possibleNewWords.Except(notDistinctWords);

            db.UniqueWords.AddRange(newDistinctWords.Select(x => new UniqueWord { Word = x }));

            //Find the words that are on the watch list
            var watchListWords = db.WatchListWords.Where(x => possibleNewWords.Contains(x.Word)).Select(x => x.Word).ToList();

            db.SaveChanges();

            int newDistinctWordsCount = newDistinctWords.Count();

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