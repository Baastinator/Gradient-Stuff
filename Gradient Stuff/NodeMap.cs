using System;
using System.Drawing;
using Gradient_Stuff.MathCode;
using Gradient_Stuff.Vector;

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
                    _map[y][x].Height = filler;
                    _map[y][x].OHeight = filler;
                }
            }
        }

        public NodeMap(Vectori Size, float filler = 0) 
            => new NodeMap(Size.x, Size.y, filler);

        public Bitmap Bitmap
        {
            get
            {
                var output = new Bitmap(Size.x, Size.y);

                for (var y = 0; y < Size.y; y++)
                {
                    for (var x = 0; x < Size.x; x++)
                    {
                        var col = (int)Utils.Round(Get(x, y), 0);
                        output.SetPixel(x, y, Color.FromArgb(255, col, col, col));
                    }
                }

                return output;
            }
        }

        public void Set(int x, int y, float value)
        {
            if (y < 0 || y >= _map.Length) return;
            if (x < 0 || x >= _map[y].Length) return;
            _map[y][x].Height = value;
        }

        public void Set(Vectori pos, float value) => Set(pos.x, pos.y, value);

        public void Set(int x, int y, Node value) => _map[y][x] = value;
        public void Set(Vectori pos, Node value) => Set(pos.x, pos.y, value);

        public void Set(int x, int y, float value, float oValue)
        {
            Set(x, y, value);
            _map[y][x].OHeight = oValue;
        }

        public void Set(Vectori pos, float value, float oValue)
            => Set(pos.x, pos.y, value, oValue);

        public float Get(int x, int y) => _map[y][x].Height;

        public float Get(Vectori pos) => Get(pos.x, pos.y);

        public float GetO(int x, int y) => _map[y][x].OHeight;
        public float GetO(Vectori pos) => GetO(pos.x, pos.y);
       
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
                        maxes[x] = Math.Max(maxes[x], Utils.Round(Get(x, y), roundLength).ToString().Length);
                    }
                }

                var a = "";
                for (var y = 0; y < Size.y; y++)
                {
                    for (var x = 0; x < Size.x; x++)
                    {
                        a += Utils.AddWhitespace(
                            Utils.Round(Get(x, y), roundLength)
                                .ToString()
                            , maxes[x] - 1
                        ) + (!(x == Size.x - 1 && y == Size.y - 1) ? "  " : "");
                    }

                    a += "\n";
                }


                return a;
            }
        }
    }
}