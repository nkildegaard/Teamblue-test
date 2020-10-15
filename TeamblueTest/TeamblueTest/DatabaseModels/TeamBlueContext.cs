using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TeamblueTest.DatabaseModels
{
    public class TeamBlueContext : DbContext
    {
        public TeamBlueContext(DbContextOptions<TeamBlueContext> options)
            : base(options)
        { }

        //public TeamBlueContext(string connectionString)
        //    : base(connectionString)
        //{ }

        public DbSet<UniqueWord> UniqueWords { get; set; }
        public DbSet<WatchListWord> WatchListWords { get; set; }

    }
}
