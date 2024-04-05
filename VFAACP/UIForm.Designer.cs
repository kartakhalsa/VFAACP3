namespace VFAACP
{
    partial class UIForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
			this.panelHeader = new System.Windows.Forms.Panel();
			this.btnLiveDisplay = new System.Windows.Forms.Button();
			this.btnSelectTray = new System.Windows.Forms.Button();
			this.btnPrintReport = new System.Windows.Forms.Button();
			this.btnPrintScreen = new System.Windows.Forms.Button();
			this.btnSetupNewMeasurement = new System.Windows.Forms.Button();
			this.lblStatusMX = new System.Windows.Forms.Label();
			this.lblStatusCP = new System.Windows.Forms.Label();
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPublish = new System.Windows.Forms.Button();
			this.btnMeasure = new System.Windows.Forms.Button();
			this.btnLoadUnload = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadUnloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.publishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.measurePublishRepeatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mxControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.killMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minimizeMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maximizeMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showMxInTaskbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dontShowMxInTaskbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setMxStyleAppWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setMxStyleToolWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideMxOffScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.connectToMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectFromMxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableJoystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.homeTiltAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveTiltAxesLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateTrayLevelCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showCrosshairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.clearAllSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSelectedSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateTrayFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setCatseyeOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshProgramSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.includeAllSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.include4SitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.include8SitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.include12SitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.include16SitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.include32SitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.excludeAllSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.includeSelectedSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.excludeSelectedSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.splitContainerSiteInfo = new System.Windows.Forms.SplitContainer();
			this.siteMapPanel = new System.Windows.Forms.Panel();
			this.siteBoxPanel = new System.Windows.Forms.Panel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusSetupTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusProtocolTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusProtocolValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLotTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLotValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusMoldPlateTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusMoldPlateValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusOperatorTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusOperatorValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusRecipeTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusRecipeValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusDesignTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusDesignValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusTolFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusTolFileValue = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusTrayLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusTrayValue = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusMxStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusMxRemoteLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusCPTitleLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusCPStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusDateTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panelHeader.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSiteInfo)).BeginInit();
			this.splitContainerSiteInfo.Panel1.SuspendLayout();
			this.splitContainerSiteInfo.Panel2.SuspendLayout();
			this.splitContainerSiteInfo.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelHeader
			// 
			this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelHeader.Controls.Add(this.btnLiveDisplay);
			this.panelHeader.Controls.Add(this.btnSelectTray);
			this.panelHeader.Controls.Add(this.btnPrintReport);
			this.panelHeader.Controls.Add(this.btnPrintScreen);
			this.panelHeader.Controls.Add(this.btnSetupNewMeasurement);
			this.panelHeader.Controls.Add(this.lblStatusMX);
			this.panelHeader.Controls.Add(this.lblStatusCP);
			this.panelHeader.Controls.Add(this.btnAbort);
			this.panelHeader.Controls.Add(this.btnStop);
			this.panelHeader.Controls.Add(this.btnPublish);
			this.panelHeader.Controls.Add(this.btnMeasure);
			this.panelHeader.Controls.Add(this.btnLoadUnload);
			this.panelHeader.Location = new System.Drawing.Point(0, 24);
			this.panelHeader.Name = "panelHeader";
			this.panelHeader.Size = new System.Drawing.Size(1428, 135);
			this.panelHeader.TabIndex = 0;
			// 
			// btnLiveDisplay
			// 
			this.btnLiveDisplay.Location = new System.Drawing.Point(934, 27);
			this.btnLiveDisplay.Name = "btnLiveDisplay";
			this.btnLiveDisplay.Size = new System.Drawing.Size(145, 35);
			this.btnLiveDisplay.TabIndex = 15;
			this.btnLiveDisplay.Text = "Live Display";
			this.btnLiveDisplay.UseVisualStyleBackColor = true;
			this.btnLiveDisplay.Click += new System.EventHandler(this.btnLiveDisplay_Click);
			// 
			// btnSelectTray
			// 
			this.btnSelectTray.Location = new System.Drawing.Point(31, 27);
			this.btnSelectTray.Name = "btnSelectTray";
			this.btnSelectTray.Size = new System.Drawing.Size(145, 35);
			this.btnSelectTray.TabIndex = 14;
			this.btnSelectTray.Text = "Select Tray";
			this.btnSelectTray.UseVisualStyleBackColor = true;
			this.btnSelectTray.Click += new System.EventHandler(this.btnSelectTray_Click);
			// 
			// btnPrintReport
			// 
			this.btnPrintReport.Location = new System.Drawing.Point(783, 27);
			this.btnPrintReport.Name = "btnPrintReport";
			this.btnPrintReport.Size = new System.Drawing.Size(145, 35);
			this.btnPrintReport.TabIndex = 13;
			this.btnPrintReport.Text = "Print Report";
			this.btnPrintReport.UseVisualStyleBackColor = true;
			this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
			// 
			// btnPrintScreen
			// 
			this.btnPrintScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrintScreen.Image = global::VFAACP.Properties.Resources.Printer32;
			this.btnPrintScreen.Location = new System.Drawing.Point(1367, 27);
			this.btnPrintScreen.Name = "btnPrintScreen";
			this.btnPrintScreen.Size = new System.Drawing.Size(35, 35);
			this.btnPrintScreen.TabIndex = 12;
			this.toolTip1.SetToolTip(this.btnPrintScreen, "Print");
			this.btnPrintScreen.UseVisualStyleBackColor = true;
			this.btnPrintScreen.Click += new System.EventHandler(this.btnPrintScreen_Click);
			// 
			// btnSetupNewMeasurement
			// 
			this.btnSetupNewMeasurement.Location = new System.Drawing.Point(182, 27);
			this.btnSetupNewMeasurement.Name = "btnSetupNewMeasurement";
			this.btnSetupNewMeasurement.Size = new System.Drawing.Size(145, 35);
			this.btnSetupNewMeasurement.TabIndex = 11;
			this.btnSetupNewMeasurement.Text = "Setup New Measurement";
			this.btnSetupNewMeasurement.UseVisualStyleBackColor = true;
			this.btnSetupNewMeasurement.Click += new System.EventHandler(this.btnSetupNewMeasurement_Click);
			// 
			// lblStatusMX
			// 
			this.lblStatusMX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatusMX.AutoEllipsis = true;
			this.lblStatusMX.BackColor = System.Drawing.Color.WhiteSmoke;
			this.lblStatusMX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblStatusMX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatusMX.Location = new System.Drawing.Point(0, 107);
			this.lblStatusMX.Name = "lblStatusMX";
			this.lblStatusMX.Size = new System.Drawing.Size(1428, 22);
			this.lblStatusMX.TabIndex = 10;
			this.lblStatusMX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStatusCP
			// 
			this.lblStatusCP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStatusCP.AutoEllipsis = true;
			this.lblStatusCP.BackColor = System.Drawing.Color.WhiteSmoke;
			this.lblStatusCP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblStatusCP.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatusCP.Location = new System.Drawing.Point(0, 86);
			this.lblStatusCP.Name = "lblStatusCP";
			this.lblStatusCP.Size = new System.Drawing.Size(1428, 22);
			this.lblStatusCP.TabIndex = 9;
			this.lblStatusCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnAbort
			// 
			this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAbort.Image = global::VFAACP.Properties.Resources.MediaStop32;
			this.btnAbort.Location = new System.Drawing.Point(1250, 27);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(70, 35);
			this.btnAbort.TabIndex = 6;
			this.toolTip1.SetToolTip(this.btnAbort, "Abort");
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Image = global::VFAACP.Properties.Resources.MediaPause32;
			this.btnStop.Location = new System.Drawing.Point(1174, 27);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(70, 35);
			this.btnStop.TabIndex = 5;
			this.toolTip1.SetToolTip(this.btnStop, "Pause after Current Measurement is Complete");
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnPublish
			// 
			this.btnPublish.Location = new System.Drawing.Point(632, 27);
			this.btnPublish.Name = "btnPublish";
			this.btnPublish.Size = new System.Drawing.Size(145, 35);
			this.btnPublish.TabIndex = 4;
			this.btnPublish.Text = "Publish";
			this.btnPublish.UseVisualStyleBackColor = true;
			this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
			// 
			// btnMeasure
			// 
			this.btnMeasure.Location = new System.Drawing.Point(482, 27);
			this.btnMeasure.Name = "btnMeasure";
			this.btnMeasure.Size = new System.Drawing.Size(145, 35);
			this.btnMeasure.TabIndex = 3;
			this.btnMeasure.Text = "Measure Included Sites";
			this.btnMeasure.UseVisualStyleBackColor = true;
			this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
			// 
			// btnLoadUnload
			// 
			this.btnLoadUnload.Location = new System.Drawing.Point(332, 27);
			this.btnLoadUnload.Name = "btnLoadUnload";
			this.btnLoadUnload.Size = new System.Drawing.Size(145, 35);
			this.btnLoadUnload.TabIndex = 2;
			this.btnLoadUnload.Text = "Load/Unload";
			this.btnLoadUnload.UseVisualStyleBackColor = true;
			this.btnLoadUnload.Click += new System.EventHandler(this.btnLoadUnload_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1428, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadUnloadToolStripMenuItem,
            this.measureToolStripMenuItem,
            this.publishToolStripMenuItem,
            this.measurePublishRepeatToolStripMenuItem,
            this.toolStripMenuSep1,
            this.mxControlToolStripMenuItem,
            this.toolStripSeparator1,
            this.connectToMxToolStripMenuItem,
            this.disconnectFromMxToolStripMenuItem,
            this.resetMCToolStripMenuItem,
            this.enableJoystickToolStripMenuItem,
            this.homeTiltAxesToolStripMenuItem,
            this.moveTiltAxesLevelToolStripMenuItem,
            this.updateTrayLevelCoordinatesToolStripMenuItem,
            this.showCrosshairToolStripMenuItem,
            this.toolStripMenuSep3,
            this.clearAllSitesToolStripMenuItem,
            this.clearSelectedSitesToolStripMenuItem,
            this.updateTrayFileToolStripMenuItem,
            this.setCatseyeOffsetToolStripMenuItem,
            this.toolStripMenuSep4,
            this.refreshTrayToolStripMenuItem,
            this.refreshProgramSettingsToolStripMenuItem,
            this.toolStripMenuSep5,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadUnloadToolStripMenuItem
			// 
			this.loadUnloadToolStripMenuItem.Name = "loadUnloadToolStripMenuItem";
			this.loadUnloadToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.loadUnloadToolStripMenuItem.Text = "Load/Unload";
			this.loadUnloadToolStripMenuItem.Click += new System.EventHandler(this.loadUnloadToolStripMenuItem_Click);
			// 
			// measureToolStripMenuItem
			// 
			this.measureToolStripMenuItem.Name = "measureToolStripMenuItem";
			this.measureToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.measureToolStripMenuItem.Text = "Measure Included Sites";
			this.measureToolStripMenuItem.Click += new System.EventHandler(this.measureToolStripMenuItem_Click);
			// 
			// publishToolStripMenuItem
			// 
			this.publishToolStripMenuItem.Name = "publishToolStripMenuItem";
			this.publishToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.publishToolStripMenuItem.Text = "Publish";
			this.publishToolStripMenuItem.Click += new System.EventHandler(this.publishToolStripMenuItem_Click);
			// 
			// measurePublishRepeatToolStripMenuItem
			// 
			this.measurePublishRepeatToolStripMenuItem.Name = "measurePublishRepeatToolStripMenuItem";
			this.measurePublishRepeatToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.measurePublishRepeatToolStripMenuItem.Text = "Measure/Publish/Repeat";
			this.measurePublishRepeatToolStripMenuItem.Click += new System.EventHandler(this.measurePublishRepeatToolStripMenuItem_Click);
			// 
			// toolStripMenuSep1
			// 
			this.toolStripMenuSep1.Name = "toolStripMenuSep1";
			this.toolStripMenuSep1.Size = new System.Drawing.Size(230, 6);
			// 
			// mxControlToolStripMenuItem
			// 
			this.mxControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMxToolStripMenuItem,
            this.closeMxToolStripMenuItem,
            this.killMxToolStripMenuItem,
            this.minimizeMxToolStripMenuItem,
            this.maximizeMxToolStripMenuItem,
            this.showMxToolStripMenuItem,
            this.showMxInTaskbarToolStripMenuItem,
            this.dontShowMxInTaskbarToolStripMenuItem,
            this.setMxStyleAppWindowToolStripMenuItem,
            this.setMxStyleToolWindowToolStripMenuItem,
            this.hideMxOffScreenToolStripMenuItem});
			this.mxControlToolStripMenuItem.Name = "mxControlToolStripMenuItem";
			this.mxControlToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.mxControlToolStripMenuItem.Text = "Mx Control";
			// 
			// startMxToolStripMenuItem
			// 
			this.startMxToolStripMenuItem.Name = "startMxToolStripMenuItem";
			this.startMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.startMxToolStripMenuItem.Text = "Start Mx";
			this.startMxToolStripMenuItem.Click += new System.EventHandler(this.startMxToolStripMenuItem_Click);
			// 
			// closeMxToolStripMenuItem
			// 
			this.closeMxToolStripMenuItem.Name = "closeMxToolStripMenuItem";
			this.closeMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.closeMxToolStripMenuItem.Text = "Close Mx";
			this.closeMxToolStripMenuItem.Click += new System.EventHandler(this.closeMxToolStripMenuItem_Click);
			// 
			// killMxToolStripMenuItem
			// 
			this.killMxToolStripMenuItem.Name = "killMxToolStripMenuItem";
			this.killMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.killMxToolStripMenuItem.Text = "Kill Mx";
			this.killMxToolStripMenuItem.Click += new System.EventHandler(this.killMxToolStripMenuItem_Click);
			// 
			// minimizeMxToolStripMenuItem
			// 
			this.minimizeMxToolStripMenuItem.Name = "minimizeMxToolStripMenuItem";
			this.minimizeMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.minimizeMxToolStripMenuItem.Text = "Minimize Mx";
			this.minimizeMxToolStripMenuItem.Click += new System.EventHandler(this.minimizeMxToolStripMenuItem_Click);
			// 
			// maximizeMxToolStripMenuItem
			// 
			this.maximizeMxToolStripMenuItem.Name = "maximizeMxToolStripMenuItem";
			this.maximizeMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.maximizeMxToolStripMenuItem.Text = "Maximize Mx";
			this.maximizeMxToolStripMenuItem.Click += new System.EventHandler(this.maximizeMxToolStripMenuItem_Click);
			// 
			// showMxToolStripMenuItem
			// 
			this.showMxToolStripMenuItem.Name = "showMxToolStripMenuItem";
			this.showMxToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.showMxToolStripMenuItem.Text = "Show Mx";
			this.showMxToolStripMenuItem.Click += new System.EventHandler(this.showMxToolStripMenuItem_Click);
			// 
			// showMxInTaskbarToolStripMenuItem
			// 
			this.showMxInTaskbarToolStripMenuItem.Enabled = false;
			this.showMxInTaskbarToolStripMenuItem.Name = "showMxInTaskbarToolStripMenuItem";
			this.showMxInTaskbarToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.showMxInTaskbarToolStripMenuItem.Text = "Show Mx in Taskbar";
			this.showMxInTaskbarToolStripMenuItem.Visible = false;
			this.showMxInTaskbarToolStripMenuItem.Click += new System.EventHandler(this.showMxInTaskbarToolStripMenuItem_Click);
			// 
			// dontShowMxInTaskbarToolStripMenuItem
			// 
			this.dontShowMxInTaskbarToolStripMenuItem.Enabled = false;
			this.dontShowMxInTaskbarToolStripMenuItem.Name = "dontShowMxInTaskbarToolStripMenuItem";
			this.dontShowMxInTaskbarToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.dontShowMxInTaskbarToolStripMenuItem.Text = "Don\'t Show Mx in Taskbar";
			this.dontShowMxInTaskbarToolStripMenuItem.Visible = false;
			this.dontShowMxInTaskbarToolStripMenuItem.Click += new System.EventHandler(this.dontShowMxInTaskbarToolStripMenuItem_Click);
			// 
			// setMxStyleAppWindowToolStripMenuItem
			// 
			this.setMxStyleAppWindowToolStripMenuItem.Enabled = false;
			this.setMxStyleAppWindowToolStripMenuItem.Name = "setMxStyleAppWindowToolStripMenuItem";
			this.setMxStyleAppWindowToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.setMxStyleAppWindowToolStripMenuItem.Text = "Set Mx Style App Window";
			this.setMxStyleAppWindowToolStripMenuItem.Visible = false;
			this.setMxStyleAppWindowToolStripMenuItem.Click += new System.EventHandler(this.setMxStyleAppWindowToolStripMenuItem_Click);
			// 
			// setMxStyleToolWindowToolStripMenuItem
			// 
			this.setMxStyleToolWindowToolStripMenuItem.Enabled = false;
			this.setMxStyleToolWindowToolStripMenuItem.Name = "setMxStyleToolWindowToolStripMenuItem";
			this.setMxStyleToolWindowToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.setMxStyleToolWindowToolStripMenuItem.Text = "Set Mx Style Tool Window";
			this.setMxStyleToolWindowToolStripMenuItem.Visible = false;
			this.setMxStyleToolWindowToolStripMenuItem.Click += new System.EventHandler(this.setMxStyleToolWindowToolStripMenuItem_Click);
			// 
			// hideMxOffScreenToolStripMenuItem
			// 
			this.hideMxOffScreenToolStripMenuItem.Name = "hideMxOffScreenToolStripMenuItem";
			this.hideMxOffScreenToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.hideMxOffScreenToolStripMenuItem.Text = "Hide Mx Off Screen";
			this.hideMxOffScreenToolStripMenuItem.Click += new System.EventHandler(this.hideMxWindowOffScreenToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(230, 6);
			// 
			// connectToMxToolStripMenuItem
			// 
			this.connectToMxToolStripMenuItem.Name = "connectToMxToolStripMenuItem";
			this.connectToMxToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.connectToMxToolStripMenuItem.Text = "Connect to Mx";
			this.connectToMxToolStripMenuItem.Click += new System.EventHandler(this.connectToMxToolStripMenuItem_Click);
			// 
			// disconnectFromMxToolStripMenuItem
			// 
			this.disconnectFromMxToolStripMenuItem.Name = "disconnectFromMxToolStripMenuItem";
			this.disconnectFromMxToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.disconnectFromMxToolStripMenuItem.Text = "Disconnect from Mx";
			this.disconnectFromMxToolStripMenuItem.Click += new System.EventHandler(this.disconnectFromMxToolStripMenuItem_Click);
			// 
			// resetMCToolStripMenuItem
			// 
			this.resetMCToolStripMenuItem.Name = "resetMCToolStripMenuItem";
			this.resetMCToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.resetMCToolStripMenuItem.Text = "Reset Motion";
			this.resetMCToolStripMenuItem.Click += new System.EventHandler(this.resetMCToolStripMenuItem_Click);
			// 
			// enableJoystickToolStripMenuItem
			// 
			this.enableJoystickToolStripMenuItem.Name = "enableJoystickToolStripMenuItem";
			this.enableJoystickToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.enableJoystickToolStripMenuItem.Text = "Enable Joystick";
			this.enableJoystickToolStripMenuItem.Click += new System.EventHandler(this.enableJoystickToolStripMenuItem_Click);
			// 
			// homeTiltAxesToolStripMenuItem
			// 
			this.homeTiltAxesToolStripMenuItem.Name = "homeTiltAxesToolStripMenuItem";
			this.homeTiltAxesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.homeTiltAxesToolStripMenuItem.Text = "Home Tilt Axes";
			this.homeTiltAxesToolStripMenuItem.Click += new System.EventHandler(this.homeTiltAxesToolStripMenuItem_Click);
			// 
			// moveTiltAxesLevelToolStripMenuItem
			// 
			this.moveTiltAxesLevelToolStripMenuItem.Name = "moveTiltAxesLevelToolStripMenuItem";
			this.moveTiltAxesLevelToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.moveTiltAxesLevelToolStripMenuItem.Text = "Move Tilt Axes Level";
			this.moveTiltAxesLevelToolStripMenuItem.Click += new System.EventHandler(this.moveTiltAxesLevelToolStripMenuItem_Click);
			// 
			// updateTrayLevelCoordinatesToolStripMenuItem
			// 
			this.updateTrayLevelCoordinatesToolStripMenuItem.Name = "updateTrayLevelCoordinatesToolStripMenuItem";
			this.updateTrayLevelCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.updateTrayLevelCoordinatesToolStripMenuItem.Text = "Update Tray Level Coordinates";
			this.updateTrayLevelCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.updateTrayLevelCoordinatesToolStripMenuItem_Click);
			// 
			// showCrosshairToolStripMenuItem
			// 
			this.showCrosshairToolStripMenuItem.CheckOnClick = true;
			this.showCrosshairToolStripMenuItem.Name = "showCrosshairToolStripMenuItem";
			this.showCrosshairToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.showCrosshairToolStripMenuItem.Text = "Show Crosshair";
			this.showCrosshairToolStripMenuItem.Click += new System.EventHandler(this.showCrosshairToolStripMenuItem_Click);
			// 
			// toolStripMenuSep3
			// 
			this.toolStripMenuSep3.Name = "toolStripMenuSep3";
			this.toolStripMenuSep3.Size = new System.Drawing.Size(230, 6);
			// 
			// clearAllSitesToolStripMenuItem
			// 
			this.clearAllSitesToolStripMenuItem.Name = "clearAllSitesToolStripMenuItem";
			this.clearAllSitesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.clearAllSitesToolStripMenuItem.Text = "Clear Data For All Sites";
			this.clearAllSitesToolStripMenuItem.Click += new System.EventHandler(this.clearAllSitesToolStripMenuItem_Click);
			// 
			// clearSelectedSitesToolStripMenuItem
			// 
			this.clearSelectedSitesToolStripMenuItem.Name = "clearSelectedSitesToolStripMenuItem";
			this.clearSelectedSitesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.clearSelectedSitesToolStripMenuItem.Text = "Clear Data For Selected Sites";
			this.clearSelectedSitesToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedSitesToolStripMenuItem_Click);
			// 
			// updateTrayFileToolStripMenuItem
			// 
			this.updateTrayFileToolStripMenuItem.Name = "updateTrayFileToolStripMenuItem";
			this.updateTrayFileToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.updateTrayFileToolStripMenuItem.Text = "Update Tray File";
			this.updateTrayFileToolStripMenuItem.Click += new System.EventHandler(this.updateTrayFileToolStripMenuItem_Click);
			// 
			// setCatseyeOffsetToolStripMenuItem
			// 
			this.setCatseyeOffsetToolStripMenuItem.Name = "setCatseyeOffsetToolStripMenuItem";
			this.setCatseyeOffsetToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.setCatseyeOffsetToolStripMenuItem.Text = "Set Catseye Offset";
			this.setCatseyeOffsetToolStripMenuItem.Click += new System.EventHandler(this.setCatseyeOffsetToolStripMenuItem_Click);
			// 
			// toolStripMenuSep4
			// 
			this.toolStripMenuSep4.Name = "toolStripMenuSep4";
			this.toolStripMenuSep4.Size = new System.Drawing.Size(230, 6);
			// 
			// refreshTrayToolStripMenuItem
			// 
			this.refreshTrayToolStripMenuItem.Name = "refreshTrayToolStripMenuItem";
			this.refreshTrayToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.refreshTrayToolStripMenuItem.Text = "Refresh Tray";
			this.refreshTrayToolStripMenuItem.Click += new System.EventHandler(this.refreshTrayToolStripMenuItem_Click);
			// 
			// refreshProgramSettingsToolStripMenuItem
			// 
			this.refreshProgramSettingsToolStripMenuItem.Name = "refreshProgramSettingsToolStripMenuItem";
			this.refreshProgramSettingsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.refreshProgramSettingsToolStripMenuItem.Text = "Refresh Program Settings";
			this.refreshProgramSettingsToolStripMenuItem.Click += new System.EventHandler(this.refreshProgramSettingsToolStripMenuItem_Click);
			// 
			// toolStripMenuSep5
			// 
			this.toolStripMenuSep5.Name = "toolStripMenuSep5";
			this.toolStripMenuSep5.Size = new System.Drawing.Size(230, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.includeAllSitesToolStripMenuItem,
            this.include4SitesToolStripMenuItem,
            this.include8SitesToolStripMenuItem,
            this.include12SitesToolStripMenuItem,
            this.include16SitesToolStripMenuItem,
            this.include32SitesToolStripMenuItem,
            this.excludeAllSitesToolStripMenuItem,
            this.includeSelectedSitesToolStripMenuItem,
            this.excludeSelectedSitesToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// includeAllSitesToolStripMenuItem
			// 
			this.includeAllSitesToolStripMenuItem.Name = "includeAllSitesToolStripMenuItem";
			this.includeAllSitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.includeAllSitesToolStripMenuItem.Text = "Include All Sites";
			this.includeAllSitesToolStripMenuItem.Click += new System.EventHandler(this.includeAllSitesToolStripMenuItem_Click);
			// 
			// include4SitesToolStripMenuItem
			// 
			this.include4SitesToolStripMenuItem.Name = "include4SitesToolStripMenuItem";
			this.include4SitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.include4SitesToolStripMenuItem.Text = "Include Sites 1-4";
			this.include4SitesToolStripMenuItem.Click += new System.EventHandler(this.includeNSitesToolStripMenuItem_Click);
			// 
			// include8SitesToolStripMenuItem
			// 
			this.include8SitesToolStripMenuItem.Name = "include8SitesToolStripMenuItem";
			this.include8SitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.include8SitesToolStripMenuItem.Text = "Include Sites 1-8";
			this.include8SitesToolStripMenuItem.Click += new System.EventHandler(this.includeNSitesToolStripMenuItem_Click);
			// 
			// include12SitesToolStripMenuItem
			// 
			this.include12SitesToolStripMenuItem.Name = "include12SitesToolStripMenuItem";
			this.include12SitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.include12SitesToolStripMenuItem.Text = "Include Sites 1-12";
			this.include12SitesToolStripMenuItem.Click += new System.EventHandler(this.includeNSitesToolStripMenuItem_Click);
			// 
			// include16SitesToolStripMenuItem
			// 
			this.include16SitesToolStripMenuItem.Name = "include16SitesToolStripMenuItem";
			this.include16SitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.include16SitesToolStripMenuItem.Text = "Include Sites 1-16";
			this.include16SitesToolStripMenuItem.Click += new System.EventHandler(this.includeNSitesToolStripMenuItem_Click);
			// 
			// include32SitesToolStripMenuItem
			// 
			this.include32SitesToolStripMenuItem.Name = "include32SitesToolStripMenuItem";
			this.include32SitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.include32SitesToolStripMenuItem.Text = "Include Sites 1-32";
			this.include32SitesToolStripMenuItem.Click += new System.EventHandler(this.includeNSitesToolStripMenuItem_Click);
			// 
			// excludeAllSitesToolStripMenuItem
			// 
			this.excludeAllSitesToolStripMenuItem.Name = "excludeAllSitesToolStripMenuItem";
			this.excludeAllSitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.excludeAllSitesToolStripMenuItem.Text = "Exclude All Sites";
			this.excludeAllSitesToolStripMenuItem.Click += new System.EventHandler(this.excludeAllSitesToolStripMenuItem_Click);
			// 
			// includeSelectedSitesToolStripMenuItem
			// 
			this.includeSelectedSitesToolStripMenuItem.Name = "includeSelectedSitesToolStripMenuItem";
			this.includeSelectedSitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.includeSelectedSitesToolStripMenuItem.Text = "Include Selected Sites";
			this.includeSelectedSitesToolStripMenuItem.Click += new System.EventHandler(this.includeSelectedSitesToolStripMenuItem_Click);
			// 
			// excludeSelectedSitesToolStripMenuItem
			// 
			this.excludeSelectedSitesToolStripMenuItem.Name = "excludeSelectedSitesToolStripMenuItem";
			this.excludeSelectedSitesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.excludeSelectedSitesToolStripMenuItem.Text = "Exclude Selected Sites";
			this.excludeSelectedSitesToolStripMenuItem.Click += new System.EventHandler(this.excludeSelectedSitesToolStripMenuItem_Click);
			// 
			// configurationToolStripMenuItem
			// 
			this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
			this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
			this.configurationToolStripMenuItem.Text = "Configuration";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.aboutToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem1
			// 
			this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem1.Text = "About";
			this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
			// 
			// toolStripMenuSep2
			// 
			this.toolStripMenuSep2.Name = "toolStripMenuSep2";
			this.toolStripMenuSep2.Size = new System.Drawing.Size(219, 6);
			// 
			// splitContainerSiteInfo
			// 
			this.splitContainerSiteInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainerSiteInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainerSiteInfo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.splitContainerSiteInfo.Location = new System.Drawing.Point(0, 156);
			this.splitContainerSiteInfo.Name = "splitContainerSiteInfo";
			// 
			// splitContainerSiteInfo.Panel1
			// 
			this.splitContainerSiteInfo.Panel1.BackColor = System.Drawing.Color.White;
			this.splitContainerSiteInfo.Panel1.Controls.Add(this.siteMapPanel);
			// 
			// splitContainerSiteInfo.Panel2
			// 
			this.splitContainerSiteInfo.Panel2.BackColor = System.Drawing.Color.White;
			this.splitContainerSiteInfo.Panel2.Controls.Add(this.siteBoxPanel);
			this.splitContainerSiteInfo.Size = new System.Drawing.Size(1428, 459);
			this.splitContainerSiteInfo.SplitterDistance = 645;
			this.splitContainerSiteInfo.SplitterWidth = 3;
			this.splitContainerSiteInfo.TabIndex = 2;
			// 
			// siteMapPanel
			// 
			this.siteMapPanel.BackColor = System.Drawing.Color.White;
			this.siteMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.siteMapPanel.Location = new System.Drawing.Point(0, 0);
			this.siteMapPanel.Name = "siteMapPanel";
			this.siteMapPanel.Size = new System.Drawing.Size(643, 457);
			this.siteMapPanel.TabIndex = 0;
			this.siteMapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.siteMapPanel_Paint);
			this.siteMapPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.siteMapPanel_MouseClick);
			this.siteMapPanel.Resize += new System.EventHandler(this.siteMapPanel_Resize);
			// 
			// siteBoxPanel
			// 
			this.siteBoxPanel.AutoScroll = true;
			this.siteBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.siteBoxPanel.Location = new System.Drawing.Point(0, 0);
			this.siteBoxPanel.Name = "siteBoxPanel";
			this.siteBoxPanel.Size = new System.Drawing.Size(778, 457);
			this.siteBoxPanel.TabIndex = 7;
			this.siteBoxPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.siteBoxPanel_Scroll);
			this.siteBoxPanel.SizeChanged += new System.EventHandler(this.siteBoxPanel_SizeChanged);
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusSetupTitleLabel,
            this.toolStripStatusProtocolTitleLabel,
            this.toolStripStatusProtocolValueLabel,
            this.toolStripStatusLotTitleLabel,
            this.toolStripStatusLotValueLabel,
            this.toolStripStatusMoldPlateTitleLabel,
            this.toolStripStatusMoldPlateValueLabel,
            this.toolStripStatusOperatorTitleLabel,
            this.toolStripStatusOperatorValueLabel,
            this.toolStripStatusRecipeTitleLabel,
            this.toolStripStatusRecipeValueLabel,
            this.toolStripStatusDesignTitleLabel,
            this.toolStripStatusDesignValueLabel,
            this.toolStripStatusTolFileLabel,
            this.toolStripStatusTolFileValue,
            this.toolStripStatusTrayLabel,
            this.toolStripStatusTrayValue,
            this.toolStripStatusLabel2,
            this.toolStripStatusMxStatusLabel,
            this.toolStripStatusMxRemoteLabel,
            this.toolStripStatusCPTitleLabel,
            this.toolStripStatusCPStateLabel,
            this.toolStripStatusDateTime});
			this.statusStrip1.Location = new System.Drawing.Point(0, 616);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(1428, 24);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip";
			// 
			// toolStripStatusSetupTitleLabel
			// 
			this.toolStripStatusSetupTitleLabel.AutoSize = false;
			this.toolStripStatusSetupTitleLabel.BackColor = System.Drawing.Color.CornflowerBlue;
			this.toolStripStatusSetupTitleLabel.ForeColor = System.Drawing.Color.White;
			this.toolStripStatusSetupTitleLabel.Name = "toolStripStatusSetupTitleLabel";
			this.toolStripStatusSetupTitleLabel.Size = new System.Drawing.Size(95, 19);
			this.toolStripStatusSetupTitleLabel.Text = "Current Setup";
			// 
			// toolStripStatusProtocolTitleLabel
			// 
			this.toolStripStatusProtocolTitleLabel.Name = "toolStripStatusProtocolTitleLabel";
			this.toolStripStatusProtocolTitleLabel.Size = new System.Drawing.Size(55, 19);
			this.toolStripStatusProtocolTitleLabel.Text = "Protocol:";
			this.toolStripStatusProtocolTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusProtocolValueLabel
			// 
			this.toolStripStatusProtocolValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusProtocolValueLabel.Name = "toolStripStatusProtocolValueLabel";
			this.toolStripStatusProtocolValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusProtocolValueLabel.Text = "*";
			this.toolStripStatusProtocolValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusProtocolValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusProtocolValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusLotTitleLabel
			// 
			this.toolStripStatusLotTitleLabel.Name = "toolStripStatusLotTitleLabel";
			this.toolStripStatusLotTitleLabel.Size = new System.Drawing.Size(27, 19);
			this.toolStripStatusLotTitleLabel.Text = "Lot:";
			this.toolStripStatusLotTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLotValueLabel
			// 
			this.toolStripStatusLotValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLotValueLabel.Name = "toolStripStatusLotValueLabel";
			this.toolStripStatusLotValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusLotValueLabel.Text = "*";
			this.toolStripStatusLotValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusLotValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusLotValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusMoldPlateTitleLabel
			// 
			this.toolStripStatusMoldPlateTitleLabel.Name = "toolStripStatusMoldPlateTitleLabel";
			this.toolStripStatusMoldPlateTitleLabel.Size = new System.Drawing.Size(67, 19);
			this.toolStripStatusMoldPlateTitleLabel.Text = "Mold Plate:";
			this.toolStripStatusMoldPlateTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusMoldPlateValueLabel
			// 
			this.toolStripStatusMoldPlateValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusMoldPlateValueLabel.Name = "toolStripStatusMoldPlateValueLabel";
			this.toolStripStatusMoldPlateValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusMoldPlateValueLabel.Text = "*";
			this.toolStripStatusMoldPlateValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusMoldPlateValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusMoldPlateValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusOperatorTitleLabel
			// 
			this.toolStripStatusOperatorTitleLabel.Name = "toolStripStatusOperatorTitleLabel";
			this.toolStripStatusOperatorTitleLabel.Size = new System.Drawing.Size(57, 19);
			this.toolStripStatusOperatorTitleLabel.Text = "Operator:";
			this.toolStripStatusOperatorTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusOperatorValueLabel
			// 
			this.toolStripStatusOperatorValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusOperatorValueLabel.Name = "toolStripStatusOperatorValueLabel";
			this.toolStripStatusOperatorValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusOperatorValueLabel.Text = "*";
			this.toolStripStatusOperatorValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusOperatorValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusOperatorValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusRecipeTitleLabel
			// 
			this.toolStripStatusRecipeTitleLabel.Name = "toolStripStatusRecipeTitleLabel";
			this.toolStripStatusRecipeTitleLabel.Size = new System.Drawing.Size(45, 19);
			this.toolStripStatusRecipeTitleLabel.Text = "Recipe:";
			this.toolStripStatusRecipeTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusRecipeValueLabel
			// 
			this.toolStripStatusRecipeValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusRecipeValueLabel.Name = "toolStripStatusRecipeValueLabel";
			this.toolStripStatusRecipeValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusRecipeValueLabel.Text = "*";
			this.toolStripStatusRecipeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusRecipeValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusRecipeValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusDesignTitleLabel
			// 
			this.toolStripStatusDesignTitleLabel.Name = "toolStripStatusDesignTitleLabel";
			this.toolStripStatusDesignTitleLabel.Size = new System.Drawing.Size(46, 19);
			this.toolStripStatusDesignTitleLabel.Text = "Design:";
			this.toolStripStatusDesignTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusDesignValueLabel
			// 
			this.toolStripStatusDesignValueLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusDesignValueLabel.Name = "toolStripStatusDesignValueLabel";
			this.toolStripStatusDesignValueLabel.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusDesignValueLabel.Text = "*";
			this.toolStripStatusDesignValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusDesignValueLabel.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusDesignValueLabel.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusTolFileLabel
			// 
			this.toolStripStatusTolFileLabel.Name = "toolStripStatusTolFileLabel";
			this.toolStripStatusTolFileLabel.Size = new System.Drawing.Size(81, 19);
			this.toolStripStatusTolFileLabel.Text = "Tolerance File:";
			// 
			// toolStripStatusTolFileValue
			// 
			this.toolStripStatusTolFileValue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusTolFileValue.Name = "toolStripStatusTolFileValue";
			this.toolStripStatusTolFileValue.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusTolFileValue.Text = "*";
			this.toolStripStatusTolFileValue.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusTolFileValue.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusTrayLabel
			// 
			this.toolStripStatusTrayLabel.Name = "toolStripStatusTrayLabel";
			this.toolStripStatusTrayLabel.Size = new System.Drawing.Size(31, 19);
			this.toolStripStatusTrayLabel.Text = "Tray:";
			// 
			// toolStripStatusTrayValue
			// 
			this.toolStripStatusTrayValue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusTrayValue.Name = "toolStripStatusTrayValue";
			this.toolStripStatusTrayValue.Size = new System.Drawing.Size(16, 19);
			this.toolStripStatusTrayValue.Text = "*";
			this.toolStripStatusTrayValue.MouseLeave += new System.EventHandler(this.toolStripStatusItemValue_MouseLeave);
			this.toolStripStatusTrayValue.MouseHover += new System.EventHandler(this.toolStripStatusItemValue_MouseHover);
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(376, 19);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// toolStripStatusMxStatusLabel
			// 
			this.toolStripStatusMxStatusLabel.AutoSize = false;
			this.toolStripStatusMxStatusLabel.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripStatusMxStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusMxStatusLabel.Name = "toolStripStatusMxStatusLabel";
			this.toolStripStatusMxStatusLabel.Size = new System.Drawing.Size(30, 19);
			this.toolStripStatusMxStatusLabel.Text = "Mx";
			// 
			// toolStripStatusMxRemoteLabel
			// 
			this.toolStripStatusMxRemoteLabel.AutoSize = false;
			this.toolStripStatusMxRemoteLabel.BackColor = System.Drawing.Color.Red;
			this.toolStripStatusMxRemoteLabel.ForeColor = System.Drawing.Color.White;
			this.toolStripStatusMxRemoteLabel.Name = "toolStripStatusMxRemoteLabel";
			this.toolStripStatusMxRemoteLabel.Size = new System.Drawing.Size(110, 19);
			this.toolStripStatusMxRemoteLabel.Text = "Unknown";
			// 
			// toolStripStatusCPTitleLabel
			// 
			this.toolStripStatusCPTitleLabel.AutoSize = false;
			this.toolStripStatusCPTitleLabel.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripStatusCPTitleLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusCPTitleLabel.Name = "toolStripStatusCPTitleLabel";
			this.toolStripStatusCPTitleLabel.Size = new System.Drawing.Size(40, 19);
			this.toolStripStatusCPTitleLabel.Text = "ACP";
			// 
			// toolStripStatusCPStateLabel
			// 
			this.toolStripStatusCPStateLabel.AutoSize = false;
			this.toolStripStatusCPStateLabel.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripStatusCPStateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.toolStripStatusCPStateLabel.Name = "toolStripStatusCPStateLabel";
			this.toolStripStatusCPStateLabel.Size = new System.Drawing.Size(100, 19);
			this.toolStripStatusCPStateLabel.Text = "N/A";
			// 
			// toolStripStatusDateTime
			// 
			this.toolStripStatusDateTime.AutoSize = false;
			this.toolStripStatusDateTime.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripStatusDateTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusDateTime.Name = "toolStripStatusDateTime";
			this.toolStripStatusDateTime.Size = new System.Drawing.Size(125, 19);
			// 
			// UIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1428, 640);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainerSiteInfo);
			this.Controls.Add(this.panelHeader);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(1335, 500);
			this.Name = "UIForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Zygo VeriFire Asphere Automation Control Program";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UIForm_FormClosing);
			this.Load += new System.EventHandler(this.UIForm_Load);
			this.Shown += new System.EventHandler(this.UIForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyUp);
			this.panelHeader.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainerSiteInfo.Panel1.ResumeLayout(false);
			this.splitContainerSiteInfo.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerSiteInfo)).EndInit();
			this.splitContainerSiteInfo.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.Button btnLoadUnload;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadUnloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToMxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectFromMxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTrayFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateTrayLevelCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem includeAllSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem include4SitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem include8SitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem include12SitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem include16SitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem include32SitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excludeAllSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem includeSelectedSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excludeSelectedSitesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Label lblStatusMX;
        private System.Windows.Forms.Label lblStatusCP;
        private System.Windows.Forms.SplitContainer splitContainerSiteInfo;
        private System.Windows.Forms.Panel siteMapPanel;
        private System.Windows.Forms.Panel siteBoxPanel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem refreshTrayToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDateTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMxStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMxRemoteLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusCPTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusCPStateLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep4;
        private System.Windows.Forms.ToolStripMenuItem refreshProgramSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep5;
        private System.Windows.Forms.ToolStripMenuItem showCrosshairToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSep3;
        private System.Windows.Forms.ToolStripMenuItem resetMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeTiltAxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.Button btnPrintScreen;
        private System.Windows.Forms.Button btnSetupNewMeasurement;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLotTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLotValueLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMoldPlateTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMoldPlateValueLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusOperatorTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusOperatorValueLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusRecipeTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusRecipeValueLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDesignTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDesignValueLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSetupTitleLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTolFileLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTolFileValue;
        private System.Windows.Forms.ToolStripMenuItem setCatseyeOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measurePublishRepeatToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProtocolTitleLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProtocolValueLabel;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Button btnSelectTray;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTrayLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTrayValue;
        private System.Windows.Forms.ToolStripMenuItem enableJoystickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveTiltAxesLevelToolStripMenuItem;
		private System.Windows.Forms.Button btnLiveDisplay;
		private System.Windows.Forms.ToolStripMenuItem mxControlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem closeMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem killMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimizeMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem maximizeMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showMxToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dontShowMxInTaskbarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showMxInTaskbarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setMxStyleToolWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setMxStyleAppWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hideMxOffScreenToolStripMenuItem;
	}
}

