using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VFAACP
{
    public partial class StandardsPickerDialog : Form
    {
        private string _verificationPartRecipeFile;

        public StandardsPickerDialog()
        {
            InitializeComponent();

            _verificationPartRecipeFile = "";
        }

        private void StandardsPickerDialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;

			List<string> categories = VerificationParts.GetCategories();
			cbTypes.Items.Clear();
			foreach (string name in categories)
			{
				cbTypes.Items.Add(name);
			}
			if (categories.Count <= 1)
			{
				cbTypes.Enabled = false;
			}
			else
			{
				cbTypes.Enabled = true;
			}
			string s = VerificationParts.LastCategorySelected;
			if ((s != null) && (cbTypes.Items.Contains(s)))
			{
				cbTypes.SelectedIndex = cbTypes.Items.IndexOf(s);
			}
			else
			{
				cbTypes.SelectedIndex = 0;
			}
		} // StandardsPickerDialog_Load()

		private void FormatListView(string category)
		{
			List<VerificationPart> parts = VerificationParts.GetPartsInCategory(category);
			lvVerificationParts.Items.Clear();
			foreach (VerificationPart part in parts)
			{
                ListViewItem lvi = new ListViewItem();
				lvi.Text = part.RecipeName;
				lvi.Tag = part;
				lvVerificationParts.Items.Add(lvi);
			}
			btnOK.Enabled = false;
		} // FormatListView()

        public string VerificationPartRecipeFile
        {
            get { return _verificationPartRecipeFile; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

 		private void cbVerificationCategories_SelectedIndexChanged(object sender, EventArgs e)
		{
			string category = (string)cbTypes.SelectedItem;
			FormatListView(category);
			VerificationParts.LastCategorySelected = category;
		} // cbVerificationCategories_SelectedIndexChanged()

       private void lvVerificationParts_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvVerificationParts.SelectedItems.Count > 0)
            {
				VerificationPart part = (VerificationPart) e.Item.Tag;
				_verificationPartRecipeFile = part.RecipeFile;
                btnOK.Enabled = true;
            }
            else
            {
                _verificationPartRecipeFile = "";
                btnOK.Enabled = false;
            }
		} // lvVerificationParts_ItemSelectionChanged()

    }
}
