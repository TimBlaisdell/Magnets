using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Magnets {
    partial class MouseTracker {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.lblMousePosition = new System.Windows.Forms.Label();
            this.panelMagnet = new System.Windows.Forms.Panel();
            this.txtStrength = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblLocationLabel = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.panelMagnet.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMousePosition
            // 
            this.lblMousePosition.AutoSize = true;
            this.lblMousePosition.Location = new System.Drawing.Point(0, 0);
            this.lblMousePosition.Name = "lblMousePosition";
            this.lblMousePosition.Size = new System.Drawing.Size(33, 13);
            this.lblMousePosition.TabIndex = 0;
            this.lblMousePosition.Text = "(X, Y)";
            this.lblMousePosition.SizeChanged += new System.EventHandler(this.lblMousePosition_SizeChanged);
            // 
            // panelMagnet
            // 
            this.panelMagnet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMagnet.Controls.Add(this.txtStrength);
            this.panelMagnet.Controls.Add(this.txtType);
            this.panelMagnet.Controls.Add(this.txtLocation);
            this.panelMagnet.Controls.Add(this.label1);
            this.panelMagnet.Controls.Add(this.lblType);
            this.panelMagnet.Controls.Add(this.lblLocationLabel);
            this.panelMagnet.Location = new System.Drawing.Point(3, 20);
            this.panelMagnet.Name = "panelMagnet";
            this.panelMagnet.Size = new System.Drawing.Size(126, 50);
            this.panelMagnet.TabIndex = 2;
            this.panelMagnet.Visible = false;
            // 
            // txtStrength
            // 
            this.txtStrength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStrength.BackColor = System.Drawing.SystemColors.Control;
            this.txtStrength.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStrength.Location = new System.Drawing.Point(53, 33);
            this.txtStrength.Name = "txtStrength";
            this.txtStrength.Size = new System.Drawing.Size(70, 13);
            this.txtStrength.TabIndex = 3;
            this.txtStrength.Text = "1.5";
            this.txtStrength.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
            // 
            // txtType
            // 
            this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtType.BackColor = System.Drawing.SystemColors.Control;
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtType.Location = new System.Drawing.Point(53, 19);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(70, 13);
            this.txtType.TabIndex = 3;
            this.txtType.Text = "Attractive";
            this.txtType.Click += new System.EventHandler(this.txtType_Click);
            this.txtType.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocation.Location = new System.Drawing.Point(53, 5);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(70, 13);
            this.txtLocation.TabIndex = 3;
            this.txtLocation.Text = "(X, Y)";
            this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Strength:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(-3, 19);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // lblLocationLabel
            // 
            this.lblLocationLabel.AutoSize = true;
            this.lblLocationLabel.Location = new System.Drawing.Point(-3, 5);
            this.lblLocationLabel.Name = "lblLocationLabel";
            this.lblLocationLabel.Size = new System.Drawing.Size(51, 13);
            this.lblLocationLabel.TabIndex = 2;
            this.lblLocationLabel.Text = "Location:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(53, 3);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(33, 13);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "(X, Y)";
            this.lblLocation.Visible = false;
            // 
            // btnDone
            // 
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.Location = new System.Drawing.Point(83, 73);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(43, 23);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Visible = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // MouseTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(126, 96);
            this.ControlBox = false;
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.panelMagnet);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblMousePosition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MouseTracker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Shown += new System.EventHandler(this.MouseTracker_Shown);
            this.LocationChanged += new System.EventHandler(this.MouseTracker_LocationChanged);
            this.panelMagnet.ResumeLayout(false);
            this.panelMagnet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblMousePosition;
        private Panel panelMagnet;
        private TextBox txtLocation;
        private Label lblLocationLabel;
        private Label lblLocation;
        private TextBox txtStrength;
        private TextBox txtType;
        private Label label1;
        private Label lblType;
        private Button btnDone;
    }
}