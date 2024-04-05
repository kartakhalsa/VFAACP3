namespace VFAACP
{
    partial class SiteBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.lblCurSite = new System.Windows.Forms.Label();
			this.chkIncluded = new System.Windows.Forms.CheckBox();
			this.btnMove = new System.Windows.Forms.Button();
			this.btnMeasure = new System.Windows.Forms.Button();
			this.lblNumSites = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblPrintIcon = new System.Windows.Forms.Label();
			this.chkAutoStore = new System.Windows.Forms.CheckBox();
			this.btnStore = new System.Windows.Forms.Button();
			this.txtResults = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pbSiteBitmap = new System.Windows.Forms.PictureBox();
			this.pbSiteBitmap2 = new System.Windows.Forms.PictureBox();
			this.btnPrint = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSiteBitmap)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSiteBitmap2)).BeginInit();
			this.SuspendLayout();
			// 
			// lblCurSite
			// 
			this.lblCurSite.AutoEllipsis = true;
			this.lblCurSite.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCurSite.Location = new System.Drawing.Point(46, 6);
			this.lblCurSite.Name = "lblCurSite";
			this.lblCurSite.Size = new System.Drawing.Size(33, 23);
			this.lblCurSite.TabIndex = 0;
			this.lblCurSite.Text = "999";
			this.lblCurSite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkIncluded
			// 
			this.chkIncluded.AutoSize = true;
			this.chkIncluded.Location = new System.Drawing.Point(156, 9);
			this.chkIncluded.Name = "chkIncluded";
			this.chkIncluded.Size = new System.Drawing.Size(15, 14);
			this.chkIncluded.TabIndex = 1;
			this.chkIncluded.UseVisualStyleBackColor = true;
			this.chkIncluded.CheckedChanged += new System.EventHandler(this.chkIncluded_CheckedChanged);
			this.chkIncluded.MouseLeave += new System.EventHandler(this.chkIncluded_MouseLeave);
			this.chkIncluded.MouseHover += new System.EventHandler(this.chkIncluded_MouseHover);
			// 
			// btnMove
			// 
			this.btnMove.Location = new System.Drawing.Point(178, 3);
			this.btnMove.Name = "btnMove";
			this.btnMove.Size = new System.Drawing.Size(44, 25);
			this.btnMove.TabIndex = 2;
			this.btnMove.Text = "Move";
			this.btnMove.UseVisualStyleBackColor = true;
			this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
			// 
			// btnMeasure
			// 
			this.btnMeasure.Location = new System.Drawing.Point(303, 3);
			this.btnMeasure.Name = "btnMeasure";
			this.btnMeasure.Size = new System.Drawing.Size(57, 25);
			this.btnMeasure.TabIndex = 3;
			this.btnMeasure.Text = "Measure";
			this.btnMeasure.UseVisualStyleBackColor = true;
			this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
			// 
			// lblNumSites
			// 
			this.lblNumSites.AutoEllipsis = true;
			this.lblNumSites.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNumSites.Location = new System.Drawing.Point(101, 6);
			this.lblNumSites.Name = "lblNumSites";
			this.lblNumSites.Size = new System.Drawing.Size(33, 23);
			this.lblNumSites.TabIndex = 6;
			this.lblNumSites.Text = "999";
			this.lblNumSites.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoEllipsis = true;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(6, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Site";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoEllipsis = true;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(78, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "of";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPrintIcon
			// 
			this.lblPrintIcon.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lblPrintIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.lblPrintIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPrintIcon.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPrintIcon.ForeColor = System.Drawing.Color.White;
			this.lblPrintIcon.Location = new System.Drawing.Point(380, 193);
			this.lblPrintIcon.Name = "lblPrintIcon";
			this.lblPrintIcon.Size = new System.Drawing.Size(18, 18);
			this.lblPrintIcon.TabIndex = 10;
			this.lblPrintIcon.Text = "P";
			this.lblPrintIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkAutoStore
			// 
			this.chkAutoStore.AutoSize = true;
			this.chkAutoStore.Checked = true;
			this.chkAutoStore.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoStore.Location = new System.Drawing.Point(234, 9);
			this.chkAutoStore.Name = "chkAutoStore";
			this.chkAutoStore.Size = new System.Drawing.Size(15, 14);
			this.chkAutoStore.TabIndex = 12;
			this.chkAutoStore.UseVisualStyleBackColor = true;
			this.chkAutoStore.MouseLeave += new System.EventHandler(this.chkAutoStore_MouseLeave);
			this.chkAutoStore.MouseHover += new System.EventHandler(this.chkAutoStore_MouseHover);
			// 
			// btnStore
			// 
			this.btnStore.BackColor = System.Drawing.Color.White;
			this.btnStore.Location = new System.Drawing.Point(249, 3);
			this.btnStore.Name = "btnStore";
			this.btnStore.Size = new System.Drawing.Size(44, 25);
			this.btnStore.TabIndex = 11;
			this.btnStore.Text = "Store";
			this.btnStore.UseVisualStyleBackColor = true;
			this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
			// 
			// txtResults
			// 
			this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.txtResults.BackColor = System.Drawing.Color.White;
			this.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtResults.Cursor = System.Windows.Forms.Cursors.Default;
			this.txtResults.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtResults.Location = new System.Drawing.Point(3, 32);
			this.txtResults.Multiline = true;
			this.txtResults.Name = "txtResults";
			this.txtResults.ReadOnly = true;
			this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtResults.Size = new System.Drawing.Size(407, 185);
			this.txtResults.TabIndex = 13;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(416, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pbSiteBitmap);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pbSiteBitmap2);
			this.splitContainer1.Size = new System.Drawing.Size(610, 214);
			this.splitContainer1.SplitterDistance = 391;
			this.splitContainer1.TabIndex = 16;
			// 
			// pbSiteBitmap
			// 
			this.pbSiteBitmap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbSiteBitmap.Location = new System.Drawing.Point(0, 0);
			this.pbSiteBitmap.Name = "pbSiteBitmap";
			this.pbSiteBitmap.Size = new System.Drawing.Size(391, 214);
			this.pbSiteBitmap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbSiteBitmap.TabIndex = 4;
			this.pbSiteBitmap.TabStop = false;
			this.pbSiteBitmap.Click += new System.EventHandler(this.pbSiteBitmap_Click);
			this.pbSiteBitmap.DoubleClick += new System.EventHandler(this.pbSiteBitmap_DoubleClick);
			// 
			// pbSiteBitmap2
			// 
			this.pbSiteBitmap2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbSiteBitmap2.Location = new System.Drawing.Point(0, 0);
			this.pbSiteBitmap2.Name = "pbSiteBitmap2";
			this.pbSiteBitmap2.Size = new System.Drawing.Size(215, 214);
			this.pbSiteBitmap2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbSiteBitmap2.TabIndex = 15;
			this.pbSiteBitmap2.TabStop = false;
			// 
			// btnPrint
			// 
			this.btnPrint.BackColor = System.Drawing.Color.White;
			this.btnPrint.Image = global::VFAACP.Properties.Resources.Printer32;
			this.btnPrint.Location = new System.Drawing.Point(372, 3);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(38, 25);
			this.btnPrint.TabIndex = 14;
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// SiteBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.lblPrintIcon);
			this.Controls.Add(this.txtResults);
			this.Controls.Add(this.chkAutoStore);
			this.Controls.Add(this.btnStore);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblNumSites);
			this.Controls.Add(this.btnMeasure);
			this.Controls.Add(this.btnMove);
			this.Controls.Add(this.chkIncluded);
			this.Controls.Add(this.lblCurSite);
			this.DoubleBuffered = true;
			this.Name = "SiteBox";
			this.Size = new System.Drawing.Size(1029, 220);
			this.SizeChanged += new System.EventHandler(this.SiteBox_SizeChanged);
			this.Click += new System.EventHandler(this.SiteBox_Click);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbSiteBitmap)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSiteBitmap2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurSite;
        private System.Windows.Forms.CheckBox chkIncluded;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.PictureBox pbSiteBitmap;
        private System.Windows.Forms.Label lblNumSites;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPrintIcon;
        private System.Windows.Forms.CheckBox chkAutoStore;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.TextBox txtResults;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.PictureBox pbSiteBitmap2;
		private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
