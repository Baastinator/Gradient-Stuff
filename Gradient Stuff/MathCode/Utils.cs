using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Gradient_Stuff.Vector;

namespace Gradient_Stuff.MathCode
{
    public static class Utils
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

        public static void DrawCircle(this Graphics g, Pen pen,
            float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
            float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static string AddWhitespace(string input, int totalSize, char Filler = ' ')
        {
            //if (input.Length > totalSize) throw new Exception("add whitespace: yo wtf you tryna do");
            var WS = "";
            var delta = totalSize - input.Length;
            for (var i = 0; i <= delta; i++)
            {
                WS += Filler;
            }

            return input + WS;
        }
    }
}
