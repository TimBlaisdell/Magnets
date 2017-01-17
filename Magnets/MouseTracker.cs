using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Magnets {
    public partial class MouseTracker : Form {
        public MouseTracker() {
            InitializeComponent();
        }
        public MouseTracker(MagnetsForm parent) : this() {
            _parent = parent;
        }
        public bool EditMode {
            get { return _editMode; }
            set {
                if (_editMode == value) return;
                _editMode = value;
                FormBorderStyle = _editMode ? FormBorderStyle.FixedDialog : FormBorderStyle.None;
                btnDone.Visible = _editMode;
                ResizeForm();
            }
        }
        public bool MagIsAttractive {
            get { return txtType.Text == "Attractive"; }
        }
        public string MagLocation {
            get { return txtLocation.Text; }
        }
        public string MagStrength {
            get { return txtStrength.Text; }
        }
        public string MousePosText {
            get { return lblMousePosition.Text; }
            set { lblMousePosition.Text = value; }
        }
        public event EventHandler Done;
        public event EventHandler ValueChanged;
        private void btnDone_Click(object sender, EventArgs e) {
            EditMode = false;
            Done?.Invoke(this, new EventArgs());
        }
        private void lblMousePosition_SizeChanged(object sender, EventArgs e) {
            ResizeForm();
        }
        private void MouseTracker_LocationChanged(object sender, EventArgs e) {
            if (_oldLocation != null) {
                var r = _parent.ScreenRectToImage(new Rectangle(_oldLocation.Value, Size));
                r.Inflate(r.Width * 2, r.Height * 2);
                _parent.InvalidateRect(r);
            }
            _oldLocation = Location;
        }
        private void MouseTracker_Shown(object sender, EventArgs e) {
            Size = lblMousePosition.Size;
            SetParent(Handle, _parent.Handle);
        }
        private void txtLocation_TextChanged(object sender, EventArgs e) {
            lblLocation.Text = txtLocation.Text;
            if (_editMode) {
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        private void txtType_Click(object sender, EventArgs e) {
            if (_editMode) {
                txtType.Text = txtType.Text == "Attractive" ? "Repulsive" : "Attractive";
                ValueChanged?.Invoke(this, new EventArgs());
            }
        }
        public void ShowMagnetInfo(Magnet mag) {
            if (mag == null) {
                panelMagnet.Visible = false;
            }
            else {
                txtLocation.Text = $"({Math.Round(mag.Location.X, 3)}, {Math.Round(mag.Location.Y, 3)})";
                txtType.Text = mag.Force < 0 ? "Attractive" : "Repulsive";
                txtStrength.Text = Math.Abs(mag.Force).ToString();
                panelMagnet.Visible = true;
            }
            ResizeForm();
        }
        private void ResizeForm() {
            _parent.InvalidateRect(_parent.ScreenRectToImage(new Rectangle(Location, Size)));
            var size = lblMousePosition.Size;
            if (panelMagnet.Visible) {
                size = new Size(lblLocation.Right, panelMagnet.Bottom + (btnDone.Visible ? btnDone.Height + 7 : 0));
            }
            Size = size;
            _parent.InvalidateRect(_parent.ScreenRectToImage(new Rectangle(Location, Size)));
        }
        [DllImport("user32.dll")] private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private bool _editMode;
        private Point? _oldLocation;
        private readonly MagnetsForm _parent;
    }
}