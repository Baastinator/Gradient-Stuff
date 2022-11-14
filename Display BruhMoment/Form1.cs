using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gradient_Stuff;
using Gradient_Stuff.Hydraulic_Erosion;
using Gradient_Stuff.MathCode;
using Gradient_Stuff.Vector;

namespace Display_BruhMoment
{
    public partial class Form1 : Form
    {
        private static NodeMap NP;
        private static NodeMap baseNP;

        private static List<Particle> waterList = new List<Particle>();

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

            int size = 9;

            baseNP = new NodeMap(size, size);
            for (int y = 0; y < baseNP.Size.y; y++)
            {
                for (int x = 0; x < baseNP.Size.x; x++)
                {
                    //var col = (float)((noise.Smooth2D(x, y, seed)+1) * 255f / 2f);
                    
                    float X = (x - size / 2f) / 5f;

                    float Y = (y - size / 2f) / 5f;

                    var col = 255 * (
                        Math.Exp(-(X * X + Y * Y)) -
                        Math.Exp(-(X * X + 3 * Y * Y)) -
                        Math.Exp(-(X * X + Y * Y) / 3d) / 2d + 1
                    );

                    baseNP.Set(x, y, (float)col);
                }
            }

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
                    var coords = Utils.polarToCartesian(baseNP.Size, 230 - arg, r);
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


            int vecSize = 25;

            NP = NP.UpscaleBicubic(100);

            var bmp = NP.Bitmap;

            if (showVectors.Checked)
            {
                for (int y = 0; y < NP.Size.y - 2; y++)
                {
                    for (int x = 0; x < NP.Size.x - 2; x++)
                    {
                        if (x % vecSize == 0 && y % vecSize == 0)
                        {
                            var grad = NP.GetGradient(new Vectori(x + 1, y + 1));
                            bmp = bmp.drawVector(
                                new Vectori(x, y),
                                -grad,
                                vecSize
                            );
                        }
                    }
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

        private void showVectors_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
