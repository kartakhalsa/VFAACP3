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
    public partial class PasswordDialog : Form
    {
        private string enteredPwd;

        public PasswordDialog()
        {
            InitializeComponent();
        }

        public string EnteredPassword
        {
            get { return enteredPwd; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            enteredPwd = tbPasswordEntry.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void PasswordDialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;
        }
    }
}
