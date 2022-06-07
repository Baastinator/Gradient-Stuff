using System;

namespace Gradient_Stuff
{
    public class Vector
    {
        private double _x;
        private double _y;

        public double x
        {
            get => _x;
            set => _x = value;
        }

        public double y
        {
            get => _y;
            set => _y = value;
        }

        public double length => Math.Sqrt(x * x + y * y);
        public Vector normalized => new Vector(x / length, y / length);

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y);
        public static double operator *(Vector a, Vector b) => a.x * b.x + a.y * b.y;
        public static Vector operator *(Vector a, double s) => new Vector(a.x * s, a.y * s);
        public static Vector operator /(Vector a, double d) => new Vector(a.x / d, a.y / d);
        public static double CrossProduct(Vector a, Vector b) => a.x * b.y - a.y * b.x;
    }
}
