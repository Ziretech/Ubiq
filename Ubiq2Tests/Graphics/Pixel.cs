using System;
using OpenTK.Graphics.OpenGL;

namespace Ubiq2Tests.Graphics
{
    public class Pixel
    {
        private readonly float[] _data = new float[3];

        public Pixel()
        {
        }

        public Pixel(float r, float g, float b)
        {
            _data[0] = r;
            _data[1] = g;
            _data[2] = b;
        }

        public void ReadBuffer(int x, int y)
        {
            GL.ReadPixels(x, y, 1, 1, PixelFormat.Rgb, PixelType.Float, _data);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Pixel);
        }

        public bool Equals(Pixel pixel)
        {
            return pixel != null &&
                   Math.Abs(_data[0] - pixel._data[0]) < 0.001f &&
                   Math.Abs(_data[1] - pixel._data[1]) < 0.001f &&
                   Math.Abs(_data[2] - pixel._data[2]) < 0.001f;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return _data[0] + ", " + _data[1] + ", " + _data[2];
        }
    }
}
