using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamblueTest.Models
{
    public class TextRequest
    {
        public string Text { get; set; }
    }

    public class TextResponse
    {
        public int DistinctUniqueWords { get; set; }
        public IEnumerable<string> WatchlistWords { get; set; }
    }
}
