using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ubiq2.Graphics
{
    public class SizeInPixels : Size
    {
        public SizeInPixels(int width, int height) : base(width, height)
        {
        }

        public SizeInPixels(Size size) : base(size)
        {
        }
    }
}
