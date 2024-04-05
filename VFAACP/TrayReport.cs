using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace VFAACP
{
    public partial class TrayReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TrayReport()
        {
            InitializeComponent();
        }

        private XRPanel GetSitePanel(int siteIndex)
        {
            switch (siteIndex % 4)
            {
                case 0:
                    return site1_Panel;
                case 1:
                    return site2_Panel;
                case 2:
                    return site3_Panel;
                case 3:
                    return site4_Panel;
            }
            return null;
        }

        private XRLabel GetSiteIdTextBox(int siteIndex)
        {
            switch (siteIndex % 4)
            {
                case 0:
                    return site1_id_TextBox;
                case 1:
                    return site2_id_TextBox;
                case 2:
                    return site3_id_TextBox;
                case 3:
                    return site4_id_TextBox;
            }
            return null;
        }

        private XRLabel GetSiteResultsTextBox(int siteIndex)
        {
            switch (siteIndex % 4)
            {
                case 0:
                    return site1_results_TextBox;
                case 1:
                    return site2_results_TextBox;
                case 2:
                    return site3_results_TextBox;
                case 3:
                    return site4_results_TextBox;
            }
            return null;
        }

        private XRPictureBox GetSiteBitmapBox1(int siteIndex)
        {
            switch (siteIndex % 4)
            {
                case 0:
                    return site1_plot1_PicBox;
                case 1:
                    return site2_plot1_PicBox;
                case 2:
                    return site3_plot1_PicBox;
                case 3:
                    return site4_plot1_PicBox;
            }
            return null;
        }

        private XRPictureBox GetSiteBitmapBox2(int siteIndex)
        {
            switch (siteIndex % 4)
            {
                case 0:
                    return site1_plot2_PicBox;
                case 1:
                    return site2_plot2_PicBox;
                case 2:
                    return site3_plot2_PicBox;
                case 3:
                    return site4_plot2_PicBox;
            }
            return null;
        }

        public static TrayReport CreateReport(SiteListInfo siteListInfo, string comments)
        {
            const int maxMeasContextLineLen = 78;
            List<TrayReport> reportList = new List<TrayReport>();
            TrayReport curPage = new TrayReport();
            reportList.Add(curPage);

            string measContext = ReportUtils.GetMeasContextDescr(maxMeasContextLineLen);

            string operComment;
            operComment = "OPERATOR ADDED INFORMATION\n";
            operComment += comments;

            Bitmap siteMap = new Bitmap(1000, 1000);
            siteMap = siteListInfo.Draw(siteMap, 35.0F /*fontSizeScale*/);

            curPage.measContext_TextBox.Text = measContext;
            curPage.operComment_TextBox.Text = operComment;
            curPage.siteMap_PicBox.Image = siteMap;

            List<Site> siteList = siteListInfo.IncludedSites;
            if (siteList.Count == 0)
                return curPage;
            int siteIndex = 0;
            int numPages = 1;
            int numSitesThisPage = 0;
            foreach (Site site in siteList)
            {
                if (numSitesThisPage >= 4)
                {
                    curPage = new TrayReport();
                    curPage.measContext_TextBox.Text = measContext;
                    curPage.operComment_TextBox.Text = operComment;
                    curPage.siteMap_PicBox.Image = siteMap;
                    reportList.Add(curPage);
                    ++numPages;
                    numSitesThisPage = 0;
                }
                XRPanel panel = curPage.GetSitePanel(siteIndex);
                panel.Visible = true;

                SiteBox siteBox = site.SiteBox;
                string idText;
                idText = "Site " + siteBox.SiteNum.ToString();
                if (site.IsCalibrationSite)
                    idText += "C";

                XRLabel idTextBox = curPage.GetSiteIdTextBox(siteIndex);
                idTextBox.Text = idText;
                Color backColor;
                if (site.IsMeasuredOK || site.IsMeasuredErr)
                    backColor = siteBox.BoxBackColor;
                else
                    backColor = Color.White;
                if (backColor.ToArgb() != Color.White.ToArgb())
                    idTextBox.BackColor = backColor;

                XRLabel resultsTextBox = curPage.GetSiteResultsTextBox(siteIndex);
                string resultText = siteBox.SiteResultText;
                if (!site.IsCalibrationSite && resultText.StartsWith("Design"))
                {   // Discard first line to save space
                    int i = resultText.IndexOf('\n'); // First newline char
                    resultText = resultText.Substring(i + 1);
                }
                resultsTextBox.Text = resultText;

                Image bitmap1 = siteBox.SiteBitmap;
                if (bitmap1 != null)
                {
                    XRPictureBox picBox = curPage.GetSiteBitmapBox1(siteIndex);
                    picBox.Image = bitmap1;
                    picBox.BackColor = Color.Black;
                }

                Image bitmap2 = siteBox.SiteBitmap2;
                if (bitmap2 != null)
                {
                    XRPictureBox picBox = curPage.GetSiteBitmapBox2(siteIndex);
                    picBox.Image = bitmap2;
                    picBox.BackColor = Color.Black;
                }

                ++siteIndex;
                ++numSitesThisPage;
            } // foreach site

            TrayReport report;
            if (numPages == 1)
            {
                report = curPage;
            }
            else
            {
                TrayReport[] reportAry = reportList.ToArray();
                report = reportAry[0];
                report.CreateDocument();
                for (int i = 1; i < reportAry.Length; i++)
                {
                    TrayReport r = reportAry[i];
                    r.CreateDocument();
                    report.Pages.AddRange(r.Pages);
                }
                report.PrintingSystem.ContinuousPageNumbering = true;
            }
            return report;
        } // CreateReport()

    }
}
