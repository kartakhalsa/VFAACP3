using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace VFAACP
{
    public partial class SelectDesignFileDialog : Form
    {
        private string _designFile;

        public SelectDesignFileDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.VFA_ACP;
            _designFile = null;
		}

		public string DesignFile
        {
            get { return _designFile; }
        }

        private void btnBrowseForEngineeringDesignFile_Click(object sender, EventArgs e)
        {
            string designFile;
            bool ok = MeasSetup.Browse_For_Engineering_Design_File(this, out designFile);
            if (ok)
                SetDialogOutputs(designFile);
        }

        private void btnBrowseForProductionDesignFile_Click(object sender, EventArgs e)
        {
            string designFile;
            bool ok = MeasSetup.Browse_For_Production_Design_File(this, out designFile);
            if (ok)
                SetDialogOutputs(designFile);
        }

        private void SetDialogOutputs(string designFile)
        {
            _designFile = null;
			bool ok;
			if (!string.IsNullOrEmpty(designFile))
			{
				string extension = Path.GetExtension(designFile).ToUpper();
				if (string.Compare(extension, ".CSV") == 0)
				{
					ok = MeasSetup.ValidateDesignCsvFile(this, designFile);
					if (!ok)
						return;
				}
			}
            _designFile = designFile;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
