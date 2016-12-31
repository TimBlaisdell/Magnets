using System;
using System.Drawing;
using System.Windows.Forms;

namespace Magnets {
    public partial class MousePos : Form {
        public MousePos() {
            InitializeComponent();
        }
        public Point Pos {
            get { return _loc; }
            set {
                _loc = value;
                lblMousePos.Text = $"({_loc.X}, {_loc.Y})";
                Location = value;
            }
        }
        private void lblMousePos_TextChanged(object sender, EventArgs e) {
            Size = lblMousePos.Size;
        }
        private Point _loc;
    }
}