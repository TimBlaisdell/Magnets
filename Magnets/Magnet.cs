using System;
using System.Drawing;
using System.Linq;
using Magnets.Properties;

namespace Magnets {
    public class Magnet {
        public Magnet(PointD loc, double force) {
            _location = loc;
            //_diameter = diam;
            //_color = color;
            Force = force;
            Velocity = new PointD(0, 0);
        }
        //public Color Color {
        //    get { return Force < 0 ? Color.Blue : Force > 0 ? Color.Red : Color.White; }
        //}
        public double Diameter {
            get { return Math.Max(Math.Abs((Force == 0 ? 1 : Force) * 50), 40); }
            //set {
            //    _diameter = value;
            //    lock (this) {
            //        _rect = null;
            //        _image = null;
            //    }
            //}
        }
        /// <summary>
        ///     Positive force is repellent, negative is attractive.
        /// </summary>
        public double Force {
            get { return _force; }
            set {
                if (value == _force) return;
                _force = value;
                lock (this) {
                    _rect = null;
                    _image = null;
                }
            }
        }
        public Bitmap Image {
            get {
                lock (this) {
                    if (_image == null) {
                        Size sz = new Size((int) Math.Ceiling(Rect.Width), (int) Math.Ceiling(Rect.Height));
                        _image = new Bitmap(sz.Width, sz.Height);
                        var img = Force < 0 ? Resources.blue_magnet : Force == 0 ? Resources.white_magnet : Resources.red_magnet;
                        using (var gfx = Graphics.FromImage(_image)) {
                            gfx.DrawImage(img, new Rectangle(0, 0, sz.Width, sz.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                            //gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, sz.Width, sz.Height));
                            //gfx.FillEllipse(new SolidBrush(Color),
                            //                new RectangleF(Convert.ToSingle(sz.Width - Rect.Width),
                            //                               Convert.ToSingle(sz.Height - Rect.Height),
                            //                               Convert.ToSingle(Rect.Width),
                            //                               Convert.ToSingle(Rect.Height)));
                        }
                    }
                    return _image;
                }
            }
        }
        public PointD Location {
            get { return _location; }
            set {
                _location = value;
                lock (this) {
                    _rect = null;
                    //_image = null;
                }
            }
        }
        public RectangleD Rect {
            get {
                if (_rect == null) _rect = new RectangleD((float) (_location.X - Diameter / 2), (float) (_location.Y - Diameter / 2), (float) Diameter, (float) Diameter);
                return _rect.Value;
            }
        }
        public PointD Velocity { get; set; }
        public static double ForceMultiplier { get; set; } = 0.1;
        public double DistanceFrom(PointD p) {
            return Math.Sqrt(Math.Pow(p.X - Location.X, 2) + Math.Pow(p.Y - Location.Y, 2));
        }
        public double DistanceTo(Magnet m) {
            return Math.Sqrt(Math.Pow(m.Location.X - Location.X, 2) + Math.Pow(m.Location.Y - Location.Y, 2));
        }
        public PointD ForceFrom(Magnet m) {
            var dist = DistanceTo(m);
            if (dist < m.Diameter / 2) dist = Diameter / 2;
            var angle = Angle(Location, m.Location);
            var magnitude = ForceMultiplier * m.Force * -1 / (dist * dist);
            PointD vector = new PointD(1, 0);
            vector = RotatePoint(vector, new PointD(0, 0), angle);
            return new PointD(vector.X * magnitude, vector.Y * magnitude);
        }
        public void MoveByRotation(PointD center, double angleInDegrees) {
            var newloc = RotatePoint(Location, center, angleInDegrees);
            Location = newloc;
        }
        public override string ToString() {
            return $"[({Location.X}, {Location.Y}), {Force}, {Diameter}]";
        }
        public static double Angle(PointD p1, PointD p2) {
            double dY = p2.Y - p1.Y;
            double dX = p2.X - p1.X;
            //if (dY < 0.0001) return dX >= 0 ? 0 : 180;
            //if (dX < 0.0001) return dY >= 0 ? 0 : 180;
            return Math.Atan2(dY, dX) * 180D / Math.PI;
        }
        public static Magnet FromString(string s) {
            string[] strs = s.Split(',').Select(ss => ss.Trim('(', ')', '[', ']', ' ')).ToArray();
            if (strs.Length != 4) throw new Exception("Invalid strring in Magnet.FromString");
            return new Magnet(new PointD(double.Parse(strs[0]), double.Parse(strs[1])),
                              double.Parse(strs[2]));
            //Color.FromArgb(int.Parse(strs[4]), int.Parse(strs[5]), int.Parse(strs[6])));
        }
        /// <summary>
        ///     Rotates one point arount another one
        /// </summary>
        /// <param name="pointToRotate">the point to rotate</param>
        /// <param name="centerPoint">the centre point of rotation</param>
        /// <param name="angleInDegrees">the rotation angle in degrees </param>
        /// <returns>Rotated point</returns>
        public static PointD RotatePoint(PointD pointToRotate, PointD centerPoint, double angleInDegrees) {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new PointD {
                       X = cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X,
                       Y = sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y
                   };
        }
        //private Color _color;
        //private double _diameter;
        private Bitmap _image;
        private PointD _location;
        private RectangleD? _rect;
        private double _force;
    }
}