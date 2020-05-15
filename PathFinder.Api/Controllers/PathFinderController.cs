using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PathFinder.Api.Model.Api;
using PathFinder.Api.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

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
            return Ok($"This is pathfinder. Use POST with body {{\"array\":[...]}}, or go to {HttpContext.Request.GetEncodedUrl()}/results to review ");
        }

        [HttpGet]
        [Route("Results")]
        public async Task<ActionResult> Get(int? limit, int? offset, CancellationToken cancellationToken)
        {
            limit ??= 10;
            offset ??= 0;
            IEnumerable<PathFinderResultApiModel> list = await _pathFinderService.GetResults(limit.Value, offset.Value, cancellationToken);
            return Ok(new {limit, offset, results = list });
        }

        [HttpGet]
        [Route("Results/{id}")]
        public async Task<ActionResult> Get(int id, CancellationToken cancellationToken)
        {
            PathFinderResultApiModel result = await _pathFinderService.GetResultById(id, cancellationToken);
            return Ok(result);
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
