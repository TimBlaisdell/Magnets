using System.Drawing;

namespace Magnets {
    public struct PointD {
        public PointD(double x, double y) {
            X = x;
            Y = y;
        }
        public PointD(Point p) {
            X = p.X;
            Y = p.Y;
        }
        public double X;
        public double Y;
        public override string ToString() => $"({X}, {Y})";
    }
}