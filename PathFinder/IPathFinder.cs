using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace PathFinder
{
    public interface IPathFinder
    {
        PathFinderResult Find(int[] array);
    }

    public class PathFinderResult
    {
        public bool IsTraversable { get; set; }
        public int[] Indices { get; set; }
    }

    public class PathFinder : IPathFinder
    {

        public PathFinderResult Find(int[] array)
        {
            if (array.Length <= 1) throw new ArgumentException("Array should have at least 2 integers");
            var currentIndex = 0;
            var indices = new List<int>(){0};
            while (currentIndex < array.Length - 1)
            {
                if (array[currentIndex] <= 0) return new PathFinderResult { IsTraversable = false };
              
                var maxAdvance = 0;
                var bestStep = 0;
                var stepTo = currentIndex + array[currentIndex];
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
                //If the next step exceeds 
                if (currentIndex + array[currentIndex] > array.Length - 1)
                {
                    indices.Add(array.Length - 1);
                    break;
                }
            }

            return new PathFinderResult { IsTraversable = true, Indices = indices.ToArray() };
        }

        private string ArrayToStr(int[] array, int? markIndex = null)
        {
            var sb = new StringBuilder();
            var asd = new List<string>();
            for (int i = 0; i < array.Length; i++)
                asd.Add(i == markIndex ? $"*{array[i]}*" : array[i].ToString());
            return $"[{string.Join(", ", asd)}]";
        }
    }

}
