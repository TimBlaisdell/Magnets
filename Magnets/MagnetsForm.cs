using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using IODPUtils.JSON;

namespace Magnets {
    public partial class MagnetsForm : Form {
        public MagnetsForm() : this(new Size(500, 500)) {
        }
        public MagnetsForm(Size size) {
            InitializeComponent();
            _bgoverlay = new Bitmap(size.Width, size.Height);
            using (var gfx = Graphics.FromImage(_bgoverlay)) {
                gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, size.Width, size.Height));
            }
            _image = new Bitmap(size.Width, size.Height);
            using (var gfx = Graphics.FromImage(_image)) {
                gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, size.Width, size.Height));
            }
            _imageSize = size;
            Magnet = new Magnet(new PointD(size.Width / 2.0, size.Height / 4.0), 0) {Velocity = new PointD(-0.1, 0)};
            _invalidRect = new RectangleD(0, 0, size.Width, size.Height);
        }
        public double BaseRotation { get; set; }
        public int Friction { get; set; }
        public SizeD ImageSize => new SizeD(_imageSize);
        public Color LineColor => Color.FromArgb((int) Math.Round(_lineColorValues[0]), (int) Math.Round(_lineColorValues[1]), (int) Math.Round(_lineColorValues[2]));
        public Magnet Magnet { get; }
        public bool ShowMovingMagnet {
            get { return _showMovingMagnet; }
            set {
                _showMovingMagnet = value;
                InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        public bool ShowStationaryMagnets {
            get { return _showStationaryMagnets; }
            set {
                _showStationaryMagnets = value;
                InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        public Magnet[] StationaryMagnets => _stationaryMags.ToArray();
        public Color TargetColor { get; set; }
        public event EventHandler MagnetsChanged;
        private void bwWorker_DoWork(object sender, DoWorkEventArgs e) {
            _running = true;
            while (_running) {
                PointD totalforce = new PointD(0, 0);
                foreach (var m in _stationaryMags) {
                    var f = Magnet.ForceFrom(m);
                    totalforce.X += f.X;
                    totalforce.Y += f.Y;
                }
                //if (Friction > 0) {
                //    PointD vector = new PointD(0.1 * Friction, 0);
                //    double angle = Magnet.Angle(new PointD(0, 0), Magnet.Velocity);
                //    vector = Magnet.RotatePoint(vector, new PointD(0, 0), angle);
                //    totalforce.X += vector.X;
                //    totalforce.Y += vector.Y;
                //}
                Magnet.Velocity = new PointD(Magnet.Velocity.X + totalforce.X, Magnet.Velocity.Y + totalforce.Y);
                if (Friction > 0) {
                    //double d = 0.000000001 * Friction;
                    //PointD effect = new PointF(Magnet.Velocity.X < -1 * d ? d : Magnet.Velocity.X > d ? -1 * d : -1 * Magnet.Velocity.X,
                    //                           Magnet.Velocity.Y < -1 * d ? d : Magnet.Velocity.Y > d ? -1 * d : -1 * Magnet.Velocity.Y);
                    Magnet.Velocity = new PointD(Magnet.Velocity.X * 0.9999999, Magnet.Velocity.Y * 0.9999999);
                }
                var oldloc = Magnet.Location;
                var oldrect = Magnet.Rect;
                var newloc = new PointD(Magnet.Location.X + Magnet.Velocity.X, Magnet.Location.Y + Magnet.Velocity.Y);
                Magnet.Location = newloc;
                Point p1 = new Point((int) Math.Round(oldloc.X), (int) Math.Round(oldloc.Y));
                Point p2 = new Point((int) Math.Round(newloc.X), (int) Math.Round(newloc.Y));
                if (p1 != p2) {
                    lock (_bgoverlay) {
                        using (var gfx = Graphics.FromImage(_bgoverlay)) {
                            Pen p = new Pen(Color.FromArgb((int) Math.Round(_lineColorValues[0]), (int) Math.Round(_lineColorValues[1]), (int) Math.Round(_lineColorValues[2])), 1);
                            gfx.DrawLine(p, p1, p2);
                            if (p.Color.R != TargetColor.R) _lineColorValues[0] += 0.01 * (p.Color.R < TargetColor.R ? 1 : -1);
                            if (p.Color.G != TargetColor.G) _lineColorValues[1] += 0.01 * (p.Color.G < TargetColor.G ? 1 : -1);
                            if (p.Color.B != TargetColor.B) _lineColorValues[2] += 0.01 * (p.Color.B < TargetColor.B ? 1 : -1);
                        }
                    }
                }
                InvalidateRect(RectangleD.Union(oldrect, Magnet.Rect));
                if (Math.Abs(BaseRotation) > 0.00001) {
                    var center = new PointD(_imageSize.Width / 2D, _imageSize.Height / 2D);
                    foreach (var m in _stationaryMags) {
                        if (_showStationaryMagnets) InvalidateRect(m.Rect);
                        m.MoveByRotation(center, BaseRotation);
                        if (_showStationaryMagnets) InvalidateRect(m.Rect);
                    }
                }
                Thread.Sleep(0);
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            var clientpoint = PointToClient(MousePosition);
            var p = ScreenPointToImage(new PointD(clientpoint));
            var mag = _stationaryMags.FirstOrDefault(magnet => magnet.DistanceFrom(p) < magnet.Diameter / 2.0);
            if (mag != null) {
                menuAddMagnetHere.Visible = false;
                menuRemoveSelectedMagnet.Visible = true;
            }
            else {
                menuAddMagnetHere.Visible = true;
                menuRemoveSelectedMagnet.Visible = false;
            }
        }
        private void MagnetsForm_MouseDown(object sender, MouseEventArgs e) {
            _lastMouseDownPos = e.Location;
        }
        private void MagnetsForm_MouseEnter(object sender, EventArgs e) {
            if (_editingMagnet != null) return;
            Capture = true;
            var clientpoint = PointToClient(MousePosition);
            var p = ScreenPointToImage(new PointD(clientpoint));
            _mouseTracker?.Close();
            _mouseTracker = new MouseTracker(this);
            _mouseTracker.Done += (o, args) => {
                                      InvalidateRect();
                                      _editingMagnet = null;
                                  };
            _mouseTracker.ValueChanged += MouseTrackerOnValueChanged;
            clientpoint = clientpoint.X < ClientRectangle.Width / 2
                ? new Point(clientpoint.X + 8, clientpoint.Y - _mouseTracker.Height / 2)
                : new Point(clientpoint.X - (_mouseTracker.Width + 8), clientpoint.Y - _mouseTracker.Height / 2);
            _mouseTracker.Location = clientpoint;
            _mouseTracker.MousePosText = $"{Math.Round(p.X)}, {Math.Round(p.Y)}";
            _mouseTracker.Show();
        }
        private void MagnetsForm_MouseLeave(object sender, EventArgs e) {
            if (_editingMagnet != null) return;
            var p = MousePosition;
            var clientpoint = PointToClient(p);
            var rect = new Rectangle(Location, Size);
            if (rect.Contains(p)) return;
            Capture = false;
            clientpoint = clientpoint.X < ClientRectangle.Width / 2
                ? new Point(clientpoint.X + 8, clientpoint.Y - _mouseTracker.Height / 2)
                : new Point(clientpoint.X - (_mouseTracker.Width + 8), clientpoint.Y - _mouseTracker.Height / 2);
            rect = new Rectangle(clientpoint, _mouseTracker.Size);
            rect.Inflate(10, 10);
            InvalidateRect(ScreenRectToImage(rect));
            _mouseTracker?.Close();
            _mouseTracker = null;
        }
        private void MagnetsForm_MouseMove(object sender, MouseEventArgs e) {
            if (_editingMagnet != null) return;
            if (_mouseTracker == null) return;
            var clientpoint = PointToClient(MousePosition);
            if (clientpoint == _mouseTracker.Location) return;
            var p = ScreenPointToImage(new PointD(clientpoint));
            clientpoint = clientpoint.X < ClientRectangle.Width / 2
                ? new Point(clientpoint.X + 15, clientpoint.Y - _mouseTracker.Height / 2)
                : new Point(clientpoint.X - (_mouseTracker.Width + 15), clientpoint.Y - _mouseTracker.Height / 2);
            var rect = new Rectangle(_mouseTracker.Location, _mouseTracker.Size);
            rect.Inflate(10, 10);
            //InvalidateRect(ScreenRectToImage(rect));
            _mouseTracker.MousePosText = $"{Math.Round(p.X)}, {Math.Round(p.Y)}";
            var mag = _stationaryMags.FirstOrDefault(magnet => magnet.DistanceFrom(p) < magnet.Diameter / 2.0);
            if (mag != null) {
                _mouseTracker.ShowMagnetInfo(mag);
            }
            else _mouseTracker.ShowMagnetInfo(null);
            rect = Rectangle.Union(rect, new Rectangle(clientpoint, _mouseTracker.Size));
            rect.Inflate(10, 10);
            _mouseTracker.Location = clientpoint;
            InvalidateRect(ScreenRectToImage(rect));
        }
        private void MagnetsForm_MouseUp(object sender, MouseEventArgs e) {
            var p = ScreenPointToImage(new PointD(e.Location));
            foreach (var m in _stationaryMags) {
                if (m.Rect.Contains(p)) {
                    break;
                }
            }
        }
        private void MagnetsForm_Shown(object sender, EventArgs e) {
            timerCleanup.Start();
            //var f = new MouseTracker(this);
            //f.Location = new Point(0, 0);
            //f.Show();
        }
        private void MagnetsForm_SizeChanged(object sender, EventArgs e) {
            InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
        }
        private void menuAddMagnetHere_Click(object sender, EventArgs e) {
            var pos = ScreenPointToImage(new PointD(_lastMouseDownPos));
            pos.X = (int) Math.Round(pos.X / 10) * 10;
            pos.Y = (int) Math.Round(pos.Y / 10) * 10;
            var mag = new Magnet(pos, _stationaryMags.Average(m => m.Force));
            _stationaryMags.Add(mag);
            InvalidateRect(mag.Rect);
            MagnetsChanged?.Invoke(this, new EventArgs());
        }
        private void menuEditMagnet_Click(object sender, EventArgs e) {
            var pos = ScreenPointToImage(new PointD(_lastMouseDownPos));
            var mag = _stationaryMags.FirstOrDefault(m => m.DistanceFrom(pos) <= m.Diameter / 2);
            if (mag != null) {
                _mouseTracker.EditMode = true;
                _editingMagnet = mag;
            }
        }
        private void menuLoadMagnets_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string text = File.ReadAllText(openFileDialog.FileName);
                var jobj = new JSONObject(text);
                InitLocation = PointD.Parse(jobj.getString("InitialLocation"));
                InitVelocity = PointD.Parse(jobj.getString("InitialVelocity"));
                Magnet.ForceMultiplier = double.Parse(jobj.optString("ForceMultiplier", jobj.optString("ForceMultipler", "1"))); // because I'd misspelled it orignally.
                JSONArray json = jobj.getJSONArray("Magnets");
                _stationaryMags.Clear();
                //var mag = Magnet.FromString(json.getString(0));
                Magnet.Location = InitLocation;
                Magnet.Velocity = InitVelocity;
                //Magnet.Color = mag.Color;
                //Magnet.Diameter = mag.Diameter;
                //Magnet.Force = mag.Force;
                for (int i = 1; i < json.Count; ++i) {
                    _stationaryMags.Add(Magnet.FromString(json.getString(i)));
                }
                MagnetsChanged?.Invoke(this, new EventArgs());
                InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        private void menuRemoveSelectedMagnet_Click(object sender, EventArgs e) {
            var pos = ScreenPointToImage(new PointD(_lastMouseDownPos));
            var mag = _stationaryMags.FirstOrDefault(m => m.DistanceFrom(pos) <= m.Diameter / 2);
            if (mag != null) {
                _stationaryMags.Remove(mag);
                InvalidateRect(mag.Rect);
                MagnetsChanged?.Invoke(this, new EventArgs());
            }
        }
        private void menuSaveMagnets_Click(object sender, EventArgs e) {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                var jobj = new JSONObject();
                jobj.put("InitialLocation", InitLocation.ToString());
                jobj.put("InitialVelocity", InitVelocity.ToString());
                jobj.put("ForceMultiplier", Magnet.ForceMultiplier.ToString());
                var json = new JSONArray();
                json.put(Magnet.ToString());
                foreach (var mag in _stationaryMags) json.put(mag.ToString());
                jobj.put("Magnets", json);
                File.WriteAllText(saveFileDialog.FileName, jobj.ToString());
            }
        }
        private void MouseTrackerOnValueChanged(object sender, EventArgs eventArgs) {
            if (_editingMagnet == null || _mouseTracker == null) return;
            // force
            double d;
            if (!double.TryParse(_mouseTracker.MagStrength, out d)) return;
            _editingMagnet.Force = Math.Abs(d) * (_mouseTracker.MagIsAttractive ? -1 : 1);
            // location
            if (string.IsNullOrEmpty(_mouseTracker.MagLocation)) return;
            string val = _mouseTracker.MagLocation.Trim('(', ')');
            string[] vals = val.Split(',');
            double x, y;
            if (vals.Length != 2 || !double.TryParse(vals[0], out x) || !double.TryParse(vals[1], out y)) return;
            _editingMagnet.Location = new PointD(x, y);
            InvalidateRect();
        }
        private void timerAnimate_Tick(object sender, EventArgs e) {
            if (_invalidRect.Width < 0.01 || _invalidRect.Height < 0.01) return;
            Invalidate();
        }
        public void AddStationaryMagnet(Magnet m) {
            _stationaryMags.Add(m);
            InvalidateRect(m.Rect);
        }
        public void ClearPath() {
            lock (_bgoverlay) {
                _bgoverlay = new Bitmap(_imageSize.Width, _imageSize.Height);
                using (var gfx = Graphics.FromImage(_bgoverlay)) {
                    gfx.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, _imageSize.Width, _imageSize.Height));
                }
            }
            InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
        }
        public void InitializeMagnet(PointD loc, PointD vel) {
            InvalidateRect(Magnet.Rect);
            Magnet.Location = loc;
            InvalidateRect(Magnet.Rect);
            Magnet.Velocity = vel;
            InitLocation = loc;
            InitVelocity = vel;
        }
        public void InvalidateRect() {
            InvalidateRect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
        }
        public void InvalidateRect(RectangleD rect) {
            if (rect.Width < 0.01 || rect.Height < 0.01) return;
            rect.Inflate(3, 3);
            //lock (_lockobj) {
            _invalidRect = _invalidRect.Width < 0.01 || _invalidRect.Height < 0.01 ? rect : RectangleD.Union(_invalidRect, rect);
            _invalidRect.Intersect(new RectangleD(0, 0, _imageSize.Width, _imageSize.Height));
            //}
            Invalidate();
        }
        public PointD ScreenPointToImage(PointD p) {
            double scaleX = (double) _imageSize.Width / ClientRectangle.Width;
            double scaleY = (double) _imageSize.Height / ClientRectangle.Height;
            return new PointD(p.X * scaleX, p.Y * scaleY);
        }
        public RectangleD ScreenRectToImage(Rectangle rect) {
            double scaleX = _imageSize.Width / (double) ClientRectangle.Width;
            double scaleY = _imageSize.Height / (double) ClientRectangle.Height;
            return new RectangleD(rect.X * scaleX, rect.Y * scaleY, rect.Width * scaleX, rect.Height * scaleY);
        }
        public void Start() {
            bwWorker.RunWorkerAsync();
            timerAnimate.Start();
        }
        public void Stop() {
            _running = false;
            timerAnimate.Stop();
        }
        protected override void OnPaint(PaintEventArgs e) {
            //lock (_lockobj) {
            try {
                if (_invalidRect.Height < 0.01D || _invalidRect.Width < 0.01D) return;
                using (var gfx = Graphics.FromImage(_image)) {
                    gfx.FillRectangle(Brushes.Black, (RectangleF) _invalidRect);
                    lock (_bgoverlay) {
                        gfx.DrawImage(_bgoverlay, (RectangleF) _invalidRect, (RectangleF) _invalidRect, GraphicsUnit.Pixel);
                    }
                    if (ShowStationaryMagnets) {
                        foreach (var mag in _stationaryMags) {
                            lock (mag) {
                                if (mag.Rect.IntersectsWith(_invalidRect)) {
                                    gfx.DrawImage(mag.Image,
                                                  (RectangleF) mag.Rect,
                                                  new RectangleF(0, 0, Convert.ToSingle(mag.Rect.Width), Convert.ToSingle(mag.Rect.Height)),
                                                  GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                    if (_showMovingMagnet) {
                        lock (Magnet) {
                            if (Magnet.Rect.IntersectsWith(_invalidRect))
                                gfx.DrawImage(Magnet.Image,
                                              (RectangleF) Magnet.Rect,
                                              new RectangleF(0, 0, Convert.ToSingle(Magnet.Rect.Width), Convert.ToSingle(Magnet.Rect.Height)),
                                              GraphicsUnit.Pixel);
                        }
                    }
                }
                var scrRect = ImageRectToScreen(_invalidRect);
                e.Graphics.DrawImage(_image, (RectangleF) scrRect, (RectangleF) _invalidRect, GraphicsUnit.Pixel);
                _invalidRect = new RectangleD(0, 0, 0, 0);
                //}
            }
            catch (Exception ex) {
                int i = 0;
                // do nothing.
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            // do nothing.
        }
        private RectangleD ImageRectToScreen(RectangleD rect) {
            double scaleX = (double) ClientRectangle.Width / _imageSize.Width;
            double scaleY = (double) ClientRectangle.Height / _imageSize.Height;
            return new RectangleD((float) (rect.X * scaleX), (float) (rect.Y * scaleY), (float) (rect.Width * scaleX), (float) (rect.Height * scaleY));
        }
        public Bitmap _bgoverlay;
        private Magnet _editingMagnet;
        private readonly Bitmap _image;
        private Size _imageSize;
        private RectangleD _invalidRect;
        private Point _lastMouseDownPos;
        private readonly double[] _lineColorValues = new double[3];
        private readonly object _lockobj = new object();
        private MouseTracker _mouseTracker;
        private bool _running;
        private bool _showMovingMagnet = true;
        private bool _showStationaryMagnets = true;
        private readonly List<Magnet> _stationaryMags = new List<Magnet>();
        public PointD InitLocation { get; private set; }
        public PointD InitVelocity { get; private set; }

        private void timerCleanup_Tick(object sender, EventArgs e) {
            InvalidateRect(new RectangleD(0, 0, ImageSize.Width, ImageSize.Height));
        }
    }
}