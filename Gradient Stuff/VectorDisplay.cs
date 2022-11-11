using System;
using System.Drawing;

namespace Gradient_Stuff
{
    public static class VectorDisplay
    {
        public static Bitmap drawVector(Bitmap bmp, Vectori pos, Vectori dir, int mapScalar)
        {
            var nDir = new Vectori(
                (int) (mapScalar * dir.x / (float) Math.Sqrt(dir.x * dir.x + dir.y * dir.y)),
                (int) (mapScalar * dir.y / (float) Math.Sqrt(dir.x * dir.x + dir.y * dir.y))
            );

            var nDir1 = new Vectori(
                dir.x == 0 ? 0 : dir.x / Math.Abs(dir.x),
                dir.y == 0 ? 0 : dir.y / Math.Abs(dir.y)
            );

            

            return bmp;
        }

        private static Bitmap drawLine(Bitmap bmp, Vectori pos, Vectori direction)
        {
            Vectori end = pos + direction;

            int minX = (int)Math.Min(pos.x, end.x);
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