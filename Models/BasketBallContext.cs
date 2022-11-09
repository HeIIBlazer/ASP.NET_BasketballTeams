using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BasketBall_JPTV20.Models
{
    public class BasketBallContext:DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }
    }
}