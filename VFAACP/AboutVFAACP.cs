using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace VFAACP
{
    partial class AboutVFAACP : Form
    {
        public AboutVFAACP()
        {
            InitializeComponent();
            this.Text = "About";
			this.labelProductName.Text = Program.GetAssemblyProduct();
            this.labelVersion.Text = "Version " + Program.GetAssemblyVersion();
			this.labelCopyright.Text = Program.GetAssemblyCopyright();
			this.labelCompanyName.Text = Program.GetAssemblyCompany();
            string text = "";
            text += Program.GetAssemblyDescription();
            text += Environment.NewLine;
            text += Environment.NewLine;
            text += "Settings file:" + Environment.NewLine;
            text += Program.SettingsFile;
            text += Environment.NewLine;
            text += Environment.NewLine;
            text += "Log file:" + Environment.NewLine;
            text += Program.LogFile;
            this.textBoxDescription.Text = text;
        }


        private void AboutVFAACP_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;
        }
    }
}
