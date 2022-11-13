using System;

namespace Gradient_Stuff
{
    public class Gradient
    {
        public static Vectorf GetGradient(NodeMap NP, Vectori pos)
        {
            var L = pos.x == 0 ? NP.Get(pos) : NP.Get(pos.x - 1, pos.y);
            var U = pos.y == 0 ? NP.Get(pos) : NP.Get(pos.x, pos.y - 1);
            var R = pos.x > NP.Size.x ? NP.Get(pos) : NP.Get(pos.x + 1, pos.y);
            var D = pos.y > NP.Size.x ? NP.Get(pos) : NP.Get(pos.x, pos.y + 1);
            var s = NP.Get(pos);
                
            Console.WriteLine($"X: {L} {s} {R}\nY: {U} {s} {D}");

            var X = ((R - s) + (s - L)) ;
            var Y = ((D - s) + (s - U)) ;

            return new Vectorf(X, Y);
        }
    }
}   