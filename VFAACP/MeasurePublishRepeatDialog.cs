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
    public partial class MeasurePublishRepeatDialog : Form
    {
		public MeasurePublishRepeatDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.VFA_ACP;
        }

        public int NumIterations
        {
            get { return (int) nudIterations.Value; }
        }

		public bool PublishFullData
		{
			get { return chkPublishFullData.Checked;  }
		}

		public bool PublishSummaryCsvFile
		{
			get { return chkPublishSummaryCsvFile.Checked; }
		}

		private void Dialog_Load(object sender, EventArgs e)
		{
			this.ActiveControl = nudIterations;
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

		private void chkPublishFullData_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkPublishFullData.Checked)
			{
				this.chkPublishSummaryCsvFile.Checked = true;
				this.chkPublishSummaryCsvFile.Enabled = false;
			}
			else
			{
				this.chkPublishSummaryCsvFile.Enabled = true;
			}
		}
    }
}
