using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFAACP
{
    public partial class ToolingCalibrationDialog : Form
    {
		private string _protocolNumber;
		private string _lotNumber;
        private string _operatorName;

        public ToolingCalibrationDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.VFA_ACP;
        }

		public string ProtocolNumber
		{
			get { return _protocolNumber; }
			set { _protocolNumber = value; }
		}

        public string LotNumber
        {
			get { return _lotNumber; }
			set { _lotNumber = value; }
        }

        public string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }

        private void Dialog_Load(object sender, EventArgs e)
		{
			txtProtocolNumber.Text = _protocolNumber;
			txtLotNumber.Text = _lotNumber;
            txtOperatorName.Text = _operatorName;
			this.ActiveControl = txtProtocolNumber;
		}

        private void btnOK_Click(object sender, EventArgs e)
        {
			string msg = null;
			char[] invalidChars = Path.GetInvalidFileNameChars().Concat(new char[] { ' ' }).ToArray();

            string protocolNumber = txtProtocolNumber.Text.Trim();
			if (protocolNumber.IndexOfAny(invalidChars) >= 0)
			{
				msg = "Protocol Number contains an invalid character.";
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (protocolNumber.Length == 0)
			{
				msg = "Protocol Number cannot be blank.";
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

            string lotNumber = txtLotNumber.Text.Trim();
			if (lotNumber.IndexOfAny(invalidChars) >= 0)
			{
				msg = "Lot Number contains an invalid character.";
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (lotNumber.Length == 0)
			{
				msg = "Lot Number cannot be blank.";
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

            string operatorName = txtOperatorName.Text.Trim();
            if (operatorName.Length == 0)
            {
                msg = "Operator Name cannot be blank.";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _protocolNumber = protocolNumber;
            _lotNumber = lotNumber;
            _operatorName = operatorName;
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
