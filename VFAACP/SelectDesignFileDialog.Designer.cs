namespace VFAACP
{
    partial class SelectDesignFileDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDesignFileDialog));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnBrowseForEngineeringDesignFile = new System.Windows.Forms.Button();
			this.btnBrowseForProductionDesignFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(162, 115);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(102, 35);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnBrowseForEngineeringDesignFile
			// 
			this.btnBrowseForEngineeringDesignFile.Image = global::VFAACP.Properties.Resources.FolderFind32;
			this.btnBrowseForEngineeringDesignFile.Location = new System.Drawing.Point(18, 20);
			this.btnBrowseForEngineeringDesignFile.Name = "btnBrowseForEngineeringDesignFile";
			this.btnBrowseForEngineeringDesignFile.Size = new System.Drawing.Size(186, 84);
			this.btnBrowseForEngineeringDesignFile.TabIndex = 4;
			this.btnBrowseForEngineeringDesignFile.Text = "Browse for Engineering Design File ";
			this.btnBrowseForEngineeringDesignFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnBrowseForEngineeringDesignFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnBrowseForEngineeringDesignFile.UseVisualStyleBackColor = true;
			this.btnBrowseForEngineeringDesignFile.Click += new System.EventHandler(this.btnBrowseForEngineeringDesignFile_Click);
			// 
			// btnBrowseForProductionDesignFile
			// 
			this.btnBrowseForProductionDesignFile.Image = global::VFAACP.Properties.Resources.FolderFind32;
			this.btnBrowseForProductionDesignFile.Location = new System.Drawing.Point(221, 20);
			this.btnBrowseForProductionDesignFile.Name = "btnBrowseForProductionDesignFile";
			this.btnBrowseForProductionDesignFile.Size = new System.Drawing.Size(186, 84);
			this.btnBrowseForProductionDesignFile.TabIndex = 5;
			this.btnBrowseForProductionDesignFile.Text = "Browse for Production Design File ";
			this.btnBrowseForProductionDesignFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnBrowseForProductionDesignFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnBrowseForProductionDesignFile.UseVisualStyleBackColor = true;
			this.btnBrowseForProductionDesignFile.Click += new System.EventHandler(this.btnBrowseForProductionDesignFile_Click);
			// 
			// SelectDesignFileDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(426, 162);
			this.Controls.Add(this.btnBrowseForProductionDesignFile);
			this.Controls.Add(this.btnBrowseForEngineeringDesignFile);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectDesignFileDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Design FIle";
			this.TopMost = true;
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowseForEngineeringDesignFile;
        private System.Windows.Forms.Button btnBrowseForProductionDesignFile;
    }
}