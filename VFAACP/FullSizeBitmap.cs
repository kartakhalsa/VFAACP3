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
    public partial class FullSizeBitmap : Form
    {
        private Image displayImage;
        private int siteNum;

        public FullSizeBitmap()
        {
            InitializeComponent();
        }

        public int SiteNum
        {
            set { siteNum = value; }
        }

        public Image DisplayImage
        {
            set { displayImage = value; }
        }

        private void FullSizeBitmap_Load(object sender, EventArgs e)
        {
            this.Text = "Site " + siteNum.ToString() + " Bitmap";
            if (displayImage != null)
            {
                this.picBox.Image = displayImage;
            }
        }
    }
}
