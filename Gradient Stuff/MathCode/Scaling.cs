using System;
using System.Drawing;
using Gradient_Stuff.Vector;

namespace Gradient_Stuff.MathCode
{
    public static class Scaling
    {
        public static float BicubicInterpolateBetweenNodes(this NodeMap NP, Vectori pos, Vectorf interpos)
        {

            static int Clamp(int a, int size) => Math.Min(Math.Max(a, 0), size - 1);

            var points = new float[4][];

            int[] yn = new int[4];

            int[] xn = new int[4];

            for (int i = 0; i < 4; i++)
            {
                xn[i] = Clamp(pos.x + i - 1, NP.Size.x);
                yn[i] = Clamp(pos.y + i - 1, NP.Size.y);
            }

            for (var x = 0; x < 4; x++)
            {
                points[x] = new float[4];
                for (var y = 0; y < 4; y++)
                {
                    points[x][y] = NP.Get(xn[x], yn[y]);
                }
            }

            return Interpolation.BicubicInterpolate(points, interpos + new Vectori(1, 1));
        }

        public static float BicubicInterpolateBetweenNodes(this NodeMap NP, Vectorf pos)
            => NP.BicubicInterpolateBetweenNodes(new Vectori((int)pos.x, (int)pos.y), new Vectorf(pos.x % 1, pos.y % 1));

        public static NodeMap UpscaleRangeBicubic(this NodeMap NP, Vectori start, Vectori end, int scalar)
        {
            if (start.x >= end.x || start.y >= end.y)
            {
                throw new Exception("UpscaleRange too small");
            }

            Vectori size = end - start + new Vectori(1, 1);
            Vectori sSize = scalar * size;
            NodeMap baseNP = new NodeMap(sSize);

            int X = 0;
            int Y = 0;
            for (float y = 0; y < size.y; y += 1 / (float)scalar)
            {
                for (float x = 0; x < size.x; x += 1 / (float)scalar)
                {
                    baseNP.Set(X, Y, NP.BicubicInterpolateBetweenNodes(new Vectorf(x, y)));
                    X++;
                }

                X = 0;
                Y++;
            }

            return baseNP;
        }

        public static NodeMap UpscaleBicubic(this NodeMap NP, int scalar)
            => NP.UpscaleRangeBicubic(new Vectori(0, 0), NP.Size - new Vectori(1, 1), scalar);

        public static NodeMap UpscaleBetweenNodesBilinear(this NodeMap NP, Vectori pos, int scalar)
        {
            var output = new NodeMap(scalar, scalar);
            var baseNP = new NodeMap(2, 2);
            var xAdd = pos.x + 1 >= NP.Size.x ? 0 : 1;
            var yAdd = pos.y + 1 >= NP.Size.y ? 0 : 1;
            baseNP.Set(0, 0, NP.Get(pos));
            baseNP.Set(1, 0, NP.Get(pos + new Vectori(xAdd, 0)));
            baseNP.Set(0, 1, NP.Get(pos + new Vectori(0, yAdd)));
            baseNP.Set(1, 1, NP.Get(pos + new Vectori(xAdd, yAdd)));

            var Y = 0;
            var X = 0;
            for (var y = 0f; y < 1; y += 1f / scalar)
            {
                for (var x = 0f; x < 1; x += 1f / scalar)
                {
                    output.Set(X, Y, Interpolation.BilinearInterpolate(
                        baseNP.Get(0, 0),
                        baseNP.Get(1, 0),
                        baseNP.Get(0, 1),
                        baseNP.Get(1, 1),
                        new Vectorf(x, y)
                    ));

                    X++;
                }

                X = 0;
                Y++;
            }

            return output;
        }

        public static NodeMap UpscaleRangeBilinear(this NodeMap NP, Vectori start, Vectori end, int scalar)
        {
            var size = end - start + new Vectori(1, 1);
            var output = new NodeMap(size * scalar);

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    //var baseNP = UpscaleBetweenNodesBilinear(start + new Vectori(x, y), scalar);
                    var baseNP = NP.UpscaleBetweenNodesBilinear(start + new Vectori(x, y), scalar);

                    for (var by = 0; by < scalar; by++)
                    {
                        for (var bx = 0; bx < scalar; bx++)
                        {
                            output.Set(new Vectori(scalar * x + bx, scalar * y + by), baseNP.Get(bx, by));
                        }
                    }
                }
            }

