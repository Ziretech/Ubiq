using System;

namespace Ubiq2.Graphics
{
    public class Size
    {
        public readonly int Width;
        public readonly int Height;

        public Size(int width, int height)
        {
            ValidateWidthAndHeightPositive(width, height);

            Width = width;
            Height = height;
        }

        public Size(Size size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public override string ToString()
        {
            return Width + ", " + Height;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as Size);
        }

        public bool Equals(Size size)
        {
            return size != null && size.Width == Width && size.Height == Height;
        }

        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        private void ValidateWidthAndHeightPositive(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", width, "Width must be a positive, non-negative integer.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", height, "Height must be a positive, non-negative integer.");
        }
    }
}
