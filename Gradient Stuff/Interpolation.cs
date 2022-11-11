using System;

namespace Gradient_Stuff
{
    public static class Interpolation
    {
        public static float NN1dInterpolate(float a, float b, float pos)
        {
            if (pos > 1 || pos < 0) throw new Exception("position out of bounds");
            return pos >= 0.5 ? b : a;
        }
        //a 0,0 //b 1,0 //c 0,1 //d 1,1

        public static float NN2dInterpolate(float a, float b, float c, float d, Vectorf pos)
        {
            var ab = NN1dInterpolate(a, b, pos.x);
            var cd = NN1dInterpolate(c, d, pos.x);
            
            return NN1dInterpolate(ab, cd, pos.y);
        }
        public static float LinearInterpolate(float start, float end, float position)
            => start + position * (end - start);

        //NW 0,0 //NE 1,0 //SW 0,1 //SE 1,1
        public static float BilinearInterpolate(float NW, float NE, float SW, float SE, Vectorf Pos)
            => LinearInterpolate(LinearInterpolate(NW, SW, Pos.y), LinearInterpolate(NE, SE, Pos.y), Pos.x);

        public static float CubicInterpolate(float[] points, float position)
        {
            if (position < 1 || position > 2)
            {
                throw new Exception("Interpolate out of bounds");
            }

            float sum = 0;

            for (var n = 0; n < 4; n++)
            {
                float product = 1;

                for (var i = 0; i < 4; i++)
                {
                    if (i != n)
                    {
                        product *= (position - i) / (n - i);
                    }
                }

                sum += points[n] * product;
            }

            return sum;
        }

        public static float BicubicInterpolate(float[][] points, Vectorf pos)
        {
            if (pos.x < 1 || pos.y < 1 || pos.x > 2 || pos.y > 2)
            {
                throw new Exception("Interpolate out of bounds");
            }

            float[] cPoints =
            {
                CubicInterpolate(points[0], pos.y),
                CubicInterpolate(points[1], pos.y),
                CubicInterpolate(points[2], pos.y),
                CubicInterpolate(points[3], pos.y),
            };

            return CubicInterpolate(cPoints, pos.x);
        }
    }
}