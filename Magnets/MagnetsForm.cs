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
                gfx.FillRectangle(Brushes.Transparent, new RectangleF(0, 0, size.Width, size.Height));
            }
            _image = new Bitmap(size.Width, size.Height);
            using (var gfx = Graphics.FromImage(_image)) {
                gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, size.Width, size.Height));
            }
            _imageSize = size;
            Magnet = new Magnet(50, new PointF(size.Width / 2.0F, size.Height / 4.0F), 0, Color.White) {Velocity = new PointF(-0.1F, 0)};
            _invalidRect = new RectangleF(0, 0, size.Width, size.Height);
        }
        public double BaseRotation { get; set; }
        public SizeF ImageSize => _imageSize;
        public Color LineColor => Color.FromArgb((int) Math.Round(_lineColorValues[0]), (int) Math.Round(_lineColorValues[1]), (int) Math.Round(_lineColorValues[2]));
        public Magnet Magnet { get; }
        public Magnet SelectedMagnet {
            get { return _selectedMagnet; }
            set {
                if (!_stationaryMags.Any(m => ReferenceEquals(value, m))) return;
                if (_selectedMagnet != null) {
                    _selectedMagnet.Color = Color.Blue;
                    InvalidateRect(_selectedMagnet.Rect);
                }
                _selectedMagnet = value;
                _selectedMagnet.Color = Color.Yellow;
                InvalidateRect(_selectedMagnet.Rect);
                SelectedMagnetChanged?.Invoke(this, new EventArgs());
            }
        }
        public bool ShowMovingMagnet {
            get { return _showMovingMagnet; }
            set {
                _showMovingMagnet = value;
                InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        public bool ShowStationaryMagnets {
            get { return _showStationaryMagnets; }
            set {
                _showStationaryMagnets = value;
                InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        public Magnet[] StationaryMagnets => _stationaryMags.ToArray();
        public Color TargetColor { get; set; }
        public event EventHandler MagnetsChanged;
        public event EventHandler SelectedMagnetChanged;
        private void bwWorker_DoWork(object sender, DoWorkEventArgs e) {
            _running = true;
            while (_running) {
                PointF totalforce = new PointF(0, 0);
                foreach (var m in _stationaryMags) {
                    var f = Magnet.ForceFrom(m);
                    totalforce.X += f.X;
                    totalforce.Y += f.Y;
                }
                Magnet.Velocity = new PointF(Magnet.Velocity.X + totalforce.X, Magnet.Velocity.Y + totalforce.Y);
                var oldloc = Magnet.Location;
                var oldrect = Magnet.Rect;
                var newloc = new PointF(Magnet.Location.X + Magnet.Velocity.X, Magnet.Location.Y + Magnet.Velocity.Y);
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
                InvalidateRect(RectangleF.Union(oldrect, Magnet.Rect));
                if (Math.Abs(BaseRotation) > 0.00001) {
                    var center = new PointF(_imageSize.Width / 2F, _imageSize.Height / 2F);
                    foreach (var m in _stationaryMags) {
                        if (_showStationaryMagnets) InvalidateRect(m.Rect);
                        m.MoveByRotation(center, BaseRotation);
                        if (_showStationaryMagnets) InvalidateRect(m.Rect);
                    }
                }
                Thread.Sleep(0);
            }
        }
        private void MagnetsForm_MouseDown(object sender, MouseEventArgs e) {
            _lastMouseDownPos = e.Location;
        }
        private void MagnetsForm_MouseUp(object sender, MouseEventArgs e) {
            var p = ScreenPointToImage(e.Location);
            foreach (var m in _stationaryMags) {
                if (m.Rect.Contains(p)) {
                    SelectedMagnet = m;
                    break;
                }
            }
        }
        private void MagnetsForm_SizeChanged(object sender, EventArgs e) {
            InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
        }
        private void menuAddMagnetHere_Click(object sender, EventArgs e) {
            var pos = ScreenPointToImage(_lastMouseDownPos);
            pos.X = (int) Math.Round(pos.X / 10) * 10;
            pos.Y = (int) Math.Round(pos.Y / 10) * 10;
            var mag = new Magnet(50, pos, 100, Color.Blue);
            _stationaryMags.Add(mag);
            InvalidateRect(mag.Rect);
            MagnetsChanged?.Invoke(this, new EventArgs());
        }
        private void menuLoadMagnets_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string text = File.ReadAllText(openFileDialog.FileName);
                JSONArray json = new JSONArray(text);
                _stationaryMags.Clear();
                var mag = Magnet.FromString(json.getString(0));
                Magnet.Location = mag.Location;
                Magnet.Velocity = mag.Velocity;
                Magnet.Color = mag.Color;
                Magnet.Diameter = mag.Diameter;
                Magnet.Force = mag.Force;
                for (int i = 1; i < json.Count; ++i) {
                    _stationaryMags.Add(Magnet.FromString(json.getString(i)));
                }
                MagnetsChanged?.Invoke(this, new EventArgs());
                InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
            }
        }
        private void menuRemoveSelectedMagnet_Click(object sender, EventArgs e) {
            if (_selectedMagnet == null) return;
            _stationaryMags.Remove(_selectedMagnet);
            InvalidateRect(_selectedMagnet.Rect);
            _selectedMagnet = null;
            MagnetsChanged?.Invoke(this, new EventArgs());
        }
        private void menuSaveMagnets_Click(object sender, EventArgs e) {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                var json = new JSONArray();
                json.put(Magnet.ToString());
                foreach (var mag in _stationaryMags) json.put(mag.ToString());
                File.WriteAllText(saveFileDialog.FileName, json.ToString());
            }
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
                    gfx.FillRectangle(Brushes.Transparent, new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
                }
            }
            InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
        }
        public void InitializeMagnet(PointF loc, PointF vel) {
            InvalidateRect(Magnet.Rect);
            Magnet.Location = loc;
            InvalidateRect(Magnet.Rect);
            Magnet.Velocity = vel;
        }
        public void InvalidateRect(RectangleF rect) {
            if (rect.Width < 0.01 || rect.Height < 0.01) return;
            rect.Inflate(3, 3);
            _invalidRect = _invalidRect.Width < 0.01 || _invalidRect.Height < 0.01 ? rect : RectangleF.Union(_invalidRect, rect);
            _invalidRect.Intersect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
            Invalidate();
        }
        public PointF ScreenPointToImage(PointF p) {
            double scaleX = (double) _imageSize.Width / ClientRectangle.Width;
            double scaleY = (double) _imageSize.Height / ClientRectangle.Height;
            return new PointF((float) (p.X * scaleX), (float) (p.Y * scaleY));
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
            if (_invalidRect.Height < 0.01 || _invalidRect.Width < 0.01) return;
            using (var gfx = Graphics.FromImage(_image)) {
                gfx.FillRectangle(Brushes.Black, _invalidRect);
                lock (_bgoverlay) {
                    gfx.DrawImage(_bgoverlay, _invalidRect, _invalidRect, GraphicsUnit.Pixel);
                }
                if (ShowStationaryMagnets) {
                    foreach (var mag in _stationaryMags) {
                        lock (mag) {
                            if (mag.Rect.IntersectsWith(_invalidRect)) {
                                gfx.DrawImage(mag.Image, mag.Rect, new RectangleF(0, 0, mag.Rect.Width, mag.Rect.Height), GraphicsUnit.Pixel);
                            }
                        }
                    }
                }
                if (_showMovingMagnet) {
                    lock (Magnet) {
                        if (Magnet.Rect.IntersectsWith(_invalidRect))
                            gfx.DrawImage(Magnet.Image, Magnet.Rect, new RectangleF(0, 0, Magnet.Rect.Width, Magnet.Rect.Height), GraphicsUnit.Pixel);
                    }
                }
            }
            var scrRect = ImageRectToScreen(_invalidRect);
            e.Graphics.DrawImage(_image, scrRect, _invalidRect, GraphicsUnit.Pixel);
            _invalidRect = new RectangleF(0, 0, 0, 0);
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            // do nothing.
        }
        private RectangleF ImageRectToScreen(RectangleF rect) {
            double scaleX = (double) ClientRectangle.Width / _imageSize.Width;
            double scaleY = (double) ClientRectangle.Height / _imageSize.Height;
            return new RectangleF((float) (rect.X * scaleX), (float) (rect.Y * scaleY), (float) (rect.Width * scaleX), (float) (rect.Height * scaleY));
        }
        private RectangleF ScreenRectToImage(RectangleF rect) {
            double scaleX = (double) _imageSize.Width / ClientRectangle.Width;
            double scaleY = (double) _imageSize.Height / ClientRectangle.Height;
            return new RectangleF((float) (rect.X * scaleX), (float) (rect.Y * scaleY), (float) (rect.Width * scaleX), (float) (rect.Height * scaleY));
        }
        public Bitmap _bgoverlay;
        private readonly Bitmap _image;
        private Size _imageSize;
        private RectangleF _invalidRect;
        private Point _lastMouseDownPos;
        private readonly double[] _lineColorValues = new double[3];
        private bool _running;
        private Magnet _selectedMagnet;
        private bool _showMovingMagnet = true;
        private bool _showStationaryMagnets = true;
        private readonly List<Magnet> _stationaryMags = new List<Magnet>();
        //}
        //    InvalidateRect(new RectangleF(0,0,_imageSize.Width, _imageSize.Height));
        //    lblMousePos.Location = clientpos;
        //    lblMousePos.Text = "(" + scaledpos.X + ", " + scaledpos.Y + ")";
        //    var scaledpos = ScreenPointToImage(clientpos);
        //    var clientpos = PointToClient(MousePosition);

        //private void MagnetsForm_MouseMove(object sender, MouseEventArgs e) {
        //}
        //    //InvalidateRect(scaledrect);
        //    //lblMousePos.Visible = false;
        //    //var scaledrect = ScreenRectToImage(rect);
        //    //Rectangle rect = new Rectangle(lblMousePos.Location, lblMousePos.Size);

        //private void MagnetsForm_MouseLeave(object sender, EventArgs e) {
        //}
        //    InvalidateRect(new RectangleF(0, 0, _imageSize.Width, _imageSize.Height));
        //    lblMousePos.Visible = true;
        //    lblMousePos.Location = clientpos;
        //    lblMousePos.Text = "(" + scaledpos.X + ", " + scaledpos.Y + ")";
        //    var scaledpos = ScreenPointToImage(clientpos);
        //    var clientpos = PointToClient(MousePosition);
        //private void MagnetsForm_MouseEnter(object sender, EventArgs e) {
    }
}