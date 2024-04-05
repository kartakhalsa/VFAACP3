using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.IO;

namespace VFAACP
{
    public partial class PrintScreenDialog : Form
    {
        private Form mainForm;
        private PrintDocument printDocument = new PrintDocument();
        private PageSettings pgSettings = new PageSettings();
        private Bitmap memoryImage;

        public PrintScreenDialog(Form f)
        {
            InitializeComponent();
            mainForm = f;
			
			pgSettings.Landscape = true; // Landscape is default orientation

            printDocument.DefaultPageSettings = pgSettings;
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);

            // Capture screen
            Graphics g = mainForm.CreateGraphics();
            Size s = mainForm.Size;
            memoryImage = new Bitmap(s.Width, s.Height, g);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(mainForm.Location.X, mainForm.Location.Y, 0, 0, s);
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
			Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle printBounds = e.MarginBounds;
            double aspect = ((double)memoryImage.Height / (double)memoryImage.Width);
            Rectangle rect = new Rectangle();
            if (printBounds.Width * aspect < printBounds.Height)
            {
                rect = new Rectangle(printBounds.Left, printBounds.Top, printBounds.Width, (int)(printBounds.Width * aspect));
            }
            if (printBounds.Height / aspect < printBounds.Width)
            {
                rect = new Rectangle(printBounds.Left, printBounds.Top, (int)(printBounds.Height / aspect), printBounds.Height);
            }
            g.DrawImage(memoryImage, rect);
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            Page_Setup_Cmd();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print_Cmd();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Print_Preview_Cmd();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            Save_File_Cmd();
        }

        private void PrintDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            printDocument.PrintPage -= new PrintPageEventHandler(PrintPage);
        }

        private void PrintDialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;
        }

        private void Page_Setup_Cmd()
        {
            PageSetupDialog dlg = new PageSetupDialog();
			dlg.PageSettings = pgSettings;
			dlg.AllowOrientation = true;
			dlg.AllowMargins = true;
			bool wasTopMost = mainForm.TopMost;
			if (!Debugger.IsAttached)
				mainForm.TopMost = true;
			dlg.ShowDialog(this);
			mainForm.TopMost = wasTopMost;
        }

        private void Print_Preview_Cmd()
        {
			PrintPreviewDialog dlg = new PrintPreviewDialog();
			dlg.Icon = Properties.Resources.VFA_ACP;
			dlg.Document = printDocument;
			dlg.Owner = this;
			dlg.Text = "Print Preview";
			dlg.Icon = Properties.Resources.VFA_ACP;
			dlg.StartPosition = FormStartPosition.CenterParent;
			dlg.Size = new System.Drawing.Size(1000, 563); // Ratio = 1920/1080
			dlg.TopMost = true;
			bool wasTopMost = mainForm.TopMost;
			if (!Debugger.IsAttached)
				mainForm.TopMost = true;
			dlg.ShowDialog(this);
			mainForm.TopMost = wasTopMost;
		}

        private void Print_Cmd()
        {
            PrintDialog dlg = new PrintDialog();
            dlg.Document = printDocument;
            dlg.AllowPrintToFile = false;
			DialogResult dr = dlg.ShowDialog();
            if (dr == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void Save_File_Cmd()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (MeasSetup.IsToolingCalibrationSetup)
                sfd.InitialDirectory = ProgramSettings.ToolingCalPublishFileRootDirectory;
            else
                sfd.InitialDirectory = ProgramSettings.PublishFileRootDirectory;
            sfd.DefaultExt = ".png";
            sfd.Filter = "Image Files (*.png)|*.png";
            sfd.Title = "Choose Image File Destination";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                memoryImage.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                if (File.Exists(sfd.FileName))
                {
                    MessageBox.Show(this, "File " + sfd.FileName + " saved.", "Save File Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

		private void btnDone_Click(object sender, EventArgs e)
		{
			this.Close();
		}
    }
}
