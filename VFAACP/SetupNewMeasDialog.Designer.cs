namespace VFAACP
{
    partial class SetupNewMeasDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupNewMeasDialog));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnMeasureVerificationPart = new System.Windows.Forms.Button();
			this.btnBrowseForEngineeringDesignFile = new System.Windows.Forms.Button();
			this.btnUseLotNumberToLoadDesignFile = new System.Windows.Forms.Button();
			this.btnBrowseForRecipeFile = new System.Windows.Forms.Button();
			this.btnToolingCalibrationProtocolRun = new System.Windows.Forms.Button();
			this.btnBrowseForProductionDesignFile = new System.Windows.Forms.Button();
			this.btnMoldServiceSetup = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(662, 117);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(102, 35);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnMeasureVerificationPart
			// 
			this.btnMeasureVerificationPart.Image = global::VFAACP.Properties.Resources.Check32;
			this.btnMeasureVerificationPart.Location = new System.Drawing.Point(1220, 20);
			this.btnMeasureVerificationPart.Name = "btnMeasureVerificationPart";
			this.btnMeasureVerificationPart.Size = new System.Drawing.Size(186, 84);
			this.btnMeasureVerificationPart.TabIndex = 7;
			this.btnMeasureVerificationPart.Text = "Measure Verification Part";
			this.btnMeasureVerificationPart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnMeasureVerificationPart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnMeasureVerificationPart.UseVisualStyleBackColor = true;
			this.btnMeasureVerificationPart.Click += new System.EventHandler(this.btnMeasureVerificationParts_Click);
			// 
			// btnBrowseForEngineeringDesignFile
			// 
			this.btnBrowseForEngineeringDesignFile.Image = global::VFAACP.Properties.Resources.FolderFind32;
			this.btnBrowseForEngineeringDesignFile.Location = new System.Drawing.Point(620, 20);
			this.btnBrowseForEngineeringDesignFile.Name = "btnBrowseForEngineeringDesignFile";
			this.btnBrowseForEngineeringDesignFile.Size = new System.Drawing.Size(186, 84);
			this.btnBrowseForEngineeringDesignFile.TabIndex = 4;
			this.btnBrowseForEngineeringDesignFile.Text = "Browse for Engineering Design File ";
			this.btnBrowseForEngineeringDesignFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnBrowseForEngineeringDesignFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnBrowseForEngineeringDesignFile.UseVisualStyleBackColor = true;
			this.btnBrowseForEngineeringDesignFile.Click += new System.EventHandler(this.btnBrowseForEngineeringDesignFile_Click);
			// 
			// btnUseLotNumberToLoadDesignFile
			// 
			this.btnUseLotNumberToLoadDesignFile.Image = global::VFAACP.Properties.Resources.DataInformation32;
			this.btnUseLotNumberToLoadDesignFile.Location = new System.Drawing.Point(420, 20);
			this.btnUseLotNumberToLoadDesignFile.Name = "btnUseLotNumberToLoadDesignFile";
			this.btnUseLotNumberToLoadDesignFile.Size = new System.Drawing.Size(186, 84);
			this.btnUseLotNumberToLoadDesignFile.TabIndex = 3;
			this.btnUseLotNumberToLoadDesignFile.Text = "Use Lot Number to Load Design File";
			this.btnUseLotNumberToLoadDesignFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUseLotNumberToLoadDesignFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnUseLotNumberToLoadDesignFile.UseVisualStyleBackColor = true;
			this.btnUseLotNumberToLoadDesignFile.Click += new System.EventHandler(this.btnUseLotNumberToLoadDesignFile_Click);
			// 
			// btnBrowseForRecipeFile
			// 
			this.btnBrowseForRecipeFile.Image = global::VFAACP.Properties.Resources.FolderFind32;
			this.btnBrowseForRecipeFile.Location = new System.Drawing.Point(1020, 20);
			this.btnBrowseForRecipeFile.Name = "btnBrowseForRecipeFile";
			this.btnBrowseForRecipeFile.Size = new System.Drawing.Size(186, 84);
			this.btnBrowseForRecipeFile.TabIndex = 6;
			this.btnBrowseForRecipeFile.Text = "Browse for Recipe File";
			this.btnBrowseForRecipeFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnBrowseForRecipeFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnBrowseForRecipeFile.UseVisualStyleBackColor = true;
			this.btnBrowseForRecipeFile.Click += new System.EventHandler(this.btnBrowseForRecipeFile_Click);
			// 
			// btnToolingCalibrationProtocolRun
			// 
			this.btnToolingCalibrationProtocolRun.Image = global::VFAACP.Properties.Resources.DataInformation32;
			this.btnToolingCalibrationProtocolRun.Location = new System.Drawing.Point(20, 20);
			this.btnToolingCalibrationProtocolRun.Name = "btnToolingCalibrationProtocolRun";
			this.btnToolingCalibrationProtocolRun.Size = new System.Drawing.Size(186, 84);
			this.btnToolingCalibrationProtocolRun.TabIndex = 1;
			this.btnToolingCalibrationProtocolRun.Text = "Tooling Calibration Protocol Run";
			this.btnToolingCalibrationProtocolRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnToolingCalibrationProtocolRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnToolingCalibrationProtocolRun.UseVisualStyleBackColor = true;
			this.btnToolingCalibrationProtocolRun.Click += new System.EventHandler(this.btnToolingCalibrationProtocolRun_Click);
			// 
			// btnBrowseForProductionDesignFile
			// 
			this.btnBrowseForProductionDesignFile.Image = global::VFAACP.Properties.Resources.FolderFind32;
			this.btnBrowseForProductionDesignFile.Location = new System.Drawing.Point(820, 20);
			this.btnBrowseForProductionDesignFile.Name = "btnBrowseForProductionDesignFile";
			this.btnBrowseForProductionDesignFile.Size = new System.Drawing.Size(186, 84);
			this.btnBrowseForProductionDesignFile.TabIndex = 5;
			this.btnBrowseForProductionDesignFile.Text = "Browse for Production Design File ";
			this.btnBrowseForProductionDesignFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnBrowseForProductionDesignFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnBrowseForProductionDesignFile.UseVisualStyleBackColor = true;
			this.btnBrowseForProductionDesignFile.Click += new System.EventHandler(this.btnBrowseForProductionDesignFile_Click);
			// 
			// btnMoldServiceSetup
			// 
			this.btnMoldServiceSetup.Image = global::VFAACP.Properties.Resources.DataInformation32;
			this.btnMoldServiceSetup.Location = new System.Drawing.Point(220, 20);
			this.btnMoldServiceSetup.Name = "btnMoldServiceSetup";
			this.btnMoldServiceSetup.Size = new System.Drawing.Size(186, 84);
			this.btnMoldServiceSetup.TabIndex = 2;
			this.btnMoldServiceSetup.Text = "Mold Service Setup";
			this.btnMoldServiceSetup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnMoldServiceSetup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnMoldServiceSetup.UseVisualStyleBackColor = true;
			this.btnMoldServiceSetup.Click += new System.EventHandler(this.btnMoldServiceSetup_Click);
			// 
			// SetupNewMeasDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(1426, 162);
			this.Controls.Add(this.btnMoldServiceSetup);
			this.Controls.Add(this.btnBrowseForProductionDesignFile);
			this.Controls.Add(this.btnToolingCalibrationProtocolRun);
			this.Controls.Add(this.btnBrowseForRecipeFile);
			this.Controls.Add(this.btnMeasureVerificationPart);
			this.Controls.Add(this.btnBrowseForEngineeringDesignFile);
			this.Controls.Add(this.btnUseLotNumberToLoadDesignFile);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetupNewMeasDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Setup New Measurement";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SetupNewMeasDialog_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUseLotNumberToLoadDesignFile;
        private System.Windows.Forms.Button btnBrowseForEngineeringDesignFile;
        private System.Windows.Forms.Button btnMeasureVerificationPart;
		private System.Windows.Forms.Button btnBrowseForRecipeFile;
		private System.Windows.Forms.Button btnToolingCalibrationProtocolRun;
        private System.Windows.Forms.Button btnBrowseForProductionDesignFile;
        private System.Windows.Forms.Button btnMoldServiceSetup;
    }
}