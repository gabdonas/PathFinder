using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PathFinder.Api.Model.Api
{
    public class PathFinderBatchParameterModel
    {
        [Required]
        public List<int[]> Arrays { get; set; }
    }
}
