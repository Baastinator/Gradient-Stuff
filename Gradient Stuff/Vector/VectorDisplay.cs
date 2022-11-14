using ILGPU;
using System;
using System.Drawing;
using System.Numerics;
using Gradient_Stuff.MathCode;

namespace Gradient_Stuff.Vector
{
    public static class VectorDisplay
    {
        public static Bitmap drawVector(this Bitmap bmp, Vectori pos, Vectorf dir, int mapScalar)
        {
            int brushSize = 2;

            using var graphics = Graphics.FromImage(bmp);
            if (dir.x != 0 && dir.y != 0)
            {
                Vectori nDir = new Vectori(0, 0);

                while (Math.Abs(dir.x) < 0.01f || Math.Abs(dir.y) < 0.01f)
                {
                    dir.x *= 10;
                    dir.y *= 10; 
                }

                var b = mapScalar * 3f / 8f * (Vectorf)Vector2.Normalize(dir);

                nDir.x = (int)b.x;
                nDir.y = (int)b.y;

                graphics.DrawLine(new Pen(Brushes.Red, brushSize),
                    pos.x,
                    pos.y,
                    pos.x + nDir.x,
                    pos.y + nDir.y
                );

                graphics.DrawLine(new Pen(Brushes.Red, brushSize+4),
                    pos.x - 3, pos.y, pos.x + 3, pos.y);

                return bmp;
            }

            if (dir.x == 0)
            {
                graphics.DrawLine(new Pen(Brushes.Red, brushSize),
                    pos.x,
                    pos.y,
                    pos.x,
                    pos.y + Math.Sign(dir.y) * mapScalar / 2
                );
                return bmp;
            }

            if (dir.y == 0)
            {
                graphics.DrawLine(new Pen(Brushes.Red, brushSize),
                    pos.x,
                    pos.y,
                    pos.x + Math.Sign(dir.x) * mapScalar / 2,
                    pos.y
                );
                return bmp;
            }

            graphics.DrawLine(new Pen(Brushes.Red, brushSize),
                pos.x - 1, pos.y, pos.x + 1, pos.y);
            return bmp;
        }
    }
}