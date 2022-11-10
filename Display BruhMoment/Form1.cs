using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            var bmp = new Bitmap($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Laradell\\Maps\\bruh.bmp");
            baseNP = new NodeMap(bmp.Width, bmp.Width);

            for (float r = 0; r <= 1023; r += 0.5f)
            {
                for (float arg = 0; arg < 360; arg += 1/ (4f * r / 128))
                {
                    var coords = MathB.polarToCartesian(baseNP.Size, 230 - arg, r);
                    var y = (int) ((r / 2048f) * baseNP.Size.y);
                    
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
                        baseNP.Set(x,y,255);
                    }
                }
            }

            NP = baseNP;
        }

        private void button3_Click(object sender, EventArgs e) //Next Step
        {
        }

        private void button2_Click(object sender, EventArgs e) //Display
        {
            pictureBox1.Image = NP.Downscale(2).Bitmap;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
