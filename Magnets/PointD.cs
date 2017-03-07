using System;
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
        public static PointD Parse(string s) {
            var p = new PointD();
            s = s.Trim('(', ')');
            var sa = s.Split(',');
            if (sa.Length != 2) throw new Exception("Invalid string value");
            p.X = double.Parse(sa[0]);
            p.Y = double.Parse(sa[1]);
            return p;
        }
    }
}