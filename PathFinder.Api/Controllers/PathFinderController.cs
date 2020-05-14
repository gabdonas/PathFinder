using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("PathFinder")]
    public class PathFinderController : ControllerBase
    {
        private readonly IPathFinder _pathFinder;

        public PathFinderController(IPathFinder pathFinder)
        {
            this._pathFinder = pathFinder;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("This is pathfinder");
        }

        [HttpPost]
        public ActionResult Post([FromBody] JsonElement body)
        {
            var arrays = GetArrays(body);
            if (arrays.Any(x => x.Length < 2))
            {
                ModelState.AddModelError("", "Parameter should be a valid array of at least two integers");
                return BadRequest(ModelState);
            }

            return Ok(_pathFinder.Find(arrays));
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
