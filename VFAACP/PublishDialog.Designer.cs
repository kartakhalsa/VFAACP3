namespace VFAACP
{
    partial class PublishDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishDialog));
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkPublishToExternalDatabase = new System.Windows.Forms.CheckBox();
			this.chkPublishFullData = new System.Windows.Forms.CheckBox();
			this.lblRootFolderVal = new System.Windows.Forms.Label();
			this.lblSubFolder = new System.Windows.Forms.Label();
			this.txtSubFolder = new System.Windows.Forms.TextBox();
			this.btnChangePublishDir = new System.Windows.Forms.Button();
			this.lblRootFolder = new System.Windows.Forms.Label();
			this.grpPublishFolder = new System.Windows.Forms.GroupBox();
			this.lblNumSitesToPublish = new System.Windows.Forms.Label();
			this.btnPublish = new System.Windows.Forms.Button();
			this.chkPublishSummaryCsvFile = new System.Windows.Forms.CheckBox();
			this.grpPublishFolder.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(294, 265);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(108, 36);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkPublishToExternalDatabase
			// 
			this.chkPublishToExternalDatabase.AutoSize = true;
			this.chkPublishToExternalDatabase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPublishToExternalDatabase.Location = new System.Drawing.Point(177, 52);
			this.chkPublishToExternalDatabase.Name = "chkPublishToExternalDatabase";
			this.chkPublishToExternalDatabase.Size = new System.Drawing.Size(225, 25);
			this.chkPublishToExternalDatabase.TabIndex = 3;
			this.chkPublishToExternalDatabase.Text = "Publish to External Database";
			this.chkPublishToExternalDatabase.UseVisualStyleBackColor = true;
			this.chkPublishToExternalDatabase.CheckedChanged += new System.EventHandler(this.chkPublishToExternalDatabase_CheckedChanged);
			// 
			// chkPublishFullData
			// 
			this.chkPublishFullData.AutoSize = true;
			this.chkPublishFullData.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPublishFullData.Location = new System.Drawing.Point(177, 88);
			this.chkPublishFullData.Name = "chkPublishFullData";
			this.chkPublishFullData.Size = new System.Drawing.Size(145, 25);
			this.chkPublishFullData.TabIndex = 4;
			this.chkPublishFullData.Text = "Publish Full Data";
			this.chkPublishFullData.UseVisualStyleBackColor = true;
			this.chkPublishFullData.CheckedChanged += new System.EventHandler(this.chkPublishFullData_CheckedChanged);
			// 
			// lblRootFolderVal
			// 
			this.lblRootFolderVal.AutoEllipsis = true;
			this.lblRootFolderVal.BackColor = System.Drawing.SystemColors.Control;
			this.lblRootFolderVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblRootFolderVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRootFolderVal.Location = new System.Drawing.Point(89, 22);
			this.lblRootFolderVal.Name = "lblRootFolderVal";
			this.lblRootFolderVal.Size = new System.Drawing.Size(423, 23);
			this.lblRootFolderVal.TabIndex = 19;
			this.lblRootFolderVal.Text = "Folder";
			this.lblRootFolderVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSubFolder
			// 
			this.lblSubFolder.AutoSize = true;
			this.lblSubFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSubFolder.Location = new System.Drawing.Point(20, 58);
			this.lblSubFolder.Name = "lblSubFolder";
			this.lblSubFolder.Size = new System.Drawing.Size(63, 15);
			this.lblSubFolder.TabIndex = 18;
			this.lblSubFolder.Text = "Sub Folder";
			// 
			// txtSubFolder
			// 
			this.txtSubFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSubFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSubFolder.Location = new System.Drawing.Point(89, 56);
			this.txtSubFolder.Name = "txtSubFolder";
			this.txtSubFolder.Size = new System.Drawing.Size(423, 23);
			this.txtSubFolder.TabIndex = 17;
			this.txtSubFolder.Text = "Folder";
			// 
			// btnChangePublishDir
			// 
			this.btnChangePublishDir.Location = new System.Drawing.Point(517, 21);
			this.btnChangePublishDir.Name = "btnChangePublishDir";
			this.btnChangePublishDir.Size = new System.Drawing.Size(26, 25);
			this.btnChangePublishDir.TabIndex = 16;
			this.btnChangePublishDir.Text = "...";
			this.btnChangePublishDir.UseVisualStyleBackColor = true;
			this.btnChangePublishDir.Click += new System.EventHandler(this.btnChangePublishDir_Click);
			// 
			// lblRootFolder
			// 
			this.lblRootFolder.AutoSize = true;
			this.lblRootFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRootFolder.Location = new System.Drawing.Point(15, 27);
			this.lblRootFolder.Name = "lblRootFolder";
			this.lblRootFolder.Size = new System.Drawing.Size(68, 15);
			this.lblRootFolder.TabIndex = 15;
			this.lblRootFolder.Text = "Root Folder";
			// 
			// grpPublishFolder
			// 
			this.grpPublishFolder.Controls.Add(this.txtSubFolder);
			this.grpPublishFolder.Controls.Add(this.lblRootFolderVal);
			this.grpPublishFolder.Controls.Add(this.lblRootFolder);
			this.grpPublishFolder.Controls.Add(this.lblSubFolder);
			this.grpPublishFolder.Controls.Add(this.btnChangePublishDir);
			this.grpPublishFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpPublishFolder.Location = new System.Drawing.Point(10, 160);
			this.grpPublishFolder.Name = "grpPublishFolder";
			this.grpPublishFolder.Size = new System.Drawing.Size(559, 94);
			this.grpPublishFolder.TabIndex = 20;
			this.grpPublishFolder.TabStop = false;
			this.grpPublishFolder.Text = "Publish Folder";
			// 
			// lblNumSitesToPublish
			// 
			this.lblNumSitesToPublish.AutoSize = true;
			this.lblNumSitesToPublish.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNumSitesToPublish.ForeColor = System.Drawing.Color.SteelBlue;
			this.lblNumSitesToPublish.Location = new System.Drawing.Point(173, 20);
			this.lblNumSitesToPublish.Name = "lblNumSitesToPublish";
			this.lblNumSitesToPublish.Size = new System.Drawing.Size(226, 21);
			this.lblNumSitesToPublish.TabIndex = 22;
			this.lblNumSitesToPublish.Text = "12 of 16 Sites will be Published";
			// 
			// btnPublish
			// 
			this.btnPublish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnPublish.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPublish.Location = new System.Drawing.Point(177, 265);
			this.btnPublish.Name = "btnPublish";
			this.btnPublish.Size = new System.Drawing.Size(108, 36);
			this.btnPublish.TabIndex = 23;
			this.btnPublish.Text = "Publish";
			this.btnPublish.UseVisualStyleBackColor = true;
			this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
			// 
			// chkPublishSummaryCsvFile
			// 
			this.chkPublishSummaryCsvFile.AutoSize = true;
			this.chkPublishSummaryCsvFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkPublishSummaryCsvFile.Location = new System.Drawing.Point(177, 124);
			this.chkPublishSummaryCsvFile.Name = "chkPublishSummaryCsvFile";
			this.chkPublishSummaryCsvFile.Size = new System.Drawing.Size(213, 25);
			this.chkPublishSummaryCsvFile.TabIndex = 24;
			this.chkPublishSummaryCsvFile.Text = "Publish Summary CSV File";
			this.chkPublishSummaryCsvFile.UseVisualStyleBackColor = true;
			this.chkPublishSummaryCsvFile.CheckedChanged += new System.EventHandler(this.chkPublishSummaryCsvFile_CheckedChanged);
			// 
			// PublishDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(578, 311);
			this.Controls.Add(this.chkPublishSummaryCsvFile);
			this.Controls.Add(this.btnPublish);
			this.Controls.Add(this.lblNumSitesToPublish);
			this.Controls.Add(this.grpPublishFolder);
			this.Controls.Add(this.chkPublishFullData);
			this.Controls.Add(this.chkPublishToExternalDatabase);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PublishDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Publish Options";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PublishDialog_FormClosing);
			this.Load += new System.EventHandler(this.PublishDialog_Load);
			this.grpPublishFolder.ResumeLayout(false);
			this.grpPublishFolder.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkPublishToExternalDatabase;
        private System.Windows.Forms.CheckBox chkPublishFullData;
        private System.Windows.Forms.Label lblRootFolderVal;
        private System.Windows.Forms.Label lblSubFolder;
        private System.Windows.Forms.TextBox txtSubFolder;
        private System.Windows.Forms.Button btnChangePublishDir;
        private System.Windows.Forms.Label lblRootFolder;
        private System.Windows.Forms.GroupBox grpPublishFolder;
        private System.Windows.Forms.Label lblNumSitesToPublish;
		private System.Windows.Forms.Button btnPublish;
		private System.Windows.Forms.CheckBox chkPublishSummaryCsvFile;
    }
}