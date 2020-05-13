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
                if (currentIndex + array[currentIndex] > array.Length - 1)
                {
                    indices.Add(array.Length - 1);
                    return true;
                }

                if (array[currentIndex] <= 0) return false;
                indices.Add(currentIndex);

                var maxAdvance = 0;
                var bestStep = 0;
                var stepto = currentIndex + array[currentIndex];
                for (var i = currentIndex + 1; i <= stepto; i++)
                {
                    var nextStep = array[i];
                    if (maxAdvance < i + nextStep)
                    {
                        maxAdvance = i + nextStep;
                        bestStep = i;
                    }
                }
                currentIndex = bestStep;

            }
            PrintArray(indices.ToArray());
            return true;
        }
    }

}
