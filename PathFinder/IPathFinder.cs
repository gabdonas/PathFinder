using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace PathFinder
{
    public interface IPathFinder
    {
        bool Find(int[] array);
    }

    
    public class PathFinder : IPathFinder
    {
        public bool Find(int[] array)
        {
            return FindPath(array);
        }

        private void PrintArray(int[] array, int? markIndex = null)
        {
            var sb = new StringBuilder();
            var asd = new List<string>();
            for (int i = 0; i < array.Length; i++)
                asd.Add(i == markIndex ? $"*{array[i]}*" : array[i].ToString());
            Console.WriteLine("[{0}]", string.Join(", ", asd));
        }

        public bool FindPath(int[] array)
        {
            var currentIndex = 0;
            var indices = new List<int>();

            while (currentIndex < array.Length - 1)
            {
                if (array[currentIndex] <= 0) return false;

                var maxPositionAfterJump = 0;
                var bestStep = 0;
                var steps = array[currentIndex];
                for (var i = 1; i <= steps; i++)
                {
                    var nextStep = array[currentIndex + i];
                    if (maxPositionAfterJump < i + nextStep)
                    {
                        maxPositionAfterJump = i + nextStep;
                        bestStep = currentIndex + i;
                    }
                }

                currentIndex = bestStep;
                indices.Add(currentIndex);
            }
            PrintArray(indices.ToArray());
            return true;
        }
    }

}
