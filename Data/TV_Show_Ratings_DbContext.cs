using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TV_Show_Ratings_MVC.Models;

namespace TV_Show_Ratings_MVC.Data
{
    public class TV_Show_Ratings_DbContext : DbContext
    {
        public TV_Show_Ratings_DbContext (DbContextOptions<TV_Show_Ratings_DbContext> options)
            : base(options)
        {
        }

        public DbSet<TV_Show_Ratings_MVC.Models.Subscriber> Subscriber { get; set; }

        public DbSet<TV_Show_Ratings_MVC.Models.Rating> Rating { get; set; }

        public DbSet<TV_Show_Ratings_MVC.Models.TVChannel> TVChannel { get; set; }

        public DbSet<TV_Show_Ratings_MVC.Models.TVShow> TVShow { get; set; }
    }
}
