using System;

namespace Gradient_Stuff.Vector
{
    public class Vectori
    {
        public int x { get; set; }

        public int y { get; set; }

        public int length => (int)Math.Sqrt(x * x + y * y);
        public string name => $"[ {x}, {y} ]";

        public Vectori(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vectori operator +(Vectori a, Vectori b) => new Vectori(a.x + b.x, a.y + b.y);
        public static Vectori operator -(Vectori a, Vectori b) => new Vectori(a.x - b.x, a.y - b.y);
        public static int operator *(Vectori a, Vectori b) => a.x * b.x + a.y * b.y;
        public static Vectori operator *(Vectori a, int s) => new Vectori(a.x * s, a.y * s);
        public static Vectori operator *(int s, Vectori a) => new Vectori(a.x * s, a.y * s);
        public static Vectori operator /(Vectori a, int d) => new Vectori(a.x / d, a.y / d);
        public static Vectorf operator +(Vectori a, Vectorf b) => new Vectorf(a.x + b.x, a.y + b.y);
        public static Vectorf operator +(Vectorf a, Vectori b) => new Vectorf(a.x + b.x, a.y + b.y);
        public static Vectorf operator -(Vectori a, Vectorf b) => new Vectorf(a.x - b.x, a.y - b.y);
        public static Vectorf operator -(Vectorf a, Vectori b) => new Vectorf(a.x - b.x, a.y - b.y);
        public static implicit operator Vectorf(Vectori a) => new Vectorf(a.x, a.y);
        public static int CrossProduct(Vectori a, Vectori b) => a.x * b.y - a.y * b.x;

        public Vectorf Vectorf => new Vectorf(x, y);
    }
}
