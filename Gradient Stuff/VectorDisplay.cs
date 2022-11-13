using ILGPU;
using System;
using System.Drawing;
using System.Numerics;

namespace Gradient_Stuff
{
    public static class VectorDisplay
    {
        public static Bitmap drawVector(Bitmap bmp, Vectori pos, Vectorf dir, int mapScalar)
        {
            using var graphics = Graphics.FromImage(bmp);
            if (dir.x != 0 && dir.y != 0)
            {
                Vectori nDir = new Vectori(0,0);

                if (Math.Abs(dir.x) < 0.01f || Math.Abs(dir.y) < 0.01f)
                {
                    while (Math.Abs(dir.x) < 0.01f || Math.Abs(dir.y) < 0.01f)
                    {
                        dir.x *= 10;
                        dir.y *= 10;
                    }
                }
                
                var a = new Vector2(
                    dir.x,
                    dir.y
                );

                a = Vector2.Normalize(a);

                var b = mapScalar / 2f * new Vectorf(a.X, a.Y);

                nDir.x = (int)b.x;
                nDir.y = (int)b.y;
                
                if (Math.Abs(nDir.x + 100) > 10000000 || Math.Abs(nDir.y + 100) > 10000000)
                {
                    Console.WriteLine("Bruh");
                }

                graphics.DrawLine(new Pen(Brushes.Red, 3),
                    pos.x,
                    pos.y,
                    pos.x + nDir.x,
                    pos.y + nDir.y
                );
                return bmp;
            }
            else if (dir.x == 0)
            {
                graphics.DrawLine(new Pen(Brushes.Red, 3),
                    pos.x,
                    pos.y,
                    pos.x,
                    pos.y + Math.Sign(dir.y) * mapScalar / 2
                );
            }
            else
            {
                graphics.DrawLine(new Pen(Brushes.Red, 3),
                    pos.x,
                    pos.y,
                    pos.x + Math.Sign(dir.x) * mapScalar / 2,
                    pos.y
                );
            }

            graphics.DrawLine(new Pen(Brushes.Red,3),
                pos.x-1, pos.y, pos.x+1, pos.y);
            return bmp;
        }

        private static Bitmap drawLine(Bitmap bmp, Vectori pos, Vectori direction)
        {
            Vectori end = pos + direction;

            int minX = Math.Min(pos.x, end.x);
            int maxX, minY, maxY;
            if (minX == pos.x)
            {
                minY = pos.y;
                maxX = end.x;
                maxY = end.y;
            }
            else
            {
                minY = end.y;
                maxX = pos.x;
                maxY = pos.y;
            }

            int xDiff = maxX - minX;
            int yDiff = maxY - minY;

            if (xDiff > Math.Abs(yDiff))
            {
                float y = minY;
                float dy = yDiff / (float)xDiff;
                for (int x = minX; x < maxX; x++)
                {
                    bmp.SetPixel(x, (int)Math.Floor(y + 0.5), Color.FromArgb(255, 0, 0));
                    y += dy;
                }
            }   
            else
            {
                float x = minX;
                float dx = xDiff / yDiff;
                if (maxY >= minY)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        bmp.SetPixel((int)Math.Floor(x + 0.5), y, Color.FromArgb(255, 0, 0));
                        x += dx;
                    }
                }
                else
                {
                    for (int y = minY; y < maxY; y--)
                    {
                        bmp.SetPixel((int)Math.Floor(x + 0.5), y, Color.FromArgb(255, 0, 0));
                        x -= dx;
                    }
                }
            }

            return bmp;
        }
    }
}