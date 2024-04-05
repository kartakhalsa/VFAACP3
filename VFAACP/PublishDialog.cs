using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFAACP
{
    public partial class PublishDialog : Form
    {
		public string NumSitesToPublishLabel { set; get; }

        public PublishDialog()
        {
            InitializeComponent();
            Icon = Properties.Resources.VFA_ACP;

		}

		private void PublishDialog_Load(object sender, EventArgs e)
		{
			lblNumSitesToPublish.Text = NumSitesToPublishLabel;
			List<CheckBox> visibleCheckBoxList = new List<CheckBox>();
			ConfigurationFile config = ProgramSettings.ActiveConfiguration;

			// Set control visibility and make list of visible controls
			if (config.PublishToExternalDatabase == 1)
			{
				chkPublishToExternalDatabase.Visible = true;
				chkPublishToExternalDatabase.Enabled = true;
				visibleCheckBoxList.Add(chkPublishToExternalDatabase);
			}
			else
			{
				chkPublishToExternalDatabase.Visible = false;
				chkPublishToExternalDatabase.Enabled = false;
			}

			if (config.PublishFullData == 1)
			{
				chkPublishFullData.Visible = true;
				chkPublishFullData.Enabled = true;
				visibleCheckBoxList.Add(chkPublishFullData);
			}
			else
			{
				chkPublishFullData.Visible = false;
				chkPublishFullData.Enabled = false;
			}

			if (config.PublishSummaryCsvFile == 1)
			{
				chkPublishSummaryCsvFile.Visible = true;
				chkPublishSummaryCsvFile.Enabled = true;
				visibleCheckBoxList.Add(chkPublishSummaryCsvFile);
			}
			else
			{
				chkPublishSummaryCsvFile.Visible = false;
				chkPublishSummaryCsvFile.Enabled = false;
			}

			if (chkPublishFullData.Enabled || chkPublishSummaryCsvFile.Enabled)
			{
				grpPublishFolder.Visible = true;
				grpPublishFolder.Enabled = true;
			}
			else
			{
				grpPublishFolder.Visible = false;
				grpPublishFolder.Enabled = false;
			}

			chkPublishToExternalDatabase.Checked = false;
			chkPublishFullData.Checked = false;
			chkPublishSummaryCsvFile.Checked = false;
			if (visibleCheckBoxList.Count == 1)
			{
				if (chkPublishToExternalDatabase.Visible)
					chkPublishToExternalDatabase.Checked = true;
				else if (chkPublishFullData.Visible)
					chkPublishFullData.Checked = true;
				else
					chkPublishSummaryCsvFile.Checked = true;
			}
			if (visibleCheckBoxList.Count < 3)
			{	// Move items up and reduce height of form
				int Y1 = chkPublishToExternalDatabase.Location.Y;
				int Y2 = chkPublishFullData.Location.Y;
				int YS = Y2 - Y1 - chkPublishFullData.Size.Height; // Vert space beteen first two checkboxes
				int Y = Y1;
				Point p;
				foreach (CheckBox cb in visibleCheckBoxList)
				{
					p = new Point(cb.Location.X, Y);
					cb.Location = p;
					Y += cb.Size.Height;
					Y += YS;
				}
				if (grpPublishFolder.Visible)
				{
					p = new Point(grpPublishFolder.Location.X, Y);
					grpPublishFolder.Location = p;
					Y += grpPublishFolder.Size.Height;
					Y += YS;
				}
				p = new Point(btnPublish.Location.X, Y);
				btnPublish.Location = p;
				p = new Point(btnCancel.Location.X, Y);
				btnCancel.Location = p;
				Y += btnPublish.Size.Height;
				Y += YS;
				Size s = new Size(ClientSize.Width, Y);
				ClientSize = s;
			}
			EnableOrDisablePublishButton();
			EnableOrDisablePublishFolder();
		} // PublishDialog_Load()

		public bool PublishToExtDatabase
        {
            get { return chkPublishToExternalDatabase.Checked; }
        }

        public bool PublishFullData
        {
            get { return chkPublishFullData.Checked; }
        }

		public bool PublishSummaryCsvFile
		{
			get { return chkPublishSummaryCsvFile.Checked; }
		}

		public string PublishRootFolder
        {
            get { return lblRootFolderVal.Text;  }
            set { lblRootFolderVal.Text = value; }
        }

        public string PublishSubFolder
        {
            set { txtSubFolder.Text = value; }
        }

        public string PublishPath
        {
            get { return lblRootFolderVal.Text + "\\" + PathSanitizer.SanitizeFilename(txtSubFolder.Text, '_'); }
        }

        private void btnChangePublishDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose a Publish Root Folder";
            fbd.SelectedPath = lblRootFolderVal.Text;
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                lblRootFolderVal.Text = fbd.SelectedPath;
            }
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkPublishToExternalDatabase_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisablePublishButton();
        }

        private void chkPublishFullData_CheckedChanged(object sender, EventArgs e)
        {
			if (chkPublishFullData.Checked)
			{
				chkPublishSummaryCsvFile.Enabled = false;
				chkPublishSummaryCsvFile.Checked = true;
			}
			else
			{
				chkPublishSummaryCsvFile.Enabled = true;
				chkPublishSummaryCsvFile.Checked = false;
			}
			EnableOrDisablePublishButton();
			EnableOrDisablePublishFolder();
        }

		private void chkPublishSummaryCsvFile_CheckedChanged(object sender, EventArgs e)
		{
			EnableOrDisablePublishButton();
			EnableOrDisablePublishFolder();
		}

		private void EnableOrDisablePublishButton()
        {
			if (chkPublishToExternalDatabase.Enabled && chkPublishToExternalDatabase.Checked)
			{
				btnPublish.Enabled = true;
				return;
			}
			if (chkPublishFullData.Enabled && chkPublishFullData.Checked)
			{
				btnPublish.Enabled = true;
				return;
			}
			if (chkPublishSummaryCsvFile.Enabled && chkPublishSummaryCsvFile.Checked)
			{
				btnPublish.Enabled = true;
				return;
			}
			btnPublish.Enabled = false;
		}

		private void EnableOrDisablePublishFolder()
		{
			if (!grpPublishFolder.Visible)
				return;
			if (chkPublishFullData.Enabled && chkPublishFullData.Checked)
			{
				grpPublishFolder.Enabled = true;
				return;
			}
			if (chkPublishSummaryCsvFile.Enabled && chkPublishSummaryCsvFile.Checked)
			{
				grpPublishFolder.Enabled = true;
				return;
			}
			grpPublishFolder.Enabled = false;
		}

		private void PublishDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			string tmp = txtSubFolder.Text.Trim();
			if (tmp.Length == 0)
			{
				string msg = "The Sub Folder name cannot be blank. ";
				MessageBox.Show(this, msg, "Error In Sub Folder Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Cancel = true;
			}
		}
	}


}
