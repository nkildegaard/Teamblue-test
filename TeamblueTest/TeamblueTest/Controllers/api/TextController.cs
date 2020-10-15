using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamblueTest.Models;

namespace TeamblueTest.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        [HttpPost]
        public TextResponse Index(TextRequest request)
        {
            return new TextResponse
            { 
                DistinctUniqueWords = 4, WatchlistWords = new List<string> { "A", "B", "C" } 
            };
        }
    }
}