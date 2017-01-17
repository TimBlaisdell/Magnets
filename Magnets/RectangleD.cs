using System;
using System.Collections.Generic;
using System.Drawing;

namespace Magnets {
    public struct RectangleD {
        public RectangleD(double x, double y, double width, double height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public void Inflate(double x, double y) {
            X -= x;
            Y -= y;
            Width += x * 2;
            Height += y * 2;
            if (Width < 0) Width = 0;
            if (Height < 0) Height = 0;
        }
        private static double TOLERANCE = 0.0000001;
        public static bool operator ==(RectangleD r1, RectangleD r2) {
            return Math.Abs(r1.X - r2.X) < TOLERANCE && Math.Abs(r1.Y - r2.Y) < TOLERANCE && Math.Abs(r1.Width - r2.Width) < TOLERANCE && Math.Abs(r1.Height - r2.Height) < TOLERANCE;
        }
        public static bool operator !=(RectangleD r1, RectangleD r2) {
            return !(r1 == r2);
        }
        public static RectangleD Union(IEnumerable<RectangleD> rects) {
            RectangleD r = new RectangleD(0, 0, 0, 0);
            bool first = true;
            foreach (var rect in rects) {
                if (first) r = rect;
                else r.Union(rect);
                first = false;
            }
            return r;
        }
        public void Union(RectangleD r) {
            var p = new PointD(Math.Min(X, r.X), Math.Min(Y, r.Y));
            var p2 = new PointD(Math.Max(X + Width, r.X + r.Width), Math.Max(Y + Height, r.Y + r.Height));
            X = p.X;
            Y = p.Y;
            Width = p2.X - p.X;
            Height = p2.Y - p.Y;
        }
        public static explicit operator RectangleF(RectangleD r) {
            return new RectangleF(Convert.ToSingle(r.X), Convert.ToSingle(r.Y), Convert.ToSingle(r.Width), Convert.ToSingle(r.Height));
        }
        public bool IntersectsWith(RectangleD r) {
            return Contains(r.X, r.Y) || Contains(r.X + r.Width, r.Y) || Contains(r.X + r.Width, r.Y + r.Height) || Contains(r.X, r.Y + r.Height) ||
                   r.Contains(X, Y) || r.Contains(X + Width, Y) || r.Contains(X + Width, Y + Height) || r.Contains(X, Y + Height);
        }
        public void Intersect(RectangleD r) {
            var p1 = new PointD(Math.Max(X, r.X), Math.Max(Y, r.Y));
            var p2 = new PointD(Math.Min(X + Width, r.X + r.Width), Math.Min(Y + Height, r.Y + r.Height));
            double w = p2.X - p1.X;
            double h = p2.Y - p1.Y;
            X = p1.X;
            Y = p1.Y;
            if (w < 0 || h < 0) Height = Width = 0;
            else {
                Height = h;
                Width = w;
            }
        }
        public bool Contains(PointD p) => Contains(p.X, p.Y);
        public override string ToString() => $"({X}, {Y}, {Width}, {Height})";
        public bool Contains(double x, double y) => x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        public static RectangleD Union(RectangleD r1, RectangleD r2) {
            var p = new PointD(Math.Min(r1.X, r2.X), Math.Min(r1.Y, r2.Y));
            var p2 = new PointD(Math.Max(r1.X + r1.Width, r2.X + r2.Width), Math.Max(r1.Y + r1.Height, r2.Y + r2.Height));
            return new RectangleD(p.X, p.Y, p2.X - p.X, p2.Y - p.Y);
        }
        public double X;
        public double Y;
        public double Width;
        public double Height;
    }
}