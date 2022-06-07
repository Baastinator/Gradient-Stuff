using System;

namespace Gradient_Stuff
{
    public static class MathB
    {

        public static double Round(double n, int digits) 
            => Math.Floor(n * Math.Pow(10, digits) + 0.5) / Math.Pow(10, digits);

        public static double LinearInterpolate(double start, double end, double position) 
            => start + position * (end - start);
        
        //NW 0,0 //NE 1,0 //SW 0,1 //SE 1,1
        public static double BilinearInterpolate(double NW, double NE, double SW, double SE, Vector Pos) 
            => LinearInterpolate(LinearInterpolate(NW, SW, Pos.y), LinearInterpolate(NE, SE, Pos.y), Pos.x);
    }
}
