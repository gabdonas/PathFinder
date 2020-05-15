using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PathFinder.Api.Model;

namespace PathFinder.Api.Data
{
    public class PathFinderContext: DbContext
    {
        public PathFinderContext (DbContextOptions<PathFinderContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var valueComparer = new ValueComparer<int[]>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray());
           
            var converter = new ValueConverter<int[], string>(
                v => Utils.ArrayToStr(v),
                v => Utils.StrToArray(v));
           
            modelBuilder
                .Entity<PathResult>()
                .Property(e => e.InputArray)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);
            modelBuilder
                .Entity<PathResult>()
                .Property(e => e.ResultArray)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);;

        }
        public DbSet<PathResult> PathResult { get; set; }
    }
}
