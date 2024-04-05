namespace VFAACP
{
    partial class PrintTrayReportDialog
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
			this.btnDone = new System.Windows.Forms.Button();
			this.btnSavePDF = new System.Windows.Forms.Button();
			this.btnPageSetup = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnPrintPreview = new System.Windows.Forms.Button();
			this.tbComments = new System.Windows.Forms.TextBox();
			this.lblComments = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnDone.Location = new System.Drawing.Point(147, 261);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(108, 36);
			this.btnDone.TabIndex = 6;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// btnSavePDF
			// 
			this.btnSavePDF.Image = global::VFAACP.Properties.Resources.FileFormatPNG48;
			this.btnSavePDF.Location = new System.Drawing.Point(300, 12);
			this.btnSavePDF.Name = "btnSavePDF";
			this.btnSavePDF.Size = new System.Drawing.Size(90, 101);
			this.btnSavePDF.TabIndex = 4;
			this.btnSavePDF.Text = "Save PDF";
			this.btnSavePDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSavePDF.UseVisualStyleBackColor = true;
			this.btnSavePDF.Click += new System.EventHandler(this.btnSavePDF_Click);
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
			// tbComments
			// 
			this.tbComments.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbComments.Location = new System.Drawing.Point(13, 153);
			this.tbComments.Multiline = true;
			this.tbComments.Name = "tbComments";
			this.tbComments.Size = new System.Drawing.Size(377, 94);
			this.tbComments.TabIndex = 5;
			this.tbComments.TextChanged += new System.EventHandler(this.tbComments_TextChanged);
			// 
			// lblComments
			// 
			this.lblComments.AutoSize = true;
			this.lblComments.Location = new System.Drawing.Point(12, 134);
			this.lblComments.Name = "lblComments";
			this.lblComments.Size = new System.Drawing.Size(56, 13);
			this.lblComments.TabIndex = 7;
			this.lblComments.Text = "Comments";
			// 
			// PrintTrayReportDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnDone;
			this.ClientSize = new System.Drawing.Size(403, 309);
			this.Controls.Add(this.lblComments);
			this.Controls.Add(this.tbComments);
			this.Controls.Add(this.btnSavePDF);
			this.Controls.Add(this.btnPageSetup);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnPrintPreview);
			this.Controls.Add(this.btnDone);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintTrayReportDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Print Tray Report";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.PrintDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnPageSetup;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSavePDF;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.Label lblComments;
    }
}