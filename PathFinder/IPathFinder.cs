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
            return FindPath(array, null, 0);
        }

        private void PrintArray(int[] array, int? markIndex = null)
        {
            var sb = new StringBuilder();
            var asd = new List<string>();
            for (int i = 0; i < array.Length; i++)
                asd.Add(i == markIndex ? $"*{array[i]}*" : array[i].ToString());
            Console.WriteLine("[{0}]", string.Join(", ", asd));
        }
        public bool FindPath(int[] array, bool[] visited = null, int start = 0)
        {

            if (visited == null) visited = new bool[array.Length];
            if (start < 0 || start > array.Length - 1 || visited[start]) return false;

            if (start == array.Length - 1) return true;
            var current = array[start];

            PrintArray(array, start);

            visited[start] = true;
            if (current == 0) return false;

            for (var step = Math.Abs(current); step > 0; step--)
            {
                if (FindPath(array, visited, start + Math.Sign(current) * step))
                    return true;
            }

            return false;
        }
    }

}
