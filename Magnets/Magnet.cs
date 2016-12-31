using System;
using System.Drawing;
using System.Linq;

namespace Magnets {
    public class Magnet {
        public Magnet(double diam, PointF loc, double force, Color color) {
            _location = loc;
            _diameter = diam;
            _color = color;
            Force = force;
            Velocity = new PointF(0, 0);
        }
        public Color Color {
            get { return _color; }
            set {
                _color = value;
                lock (this) {
                    _image = null;
                }
            }
        }
        public double Diameter {
            get { return _diameter; }
            set {
                _diameter = value;
                lock (this) {
                    _rect = null;
                    _image = null;
                }
            }
        }
        /// <summary>
        ///     Positive force is repellent, negative is attractive.
        /// </summary>
        public double Force { get; set; }
        public Bitmap Image {
            get {
                lock (this) {
                    if (_image == null) {
                        Size sz = new Size((int) Math.Ceiling(Rect.Width), (int) Math.Ceiling(Rect.Height));
                        _image = new Bitmap(sz.Width, sz.Height);
                        using (var gfx = Graphics.FromImage(_image)) {
                            gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, sz.Width, sz.Height));
                            gfx.FillEllipse(new SolidBrush(_color), new RectangleF(sz.Width - Rect.Width, sz.Height - Rect.Height, Rect.Width, Rect.Height));
                        }
                    }
                    return _image;
                }
            }
        }
        public PointF Location {
            get { return _location; }
            set {
                _location = value;
                lock (this) {
                    _rect = null;
                    _image = null;
                }
            }
        }
        public RectangleF Rect {
            get {
                if (_rect == null) _rect = new RectangleF((float) (_location.X - _diameter / 2), (float) (_location.Y - _diameter / 2), (float) _diameter, (float) _diameter);
                return _rect.Value;
            }
        }
        public PointF Velocity { get; set; }
        public static int ForceMultiplier { get; set; } = 1;
        public double DistanceTo(Magnet m) {
            return Math.Sqrt(Math.Pow(m.Location.X - Location.X, 2) + Math.Pow(m.Location.Y - Location.Y, 2));
        }
        public PointF ForceFrom(Magnet m) {
            var dist = DistanceTo(m);
            if (dist < m.Diameter / 2) dist = Diameter / 2;
            var angle = Angle(Location, m.Location);
            var magnitude = ForceMultiplier * m.Force * -1 / (dist * dist);
            PointF vector = new PointF(1, 0);
            vector = RotatePoint(vector, new PointF(0, 0), angle);
            return new PointF((float) (vector.X * magnitude), (float) (vector.Y * magnitude));
        }
        public void MoveByRotation(PointF center, double angleInDegrees) {
            var newloc = RotatePoint(Location, center, angleInDegrees);
            Location = newloc;
        }
        public override string ToString() {
            return $"[({Location.X}, {Location.Y}), {Force}, {Diameter}, ({Color.R},{Color.G},{Color.B})]";
        }
        public static Magnet FromString(string s) {
            string[] strs = s.Split(',').Select(ss => ss.Trim('(', ')', '[', ']', ' ')).ToArray();
            if (strs.Length != 7) throw new Exception("Invalid strring in Magnet.FromString");
            return new Magnet(double.Parse(strs[3]),
                              new PointF(float.Parse(strs[0]), float.Parse(strs[1])),
                              double.Parse(strs[2]),
                              Color.FromArgb(int.Parse(strs[4]), int.Parse(strs[5]), int.Parse(strs[6])));
        }
        private static double Angle(PointF p1, PointF p2) {
            float dY = p2.Y - p1.Y;
            float dX = p2.X - p1.X;
            return (float) (Math.Atan2(dY, dX) * 180 / Math.PI);
        }
        /// <summary>
        ///     Rotates one point arount another one
        /// </summary>
        /// <param name="pointToRotate">the point to rotate</param>
        /// <param name="centerPoint">the centre point of rotation</param>
        /// <param name="angleInDegrees">the rotation angle in degrees </param>
        /// <returns>Rotated point</returns>
        private static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, double angleInDegrees) {
            double angleInRadians = angleInDegrees * (float) (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new PointF {
                       X = (float) (cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                       Y = (float) (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
                   };
        }
        private Color _color;
        private double _diameter;
        private Bitmap _image;
        private PointF _location;
        private RectangleF? _rect;
    }
}