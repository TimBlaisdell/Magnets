namespace Magnets {
    partial class MagnetsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.bwWorker = new System.ComponentModel.BackgroundWorker();
            this.timerAnimate = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRemoveSelectedMagnet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddMagnetHere = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditMagnet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveMagnets = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLoadMagnets = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timerCleanup = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bwWorker
            // 
            this.bwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwWorker_DoWork);
            // 
            // timerAnimate
            // 
            this.timerAnimate.Interval = 30;
            this.timerAnimate.Tick += new System.EventHandler(this.timerAnimate_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRemoveSelectedMagnet,
            this.menuAddMagnetHere,
            this.menuEditMagnet,
            this.menuSaveMagnets,
            this.menuLoadMagnets});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // menuRemoveSelectedMagnet
            // 
            this.menuRemoveSelectedMagnet.Name = "menuRemoveSelectedMagnet";
            this.menuRemoveSelectedMagnet.Size = new System.Drawing.Size(194, 22);
            this.menuRemoveSelectedMagnet.Text = "Remove this magnet";
            this.menuRemoveSelectedMagnet.Click += new System.EventHandler(this.menuRemoveSelectedMagnet_Click);
            // 
            // menuAddMagnetHere
            // 
            this.menuAddMagnetHere.Name = "menuAddMagnetHere";
            this.menuAddMagnetHere.Size = new System.Drawing.Size(194, 22);
            this.menuAddMagnetHere.Text = "Add magnet here";
            this.menuAddMagnetHere.Click += new System.EventHandler(this.menuAddMagnetHere_Click);
            // 
            // menuEditMagnet
            // 
            this.menuEditMagnet.Name = "menuEditMagnet";
            this.menuEditMagnet.Size = new System.Drawing.Size(194, 22);
            this.menuEditMagnet.Text = "Edit magnet properties";
            this.menuEditMagnet.Click += new System.EventHandler(this.menuEditMagnet_Click);
            // 
            // menuSaveMagnets
            // 
            this.menuSaveMagnets.Name = "menuSaveMagnets";
            this.menuSaveMagnets.Size = new System.Drawing.Size(194, 22);
            this.menuSaveMagnets.Text = "Save magnets...";
            this.menuSaveMagnets.Click += new System.EventHandler(this.menuSaveMagnets_Click);
            // 
            // menuLoadMagnets
            // 
            this.menuLoadMagnets.Name = "menuLoadMagnets";
            this.menuLoadMagnets.Size = new System.Drawing.Size(194, 22);
            this.menuLoadMagnets.Text = "Load magnets...";
            this.menuLoadMagnets.Click += new System.EventHandler(this.menuLoadMagnets_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "json";
            this.openFileDialog.Filter = "Magnet Files|*.json";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "json";
            this.saveFileDialog.Filter = "Magnet Files|*.json";
            // 
            // timerCleanup
            // 
            this.timerCleanup.Interval = 1000;
            this.timerCleanup.Tick += new System.EventHandler(this.timerCleanup_Tick);
            // 
            // MagnetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MagnetsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.MagnetsForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MagnetsForm_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MagnetsForm_MouseDown);
            this.MouseEnter += new System.EventHandler(this.MagnetsForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MagnetsForm_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MagnetsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MagnetsForm_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bwWorker;
        private System.Windows.Forms.Timer timerAnimate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveSelectedMagnet;
        private System.Windows.Forms.ToolStripMenuItem menuAddMagnetHere;
        private System.Windows.Forms.ToolStripMenuItem menuSaveMagnets;
        private System.Windows.Forms.ToolStripMenuItem menuLoadMagnets;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem menuEditMagnet;
        private System.Windows.Forms.Timer timerCleanup;
    }
}

