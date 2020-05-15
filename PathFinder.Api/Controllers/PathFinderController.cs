using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Api.Data;
using PathFinder.Api.Model;
using PathFinder.Api.Model.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("PathFinder")]
    public class PathFinderController : ControllerBase
    {
        private readonly PathFinderContext _context;
        private readonly IPathFinder _pathFinder;

        public PathFinderController(IPathFinder pathFinder, PathFinderContext context)
        {
            _pathFinder = pathFinder;
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("This is pathfinder");
        }

        [HttpPost]
        public ActionResult Post(PathFinderParameterModel model)
        {
            var resultList = new List<PathFinderResultApiModel>();

            if (model.Arrays.Any(x => x.Length < 2))
            {
                ModelState.AddModelError("Arrays", "Should contain valid arrays of at least two integers");
                return BadRequest(ModelState);
            }

            foreach (var array in model.Arrays)
            {
                //var key = Utils.ArrayToStr(array);
                var storedResult = _context.PathResult.FirstOrDefault(x => x.InputArray == array);
                if (storedResult != null)
                    resultList.Add(storedResult.ToApiModel(true));
                else
                {
                    var result = _pathFinder.Find(array);
                    var pathResult = new PathResult()
                    {
                        InputArray = array,
                        IsTraversable = result.IsTraversable,
                        ResultArray = result.Indices
                    };
                    _context.PathResult.Add(pathResult);
                    _context.SaveChanges();
                    resultList.Add(pathResult.ToApiModel(false));
                }
            }

            return Ok(resultList);
        }


    }
}
