namespace VFAACP
{
    partial class LoadTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadTimer));
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.lblLoadTime = new System.Windows.Forms.Label();
			this.btnDone = new System.Windows.Forms.Button();
			this.btnNeedMoreTime = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timerCountdownTick);
			// 
			// lblLoadTime
			// 
			this.lblLoadTime.Font = new System.Drawing.Font("Arial", 150F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLoadTime.Location = new System.Drawing.Point(51, 19);
			this.lblLoadTime.Name = "lblLoadTime";
			this.lblLoadTime.Size = new System.Drawing.Size(317, 236);
			this.lblLoadTime.TabIndex = 0;
			this.lblLoadTime.Text = "60";
			this.lblLoadTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnDone
			// 
			this.btnDone.Location = new System.Drawing.Point(250, 270);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(118, 38);
			this.btnDone.TabIndex = 1;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// btnNeedMoreTime
			// 
			this.btnNeedMoreTime.Location = new System.Drawing.Point(52, 270);
			this.btnNeedMoreTime.Name = "btnNeedMoreTime";
			this.btnNeedMoreTime.Size = new System.Drawing.Size(118, 38);
			this.btnNeedMoreTime.TabIndex = 2;
			this.btnNeedMoreTime.Text = "Need More Time";
			this.btnNeedMoreTime.UseVisualStyleBackColor = true;
			this.btnNeedMoreTime.Click += new System.EventHandler(this.btnNeedMoreTime_Click);
			// 
			// LoadTimer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(425, 330);
			this.Controls.Add(this.btnNeedMoreTime);
			this.Controls.Add(this.btnDone);
			this.Controls.Add(this.lblLoadTime);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoadTimer";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Load/Unload Tray";
			this.Load += new System.EventHandler(this.LoadTimer_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblLoadTime;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnNeedMoreTime;
    }
}