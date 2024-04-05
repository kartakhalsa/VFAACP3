using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace VFAACP
{
    public partial class SiteReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SiteReport()
        {
            InitializeComponent();
        }

        public static SiteReport CreateReport(SiteBox siteBox, string comments)
        {
            const int maxMeasContextLineLen = 62;
            SiteReport report = new SiteReport();

            string measContext = ReportUtils.GetMeasContextDescr(maxMeasContextLineLen);
            report.measContext_TextBox.Text = measContext;

            string operComment;
            operComment = "OPERATOR ADDED INFORMATION\n";
            operComment += comments;
            report.operComment_TextBox.Text = operComment;

            Site site = siteBox.Site;
            string idText;
            idText = "Site " + siteBox.SiteNum.ToString();
            if (site.IsCalibrationSite)
                idText += "C";
            report.id_TextBox.Text = idText;

            Color backColor;
            if (site.IsMeasuredOK || site.IsMeasuredErr)
                backColor = siteBox.BoxBackColor;
            else
                backColor = Color.White;
            if (backColor.ToArgb() != Color.White.ToArgb())
                report.id_TextBox.BackColor = backColor;

            string resultText = siteBox.SiteResultText;
            if (!site.IsCalibrationSite && resultText.StartsWith("Design"))
            {   // Discard first line to save space
                int i = resultText.IndexOf('\n'); // First newline char
                resultText = resultText.Substring(i + 1);
            }
            report.results_TextBox.Text = resultText;

            Image bitmap1 = null;
            bitmap1 = siteBox.SiteBitmap;
            if (bitmap1 != null)
            {
                report.plot1_PicBox.Image = bitmap1;
                report.plot1_PicBox.BackColor = Color.Black;
            }

            Image bitmap2 = null;
            bitmap2 = siteBox.SiteBitmap2;
            if (bitmap2 != null)
            {
                report.plot2_PicBox.Image = bitmap2;
                report.plot2_PicBox.BackColor = Color.Black;
            }

            return report;
        } // CreateReport()

    }
}
