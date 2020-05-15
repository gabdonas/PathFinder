using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PathFinder.Api.Data;
using PathFinder.Api.Model;

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
        public ActionResult Post([FromBody] JsonElement body)
        {
            var resultList = new List<PathFinderResultApiModel>();
            var arrays = GetArrays(body);
            if (arrays.Any(x => x.Length < 2))
            {
                ModelState.AddModelError("", "Parameter should be a valid array of at least two integers");
                return BadRequest(ModelState);
            }

            foreach (var array in arrays)
            {
                var key = Utils.ArrayToStr(array);
                var storedResult = _context.PathResult.FirstOrDefault(x => x.InputArrayString == key);
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

        private static int[][] GetArrays(JsonElement body)
        {
            if (body.ValueKind == JsonValueKind.Array)
            {
                if (body.GetArrayLength() > 1)
                {
                    if (body[0].ValueKind == JsonValueKind.Array)
                        return JsonSerializer.Deserialize<int[][]>(body.ToString());
                    else
                        return new[] { JsonSerializer.Deserialize<int[]>(body.ToString()) };
                }
            }
            else
            {
                return GetArrays(body.EnumerateObject().First().Value);

            }
            return new int[][] { };
        }
    }
}
