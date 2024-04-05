using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFAACP
{
    public enum SiteState
    {
        Idle = 0,
        Measuring = 1,
        MeasuredOK = 2,
        MeasuredERR = 3
    }

    public partial class SiteBox : UserControl
    {
		Form mainForm;
		Site site;
		static Point lastFullSizeBitmapLocation;
		static Size lastFullSizeBitmapSize;
		static bool lastFullSizeBitmapInfoSaved = false;

        public delegate void SiteBoxEventHandler(object sender, SiteBoxEventArgs e);

        public event SiteBoxEventHandler MeasureBtnClicked;
        public event SiteBoxEventHandler StoreBtnClicked;
        public event SiteBoxEventHandler MoveBtnClicked;
        public event SiteBoxEventHandler IncludeCheckChanged;
        public event SiteBoxEventHandler SiteBoxClicked;

        private SiteState state = SiteState.Idle;
        ToolTip tt = new ToolTip();

        public SiteBox(Form mf, Site s)
        {
			mainForm = mf;
			site = s;
            InitializeComponent();
        }

        private void pbSiteBitmap_DoubleClick(object sender, EventArgs e)
        {
            ShowFullSizeBitmap(this.pbSiteBitmap.Image);
        }

		private void pbSiteBitmap2_DoubleClick(object sender, EventArgs e)
		{
			ShowFullSizeBitmap(this.pbSiteBitmap2.Image);
		}

		private void ShowFullSizeBitmap(Image image)
		{
			// Pop up a modal dialog showing a zoomed version of image
			FullSizeBitmap fsb = new FullSizeBitmap();
			fsb.SiteNum = this.SiteNum;
			fsb.DisplayImage = image;
			if (lastFullSizeBitmapInfoSaved)
			{
				fsb.Location = lastFullSizeBitmapLocation;
				fsb.Size = lastFullSizeBitmapSize;
			}
			bool wasTopMost = mainForm.TopMost;
			mainForm.TopMost = true;
			DialogResult dr = fsb.ShowDialog();
			mainForm.TopMost = wasTopMost;
			lastFullSizeBitmapLocation = fsb.Location;
			lastFullSizeBitmapSize = fsb.Size;
			lastFullSizeBitmapInfoSaved = true;
		}

		[CategoryAttribute("User Controls")]
		public new Site Site
		{
			get { return this.site; }
		}

		[CategoryAttribute("User Controls")]
        public int SiteNum
        {
            get { return int.Parse(this.lblCurSite.Text); }
            set { this.lblCurSite.Text = value.ToString(); }
        }

        [CategoryAttribute("User Controls")]
        public int NumSites
        {
            get { return int.Parse(this.lblNumSites.Text); }
            set { this.lblNumSites.Text = value.ToString(); }
        }

        [CategoryAttribute("User Controls")]
        public bool SiteIncluded
        {
            get { return this.chkIncluded.Checked; }
            set 
            { 
                this.chkIncluded.Checked = value;
                this.btnMove.Enabled = value;
                this.btnMeasure.Enabled = value;
            }
        }

        [Browsable(false)]
        public bool EnableChkIncluded
        {
            set
            {
                this.chkIncluded.Enabled = value;
            }
        }

        [Browsable(false)]
        public bool EnableMoveButton
        {
            set
            {
                this.btnMove.Enabled = value;
            }
        }

        [Browsable(false)]
        public bool EnableStoreButton
        {
            set
            {
                this.btnStore.Enabled = value;
            }
        }

		[Browsable(false)]
		public bool HideMoveButton
		{
			set
			{
				this.btnMove.Visible = !value;
			}
		}

		[Browsable(false)]
        public bool HideStoreButton
        {
            set
            {
                this.btnStore.Visible = !value;
            }
        }

		[Browsable(false)]
		public bool HideMeasureButton
		{
			set
			{
				this.btnMeasure.Visible = !value;
			}
		}

		[Browsable(false)]
		public bool HidePrintButton
		{
			set
			{
				this.btnPrint.Visible = !value;
			}
		}

		[Browsable(false)]
        public bool HighlightStoreButton
        {
            set
            {
                if (value)
                {
                    this.btnStore.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    this.btnStore.UseVisualStyleBackColor = true;
                }
            }
        }

        [Browsable(false)]
        public bool AutoStoreEnabled
        {
            get
            {
                return (this.chkAutoStore.Visible && this.chkAutoStore.Checked);
            }
        }

        [Browsable(false)]
        public bool HideAutoStoreCheckBox
        {
            set
            {
                this.chkAutoStore.Visible = !value;
            }
        }

        [Browsable(false)]
        public bool EnableMeasureButton
        {
            set
            {
                this.btnMeasure.Enabled = value;
            }
        }

		[Browsable(false)]
		public bool EnablePrintButton
		{
			set
			{
				this.btnPrint.Enabled = value;
			}
		}
		
		[Browsable(false)]
        public bool SitePublished
        {
            set
            {
                if (value == true)
                {
                    this.lblPrintIcon.Visible = true;
                }
                else
                {
                    this.lblPrintIcon.Visible = false;
                }
            }
        }

        [CategoryAttribute("User Controls")]
        public string SiteResultText
        {
            get { return this.txtResults.Text; }
            set { this.txtResults.Text = value; }
        }

        [CategoryAttribute("User Controls")]
        public Image SiteBitmap
        {
            get { return this.pbSiteBitmap.Image; }
            set
            {
                pbSiteBitmap.Image = value;
                if (value != null)
                    pbSiteBitmap.BackColor = Color.Black;
                else
                    pbSiteBitmap.BackColor = Color.White;
            }
        }

		[CategoryAttribute("User Controls")]
		public Image SiteBitmap2
		{
			get { return this.pbSiteBitmap2.Image; }
            set
            {
                pbSiteBitmap2.Image = value;
                if (value != null)
                    pbSiteBitmap2.BackColor = Color.Black;
                else
                    pbSiteBitmap2.BackColor = Color.White;
            }
        }

        [CategoryAttribute("User Controls")]
        public SiteState State
        {
            get { return this.state; }
            set 
            {
                this.state = value;

                switch (this.state)
                {
                    case SiteState.Idle:
                        this.BoxBackColor = Color.White;
                        break;
                    case SiteState.Measuring:
                        this.BoxBackColor = Color.FromArgb(255, 255, 128);
                        break;
                    case SiteState.MeasuredOK:
                        this.BoxBackColor = Color.FromArgb(128, 255, 128);
                        break;
                    case SiteState.MeasuredERR:
                        this.BoxBackColor = Color.FromArgb(255, 115, 115);
                        break;
                }
            }
        }

		public Color BoxBackColor
		{
			get { return this.txtResults.BackColor; }
			set { this.txtResults.BackColor = value; }
		}

		public void SetMeasureButtonName(string name)
		{
			this.btnMeasure.Text = name;
		}

		public Point GetPositionInForm()
		{
			Point p = this.Location;
			Control parent = this.Parent;
			while (!(parent is Form))
			{
				p.Offset(parent.Location.X, parent.Location.Y);
				parent = parent.Parent;
			}
			return p;
		}

		private void btnMove_Click(object sender, EventArgs e)
        {
            MoveBtnClicked(this, new SiteBoxEventArgs(int.Parse(this.lblCurSite.Text), false));
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            StoreBtnClicked(this, new SiteBoxEventArgs(int.Parse(this.lblCurSite.Text), false));
        }

        private void btnMeasure_Click(object sender, EventArgs e)
        {
            MeasureBtnClicked(this, new SiteBoxEventArgs(int.Parse(this.lblCurSite.Text),false));
        }

        private void chkIncluded_CheckedChanged(object sender, EventArgs e)
        {
            IncludeCheckChanged(this, new SiteBoxEventArgs(int.Parse(this.lblCurSite.Text), this.chkIncluded.Checked));   
        }

        private void lblResults_Click(object sender, EventArgs e)
        {
            SiteBoxClicked(this, new SiteBoxEventArgs(null, false));
        }

        private void SiteBox_Click(object sender, EventArgs e)
        {
            SiteBoxClicked(this, new SiteBoxEventArgs(null, false));
        }

        private void pbSiteBitmap_Click(object sender, EventArgs e)
        {
            SiteBoxClicked(this, new SiteBoxEventArgs(null, false));
        }

        private void SiteBox_SizeChanged(object sender, EventArgs e)
        {
            // When site box size changes, keep publish icon in lower, right corner
            int x = (txtResults.Location.X + txtResults.Size.Width) - 36;
            int y = (txtResults.Location.Y + txtResults.Size.Height) - 18;
            lblPrintIcon.Location = new Point(x, y);
        }

        private void chkAutoStore_MouseHover(object sender, EventArgs e)
        {
            tt.Show("Auto Store X/Y (On/Off)", chkAutoStore);
        }

        private void chkAutoStore_MouseLeave(object sender, EventArgs e)
        {
            tt.Hide(chkAutoStore);
        }

        private void chkIncluded_MouseHover(object sender, EventArgs e)
        {
            tt.Show("Include Site (On/Off)", chkIncluded);
        }

        private void chkIncluded_MouseLeave(object sender, EventArgs e)
        {
            tt.Hide(chkIncluded);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			FileSystemFuncs.AppendToLogFile("Print_Site_Cmd");
			PrintSiteReportDialog dlg = new PrintSiteReportDialog(mainForm, this, MeasSetup.PresetComment);
			bool wasTopMost = mainForm.TopMost;
			dlg.TopMost = true;
			if (!Debugger.IsAttached)
				mainForm.TopMost = true;
			dlg.ShowDialog();
			mainForm.TopMost = wasTopMost;
		}
    }

}
