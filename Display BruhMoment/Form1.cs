using System;
using System.Drawing;
using System.Windows.Forms;
using Gradient_Stuff;

namespace Display_BruhMoment
{
    public partial class Form1 : Form
    {
        private static NodeMap NP;
        private static NodeMap baseNP;

        private static readonly Random rng = new Random();

        private static readonly Noise noise = new Noise(1, 0.001, 1, 1, rng.Next(1, 10000000));

        public Form1()
        {
            InitializeComponent();
        } 
        
        private void button1_Click(object sender, EventArgs e) //First step
        {

            //var bmp = new Bitmap($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Laradell\\Maps\\bruh.bmp");
            var seed = rng.Next(1, 1000000000);

            int size = 39;

            baseNP = new NodeMap(size, size);
            for (int y = 0; y < baseNP.Size.y; y++)
            {
                for (int x = 0; x < baseNP.Size.x; x++)
                {
                    //var col = (float)((noise.Smooth2D(x, y, seed)+1) * 255f / 2f);
                    //Console.WriteLine($"{x}; {y} - {col}");
                    int X = x - size/2;
                    int Y = y - size/2;
                    var col = 255 * Math.Exp(-(X * X + Y * Y) / (10d * size)) -
                              200 * Math.Exp(-(5 * X * X + Y * Y) / (10d * size));
                    baseNP.Set(x, y, (float)col);
                }
            }
            Console.WriteLine("");

            NP = baseNP;
        }

        private void Rectangular(Bitmap bmp)
        {
            baseNP = new NodeMap(bmp.Width, bmp.Height);
            for (int y = 0; y < baseNP.Size.y; y++)
            {
                for (int x = 0; x < baseNP.Size.x; x++)
                {
                    var col = bmp.GetPixel(x, y).R;
                    baseNP.Set(x, y, col);
                }
            }
        }

        private void Polar(Bitmap bmp)
        {
            baseNP = new NodeMap(bmp.Width, bmp.Width);
            for (float r = 0; r <= 1023; r += 0.5f)
            {
                for (float arg = 0; arg < 360; arg += 1 / (4f * r / 128))
                {
                    var coords = MathB.polarToCartesian(baseNP.Size, 230 - arg, r);
                    var y = (int)(r / 2048f * baseNP.Size.y);

                    baseNP.Set(
                        coords,
                        bmp.GetPixel(
                            (int)((arg / 360f) * baseNP.Size.x),
                            y
                        ).R
                    );
                }
            }

            for (int y = 0; y < baseNP.Size.y; y++)
            {
                for (int x = 0; x < baseNP.Size.x; x++)
                {
                    if (baseNP.Get(x, y) == 0)
                    {
                        baseNP.Set(x, y, 255);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //Next Step
        {
        }

        private void button2_Click(object sender, EventArgs e) //Display
        {
            int size = 25;

            var bmp = NP.UpscaleNN(size).Bitmap;

            for (int y = 0; y < NP.Size.y - 2; y++)
            {
                for (int x = 0; x < NP.Size.x - 2; x++)
                {
                    var grad = Gradient.GetGradient(NP, new Vectori(x + 1, y + 1));

                    if (Math.Abs(grad.x) > 10000000 || Math.Abs(grad.y) > 10000000)
                    {
                        Console.WriteLine("please why do you do this");
                    }

                    Console.WriteLine($"{grad.x}; {grad.y}\nPos: {x} {y}\n");

                    bmp = VectorDisplay.drawVector(
                        bmp,
                        new Vectori(size * (x + 1), size * (y + 1)),
                        grad,
                        size)
                    ;
                }
            }

            pictureBox1.Image = bmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
