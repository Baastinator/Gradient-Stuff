using System;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Gradient_Stuff
{
    public class Program
    {
        public static void Main()
        {
            NodeMap bruh = new NodeMap(4,4);

            int i = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    i++;
                    bruh.Set(x, y, i);
                }
            }
                
            Console.WriteLine(bruh.Display);

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Console.WriteLine("{0} {1} \n{2}", x, y, bruh.UpscaleBetweenNodesBicubic(new Vectori(x, y), 1).Display);
                }
            }

            bruh.UpscaleBetweenNodesBicubic(new Vectori(1,0), 1);
            
            
            
            
            //var scaledNP = NP.Upscale(4);

            //for (var i = 0; i < scaledNP.Size.x; i++)
            //{
            //    var m = NP.Size.y / (float)NP.Size.x;
            //    Console.WriteLine(scaledNP.Get(i, (int)(i * m)));
            //}


            Console.ReadLine();
        }
    }
}