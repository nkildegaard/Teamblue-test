using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TeamblueTest.DatabaseModels
{
    public class WatchListWord
    {
        [Key]
        public string Word { get; set; }
    }
}
