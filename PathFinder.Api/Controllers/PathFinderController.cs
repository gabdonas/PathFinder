using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Model.Api;
using PathFinder.Api.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("PathFinder")]
    public class PathFinderController : ControllerBase
    {
        private readonly IPathFinderService _pathFinderService;

        public PathFinderController(IPathFinderService pathFinderService)
        {
            _pathFinderService = pathFinderService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("This is pathfinder");
        }

        [HttpPost]
        public async Task<ActionResult> Post(PathFinderBatchParameterModel model, CancellationToken cancellationToken)
        {

            if (model.Arrays.Any(x => x.Length < 2))
            {
                ModelState.AddModelError("Arrays", "Should contain valid arrays of at least two integers");
                return BadRequest(ModelState);
            }

            var result = await _pathFinderService.Process(model.Arrays, cancellationToken);

            return Ok(result);
        }


    }
}
