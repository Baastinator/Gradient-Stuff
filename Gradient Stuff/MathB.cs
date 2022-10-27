using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradient_Stuff
{
    public static class MathB
    {

        public static float Round(float n, int digits) 
            => (float)Math.Floor(n * Math.Pow(10, digits) + 0.5) / (float)Math.Pow(10, digits);

        public static float LinearInterpolate(float start, float end, float position) 
            => start + position * (end - start);
        
        //NW 0,0 //NE 1,0 //SW 0,1 //SE 1,1
        public static float BilinearInterpolate(float NW, float NE, float SW, float SE, Vectorf Pos) 
            => LinearInterpolate(LinearInterpolate(NW, SW, Pos.y), LinearInterpolate(NE, SE, Pos.y), Pos.x);


        public static int Max(IEnumerable<int> a) 
            => a.Prepend(0).Max();
    }
}
