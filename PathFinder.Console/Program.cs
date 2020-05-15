using System;

namespace PathFinder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new PathFinder().Find(new int[] { 1, -1, 2, -1, 0, 2, 0  });
        }
    }
}
