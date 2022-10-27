using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ILGPU.IR.Types;
using Microsoft.VisualBasic.CompilerServices;

namespace Gradient_Stuff
{
    public class NodeMap
    {
        public Vectori Size;

        private readonly Node[][] _map;

        public NodeMap(int xSize, int ySize, float filler = 0)
        {
            Size = new Vectori(xSize, ySize);
            _map = new Node[ySize][];
            for (var y = 0; y < ySize; y++)
            {
                _map[y] = new Node[xSize];
                for (var x = 0; x < xSize; x++)
                {
                    _map[y][x].Height = new Vector3(filler, filler, filler);
                    _map[y][x].OHeight = new Vector3(filler, filler, filler);
                }
            }
        }

        public NodeMap(Vectori Size, float filler = 0)
        {
            this.Size = Size;
            _map = new Node[Size.y][];
            for (var y = 0; y < Size.y; y++)
            {
                _map[y] = new Node[Size.x];
                for (var x = 0; x < Size.x; x++)
                {
                    _map[y][x].Height = new Vector3(filler, filler, filler);
                    _map[y][x].OHeight = new Vector3(filler, filler, filler);
                }
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                var output = new Bitmap(Size.x, Size.y);

                for (var y = 0; y < Size.y; y++)
                {
                    for (var x = 0; x < Size.x; x++)
                    {
                        var col = (int)MathB.Round(Get(x, y), 0);
                        output.SetPixel(x, y, Color.FromArgb(255, col, col, col));
                    }
                }

                return output;
            }
        }

        public void Set(int x, int y, float value)
        {
            if (x < 0 || x >= _map.Length) return;
            if (y >= 0 && y < _map[x].Length)
            {
                _map[y][x].Height = value;
            }
        }

        public void Set(Vectori pos, float value) => Set(pos.x, pos.y, value);

        public void Set(int x, int y, Node value) => _map[y][x] = value;
        public void Set(Vectori pos, Node value) => Set(pos.x, pos.y, value);

        public void Set(int x, int y, float value, float oValue)
        {
            Set(x,y,value);
            _map[y][x].OHeight = oValue;
        }
        public void Set(Vectori pos, float value, float oValue)
            => Set(pos.x, pos.y, value, oValue);

        public float Get(int x, int y) => _map[y][x].Height;
        public float Get(Vectori pos) => Get(pos.x, pos.y);

        public float GetO(int x, int y) => _map[y][x].OHeight;
        public float GetO(Vectori pos) => GetO(pos.x, pos.y);

        public NodeMap UpscaleBetweenNodes(Vectori pos, int scalar)
        {
            var output = new NodeMap(scalar,scalar);
            var baseNP = new NodeMap(2, 2);
            var xAdd = pos.x + 1 >= Size.x ? 0 : 1;
            var yAdd = pos.y + 1 >= Size.y ? 0 : 1;
            baseNP.Set(0,0, Get(pos));
            baseNP.Set(1,0, Get(pos+new Vectori(xAdd, 0)));
            baseNP.Set(0,1, Get(pos+new Vectori(0, yAdd)));
            baseNP.Set(1,1, Get(pos+new Vectori(xAdd, yAdd)));

            var Y = 0;
            var X = 0;
            for (var y = 0f; y < 1; y += 1f/scalar)
            {

                for (var x = 0f; x < 1; x += 1f/scalar)
                {
                    output.Set(X,Y, MathB.BilinearInterpolate(
                        baseNP.Get(0,0),
                        baseNP.Get(1,0),
                        baseNP.Get(0,1),
                        baseNP.Get(1,1),
                        new Vectorf(x,y)
                    ));

                    X++;
                }

                X = 0;
                Y++;
            }

            return output;
        }

        public NodeMap UpscaleRange(Vectori start, Vectori end, int scalar)
        {
            var size = end - start + new Vectori(1, 1);
            var output = new NodeMap(size*scalar);

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var baseNP = UpscaleBetweenNodes(start + new Vectori(x, y), scalar);
                    for (var by = 0; by < scalar; by++)
                    {
                        for (var bx = 0; bx < scalar; bx++)
                        {
                            output.Set(new Vectori(scalar*x+bx,scalar*y+by),baseNP.Get(bx,by));
                        }
                    }
                }
            }

            return output;
        }

        public NodeMap Upscale(int scalar) 
            => UpscaleRange(new Vectori(0, 0), Size - new Vectori(1, 1), scalar);

        public Node DownscaleBetweenNodes(Vectori pos, int divisor)
        {
            Node node;

            var sum = 0.0f;
            var oSum = 0.0f;

            for (int y = 0; y < divisor; y++)
            {
                for (int x = 0; x < divisor; x++)
                {
                    sum += Get(pos + new Vectori(x, y));
                    oSum += GetO(pos + new Vectori(x, y));
                }
            }

            node = new Node {Height = sum / (divisor * divisor), OHeight = oSum / (divisor * divisor)};

            return node;
        }

        public NodeMap DownscaleRange(Vectori start, Vectori end, int divisor)
        {
            var size = end - start + new Vectori(1,1);
            if (size.x % divisor != 0 || size.y % divisor != 0)
            {
                throw new Exception("disivor needs to be a divisor of the nodemap size.");
            }

            var NP = new NodeMap(size / divisor);

            for (int y = 0; y < NP.Size.y; y++)
            {
                for (int x = 0; x < NP.Size.x; x++)
                {
                    NP.Set(x,y,DownscaleBetweenNodes(start+new Vectori(x* divisor,y * divisor), divisor));
                }
            }

            return NP;
        }

        public NodeMap Downscale(int divisor) 
            => DownscaleRange(new Vectori(0, 0), Size - new Vectori(1, 1), divisor);

        public string Display
        {
            get
            {
                var maxes = new int[Size.x];
                var roundLength = 4;

                for (var x = 0; x < Size.x; x++)
                {
                    maxes[x] = maxes[x] == 0 ? 1 : maxes[x];
                    for (var y = 0; y < Size.y; y++)
                    {
                        maxes[x] = Math.Max(maxes[x], MathB.Round(Get(x, y), roundLength).ToString().Length);
                    }
                }

                var a = "";
                for (var y = 0; y < Size.y; y++)
                {
                    for (var x = 0; x < Size.x; x++)
                    {
                        a += Strings.AddWhitespace(
                            MathB.Round(Get(x,y), roundLength)
                                .ToString()
                            , maxes[x]-1
                        ) + (!(x == Size.x-1 && y == Size.y-1) ? "  " : "");
                    }

                    a += "\n";
                }


                return a;
            }
        }
    }
}
