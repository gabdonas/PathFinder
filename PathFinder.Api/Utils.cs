﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinder.Api
{
    public static class Utils
    {
        public static string ArrayToStr(int[] array)
        {
            return string.Join(",", array);
        }

        public static int[] StrToArray(string str)
        {
            return str.Split(',').Select(x => int.Parse(x)).ToArray();
        }
    }
}
