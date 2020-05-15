using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PathFinder.Api.Model;

namespace PathFinder.Api.Data
{
    public class PathFinderContext: DbContext
    {
        public PathFinderContext (DbContextOptions<PathFinderContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PathResult>().OwnsOne(x => x.);
        //    // Your code setting up foreign keys of whatever!
        //}
        public DbSet<PathResult> PathResult { get; set; }
    }
}
