using System;

namespace Gradient_Stuff
{
    class Program
    {
        static void Main(string[] args)
        {
            double start = 10, end = 8;

            for (double i = 2; i <= 5; i += 0.25)
            {
                double interpolated = MathB.LinearInterpolate(start, end, i);
                Console.Write(Strings.AddWhitespace(MathB.Round(interpolated,4).ToString(),5));
                Console.WriteLine(',');
            }
            Console.ReadLine();
        }
    }
}
