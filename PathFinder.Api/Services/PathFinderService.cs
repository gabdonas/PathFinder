using System;
using Microsoft.EntityFrameworkCore;
using PathFinder.Api.Data;
using PathFinder.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PathFinder.Api.Model.Api;
using Microsoft.Extensions.DependencyInjection;

namespace PathFinder.Api.Services
{
    public interface IPathFinderService
    {
        Task<IEnumerable<PathFinderResultApiModel>> Process(List<int[]> arrays, CancellationToken cancellationToken);
        Task<PathFinderResultApiModel> Process(int[] array, CancellationToken cancellationToken);
        Task<IEnumerable<PathFinderResultApiModel>> GetResults(int limit, int offset, CancellationToken cancellationToken);
        Task<PathFinderResultApiModel> GetResultById(int id, CancellationToken cancellationToken);
    }

    public class PathFinderService : IPathFinderService
    {
        private readonly IPathFinder _pathFinder;
        private readonly PathFinderContext _context;
        private readonly IServiceProvider _serviceProvider;

        public PathFinderService(IPathFinder pathFinder, PathFinderContext context, IServiceProvider serviceProvider)
        {
            _pathFinder = pathFinder;
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<PathFinderResultApiModel> GetResultById(int id, CancellationToken cancellationToken)
        {
            var result = await _context.PathResult.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result.ToApiModel(true);
        }

        public async Task<IEnumerable<PathFinderResultApiModel>> GetResults(int limit, int offset, CancellationToken cancellationToken)
        {
            var result = await _context.PathResult.Skip(offset).Take(limit).ToListAsync(cancellationToken);
            return result.Select(x => x.ToApiModel(true));
        }

        public async Task<IEnumerable<PathFinderResultApiModel>> Process(List<int[]> arrays, CancellationToken cancellationToken)
        {
            var tasks = arrays.Select(x => Process(x, cancellationToken));
            return await Task.WhenAll(tasks);
        }

        public async Task<PathFinderResultApiModel> Process(int[] array, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<PathFinderContext>();

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