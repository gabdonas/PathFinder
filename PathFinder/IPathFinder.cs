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
            return FindPath(array, 0);
        }

        private bool FindPath(int[] array, int start = 0)
        {


            if (start == array.Length - 1) return true;
            var current = array[start];
            if (current == 0) return false;

            for (var step = Math.Abs(current); step > 0; step--)
            {
                if (FindPath(array, start + current))
                    return true;
            }

            return false;
        }



    }
}
