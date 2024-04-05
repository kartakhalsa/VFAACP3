namespace VFAACP
{
	partial class MeasurePublishRepeatDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasurePublishRepeatDialog));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.nudIterations = new System.Windows.Forms.NumericUpDown();
			this.chkPublishSummaryCsvFile = new System.Windows.Forms.CheckBox();
			this.chkPublishFullData = new System.Windows.Forms.CheckBox();
			this.lblIterations = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nudIterations)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOk.Location = new System.Drawing.Point(111, 165);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(90, 36);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(212, 165);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 36);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// nudIterations
			// 
			this.nudIterations.BackColor = System.Drawing.SystemColors.Control;
			this.nudIterations.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nudIterations.Location = new System.Drawing.Point(212, 24);
			this.nudIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudIterations.Name = "nudIterations";
			this.nudIterations.Size = new System.Drawing.Size(79, 29);
			this.nudIterations.TabIndex = 1;
			this.nudIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudIterations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// chkPublishSummaryCsvFile
			// 
			this.chkPublishSummaryCsvFile.AutoSize = true;
			this.chkPublishSummaryCsvFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPublishSummaryCsvFile.Location = new System.Drawing.Point(102, 117);
			this.chkPublishSummaryCsvFile.Name = "chkPublishSummaryCsvFile";
			this.chkPublishSummaryCsvFile.Size = new System.Drawing.Size(213, 25);
			this.chkPublishSummaryCsvFile.TabIndex = 3;
			this.chkPublishSummaryCsvFile.Text = "Publish Summary CSV File";
			this.chkPublishSummaryCsvFile.UseVisualStyleBackColor = true;
			// 
			// chkPublishFullData
			// 
			this.chkPublishFullData.AutoSize = true;
			this.chkPublishFullData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPublishFullData.Location = new System.Drawing.Point(102, 77);
			this.chkPublishFullData.Name = "chkPublishFullData";
			this.chkPublishFullData.Size = new System.Drawing.Size(145, 25);
			this.chkPublishFullData.TabIndex = 2;
			this.chkPublishFullData.Text = "Publish Full Data";
			this.chkPublishFullData.UseVisualStyleBackColor = true;
			this.chkPublishFullData.CheckedChanged += new System.EventHandler(this.chkPublishFullData_CheckedChanged);
			// 
			// lblIterations
			// 
			this.lblIterations.AutoSize = true;
			this.lblIterations.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIterations.Location = new System.Drawing.Point(126, 26);
			this.lblIterations.Name = "lblIterations";
			this.lblIterations.Size = new System.Drawing.Size(75, 21);
			this.lblIterations.TabIndex = 28;
			this.lblIterations.Text = "Iterations";
			// 
			// MeasurePublishRepeatDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(416, 216);
			this.Controls.Add(this.lblIterations);
			this.Controls.Add(this.chkPublishSummaryCsvFile);
			this.Controls.Add(this.chkPublishFullData);
			this.Controls.Add(this.nudIterations);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MeasurePublishRepeatDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Measure/Publish/Repeat";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Dialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudIterations)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudIterations;
		private System.Windows.Forms.CheckBox chkPublishSummaryCsvFile;
		private System.Windows.Forms.CheckBox chkPublishFullData;
		private System.Windows.Forms.Label lblIterations;
    }
}