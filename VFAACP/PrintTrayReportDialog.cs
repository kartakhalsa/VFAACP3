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
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;

namespace VFAACP
{
    public partial class PrintTrayReportDialog : Form
    {
        private Form _mainForm;
        SiteListInfo _siteListInfo;
        private TrayReport trayReport = null;
        ReportPrintTool printTool = null;

        public PrintTrayReportDialog(Form mainForm, SiteListInfo siteListInfo, string presetComment)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _siteListInfo = siteListInfo;
            tbComments.Lines = presetComment.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void Setup()
        {
            if (trayReport != null)
                return;
            trayReport = TrayReport.CreateReport(_siteListInfo, tbComments.Text);
            printTool = new ReportPrintTool(trayReport);
            try
            {
                printTool.LoadPrinterSettings("PrinterSettings.xml");
            }
            catch { }
            printTool.PrintingSystem.EndPrint += new EventHandler(printingSystem_EndPrint);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Setup();
            bool wasTopMost = this.TopMost;
            this.TopMost = false;
            printTool.PreviewForm.TopMost = true;
            printTool.ShowPreviewDialog();
            this.TopMost = wasTopMost;
        }

        private void printingSystem_EndPrint(object sender, EventArgs e)
        {
            try
            {
                printTool.SavePrinterSettings("PrinterSettings.xml");
            }
            catch { }
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            Setup();
            printTool.ShowPageSetup();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            Setup();
            printTool.PrintDialog();
        }

        private void btnSavePDF_Click(object sender, EventArgs e)
        {
            Setup();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = ProgramSettings.PublishFileRootDirectory;
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files (*.pdf)|*.pdf";
            dlg.Title = "Choose PDF File Destination";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            string msg = "";
            try
            {
                File.Delete(dlg.FileName);
                trayReport.ExportToPdf(dlg.FileName);
            }
            catch (Exception ex)
            {
                msg = "Error saving file " + dlg.FileName;
                msg += Environment.NewLine;
                msg += ex.Message;
                MessageBox.Show(this, msg, "Save File Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDialog_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;
        }

        private void btnDone_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void tbComments_TextChanged(object sender, EventArgs e)
        {
            trayReport = null;
        }
    }
}
