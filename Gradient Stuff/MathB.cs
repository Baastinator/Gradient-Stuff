using System;
using System.Collections.Generic;
using System.Linq;

namespace Gradient_Stuff
{
    public static class MathB
    {

        public static float Round(float n, int digits) 
            => (float)Math.Floor(n * Math.Pow(10, digits) + 0.5) / (float)Math.Pow(10, digits);

        public static Vectori defaultToPolar(Vectori size, Vectori coords, float radius)
        {
            var arg = coords.x*360 / size.x;
            var abs = radius * coords.y / size.y;

            return new Vectori(
                (int) (abs * Math.Cos(arg * Math.PI / 360f)),
                (int) (abs * Math.Sin(arg * Math.PI / 360f))
            );
        }

        public static Vectori polarToCartesian(Vectori size, float arg, float radius)
        {
            return size / 2 + new Vectori(
               (int) (radius * Math.Cos(arg * Math.PI / 180f)),
               (int) (radius * Math.Sin(arg * Math.PI / 180f))
            );
        } 

        public static int Max(IEnumerable<int> a) 
            => a.Prepend(0).Max();
    }
}
