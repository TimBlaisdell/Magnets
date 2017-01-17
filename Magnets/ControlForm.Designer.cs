namespace Magnets {
    partial class ControlForm {
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
            this.btnStart = new System.Windows.Forms.Button();
            this.numForceMultiplier = new System.Windows.Forms.NumericUpDown();
            this.lblForceMultiplier = new System.Windows.Forms.Label();
            this.txtInitLoc = new System.Windows.Forms.TextBox();
            this.lblInitLoc = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCleanup = new System.Windows.Forms.Button();
            this.lblInitVel = new System.Windows.Forms.Label();
            this.txtInitVel = new System.Windows.Forms.TextBox();
            this.dgvMagnets = new System.Windows.Forms.DataGridView();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colForceMag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVelocityLabel = new System.Windows.Forms.Label();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.lblLocationLabel = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.timerMagInfo = new System.Windows.Forms.Timer(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnTargetColor = new System.Windows.Forms.Button();
            this.lblTargetColor = new System.Windows.Forms.Label();
            this.chkShowStationary = new System.Windows.Forms.CheckBox();
            this.chkShowMoving = new System.Windows.Forms.CheckBox();
            this.lblBaseRotation = new System.Windows.Forms.Label();
            this.txtBaseRotation = new System.Windows.Forms.TextBox();
            this.chkCycleColors = new System.Windows.Forms.CheckBox();
            this.trackFriction = new System.Windows.Forms.TrackBar();
            this.lblFriction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numForceMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMagnets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFriction)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(312, 324);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numForceMultiplier
            // 
            this.numForceMultiplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numForceMultiplier.Location = new System.Drawing.Point(12, 327);
            this.numForceMultiplier.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numForceMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numForceMultiplier.Name = "numForceMultiplier";
            this.numForceMultiplier.Size = new System.Drawing.Size(75, 20);
            this.numForceMultiplier.TabIndex = 1;
            this.numForceMultiplier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numForceMultiplier.ValueChanged += new System.EventHandler(this.numForceMultiplier_ValueChanged);
            // 
            // lblForceMultiplier
            // 
            this.lblForceMultiplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblForceMultiplier.AutoSize = true;
            this.lblForceMultiplier.Location = new System.Drawing.Point(12, 311);
            this.lblForceMultiplier.Name = "lblForceMultiplier";
            this.lblForceMultiplier.Size = new System.Drawing.Size(77, 13);
            this.lblForceMultiplier.TabIndex = 2;
            this.lblForceMultiplier.Text = "Force multiplier";
            // 
            // txtInitLoc
            // 
            this.txtInitLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInitLoc.Location = new System.Drawing.Point(12, 249);
            this.txtInitLoc.Name = "txtInitLoc";
            this.txtInitLoc.Size = new System.Drawing.Size(75, 20);
            this.txtInitLoc.TabIndex = 3;
            this.txtInitLoc.TextChanged += new System.EventHandler(this.txtInitLoc_TextChanged);
            // 
            // lblInitLoc
            // 
            this.lblInitLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInitLoc.AutoSize = true;
            this.lblInitLoc.Location = new System.Drawing.Point(12, 233);
            this.lblInitLoc.Name = "lblInitLoc";
            this.lblInitLoc.Size = new System.Drawing.Size(71, 13);
            this.lblInitLoc.TabIndex = 2;
            this.lblInitLoc.Text = "Initial location";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(312, 295);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCleanup
            // 
            this.btnCleanup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanup.Location = new System.Drawing.Point(312, 266);
            this.btnCleanup.Name = "btnCleanup";
            this.btnCleanup.Size = new System.Drawing.Size(75, 23);
            this.btnCleanup.TabIndex = 0;
            this.btnCleanup.Text = "Cleanup";
            this.btnCleanup.UseVisualStyleBackColor = true;
            this.btnCleanup.Click += new System.EventHandler(this.btnCleanup_Click);
            // 
            // lblInitVel
            // 
            this.lblInitVel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInitVel.AutoSize = true;
            this.lblInitVel.Location = new System.Drawing.Point(12, 272);
            this.lblInitVel.Name = "lblInitVel";
            this.lblInitVel.Size = new System.Drawing.Size(70, 13);
            this.lblInitVel.TabIndex = 2;
            this.lblInitVel.Text = "Initial velocity";
            // 
            // txtInitVel
            // 
            this.txtInitVel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInitVel.Location = new System.Drawing.Point(12, 288);
            this.txtInitVel.Name = "txtInitVel";
            this.txtInitVel.Size = new System.Drawing.Size(75, 20);
            this.txtInitVel.TabIndex = 3;
            this.txtInitVel.Text = "(-0.1, 0)";
            this.txtInitVel.TextChanged += new System.EventHandler(this.txtInitLoc_TextChanged);
            // 
            // dgvMagnets
            // 
            this.dgvMagnets.AllowUserToAddRows = false;
            this.dgvMagnets.AllowUserToDeleteRows = false;
            this.dgvMagnets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMagnets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMagnets.BackgroundColor = System.Drawing.Color.White;
            this.dgvMagnets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMagnets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMagnets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLocation,
            this.colDiameter,
            this.colForceMag});
            this.dgvMagnets.Location = new System.Drawing.Point(12, 12);
            this.dgvMagnets.MultiSelect = false;
            this.dgvMagnets.Name = "dgvMagnets";
            this.dgvMagnets.RowHeadersVisible = false;
            this.dgvMagnets.Size = new System.Drawing.Size(231, 218);
            this.dgvMagnets.TabIndex = 4;
            this.dgvMagnets.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMagnets_CellValueChanged);
            // 
            // colLocation
            // 
            this.colLocation.HeaderText = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.Width = 73;
            // 
            // colDiameter
            // 
            this.colDiameter.HeaderText = "Diameter";
            this.colDiameter.Name = "colDiameter";
            this.colDiameter.Width = 74;
            // 
            // colForceMag
            // 
            this.colForceMag.HeaderText = "Magnitude";
            this.colForceMag.Name = "colForceMag";
            this.colForceMag.Width = 82;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(260, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(63, 13);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "Magnet info";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(252, -12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 242);
            this.label1.TabIndex = 6;
            // 
            // lblVelocityLabel
            // 
            this.lblVelocityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVelocityLabel.AutoSize = true;
            this.lblVelocityLabel.Location = new System.Drawing.Point(260, 29);
            this.lblVelocityLabel.Name = "lblVelocityLabel";
            this.lblVelocityLabel.Size = new System.Drawing.Size(47, 13);
            this.lblVelocityLabel.TabIndex = 6;
            this.lblVelocityLabel.Text = "Velocity:";
            // 
            // lblVelocity
            // 
            this.lblVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.Location = new System.Drawing.Point(330, 29);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(22, 13);
            this.lblVelocity.TabIndex = 6;
            this.lblVelocity.Text = "0.0";
            // 
            // lblLocationLabel
            // 
            this.lblLocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocationLabel.AutoSize = true;
            this.lblLocationLabel.Location = new System.Drawing.Point(260, 51);
            this.lblLocationLabel.Name = "lblLocationLabel";
            this.lblLocationLabel.Size = new System.Drawing.Size(48, 13);
            this.lblLocationLabel.TabIndex = 6;
            this.lblLocationLabel.Text = "Location";
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(330, 51);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(31, 13);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "(0, 0)";
            // 
            // timerMagInfo
            // 
            this.timerMagInfo.Enabled = true;
            this.timerMagInfo.Tick += new System.EventHandler(this.timerMagInfo_Tick);
            // 
            // btnTargetColor
            // 
            this.btnTargetColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTargetColor.Location = new System.Drawing.Point(333, 68);
            this.btnTargetColor.Name = "btnTargetColor";
            this.btnTargetColor.Size = new System.Drawing.Size(23, 23);
            this.btnTargetColor.TabIndex = 7;
            this.btnTargetColor.UseVisualStyleBackColor = true;
            this.btnTargetColor.Click += new System.EventHandler(this.btnTargetColor_Click);
            // 
            // lblTargetColor
            // 
            this.lblTargetColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetColor.AutoSize = true;
            this.lblTargetColor.Location = new System.Drawing.Point(260, 73);
            this.lblTargetColor.Name = "lblTargetColor";
            this.lblTargetColor.Size = new System.Drawing.Size(67, 13);
            this.lblTargetColor.TabIndex = 8;
            this.lblTargetColor.Text = "Target color:";
            // 
            // chkShowStationary
            // 
            this.chkShowStationary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowStationary.AutoSize = true;
            this.chkShowStationary.Checked = true;
            this.chkShowStationary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStationary.Location = new System.Drawing.Point(99, 328);
            this.chkShowStationary.Name = "chkShowStationary";
            this.chkShowStationary.Size = new System.Drawing.Size(144, 17);
            this.chkShowStationary.TabIndex = 9;
            this.chkShowStationary.Text = "Show stationary magnets";
            this.chkShowStationary.UseVisualStyleBackColor = true;
            this.chkShowStationary.CheckedChanged += new System.EventHandler(this.chkShowStationary_CheckedChanged);
            // 
            // chkShowMoving
            // 
            this.chkShowMoving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowMoving.AutoSize = true;
            this.chkShowMoving.Checked = true;
            this.chkShowMoving.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowMoving.Location = new System.Drawing.Point(99, 305);
            this.chkShowMoving.Name = "chkShowMoving";
            this.chkShowMoving.Size = new System.Drawing.Size(128, 17);
            this.chkShowMoving.TabIndex = 9;
            this.chkShowMoving.Text = "Show moving magnet";
            this.chkShowMoving.UseVisualStyleBackColor = true;
            this.chkShowMoving.CheckedChanged += new System.EventHandler(this.chkShowMoving_CheckedChanged);
            // 
            // lblBaseRotation
            // 
            this.lblBaseRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBaseRotation.AutoSize = true;
            this.lblBaseRotation.Location = new System.Drawing.Point(99, 263);
            this.lblBaseRotation.Name = "lblBaseRotation";
            this.lblBaseRotation.Size = new System.Drawing.Size(69, 13);
            this.lblBaseRotation.TabIndex = 2;
            this.lblBaseRotation.Text = "Base rotation";
            // 
            // txtBaseRotation
            // 
            this.txtBaseRotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBaseRotation.Location = new System.Drawing.Point(99, 279);
            this.txtBaseRotation.Name = "txtBaseRotation";
            this.txtBaseRotation.Size = new System.Drawing.Size(75, 20);
            this.txtBaseRotation.TabIndex = 3;
            this.txtBaseRotation.Text = "0";
            this.txtBaseRotation.TextChanged += new System.EventHandler(this.txtBaseRotation_TextChanged);
            // 
            // chkCycleColors
            // 
            this.chkCycleColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCycleColors.AutoSize = true;
            this.chkCycleColors.Location = new System.Drawing.Point(278, 97);
            this.chkCycleColors.Name = "chkCycleColors";
            this.chkCycleColors.Size = new System.Drawing.Size(83, 17);
            this.chkCycleColors.TabIndex = 9;
            this.chkCycleColors.Text = "Cycle colors";
            this.chkCycleColors.UseVisualStyleBackColor = true;
            // 
            // trackFriction
            // 
            this.trackFriction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackFriction.Location = new System.Drawing.Point(260, 133);
            this.trackFriction.Maximum = 100;
            this.trackFriction.Name = "trackFriction";
            this.trackFriction.Size = new System.Drawing.Size(104, 45);
            this.trackFriction.TabIndex = 10;
            this.trackFriction.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackFriction.Visible = false;
            this.trackFriction.Scroll += new System.EventHandler(this.trackFriction_Scroll);
            // 
            // lblFriction
            // 
            this.lblFriction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFriction.AutoSize = true;
            this.lblFriction.Location = new System.Drawing.Point(260, 117);
            this.lblFriction.Name = "lblFriction";
            this.lblFriction.Size = new System.Drawing.Size(53, 13);
            this.lblFriction.TabIndex = 6;
            this.lblFriction.Text = "Friction: 0";
            this.lblFriction.Visible = false;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 359);
            this.Controls.Add(this.chkCycleColors);
            this.Controls.Add(this.chkShowMoving);
            this.Controls.Add(this.chkShowStationary);
            this.Controls.Add(this.lblTargetColor);
            this.Controls.Add(this.btnTargetColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblVelocity);
            this.Controls.Add(this.lblLocationLabel);
            this.Controls.Add(this.lblVelocityLabel);
            this.Controls.Add(this.lblFriction);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgvMagnets);
            this.Controls.Add(this.txtInitVel);
            this.Controls.Add(this.lblInitVel);
            this.Controls.Add(this.txtBaseRotation);
            this.Controls.Add(this.lblBaseRotation);
            this.Controls.Add(this.txtInitLoc);
            this.Controls.Add(this.lblInitLoc);
            this.Controls.Add(this.lblForceMultiplier);
            this.Controls.Add(this.numForceMultiplier);
            this.Controls.Add(this.btnCleanup);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.trackFriction);
            this.Name = "ControlForm";
            this.Text = "ControlForm";
            this.Shown += new System.EventHandler(this.ControlForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numForceMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMagnets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFriction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown numForceMultiplier;
        private System.Windows.Forms.Label lblForceMultiplier;
        private System.Windows.Forms.TextBox txtInitLoc;
        private System.Windows.Forms.Label lblInitLoc;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCleanup;
        private System.Windows.Forms.Label lblInitVel;
        private System.Windows.Forms.TextBox txtInitVel;
        private System.Windows.Forms.DataGridView dgvMagnets;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colForceMag;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVelocityLabel;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.Label lblLocationLabel;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Timer timerMagInfo;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button btnTargetColor;
        private System.Windows.Forms.Label lblTargetColor;
        private System.Windows.Forms.CheckBox chkShowStationary;
        private System.Windows.Forms.CheckBox chkShowMoving;
        private System.Windows.Forms.Label lblBaseRotation;
        private System.Windows.Forms.TextBox txtBaseRotation;
        private System.Windows.Forms.CheckBox chkCycleColors;
        private System.Windows.Forms.TrackBar trackFriction;
        private System.Windows.Forms.Label lblFriction;
    }
}