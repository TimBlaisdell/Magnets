using System.Drawing;

namespace Magnets {
    public struct SizeD {
        public SizeD(Size size) {
            Width = size.Width;
            Height = size.Height;
        }
        public double Width;
        public double Height;
        public override string ToString() => $"{Width}, {Height})";
    }
}