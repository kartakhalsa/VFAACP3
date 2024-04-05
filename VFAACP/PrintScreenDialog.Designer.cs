namespace VFAACP
{
    partial class PrintScreenDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintScreenDialog));
			this.btnDone = new System.Windows.Forms.Button();
			this.btnSaveFile = new System.Windows.Forms.Button();
			this.btnPageSetup = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnPrintPreview = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnDone.Location = new System.Drawing.Point(146, 138);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(108, 36);
			this.btnDone.TabIndex = 5;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// btnSaveFile
			// 
			this.btnSaveFile.Image = global::VFAACP.Properties.Resources.FileFormatPNG48;
			this.btnSaveFile.Location = new System.Drawing.Point(300, 12);
			this.btnSaveFile.Name = "btnSaveFile";
			this.btnSaveFile.Size = new System.Drawing.Size(90, 101);
			this.btnSaveFile.TabIndex = 4;
			this.btnSaveFile.Text = "Save File";
			this.btnSaveFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSaveFile.UseVisualStyleBackColor = true;
			this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
			// 
			// btnPageSetup
			// 
			this.btnPageSetup.Image = global::VFAACP.Properties.Resources.Settings48;
			this.btnPageSetup.Location = new System.Drawing.Point(12, 12);
			this.btnPageSetup.Name = "btnPageSetup";
			this.btnPageSetup.Size = new System.Drawing.Size(90, 101);
			this.btnPageSetup.TabIndex = 1;
			this.btnPageSetup.Text = "Page Setup";
			this.btnPageSetup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPageSetup.UseVisualStyleBackColor = true;
			this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Image = global::VFAACP.Properties.Resources.Printer48;
			this.btnPrint.Location = new System.Drawing.Point(204, 12);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(90, 101);
			this.btnPrint.TabIndex = 3;
			this.btnPrint.Text = "Print";
			this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnPrintPreview
			// 
			this.btnPrintPreview.Image = global::VFAACP.Properties.Resources.Preview48;
			this.btnPrintPreview.Location = new System.Drawing.Point(108, 12);
			this.btnPrintPreview.Name = "btnPrintPreview";
			this.btnPrintPreview.Size = new System.Drawing.Size(90, 101);
			this.btnPrintPreview.TabIndex = 2;
			this.btnPrintPreview.Text = "Print Preview";
			this.btnPrintPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnPrintPreview.UseVisualStyleBackColor = true;
			this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
			// 
			// PrintScreenDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnDone;
			this.ClientSize = new System.Drawing.Size(403, 186);
			this.Controls.Add(this.btnSaveFile);
			this.Controls.Add(this.btnPageSetup);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnPrintPreview);
			this.Controls.Add(this.btnDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintScreenDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Print Screen Options";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintDialog_FormClosing);
			this.Load += new System.EventHandler(this.PrintDialog_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnPageSetup;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSaveFile;
    }
}