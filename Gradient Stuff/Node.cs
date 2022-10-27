using System.Numerics;

namespace Gradient_Stuff
{
    public struct Node
    {
        private float _height;
        private float _oHeight;

        public float Height
        {
            get => _height;
            set => _height = value < 0 ? 0 : value > 255 ? 255 : value;
        }

        public float OHeight
        {
            get => _oHeight;
            set => _oHeight = value < 0 ? 0 : value > 255 ? 255 : value;
        }
    }
}