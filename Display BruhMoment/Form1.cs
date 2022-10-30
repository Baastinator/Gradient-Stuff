using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gradient_Stuff;

namespace Display_BruhMoment
{
    public partial class Form1 : Form
    {
        private static NodeMap NP;
        private static NodeMap baseNP;

        private static Random rng = new Random();

        private static int step = 1;

        public Form1()
        {
            InitializeComponent();
        } 
        
        private void button1_Click(object sender, EventArgs e) //First step
        {
            const int size = 5;
            NP = new NodeMap(size, size);
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    NP.Set(x, y, 255*(float)rng.NextDouble());
                }
            }

            NP = NP.Upscale(50);

            //var bmp = new Bitmap(@"C:\Users\admin\Desktop\Laradell\DEPRECIATED Maps\BitmapBS.bmp");
            //baseNP = new NodeMap(bmp.Width, bmp.Height);
            //NP = new NodeMap(baseNP.Size);
            //for (var y = 0; y < NP.Size.y; y++)
            //{
            //    for (var x = 0; x < NP.Size.x; x++)
            //    {
            //        var pxlCol = bmp.GetPixel(x, y).R;
            //        NP.Set(x, y, pxlCol, pxlCol);
            //    }
            //}

            //NP = NP.Downscale(8).Upscale(6);
        }

        private void button3_Click(object sender, EventArgs e) //Next Step
        {
        }

        private void button2_Click(object sender, EventArgs e) //Display
        {
            pictureBox1.Image = NP.Bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
