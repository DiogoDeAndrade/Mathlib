using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathlib
{
    public static class Extensions
    {
        public static float Range(this System.Random rnd, float valMin, float valMax)
        {
            return (float)(rnd.NextDouble() * (valMax - valMin) + valMin);
        }

        public static int Range(this System.Random rnd, int valMin, int valMax)
        {
            return rnd.Next(valMin, valMax);
        }
    }
}
