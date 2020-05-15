using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.Api.Model
{
    public class PathResult
    {
        [Key]
        public int Id { get; set; }
        public int[] InputArray { get; set; }
        public bool IsTraversable { get; set; }
        public int[] ResultArray { get; set; }
    }
}
