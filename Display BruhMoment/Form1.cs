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
            var bmp = new Bitmap(@"C:\Users\admin\Desktop\Laradell\DEPRECIATED Maps\BitmapBS.bmp");
            baseNP = new NodeMap(bmp.Width, bmp.Height);
            baseNP = baseNP.Downscale(8).Upscale(7);
            NP = new NodeMap(baseNP.Size);
            for (var y = 0; y < NP.Size.y; y++)
            {
                for (var x = 0; x < NP.Size.x; x++)
                {
                    var pxlCol = 100;
                    NP.Set(x, y, pxlCol, pxlCol);
                }
            }
        }

        private static int bruh = 4;

        private void button3_Click(object sender, EventArgs e) //Next Step
        {
        }

        private void button2_Click(object sender, EventArgs e) //Display
        {
            pictureBox1.Image = NP.Bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                var X = mouseEventArgs.X;
                var Y = mouseEventArgs.Y;
                Console.WriteLine(@"X: {0}; Y: {1}", mouseEventArgs.X, mouseEventArgs.Y);
                for (var y = Y - 5; y < Y + 5; y++)
                {
                    for (var x = X - 5; x < X + 5; x++)
                    {
                        NP.Set(x,y);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
