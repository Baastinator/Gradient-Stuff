using System;

namespace Gradient_Stuff
{
    public class Vectorf
    {
        private float _x;
        private float _y;

        public float x
        {
            get => _x;
            set => _x = value;
        }

        public float y
        {
            get => _y;
            set => _y = value;
        }

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
        public static Vectorf operator /(Vectorf a, float d) => new Vectorf(a.x / d, a.y / d);
        public static float CrossProduct(Vectorf a, Vectorf b) => a.x * b.y - a.y * b.x;

        public Vectori Vectori => new Vectori((int)x, (int)y);
    }
    public class Vectori
    {
        private int _x;
        private int _y;

        public int x
        {
            get => _x;
            set => _x = value;
        }

        public int y
        {
            get => _y;
            set => _y = value;
        }

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
        public static int CrossProduct(Vectori a, Vectori b) => a.x * b.y - a.y * b.x;

        public Vectorf Vectorf => new Vectorf(x, y);
    }
}
