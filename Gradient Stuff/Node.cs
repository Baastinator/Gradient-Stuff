using System.Numerics;
using ILGPU.IR;

namespace Gradient_Stuff
{
    public struct Node
    {
        private Vector3 _height;
        private Vector3 _oHeight;

        public Vector3 Height
        {
            get => _height;
            set
            {
                value.X = value.X < 0 ? 0 : value.X > 255 ? 255 : value.X;
                value.Y = value.Y < 0 ? 0 : value.Y > 255 ? 255 : value.Y;
                value.Z = value.Z < 0 ? 0 : value.Z > 255 ? 255 : value.Z;
                _height = value;
            }
                
        }

        public Vector3 OHeight
        {
            get => _oHeight;
            set
            {
                value.X = value.X < 0 ? 0 : value.X > 255 ? 255 : value.X;
                value.Y = value.Y < 0 ? 0 : value.Y > 255 ? 255 : value.Y;
                value.Z = value.Z < 0 ? 0 : value.Z > 255 ? 255 : value.Z;
                _oHeight = value;
            }
        }
    }
}