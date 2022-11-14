using System;
using System.Numerics;

namespace Gradient_Stuff.Vector
{
    public class Vectorf
    {
        public float x { get; set; }

        public float y { get; set; }

        public float length => (float)Math.Sqrt(x * x + y * y);
        public Vectorf normalized => new Vectorf(x / length, y / length);
        public string name => $"[ {x}, {y} ]";

        public Vectorf(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vectorf operator +(Vectorf a, Vectorf b) => new Vectorf(a.x + b.x, a.y + b.y);
        public static Vectorf operator -(Vectorf a, Vectorf b) => new Vectorf(a.x - b.x, a.y - b.y);
        public static float operator *(Vectorf a, Vectorf b) => a.x * b.x + a.y * b.y;
        public static Vectorf operator *(Vectorf a, float s) => new Vectorf(a.x * s, a.y * s);
        public static Vectorf operator *(float s, Vectorf a) => new Vectorf(a.x * s, a.y * s);
        public static Vectorf operator /(Vectorf a, float d) => new Vectorf(a.x / d, a.y / d);
        public static Vectorf operator /(float d, Vectorf a) => new Vectorf(d / a.x, d / a.y);
        public static Vectorf operator -(Vectorf a) => new Vectorf(-a.x, -a.y);
        public static explicit operator Vectori(Vectorf a) => new Vectori((int)a.x, (int)a.y);
        public static implicit operator Vector2(Vectorf a) => new Vector2(a.x, a.y);
        public static implicit operator Vectorf(Vector2 a) => new Vectorf(a.X, a.Y);
        public static float CrossProduct(Vectorf a, Vectorf b) => a.x * b.y - a.y * b.x;

        public static Vectorf Zero = new Vectorf(0, 0);
    }
}