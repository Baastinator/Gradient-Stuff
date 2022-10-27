using System;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Gradient_Stuff
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var bmp = new Bitmap(@"C:\Users\admin\Desktop\DnD\Laradell\Maps\BitmapBS.bmp");
            var NP = new NodeMap(10, 10);


            for (var y = 0; y < NP.Size.y; y++)
            {
                for (var x = 0; x < NP.Size.x; x++)
                {
                    var pxlCol = (x + y) / 10f;
                    NP.Set(x, y, pxlCol, pxlCol);
                }
            }



            Console.WriteLine(NP.Display);

            Console.WriteLine(NP.Upscale(2).Display);

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