            return output;
        }

        public static NodeMap UpscaleBilinear(this NodeMap NP, int scalar)
            => NP.UpscaleRangeBilinear(new Vectori(0, 0), NP.Size - new Vectori(1, 1), scalar);

        public static NodeMap UpscaleBetweenNodesNN(this NodeMap NP, Vectori pos, int scalar)
        {
            var output = new NodeMap(scalar, scalar);
            var baseNP = new NodeMap(2, 2);
            var xAdd = pos.x + 1 >= NP.Size.x ? 0 : 1;
            var yAdd = pos.y + 1 >= NP.Size.y ? 0 : 1;
            baseNP.Set(0, 0, NP.Get(pos));
            baseNP.Set(1, 0, NP.Get(pos + new Vectori(xAdd, 0)));
            baseNP.Set(0, 1, NP.Get(pos + new Vectori(0, yAdd)));
            baseNP.Set(1, 1, NP.Get(pos + new Vectori(xAdd, yAdd)));

            var Y = 0;
            var X = 0;
            for (var y = 0f; y < 1; y += 1f / scalar)
            {
                for (var x = 0f; x < 1; x += 1f / scalar)
                {
                    output.Set(X, Y, Interpolation.NN2dInterpolate(
                        baseNP.Get(0, 0),
                        baseNP.Get(1, 0),
                        baseNP.Get(0, 1),
                        baseNP.Get(1, 1),
                        new Vectorf(x, y)
                    ));

                    X++;
                }

                X = 0;
                Y++;
            }

            return output;
        }

        public static NodeMap UpscaleRangeNN(this NodeMap NP, Vectori start, Vectori end, int scalar)
        {
            var size = end - start + new Vectori(1, 1);
            var output = new NodeMap(size * scalar);

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    //var baseNP = UpscaleBetweenNodesBilinear(start + new Vectori(x, y), scalar);
                    var baseNP = NP.UpscaleBetweenNodesNN(start + new Vectori(x, y), scalar);

                    for (var by = 0; by < scalar; by++)
                    {
                        for (var bx = 0; bx < scalar; bx++)
                        {
                            output.Set(new Vectori(scalar * x + bx, scalar * y + by), baseNP.Get(bx, by));
                        }
                    }
                }
            }

            return output;
        }

        public static NodeMap UpscaleNN(this NodeMap NP, int scalar)
            => NP.UpscaleRangeNN(new Vectori(0, 0), NP.Size - new Vectori(2, 2), scalar);

        public static Node DownscaleBetweenNodes(this NodeMap NP, Vectori pos, int divisor)
        {
            var sum = 0.0f;
            var oSum = 0.0f;

            for (int y = 0; y < divisor; y++)
            {
                for (int x = 0; x < divisor; x++)
                {
                    sum += NP.Get(pos + new Vectori(x, y));
                    oSum += NP.GetO(pos + new Vectori(x, y));
                }
            }

            var node = new Node { Height = sum / (divisor * divisor), OHeight = oSum / (divisor * divisor) };

            return node;
        }

        public static NodeMap DownscaleRange(this NodeMap NP, Vectori start, Vectori end, int divisor)
        {
            var size = end - start + new Vectori(1, 1);
            if (size.x % divisor != 0 || size.y % divisor != 0)
            {
                throw new Exception("disivor needs to be a divisor of the nodemap size.");
            }

            var np = new NodeMap(size / divisor);

            for (int y = 0; y < np.Size.y; y++)
            {
                for (int x = 0; x < np.Size.x; x++)
                {
                    np.Set(x, y, np.DownscaleBetweenNodes(start + new Vectori(x * divisor, y * divisor), divisor));
                }
            }

            return np;
        }

        public static NodeMap Downscale(this NodeMap NP, int divisor)
            => NP.DownscaleRange(new Vectori(0, 0), NP.Size - new Vectori(1, 1), divisor);

    }
}