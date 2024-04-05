namespace VFAACP
{
    partial class ToolingCalibrationDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolingCalibrationDialog));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblLotNumber = new System.Windows.Forms.Label();
			this.txtLotNumber = new System.Windows.Forms.TextBox();
			this.lblProtocolNumber = new System.Windows.Forms.Label();
			this.txtProtocolNumber = new System.Windows.Forms.TextBox();
			this.lblOperatorName = new System.Windows.Forms.Label();
			this.txtOperatorName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOk.Location = new System.Drawing.Point(164, 194);
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
			this.btnCancel.Location = new System.Drawing.Point(291, 194);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 36);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblLotNumber
			// 
			this.lblLotNumber.AutoSize = true;
			this.lblLotNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLotNumber.Location = new System.Drawing.Point(92, 82);
			this.lblLotNumber.Name = "lblLotNumber";
			this.lblLotNumber.Size = new System.Drawing.Size(94, 21);
			this.lblLotNumber.TabIndex = 28;
			this.lblLotNumber.Text = "Lot Number";
			// 
			// txtLotNumber
			// 
			this.txtLotNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLotNumber.Location = new System.Drawing.Point(238, 79);
			this.txtLotNumber.Name = "txtLotNumber";
			this.txtLotNumber.Size = new System.Drawing.Size(209, 29);
			this.txtLotNumber.TabIndex = 2;
			// 
			// lblProtocolNumber
			// 
			this.lblProtocolNumber.AutoSize = true;
			this.lblProtocolNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProtocolNumber.Location = new System.Drawing.Point(92, 31);
			this.lblProtocolNumber.Name = "lblProtocolNumber";
			this.lblProtocolNumber.Size = new System.Drawing.Size(130, 21);
			this.lblProtocolNumber.TabIndex = 30;
			this.lblProtocolNumber.Text = "Protocol Number";
			// 
			// txtProtocolNumber
			// 
			this.txtProtocolNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtProtocolNumber.Location = new System.Drawing.Point(238, 28);
			this.txtProtocolNumber.Name = "txtProtocolNumber";
			this.txtProtocolNumber.Size = new System.Drawing.Size(209, 29);
			this.txtProtocolNumber.TabIndex = 1;
			// 
			// lblOperatorName
			// 
			this.lblOperatorName.AutoSize = true;
			this.lblOperatorName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblOperatorName.Location = new System.Drawing.Point(92, 133);
			this.lblOperatorName.Name = "lblOperatorName";
			this.lblOperatorName.Size = new System.Drawing.Size(119, 21);
			this.lblOperatorName.TabIndex = 31;
			this.lblOperatorName.Text = "Operator Name";
			// 
			// txtOperatorName
			// 
			this.txtOperatorName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtOperatorName.Location = new System.Drawing.Point(238, 130);
			this.txtOperatorName.Name = "txtOperatorName";
			this.txtOperatorName.Size = new System.Drawing.Size(209, 29);
			this.txtOperatorName.TabIndex = 3;
			// 
			// ToolingCalibrationDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(545, 255);
			this.Controls.Add(this.txtOperatorName);
			this.Controls.Add(this.lblOperatorName);
			this.Controls.Add(this.txtProtocolNumber);
			this.Controls.Add(this.lblProtocolNumber);
			this.Controls.Add(this.txtLotNumber);
			this.Controls.Add(this.lblLotNumber);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ToolingCalibrationDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tooling Calibration - Protocol Run Setup";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Dialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblLotNumber;
		private System.Windows.Forms.TextBox txtLotNumber;
		private System.Windows.Forms.Label lblProtocolNumber;
		private System.Windows.Forms.TextBox txtProtocolNumber;
        private System.Windows.Forms.Label lblOperatorName;
        private System.Windows.Forms.TextBox txtOperatorName;
    }
}