using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PathFinder
{
    public interface IPathFinder
    {
        PathFinderResult Find(int[] array);
        Task<PathFinderResult> FindAsync(int[] array, CancellationToken cancellationToken);
        IEnumerable<PathFinderResult> Find(int[][] array);
    }

    public class PathFinderResult
    {
        public bool IsTraversable { get; set; }
        public int[] Indices { get; set; }
    }

    public class PathFinder : IPathFinder
    {

        public async Task<PathFinderResult> FindAsync(int[] array, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Find(array), cancellationToken);
        }

        public PathFinderResult Find(int[] array)
        {
            if (array.Length <= 1) throw new ArgumentException("Array should have at least 2 integers");
            var currentIndex = 0;
            var indices = new List<int>() { 0 };
            while (currentIndex < array.Length)
            {
                if (array[currentIndex] <= 0) return new PathFinderResult { IsTraversable = false };

                var maxAdvance = 0;
                var bestStep = 1;
                var stepTo = currentIndex + array[currentIndex];
                if (stepTo < array.Length - 1)
                {
                    for (var i = currentIndex + 1; i <= stepTo; i++)
                    {
                        var nextStep = array[i];
                        if (maxAdvance < i + nextStep)
                        {
                            maxAdvance = i + nextStep;
                            bestStep = i;
                        }
                    }
                    currentIndex = bestStep;

                    indices.Add(currentIndex);
                }
                else
                {
                    indices.Add(array.Length - 1);
                    break;
                }
            }

            return new PathFinderResult { IsTraversable = true, Indices = indices.ToArray() };
        }

        public IEnumerable<PathFinderResult> Find(int[][] array)
        {
            foreach (var item in array)
            {
                yield return Find(item);
            }

        }


    }

}
