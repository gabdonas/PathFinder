
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
