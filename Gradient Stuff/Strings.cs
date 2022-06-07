using System;
using System.Collections.Generic;
using System.Text;

namespace Gradient_Stuff
{
    class Strings
    {
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
