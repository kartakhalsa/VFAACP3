using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace VFAACP
{
    public partial class LoadTimer : Form
    {
        private int _countdown_sec;
        private DateTime _startTime;

        public LoadTimer(int countdown_sec)
        {
            InitializeComponent();
			_countdown_sec = countdown_sec;
        }

        private void LoadTimer_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.VFA_ACP;
            this.lblLoadTime.Text = _countdown_sec.ToString();
			this.TopMost = TopMost;
            _startTime = DateTime.Now;
            this.timer.Start();
        }

        private void timerCountdownTick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - _startTime;
            int elapsed_sec = ts.Seconds;
            int remaining_sec = _countdown_sec - elapsed_sec;

            if (remaining_sec > 0)
            {
                this.lblLoadTime.Text = remaining_sec.ToString();

                if (remaining_sec <= 5) 
                {
                    // play a warning sound
                    //SystemSounds.Beep.Play();
                }
            }
            else
            {
				this.lblLoadTime.Text = null;
				Application.DoEvents();
				Thread.Sleep(500);
				Done_Cmd();
            }
        }

        private void btnNeedMoreTime_Click(object sender, EventArgs e)
        {
            NeedMoreTime_Cmd();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Done_Cmd();
        }

        private void Done_Cmd()
        {
            this.DialogResult = DialogResult.OK;
            this.timer.Stop();
            this.Close();
        }

        private void NeedMoreTime_Cmd()
        {
            this.DialogResult = DialogResult.Retry;
            this.timer.Stop();
            this.Close();
        }

    }
}
