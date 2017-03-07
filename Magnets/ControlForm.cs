using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Utilities;

namespace Magnets {
    public partial class ControlForm : Form {
        public ControlForm(MagnetsForm magform) {
            InitializeComponent();
            var c = Settings.Get<int[]>("TargetColor");
            btnTargetColor.BackColor = magform.TargetColor = Color.FromArgb(c[0], c[1], c[2]);
            _magform = magform;
            var strs = Settings.Get<string[]>("Magnets");
            foreach (string str in strs) {
                Magnet m = Magnet.FromString(str);
                _magform.AddStationaryMagnet(m);
            }
            var colorvals = Settings.Get<int[]>("TargetColor");
            _magform.BaseRotation = Settings.Get<double>("BaseRotation");
            txtBaseRotation.Text = _magform.BaseRotation.ToString();
            _magform.TargetColor = Color.FromArgb(colorvals[0], colorvals[1], colorvals[2]);
            //_magform.MouseEnter += (sender, args) => {
            //                           var p1 = _magform.PointToClient(MousePosition);
            //                           var p2 = _magform.ScreenPointToImage(new PointD(p1));
            //                           p1 = new Point((int) Math.Round(p2.X), (int) Math.Round(p2.Y));
            //                           lblMouseLoc.Text = "(" + p1.X + ", " + p1.Y + ")";
            //                           _magform.Tag = p1;
            //                           lblMouseLoc.Visible = true;
            //                       };
            //_magform.MouseLeave += (sender, args) => lblMouseLoc.Visible = false;
            //_magform.MouseMove += (sender, args) => {
            //                          var p1 = _magform.PointToClient(MousePosition);
            //                          var p2 = _magform.ScreenPointToImage(new PointD(p1));
            //                          p1 = new Point((int) Math.Round(p2.X), (int) Math.Round(p2.Y));
            //                          _magform.Tag = p1;
            //                          lblMouseLoc.Text = "(" + p1.X + ", " + p1.Y + ")";
            //                      };
            _magform.MagnetsChanged += (sender, args) => PopulateMagnetTable();
            //_magform.SelectedMagnetChanged += (sender, args) => {
            //                                      if (_magform.SelectedMagnet == null) return;
            //                                      foreach (DataGridViewRow row in dgvMagnets.Rows) {
            //                                          if (ReferenceEquals(_magform.SelectedMagnet, row.Tag)) {
            //                                              row.Selected = true;
            //                                              break;
            //                                          }
            //                                      }
            //                                  };
        }
        private void btnCleanup_Click(object sender, EventArgs e) {
            _magform.InvalidateRect(new RectangleD(0, 0, _magform.ImageSize.Width, _magform.ImageSize.Height));
        }
        private void btnReset_Click(object sender, EventArgs e) {
            txtInitLoc_TextChanged(txtInitLoc, null);
            txtInitLoc_TextChanged(txtInitVel, null);
            for (int r = 0; r < dgvMagnets.RowCount; ++r) {
                dgvMagnets_CellValueChanged(dgvMagnets, new DataGridViewCellEventArgs(0, r));
            }
            _magform.ClearPath();
        }
        private void btnStart_Click(object sender, EventArgs e) {
            if (btnStart.Text == "Start") {
                btnReset.Enabled = false;
                btnStart.Text = "Stop";
                _magform.Start();
            }
            else {
                btnReset.Enabled = true;
                btnStart.Text = "Start";
                _magform.Stop();
            }
        }
        private void btnTargetColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                _magform.TargetColor = btnTargetColor.BackColor = colorDialog.Color;
                Settings.Values["TargetColor"] = new double[] {colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B};
            }
        }
        private void chkShowMoving_CheckedChanged(object sender, EventArgs e) {
            _magform.ShowMovingMagnet = chkShowMoving.Checked;
        }
        private void chkShowStationary_CheckedChanged(object sender, EventArgs e) {
            _magform.ShowStationaryMagnets = chkShowStationary.Checked;
        }
        private void ControlForm_Shown(object sender, EventArgs e) {
            _magform.Show();
            _magform.Location = new Point(Right, Top);
            txtInitLoc.Text = Settings.Get<string>("InitialLocation");
            txtInitVel.Text = Settings.Get<string>("InitialVelocity");
            numForceMultiplier.Value = Settings.Get<int>("ForceMultiplier");
            PopulateMagnetTable();
        }
        private void dgvMagnets_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var mag = (Magnet) dgvMagnets.Rows[e.RowIndex].Tag;
            if (mag == null) return;
            var cell = dgvMagnets.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.ColumnIndex == colDiameter.Index) {
                double d;
                if (!double.TryParse((string) cell.Value, out d)) return;
                _magform.InvalidateRect(mag.Rect);
                //mag.Diameter = d;
                _magform.InvalidateRect(mag.Rect);
            }
            else if (e.ColumnIndex == colForceMag.Index) {
                double d;
                if (!double.TryParse((string) cell.Value, out d)) return;
                mag.Force = d;
            }
            else if (e.ColumnIndex == colLocation.Index) {
                string val = cell.Value as string;
                if (string.IsNullOrEmpty(val)) return;
                val = val.Trim('(', ')');
                string[] vals = val.Split(',');
                double x, y;
                if (vals.Length != 2 || !double.TryParse(vals[0], out x) || !double.TryParse(vals[1], out y)) return;
                _magform.InvalidateRect(mag.Rect);
                mag.Location = new PointD(x, y);
                _magform.InvalidateRect(mag.Rect);
            }
            Settings.Values["Magnets"] = _magform.StationaryMagnets.Select(m => m.ToString()).ToArray();
        }
        //private void lblMouseLoc_TextChanged(object sender, EventArgs e) {
        //    lblMouseLoc.Left = dgvMagnets.Right - lblMouseLoc.Width;
        //}
        private void numForceMultiplier_ValueChanged(object sender, EventArgs e) {
            Magnet.ForceMultiplier = (int) numForceMultiplier.Value;
            Settings.Values["ForceMultiplier"] = (int) numForceMultiplier.Value;
        }
        private void timerMagInfo_Tick(object sender, EventArgs e) {
            var mag = _magform.Magnet;
            double vel = Math.Round(Math.Sqrt(Math.Pow(mag.Velocity.X, 2) + Math.Pow(mag.Velocity.Y, 2)), 3);
            PointD p = new PointD(Math.Round(mag.Location.X, 2), Math.Round(mag.Location.Y, 2));
            lblVelocity.Text = vel.ToString();
            lblLocation.Text = $"({p.X}, {p.Y})";
            if (chkCycleColors.Checked) {
                if (_magform.LineColor == _magform.TargetColor) {
                    Color c = Color.FromArgb(_rand.Next(2) == 0 ? 0 : 255, _rand.Next(2) == 0 ? 0 : 255, _rand.Next(2) == 0 ? 0 : 255);
                    _magform.TargetColor = c;
                    btnTargetColor.BackColor = c;
                }
            }
        }
        private void trackFriction_Scroll(object sender, EventArgs e) {
            lblFriction.Text = "Friction: " + trackFriction.Value;
            _magform.Friction = trackFriction.Value;
        }
        private void txtBaseRotation_TextChanged(object sender, EventArgs e) {
            double d;
            if (double.TryParse(txtBaseRotation.Text, out d)) {
                Settings.Values["BaseRotation"] = d;
                _magform.BaseRotation = d;
            }
        }
        private void txtInitLoc_TextChanged(object sender, EventArgs e) {
            if (!(sender is TextBox)) return;
            var txtbox = (TextBox) sender;
            if (!txtbox.Text.StartsWith("(") || !txtbox.Text.EndsWith(")")) return;
            string[] vals = txtbox.Text.Substring(1, txtbox.Text.Length - 2).Split(',');
            if (vals.Length != 2) return;
            double x, y;
            if (!double.TryParse(vals[0], out x) || !double.TryParse(vals[1], out y)) return;
            if (ReferenceEquals(txtbox, txtInitLoc)) {
                _magform.InitializeMagnet(new PointD(x, y), _magform.Magnet.Velocity);
                Settings.Values["InitialLocation"] = txtInitLoc.Text;
            }
            else {
                _magform.InitializeMagnet(_magform.Magnet.Location, new PointD(x, y));
                Settings.Values["InitialVelocity"] = txtInitVel.Text;
            }
        }
        private void PopulateMagnetTable() {
            dgvMagnets.Rows.Clear();
            foreach (var mag in _magform.StationaryMagnets) {
                int r = dgvMagnets.Rows.Add(1);
                dgvMagnets.Rows[r].Cells[colDiameter.Index].Value = mag.Diameter.ToString();
                dgvMagnets.Rows[r].Cells[colForceMag.Index].Value = mag.Force.ToString();
                dgvMagnets.Rows[r].Cells[colLocation.Index].Value = $"({mag.Location.X}, {mag.Location.Y})";
                dgvMagnets.Rows[r].Tag = mag;
            }
            txtInitLoc.Text = _magform.InitLocation.ToString();
            txtInitVel.Text = _magform.InitVelocity.ToString();
            numForceMultiplier.Value = (int) Magnet.ForceMultiplier;
        }
        private readonly MagnetsForm _magform;
        private readonly Random _rand = new Random();
    }
}