using Microsoft.EntityFrameworkCore;
using PathFinder.Api.Data;
using PathFinder.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PathFinder.Api.Model.Api;

namespace PathFinder.Api.Services
{
    public interface IPathFinderService
    {
        Task<IEnumerable<PathFinderResultApiModel>> Process(List<int[]> arrays, CancellationToken cancellationToken);
        Task<PathFinderResultApiModel> Process(int[] array, CancellationToken cancellationToken);
    }

    public class PathFinderService : IPathFinderService
    {
        private readonly IPathFinder _pathFinder;
        private DbContextOptions<PathFinderContext> _options;

        public PathFinderService(IPathFinder pathFinder, DbContextOptions<PathFinderContext> options)
        {
            _pathFinder = pathFinder;
            _options = options;
        }

        public async Task<IEnumerable<PathFinderResultApiModel>> Process(List<int[]> arrays, CancellationToken cancellationToken)
        {
            var tasks = arrays.Select(x => Process(x, cancellationToken));
            return await Task.WhenAll(tasks);
        }

        public async Task<PathFinderResultApiModel> Process(int[] array, CancellationToken cancellationToken)
        {
            using (var context = new PathFinderContext(_options))
            {
                var storedResult = await context.PathResult.FirstOrDefaultAsync(x => x.InputArray == array, cancellationToken);
                if (storedResult != null)
                    return storedResult.ToApiModel(true);
                else
                {
                    var result = _pathFinder.Find(array);
                    var pathResult = new PathResult()
                    {
                        InputArray = array,
                        IsTraversable = result.IsTraversable,
                        ResultArray = result.Indices
                    };
                    context.PathResult.Add(pathResult);
                    await context.SaveChangesAsync(cancellationToken);
                    return pathResult.ToApiModel(false);
                }
            }
        }


    }
}
