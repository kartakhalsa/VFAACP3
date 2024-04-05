namespace VFAACP
{
    partial class StandardsPickerDialog
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StandardsPickerDialog));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.lvVerificationParts = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.lblTypes = new System.Windows.Forms.Label();
			this.lblRecipes = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(11, 429);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(127, 35);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(147, 429);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(127, 35);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "Document-32.png");
			// 
			// lvVerificationParts
			// 
			this.lvVerificationParts.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lvVerificationParts.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.lvVerificationParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvVerificationParts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvVerificationParts.FullRowSelect = true;
			this.lvVerificationParts.GridLines = true;
			this.lvVerificationParts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvVerificationParts.HideSelection = false;
			this.lvVerificationParts.LabelWrap = false;
			this.lvVerificationParts.Location = new System.Drawing.Point(16, 76);
			this.lvVerificationParts.MultiSelect = false;
			this.lvVerificationParts.Name = "lvVerificationParts";
			this.lvVerificationParts.Size = new System.Drawing.Size(256, 345);
			this.lvVerificationParts.SmallImageList = this.imageList;
			this.lvVerificationParts.TabIndex = 2;
			this.lvVerificationParts.TileSize = new System.Drawing.Size(175, 50);
			this.lvVerificationParts.UseCompatibleStateImageBehavior = false;
			this.lvVerificationParts.View = System.Windows.Forms.View.Details;
			this.lvVerificationParts.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvVerificationParts_ItemSelectionChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Recipes";
			this.columnHeader1.Width = 254;
			// 
			// cbTypes
			// 
			this.cbTypes.FormattingEnabled = true;
			this.cbTypes.Location = new System.Drawing.Point(16, 29);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(256, 21);
			this.cbTypes.TabIndex = 1;
			this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbVerificationCategories_SelectedIndexChanged);
			// 
			// lblTypes
			// 
			this.lblTypes.AutoSize = true;
			this.lblTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTypes.Location = new System.Drawing.Point(13, 10);
			this.lblTypes.Name = "lblTypes";
			this.lblTypes.Size = new System.Drawing.Size(41, 13);
			this.lblTypes.TabIndex = 5;
			this.lblTypes.Text = "Types";
			// 
			// lblRecipes
			// 
			this.lblRecipes.AutoSize = true;
			this.lblRecipes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRecipes.Location = new System.Drawing.Point(11, 57);
			this.lblRecipes.Name = "lblRecipes";
			this.lblRecipes.Size = new System.Drawing.Size(53, 13);
			this.lblRecipes.TabIndex = 6;
			this.lblRecipes.Text = "Recipes";
			// 
			// StandardsPickerDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(286, 473);
			this.Controls.Add(this.lblRecipes);
			this.Controls.Add(this.lblTypes);
			this.Controls.Add(this.cbTypes);
			this.Controls.Add(this.lvVerificationParts);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StandardsPickerDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose a Verification Part";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.StandardsPickerDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView lvVerificationParts;
		private System.Windows.Forms.ComboBox cbTypes;
		private System.Windows.Forms.Label lblTypes;
		private System.Windows.Forms.Label lblRecipes;
		private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}