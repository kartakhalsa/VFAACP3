using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace VFAACP
{
	public partial class UIForm : Form
	{
		private SiteListInfo _siteListInfo;
		private static string _lastDirTrayFiles = "";
		private static string _lastDirToolingCalPublishFileRoot = "";
		private static string _lastDirPublishFileRoot = "";
		private bool _ctrlKeyPressed;
		private System.Windows.Forms.ToolTip tt;

		public UIForm()
		{
			InitializeComponent();

			try
			{
				// Init
				InitUITasks();
			}
			catch
			{
				// Any unhandled exceptions will bubble to Program.cs and displayed as a Global Error
				throw;
			}
		}
		protected override void WndProc(ref Message message)
		{
			// https://stackoverflow.com/questions/907830/how-do-you-prevent-a-windows-from-being-moved
			// https://learn.microsoft.com/en-us/windows/win32/menurc/wm-syscommand
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_MOVE = 0xF010;
			// I considered handling SC_MINIMIZE and SC_MAXIMIZE here
			//const int SC_MINIMIZE = 0xF020;
			//const int SC_MAXIMIZE = 0xF030;
			switch (message.Msg)
			{
				case WM_SYSCOMMAND:
					int command = message.WParam.ToInt32() & 0xfff0;
					if (Program.SecureDesktop && !Debugger.IsAttached && StateVars.CP_State == CP_States.StartingMx)
					{
						switch (command)
						{
							case SC_MOVE:
							//case SC_MINIMIZE:
							//case SC_MAXIMIZE:
								return;
						}
					}
					break;
			}
			base.WndProc(ref message);
		}

		#region Custom Event Handlers

		delegate void delStateVarChanged(object sender, StateVarEventArgs e);
		public void StateVarChanged(object sender, StateVarEventArgs e)
		{
			if (InvokeRequired)
			{
				object[] args = { sender, e };
				Invoke((delStateVarChanged)StateVarChanged, args);
				return;
			}

			Show_Mx_State();
			Show_CP_State();
			bool Mx_Ready = (StateVars.Mx_State == Mx_States.Connected);
			bool Mx_Disconnected = (StateVars.Mx_State == Mx_States.Disconnected);
			bool CP_Ready = !StateVars.CP_Busy;
			bool Design_Control_Set = StateVars.Design_Control_Set;
			bool Tray_Control_Set = StateVars.Tray_Control_Set;
			bool Design_And_Tray_Controls_Set = StateVars.Design_And_Tray_Controls_Set;

			bool Any_Sites_Defined = false;
			bool Any_Selected_Sites = false;
			bool Any_Included_Sites = false;
			bool Any_Data_Not_Published = false;
			bool Any_Included_Data_Not_Published = false;
			bool Any_Site_Modified = false;
			if ((_siteListInfo.Sites != null) && (_siteListInfo.Sites.Count > 0))
			{
				Any_Sites_Defined = true;
				Any_Selected_Sites = StateVars.Any_Selected_Sites(_siteListInfo.Sites);
				Any_Data_Not_Published = StateVars.Any_Data_Not_Published(_siteListInfo.Sites);
				Any_Included_Sites = StateVars.Any_Included_Sites(_siteListInfo.Sites);
				Any_Included_Data_Not_Published = StateVars.Any_Included_Data_Not_Published(_siteListInfo.Sites);
				Any_Site_Modified = _siteListInfo.AnySiteModified();
			}
			bool Enable_Include_Exclude = !Any_Data_Not_Published || (ProgramSettings.ActiveConfiguration.DisallowInclExclSiteIfAnyNotPublished == 0);

			// Select Tray button
			btnSelectTray.Enabled = (CP_Ready && !Any_Data_Not_Published);

			// Setup New Measurement Button
			btnSetupNewMeasurement.Enabled = (Mx_Ready && CP_Ready && !Any_Data_Not_Published);

			// Load/Unload Button
			btnLoadUnload.Enabled =	(Mx_Ready && CP_Ready);
			loadUnloadToolStripMenuItem.Enabled = btnLoadUnload.Enabled;

			// Measure Button
			btnMeasure.Enabled = (Mx_Ready && CP_Ready && Design_And_Tray_Controls_Set && Any_Included_Sites);
			measureToolStripMenuItem.Enabled = btnMeasure.Enabled;

			// Publish Button
			if (Debugger.IsAttached)
				btnPublish.Enabled = CP_Ready;
			else
				btnPublish.Enabled = (CP_Ready && Design_And_Tray_Controls_Set && Any_Included_Sites && Any_Included_Data_Not_Published);
			publishToolStripMenuItem.Enabled = btnPublish.Enabled;

			// Live Display button
			btnLiveDisplay.Enabled = Mx_Ready;

			// Print Report Button
			if (ProgramSettings.ActiveConfiguration.PrintReportButton == 1)
			{
				btnPrintReport.Visible = true;
				if (Debugger.IsAttached)
					btnPrintReport.Enabled = CP_Ready;
				else
					btnPrintReport.Enabled = (CP_Ready && Design_And_Tray_Controls_Set && Any_Included_Sites && Any_Included_Data_Not_Published);
			}
			else
			{
				btnPrintReport.Visible = false;
			}

			// Stop Button
			if (ProgramSettings.ActiveConfiguration.StopMeasurementSequenceAfterCurrentSite == 1)
			{
				btnStop.Visible = true;
				btnStop.Enabled = (StateVars.CP_State == CP_States.Measuring);
			}
			else
			{
				btnStop.Visible = false;
			}

			// Abort Button
			if (StateVars.CP_Moving_Or_Measuring || StateVars.CP_StartingMx)
				btnAbort.Enabled = true;
			else
				btnAbort.Enabled = false;

			// Print Screen Button
			if (ProgramSettings.ActiveConfiguration.PrintScreenButton == 1)
			{
				btnPrintScreen.Visible = true;
				btnPrintScreen.Enabled = CP_Ready;
			}
			else
			{
				btnPrintScreen.Visible = false;
			}

			// Site Boxes
			foreach (VFAACP.SiteBox sb in siteBoxPanel.Controls)
			{
				sb.EnableChkIncluded = (CP_Ready && Enable_Include_Exclude);
				// Apply Configuration
				if (ProgramSettings.ActiveConfiguration.MoveSiteButton != 1)
				{
					sb.HideMoveButton = true;
				}
				else
				{
					sb.HideMoveButton = false;
				}
				if (ProgramSettings.ActiveConfiguration.StoreSitePositionButton != 1)
				{
					sb.HideStoreButton = true;
					sb.HideAutoStoreCheckBox = true;
				}
				else
				{
					sb.HideStoreButton = false;
					sb.HideAutoStoreCheckBox = false;
				}
				if (ProgramSettings.ActiveConfiguration.MeasureSiteButton != 1)
				{
					sb.HideMeasureButton = true;
				}
				else
				{
					sb.HideMeasureButton = false;
				}
				if (ProgramSettings.ActiveConfiguration.PrintSiteButton != 1)
				{
					sb.HidePrintButton = true;
				}
				else
				{
					sb.HidePrintButton = false;
				}
				if (sb.SiteIncluded)
				{
					sb.EnableMoveButton = (Mx_Ready && CP_Ready && Tray_Control_Set);
					sb.EnableStoreButton = (Mx_Ready && CP_Ready && Tray_Control_Set);
					sb.EnableMeasureButton = (Mx_Ready && CP_Ready && Design_And_Tray_Controls_Set);
					sb.EnablePrintButton = (Mx_Ready && CP_Ready && Design_And_Tray_Controls_Set);
				}
			}

			// Menu Items

			if (ProgramSettings.ActiveConfiguration.MeasurePublishRepeat == 1)
			{
				measurePublishRepeatToolStripMenuItem.Visible = true;
				if (Debugger.IsAttached)
					measurePublishRepeatToolStripMenuItem.Enabled = true;
				else
					measurePublishRepeatToolStripMenuItem.Enabled = btnMeasure.Enabled;
			}
			else
			{
				measurePublishRepeatToolStripMenuItem.Visible = false;
				measurePublishRepeatToolStripMenuItem.Enabled = false;
			}

			if (ProgramSettings.ActiveConfiguration.MxRemoteStartStop == 1)
			{
				connectToMxToolStripMenuItem.Visible = true;
				disconnectFromMxToolStripMenuItem.Visible = true;
				connectToMxToolStripMenuItem.Enabled = (Mx_Disconnected && CP_Ready);
				disconnectFromMxToolStripMenuItem.Enabled = (!Mx_Disconnected && CP_Ready);
			}
			else
			{
				connectToMxToolStripMenuItem.Visible = false;
				disconnectFromMxToolStripMenuItem.Visible = false;
			}

			resetMCToolStripMenuItem.Enabled = (!Mx_Disconnected && CP_Ready);

			if (ProgramSettings.ActiveConfiguration.MxControl == 1)
			{
				mxControlToolStripMenuItem.Visible = true;
			}
			else
			{
				mxControlToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.EnableJoystick == 1)
			{
				enableJoystickToolStripMenuItem.Visible = true;
				enableJoystickToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready);
			}
			else
			{
				enableJoystickToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.HomeTiltAxes == 1)
			{
				homeTiltAxesToolStripMenuItem.Visible = true;
				homeTiltAxesToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready);
			}
			else
			{
				homeTiltAxesToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.MoveTiltAxesLevel == 1)
			{
				moveTiltAxesLevelToolStripMenuItem.Visible = true;
				moveTiltAxesLevelToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready);
			}
			else
			{
				moveTiltAxesLevelToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.UpdateTrayLevelCoordinates == 1)
			{
				updateTrayLevelCoordinatesToolStripMenuItem.Visible = true;
				updateTrayLevelCoordinatesToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready); ;
			}
			else
			{
				updateTrayLevelCoordinatesToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.ShowCrosshair == 1)
			{
				toolStripMenuSep3.Visible = true;
				showCrosshairToolStripMenuItem.Visible = true;
				showCrosshairToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready && Tray_Control_Set);
			}
			else
			{
				showCrosshairToolStripMenuItem.Visible = false;
				toolStripMenuSep3.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.RefreshTray == 1)
			{
				refreshTrayToolStripMenuItem.Visible = true;
				refreshTrayToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && !Any_Data_Not_Published);
			}
			else
			{
				refreshTrayToolStripMenuItem.Visible = false;
			}

			if (ProgramSettings.ActiveConfiguration.RefreshProgramSettings == 1)
			{
				refreshProgramSettingsToolStripMenuItem.Visible = true;
				refreshProgramSettingsToolStripMenuItem.Enabled = CP_Ready;
			}
			else
			{
				refreshProgramSettingsToolStripMenuItem.Visible = false;
			}

			toolStripMenuSep5.Visible = ((ProgramSettings.ActiveConfiguration.RefreshTray == 1) || (ProgramSettings.ActiveConfiguration.RefreshProgramSettings == 1));

			if (Any_Sites_Defined)
			{
				clearAllSitesToolStripMenuItem.Enabled = (CP_Ready && Design_And_Tray_Controls_Set);
				clearSelectedSitesToolStripMenuItem.Enabled = (CP_Ready && Design_And_Tray_Controls_Set && Any_Selected_Sites);

				if (ProgramSettings.ActiveConfiguration.UpdateTrayFile == 1)
				{
					updateTrayFileToolStripMenuItem.Visible = true;
					updateTrayFileToolStripMenuItem.Enabled = CP_Ready && StateVars.Design_And_Tray_Controls_Set;
				}
				else
				{
					updateTrayFileToolStripMenuItem.Visible = false;
				}
				if (clearSelectedSitesToolStripMenuItem.Enabled)
				{
					if (ProgramSettings.ActiveConfiguration.ClearSelectedSites == 0)
					{
						clearSelectedSitesToolStripMenuItem.Visible = false;
					}
				}
				if (ProgramSettings.ActiveConfiguration.SetCatseyeOffset == 1)
				{
					setCatseyeOffsetToolStripMenuItem.Visible = true;
					setCatseyeOffsetToolStripMenuItem.Enabled = (CP_Ready && Design_And_Tray_Controls_Set && Tray_Control_Set);
				}
				else
				{
					setCatseyeOffsetToolStripMenuItem.Visible = false;
				}

				includeAllSitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude);
				int n = _siteListInfo.Sites.Count;
				include4SitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude && (n >= 4));
				include8SitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude && (n >= 8));
				include12SitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude && (n >= 12));
				include16SitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude && (n >= 16));
				include32SitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude && (n >= 32));
				excludeAllSitesToolStripMenuItem.Enabled = (CP_Ready && Tray_Control_Set && Enable_Include_Exclude);
				includeSelectedSitesToolStripMenuItem.Enabled = (CP_Ready && Any_Selected_Sites && Tray_Control_Set && Enable_Include_Exclude);
				excludeSelectedSitesToolStripMenuItem.Enabled = (CP_Ready && Any_Selected_Sites && Tray_Control_Set && Enable_Include_Exclude);
			}
			else
			{
				includeAllSitesToolStripMenuItem.Enabled = false;
				include4SitesToolStripMenuItem.Enabled = false;
				include8SitesToolStripMenuItem.Enabled = false;
				include12SitesToolStripMenuItem.Enabled = false;
				include16SitesToolStripMenuItem.Enabled = false;
				include32SitesToolStripMenuItem.Enabled = false;
				excludeAllSitesToolStripMenuItem.Enabled = false;
				includeSelectedSitesToolStripMenuItem.Enabled = false;
				excludeSelectedSitesToolStripMenuItem.Enabled = false;
				clearSelectedSitesToolStripMenuItem.Enabled = false;
				clearAllSitesToolStripMenuItem.Enabled = false;

				if (ProgramSettings.ActiveConfiguration.UpdateTrayFile == 1)
				{
					updateTrayFileToolStripMenuItem.Visible = true;
					updateTrayFileToolStripMenuItem.Enabled = false;
				}
				else updateTrayFileToolStripMenuItem.Visible = false;

				if (ProgramSettings.ActiveConfiguration.SetCatseyeOffset == 1)
				{
					setCatseyeOffsetToolStripMenuItem.Visible = true;
					setCatseyeOffsetToolStripMenuItem.Enabled = (Mx_Ready && CP_Ready && Design_And_Tray_Controls_Set);
				}
				else setCatseyeOffsetToolStripMenuItem.Visible = false;
			}
			editToolStripMenuItem.Enabled = CP_Ready;
			configurationToolStripMenuItem.Enabled = CP_Ready;
			aboutToolStripMenuItem.Enabled = CP_Ready;

			// Measurement Info Indicators
			if (!string.IsNullOrEmpty(MeasSetup.ProtocolNumber))
			{
				toolStripStatusProtocolValueLabel.Text = MeasSetup.ProtocolNumber;
			}
			else toolStripStatusProtocolValueLabel.Text = "*";

			if (!string.IsNullOrEmpty(MeasSetup.LotNumber))
			{
				toolStripStatusLotValueLabel.Text = MeasSetup.LotNumber;
			}
			else toolStripStatusLotValueLabel.Text = "*";

			if (!string.IsNullOrEmpty(MeasSetup.MoldPlateID))
			{
				toolStripStatusMoldPlateValueLabel.Text = MeasSetup.MoldPlateID;
			}
			else toolStripStatusMoldPlateValueLabel.Text = "*";

			if (!string.IsNullOrEmpty(MeasSetup.OperatorName))
			{
				toolStripStatusOperatorValueLabel.Text = MeasSetup.OperatorName;
			}
			else toolStripStatusOperatorValueLabel.Text = "*";

			if (!string.IsNullOrEmpty(MeasSetup.RecipeNameRoot))
			{
				toolStripStatusRecipeValueLabel.Text = MeasSetup.RecipeNameRoot;
			}
			else toolStripStatusRecipeValueLabel.Text = "*";

			if (!string.IsNullOrEmpty(MeasSetup.DesignName))
			{
				toolStripStatusDesignValueLabel.Text = MeasSetup.DesignName;
			}
			else
			{
				toolStripStatusDesignValueLabel.Text = "*";
			}

			// Tolerance File Indicator
			if (!string.IsNullOrEmpty(MeasSetup.ToleranceFile))
			{
				toolStripStatusTolFileValue.Text = Path.GetFileName(MeasSetup.ToleranceFile);
			}
			else
			{
				toolStripStatusTolFileValue.Text = "*";
			}

			// Tray File Indicator
			if (!string.IsNullOrEmpty(MeasSetup.TrayName))
			{
				const int max_len_tray_name = 40;
				if (MeasSetup.TrayName.Length < max_len_tray_name)
					toolStripStatusTrayValue.Text = MeasSetup.TrayName;
				else
					toolStripStatusTrayValue.Text = MeasSetup.TrayName.Substring(0, max_len_tray_name - 3) + "...";
			}
			else
			{
				toolStripStatusTrayValue.Text = "*";
			}
		}   // StateVarChanged()

		public delegate void del_ReportError(string message, bool showDialog);
		public void ReportError(string message, bool showDialog = false)
		{
			if (InvokeRequired)
			{
				Invoke(new del_ReportError(ReportError), new object[] { message, showDialog });
			}
			StateVars.CP_State = CP_States.Error;
			Set_CP_Status(message);
			if (showDialog)
			{
				MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public delegate void del_Set_CP_Status(string text, int wait_ms = 0);
		public void Set_CP_Status(string text, int wait_ms = 0)
		{
			if (InvokeRequired)
			{
				Invoke(new del_Set_CP_Status(Set_CP_Status), new object[] { text, wait_ms });
			}
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace('\n', ' ');
				text = text.Replace('\r', ' ');
			}
			if (string.Compare(text, lblStatusCP.Text) != 0 && !string.IsNullOrEmpty(text))
			{
				FileSystemFuncs.AppendToLogFile("CP Status: " + text);
			}
			lblStatusCP.Text = text;
			if (wait_ms > 0)
			{
				Thread.Sleep(wait_ms);
				//lblStatusCP.Text = null;
			}
		}
		private void Clear_CP_Status()
		{
			Set_CP_Status(null);
		}

		public delegate void del_Set_Mx_Status(string text);
		private void Set_Mx_Status(string text)
		{
			if (InvokeRequired)
			{
				Invoke(new del_Set_Mx_Status(Set_Mx_Status), new object[] { text });
			}
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace('\n', ' ');
				text = text.Replace('\r', ' ');
			}
			if (string.Compare(text, lblStatusMX.Text) != 0 && !string.IsNullOrEmpty(text))
			{
				FileSystemFuncs.AppendToLogFile("MX Status: " + text);
			}
			lblStatusMX.Text = text;
		}
		private void Clear_Mx_Status()
		{
			Set_Mx_Status(null);
		}

		public delegate void del_UpdateCrosshair(bool show, PointF pos_xy_mm);
		private void UpdateCrosshair(bool show, PointF pos_xy_mm)
		{
			if (InvokeRequired)
			{
				Invoke(new del_UpdateCrosshair(UpdateCrosshair), new object[] { show, pos_xy_mm });
			}
			if (_siteListInfo != null)
			{
				SiteMap siteMap = _siteListInfo.SiteMap;
				if (siteMap != null)
				{
					if (siteMap.UdateCrosshair(show, pos_xy_mm))
						Refresh_Site_View_Cmd();
				}
			}
		}
		private void EraseCrosshair()
		{
			UpdateCrosshair(false, new PointF(0, 0));
		}

		private void StopPollingAndEraseCrosshair()
		{
			AsyncPolling.Stop();
			EraseCrosshair();
		}
		private void StartPolling()
		{
			AsyncPolling.Start();
		}

		delegate void delHandleAsyncMgrEvent(object sender, EventArgs e1);
		public void HandleAsyncMgrEvent(object sender, EventArgs e1)
		{
			if (InvokeRequired)
			{
				object[] args = { sender, e1 };
				Invoke((delHandleAsyncMgrEvent)HandleAsyncMgrEvent, args);
			}
			else
			{
				// This handles Status events from the AsyncMgr class
				// Events are used to update graphics on the SiteMap and SiteList
				// as well as react to state after an Async command is completed

				if (e1 is AsyncEventArgs_StatusMessage)
				{
					AsyncEventArgs_StatusMessage e2 = e1 as AsyncEventArgs_StatusMessage;
					Set_CP_Status(e2.statusMessage);
				}

				if (e1 is AsyncEventArgs_MxStatusMessage)
				{
					AsyncEventArgs_MxStatusMessage e2 = e1 as AsyncEventArgs_MxStatusMessage;
					Set_Mx_Status(e2.statusMessage);
				}

				if (e1 is AsyncEventArgs_ReportError)
				{
					AsyncEventArgs_ReportError e2 = e1 as AsyncEventArgs_ReportError;
					ReportError(e2.message, e2.showDialog);
				}

				if (e1 is AsyncEventArgs_PromptMessage)
				{
					AsyncEventArgs_PromptMessage e2 = e1 as AsyncEventArgs_PromptMessage;
					MessageBox.Show(this, e2.promptMessage, "Prompt", MessageBoxButtons.OK, MessageBoxIcon.None);
				}

				if (e1 is AsyncEventArgs_LoadTrayDone)
				{
					StateVars.CP_State = CP_States.Ready;
				}

				if (e1 is AsyncEventArgs_MovingToSite)
				{
					AsyncEventArgs_MovingToSite e2 = e1 as AsyncEventArgs_MovingToSite;
					StateVars.CP_State = CP_States.MovingToSite;
					Set_CP_Status("Moving to site " + e2.siteIndex.ToString());
				}

				if (e1 is AsyncEventArgs_MoveToSiteDone)
				{
					AsyncEventArgs_MoveToSiteDone e2 = e1 as AsyncEventArgs_MoveToSiteDone;
					StateVars.CP_State = CP_States.Ready;
					Set_CP_Status("Moved to site " + e2.siteIndex.ToString(), 500);
				}

				if (e1 is AsyncEventArgs_MeasureSites)
				{
					StateVars.CP_State = CP_States.Measuring;
				}

				if (e1 is AsyncEventArgs_MeasureSitesDone)
				{
					StateVars.CP_State = CP_States.Ready;
				}

				if (e1 is AsyncEventArgs_MeasSetup)
				{
					StateVars.CP_State = CP_States.Setup;
				}

				if (e1 is AsyncEventArgs_MeasSetupError)
				{
					AsyncEventArgs_MeasSetupError e2 = e1 as AsyncEventArgs_MeasSetupError;
					ReportError("Measurement setup error:\n" + e2.errorMessage, true);
					Clear_Design_Info();
				}

				if (e1 is AsyncEventArgs_MeasSetupDone)
				{
					AsyncEventArgs_MeasSetupDone e2 = e1 as AsyncEventArgs_MeasSetupDone;
					if (!string.IsNullOrEmpty(e2.trayFile))
						Read_Tray_File(e2.trayFile);
					StateVars.Design_Control_Set = true;
					Set_CP_Status("Setup New Measurement done");
				}

				if (e1 is AsyncEventArgs_MeasuredSites)
				{
					// Update SiteListInfo with state
					AsyncEventArgs_MeasuredSites e2 = e1 as AsyncEventArgs_MeasuredSites;
					int indx = 0;
					foreach (Site s in e2.siteList)
					{
						if (s.IsIncludedUserState)
						{
							if (s.IsMeasuring && StateVars.CP_State == CP_States.Measuring)
							{
								// Set focus to SiteList panel (for SiteBoxSnapToIndex)
								siteBoxPanel.Focus();

								// Snap to show last measured SiteBox in next-to-last position
								int indxoffset = NumDisplayedSiteBoxes - 1;

								if (indx > indxoffset)
								{
									SiteBoxSnapToIndex(indx - indxoffset);
								}
								else
								{
									SiteBoxSnapToIndex(0);
								}
							}
							_siteListInfo.Sites[indx].IsMeasuredErr = s.IsMeasuredErr;
							_siteListInfo.Sites[indx].IsMeasuredOK = s.IsMeasuredOK;
							_siteListInfo.Sites[indx].IsMeasuring = s.IsMeasuring;
							_siteListInfo.Sites[indx].IsPublished = s.IsPublished;
							_siteListInfo.Sites[indx].ResultBitmap1 = s.ResultBitmap1;
							_siteListInfo.Sites[indx].ResultBitmap2 = s.ResultBitmap2;
							_siteListInfo.Sites[indx].ResultText = s.ResultText;

							// Store last site num Measured OK
							if (s.IsMeasuredOK)
								_siteListInfo.LastSiteMeasuredOK = indx + 1;
						}
						indx++;
					}
					// Refresh graphics based on SiteListInfo
					Refresh_Site_View_Cmd();
				}

				if (e1 is AsyncEventArgs_PerformAsyncOpsDone)
				{
					StateVars.CP_State = CP_States.Ready;
					StartPolling();
				}

				if (e1 is AsyncEventArgs_StartingMx)
				{
					StateVars.CP_State = CP_States.StartingMx;
					Set_CP_Status("Starting Mx");
					if (Program.SecureDesktop && !Debugger.IsAttached)
					{
						FileSystemFuncs.AppendToLogFile("SecureDesktop TopMost = true");
						TopMost = true;
					}
				}

				if (e1 is AsyncEventArgs_WaitForMxStart)
				{
					AsyncEventArgs_WaitForMxStart e2 = e1 as AsyncEventArgs_WaitForMxStart;
					if (e2.remaining_sec > 0)
					{
						Set_CP_Status("Waiting for Mx start");
						Set_Mx_Status("Please wait " + e2.remaining_sec.ToString() + " sec");
					}
				}

				if (e1 is AsyncEventArgs_WaitForMxReady)
				{
					AsyncEventArgs_WaitForMxReady e2 = e1 as AsyncEventArgs_WaitForMxReady;
					if (e2.remaining_sec > 0)
					{
						Set_CP_Status("Waiting for Mx ready");
						Set_Mx_Status("Please wait " + e2.remaining_sec.ToString() + " sec");
					}
				}

				if (e1 is AsyncEventArgs_MxIsReady)
				{
					AsyncEventArgs_MxIsReady e2 = e1 as AsyncEventArgs_MxIsReady;
					Clear_CP_Status();
					if (Program.SecureDesktop)
					{
						FileSystemFuncs.AppendToLogFile("SecureDesktop: TopMost = false");
						TopMost = false;
						MaximizeBox = true;
						MinimizeBox = true;
						MxMgr mxMgr = MxMgr.Instance();
						if (mxMgr.FinalizeSecureDesktop(verbose: true))
						{
							Clear_CP_Status();
							Clear_Mx_Status();
							StartPolling();
						}
					}
					else
					{
						Clear_CP_Status();
						Clear_Mx_Status();
					}
				}

				if (e1 is AsyncEventArgs_StartMxError)
				{
					Clear_Mx_Status();
					AsyncEventArgs_StartMxError e2 = e1 as AsyncEventArgs_StartMxError;
					ReportError("Mx startup error:\n" + e2.errorMessage, true);
					//if (Program.SecureDesktop)
					//	TopMost = false;
				}

				if (e1 is AsyncEventArgs_ConnectingToMx)
				{
					Set_CP_Status("Connecting to Mx");
				}

				if (e1 is AsyncEventArgs_DisconnectingFromMx)
				{
					Set_CP_Status("Disconnecting from Mx");
				}

				if (e1 is AsyncEventArgs_MxConnected)
				{
					StateVars.Mx_State = Mx_States.Connected;
					StateVars.CP_State = CP_States.Ready;
					Set_CP_Status("Connected to Mx");
					Clear_Mx_Status();
					StartPolling();
				}

				if (e1 is AsyncEventArgs_MxDisconnected)
				{
					StateVars.Mx_State = Mx_States.Disconnected;
					StateVars.CP_State = CP_States.Ready;
					StopPollingAndEraseCrosshair();
					Set_CP_Status("Disconnected from Mx");
				}

				if (e1 is AsyncEventArgs_EStop)
				{
					StateVars.Mx_State = Mx_States.EStop;
					Recover_From_Estop();
				}

				if (e1 is AsyncEventArgs_MStop)
				{
					StateVars.Mx_State = Mx_States.MStop;
				}

				if (e1 is AsyncEventArgs_SafetyFault)
				{
					StateVars.Mx_State = Mx_States.SafetyFault;
				}

				if (e1 is AsyncEventArgs_StageNotHomed)
				{
					StateVars.Mx_State = Mx_States.StageNotHomed;
				}

				if (e1 is AsyncEventArgs_ResettingMotion)
				{
					StateVars.CP_State = CP_States.ResetMotion;
					Set_CP_Status("Resetting motion");
				}

				if (e1 is AsyncEventArgs_HomingStage)
				{
					StateVars.CP_State = CP_States.HomingStage;
					Set_CP_Status("Homing stage axes");
				}

				if (e1 is AsyncEventArgs_ResetMotionDone)
				{
					StateVars.Mx_State = Mx_States.Connected;
					StateVars.CP_State = CP_States.Ready;
					Set_CP_Status("Reset motion done", 500);
				}

				if (e1 is AsyncEventArgs_HomingTiltAxes)
				{
					StateVars.CP_State = CP_States.HomingStage;
					Set_CP_Status("Homing tilt axes");
				}

				if (e1 is AsyncEventArgs_HomeTiltAxesDone)
				{
					StateVars.Mx_State = Mx_States.Connected;
					StateVars.CP_State = CP_States.Ready;
					Set_CP_Status("Home tilt axes done", 500);
				}

				if (e1 is AsyncEventArgs_MovingToLoadUnload)
				{
					StateVars.CP_State = CP_States.MovingToLoadUnload;
					Set_CP_Status("Moving to Load/Unload position");
				}

				if (e1 is AsyncEventArgs_MoveToLoadUnloadDone)
				{
					StateVars.CP_State = CP_States.AtLoadUnload;
					Set_CP_Status("Moved to Load/Unload position", 500);
				}

				if (e1 is AsyncEventArgs_AtLoadUnload)
				{
					StateVars.CP_State = CP_States.AtLoadUnload;
				}

				if (e1 is AsyncEventArgs_NotAtLoadUnload)
				{
					StateVars.CP_State = CP_States.Ready;
				}

				if (e1 is AsyncEventArgs_Publish)
				{
					StateVars.CP_State = CP_States.Publishing;
				}

				if (e1 is AsyncEventArgs_PublishDone)
				{
					Publish_Response_Cmd();
					Refresh_Site_View_Cmd();
					StateVars.CP_State = CP_States.Ready;
				}

				if (e1 is AsyncEventArgs_CrosshairPos)
				{
					AsyncEventArgs_CrosshairPos e2 = e1 as AsyncEventArgs_CrosshairPos;
					UpdateCrosshair(e2.show, e2.pos_xy_mm);
				}

				if (e1 is AsyncEventArgs_NewCoordsForSite)
				{
					if (_siteListInfo != null)
					{
						AsyncEventArgs_NewCoordsForSite e2 = e1 as AsyncEventArgs_NewCoordsForSite;
						Store_New_Coords_For_Site(e2.siteNum, e2.stageCoords, e2.prompt);
					}
				}

				if (e1 is AsyncEventArgs_CatseyeOffset)
				{
					AsyncEventArgs_CatseyeOffset e2 = e1 as AsyncEventArgs_CatseyeOffset;
					double curX = e2.stageCoords.X_mm;
					double curY = e2.stageCoords.Y_mm;
					double curZ = e2.stageCoords.Z_mm;
					Apply_Catseye_Offset(curX, curY, curZ);
				}

				if (e1 is AsyncEventArgs_TrayLevelCoords)
				{
					AsyncEventArgs_TrayLevelCoords e2 = e1 as AsyncEventArgs_TrayLevelCoords;
					double newR = e2.stageCoords.R_deg;
					double newP = e2.stageCoords.P_deg;
					Apply_Tray_Level_Coords(newR, newP);
				}

			}
		}   // HandleAsyncMgrEvent()

		#endregion

		#region Button Bar

		private void btnSetupNewMeasurement_Click(object sender, EventArgs e)
		{
			StopPollingAndEraseCrosshair();
			string msg = "";
			string msg2 = "";
			if (!Check_External_Database_File_Before_Setup())
			{
				StartPolling();
				return;
			}
			if (StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
			{
				msg = "Data has not been published.\n\n";
				msg += "Publish or clear data before setting up for a new measurement.";
				MessageBox.Show(this, msg, "Data Not Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
				StartPolling();
				return;
			}
			Clear_Design_Info();
			Clear_All_Sites(displayWarning: false);

			// Clean the MeasSetupDirectory
			msg2 = FileSystemFuncs.DeleteDirContents(ProgramSettings.MeasSetupDirectory);
			if (!string.IsNullOrEmpty(msg2))
			{
				msg = "Error deleting contents of folder " + ProgramSettings.MeasSetupDirectory + ".\n\n";
				msg += msg2;
				MessageBox.Show(this, msg, "Setup New Measurement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Set_CP_Status(msg);
				StartPolling();
				return;
			}
			// Clean the SiteDataDirectory
			msg2 = FileSystemFuncs.DeleteDirContents(ProgramSettings.SiteDataDirectory);
			if (!string.IsNullOrEmpty(msg2))
			{
				msg = "Error deleting contents of folder " + ProgramSettings.SiteDataDirectory + ".\n\n";
				msg += msg2;
				MessageBox.Show(this, msg, "Setup New Measurement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Set_CP_Status(msg);
				StartPolling();
				return;
			}

			Set_CP_Status("Setup New Measurement");
			string newTrayFile = null;
			bool ok = MeasSetup.Setup_New_Measurement(this, out newTrayFile);
			if (!ok)
			{
				StartPolling();
				return;
			}
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpMeasSetup(MeasSetup.CurrentRecipe, newTrayFile));
		}

		private void btnLoadUnload_Click(object sender, EventArgs e)
		{
			Load_Unload_Cmd();
		}

		private void btnMeasure_Click(object sender, EventArgs e)
		{
			Measure_Tray_Cmd();
		}

		private void btnPublish_Click(object sender, EventArgs e)
		{
			
			//MxMgr mxMgr = MxMgr.Instance();
			//mxMgr.Diag2();
			Publish_Data_Cmd();
		}

		private void btnPrintReport_Click(object sender, EventArgs e)
		{
			Print_Report_Cmd();
		}

		private void btnLiveDisplay_Click(object sender, EventArgs e)
		{
			bool close_then_open = ModifierKeys.HasFlag(Keys.Control);
			Live_Display_Cmd(close_then_open);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			Stop_Cmd();
		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
			Abort_Cmd();
		}

		private void btnPrintScreen_Click(object sender, EventArgs e)
		{
			Print_Screen_Cmd();
		}

		#endregion

		#region SiteBox Code

		private int NumDisplayedSiteBoxes
		{
			get
			{
				return ProgramSettings.DisplayedSiteBoxes;
			}
		}

		private int SiteBoxSnapToIndex()
		{
			// When dragging the scrollbar, this 'snaps' the scroll to the nearest SiteBox
			int sindx = 0;
			if (_siteListInfo.Sites != null)
			{
				if (siteBoxPanel.Controls.Count == _siteListInfo.Sites.Count())
				{
					// Snap to closest SiteBox index
					int vboxsize = (int)siteBoxPanel.Height / NumDisplayedSiteBoxes;
					int scrVal = siteBoxPanel.VerticalScroll.Value;
					sindx = (int)Math.Round((float)scrVal / (float)vboxsize);
					siteBoxPanel.AutoScrollPosition = new Point(0, (int)((float)sindx * (float)vboxsize));
				}
			}
			return sindx;
		}

		private void SiteBoxSnapToIndex(int SiteBoxIndex)
		{
			// This snaps the scrollbar to the indicated SiteBox index number
			if (_siteListInfo.Sites != null)
			{
				if (SiteBoxIndex >= 0 && SiteBoxIndex <= _siteListInfo.Sites.Count())
				{
					int vboxsize = (int)siteBoxPanel.Height / NumDisplayedSiteBoxes;
					siteBoxPanel.AutoScrollPosition = new Point(0, (int)((float)SiteBoxIndex * (float)vboxsize));
				}
			}
		}

		private void ResetSiteBoxDisplay()
		{
			siteBoxPanel.SuspendLayout();
			foreach (VFAACP.SiteBox sb in siteBoxPanel.Controls)
			{
				sb.MeasureBtnClicked -= new VFAACP.SiteBox.SiteBoxEventHandler(sb_MeasureBtnClicked);
				sb.MoveBtnClicked -= new VFAACP.SiteBox.SiteBoxEventHandler(sb_MoveBtnClicked);
				sb.StoreBtnClicked -= new VFAACP.SiteBox.SiteBoxEventHandler(sb_StoreBtnClicked);
				sb.IncludeCheckChanged -= new VFAACP.SiteBox.SiteBoxEventHandler(sb_IncludeCheckChanged);
				sb.SiteBoxClicked -= new VFAACP.SiteBox.SiteBoxEventHandler(sb_SiteBoxClicked);
			}
			siteBoxPanel.Controls.Clear();
			siteBoxPanel.ResumeLayout();
		}
		private void FillSiteBoxDisplay()
		{
			// Fill siteBoxPanel with SiteBoxes and register for events.
			siteBoxPanel.SuspendLayout();

			int curSiteIndx = SiteBoxSnapToIndex();
			siteBoxPanel.Controls.Clear();

			int indx = 0;
			int h = (int)siteBoxPanel.Height / NumDisplayedSiteBoxes;
			int sbw = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + 2;
			int w = siteBoxPanel.Width - sbw;
			siteBoxPanel.AutoScrollPosition = new Point(0, 0);

			if (_siteListInfo.Sites != null)
			{
				foreach (Site s in _siteListInfo.Sites)
				{
					SiteBox sb = new SiteBox(this, s);

					// Register Events
					sb.MeasureBtnClicked += new VFAACP.SiteBox.SiteBoxEventHandler(sb_MeasureBtnClicked);
					sb.MoveBtnClicked += new VFAACP.SiteBox.SiteBoxEventHandler(sb_MoveBtnClicked);
					sb.StoreBtnClicked += new VFAACP.SiteBox.SiteBoxEventHandler(sb_StoreBtnClicked);
					sb.IncludeCheckChanged += new VFAACP.SiteBox.SiteBoxEventHandler(sb_IncludeCheckChanged);
					sb.SiteBoxClicked += new VFAACP.SiteBox.SiteBoxEventHandler(sb_SiteBoxClicked);

					sb.Size = new Size(w, h);
					sb.Location = new Point(0, (int)(h * indx));
					sb.NumSites = _siteListInfo.Sites.Count();
					sb.SiteNum = indx + 1;

					siteBoxPanel.Controls.Add(sb);
					s.SiteBox = sb;

					indx++;
				}
			}

			// Refresh graphics
			Refresh_Site_View_Cmd();

			siteBoxPanel.ResumeLayout();
		}	// FillSiteBoxDisplay()

		void sb_IncludeCheckChanged(object sender, SiteBoxEventArgs e)
		{
			// Handles a SiteBox event when the check box is changed
			int indx = (int)e.Param - 1;
			_siteListInfo.Sites[indx].IsIncludedUserState = (bool)e.State;
			Refresh_Site_View_Cmd();
			Clear_CP_Status();
		}

		void sb_MoveBtnClicked(object sender, SiteBoxEventArgs e)
		{
			// Handles a SiteBox event when the Move button is clicked
			Move_To_Site_Cmd((int)e.Param);
		}

		void sb_StoreBtnClicked(object sender, SiteBoxEventArgs e)
		{
			// Handles a SiteBox event when the Store button is clicked
			Get_New_Coords_For_Site_Cmd((int)e.Param);
		}

		void sb_MeasureBtnClicked(object sender, SiteBoxEventArgs e)
		{
			// Handles a SiteBox event when the Measure button is clicked
			Measure_Site_Cmd((int)e.Param);
		}

		void sb_SiteBoxClicked(object sender, SiteBoxEventArgs e)
		{
			// Set focus to the Panel to enable scrolling
			siteBoxPanel.Focus();
		}

		private void RedrawSiteBoxDisplay()
		{
			// This resizes the SiteList when DBPanel2 size is changed
			// The displayed sites are maintained during the resize
			siteBoxPanel.SuspendLayout();

			int curSiteIndx = SiteBoxSnapToIndex();
			int indx = 0;
			int h = (int)siteBoxPanel.Height / NumDisplayedSiteBoxes;
			int sbw = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + 2;
			int w = siteBoxPanel.Width - sbw;
			siteBoxPanel.AutoScrollPosition = new Point(0, 0);

			foreach (VFAACP.SiteBox sb in siteBoxPanel.Controls)
			{
				sb.Size = new Size(w, h);
				sb.Location = new Point(0, (int)(h * indx));
				indx++;
			}

			siteBoxPanel.ResumeLayout();

			SiteBoxSnapToIndex(curSiteIndx);

		}
		private void siteBoxPanel_SizeChanged(object sender, EventArgs e)
		{
			if (_siteListInfo != null)
			{
				if (_siteListInfo.Sites != null)
				{
					if (siteBoxPanel.Controls.Count == _siteListInfo.Sites.Count())
					{
						RedrawSiteBoxDisplay();
					}
				}
			}
		}
		private void siteBoxPanel_Scroll(object sender, ScrollEventArgs e)
		{
			// Snap to nearest index when mouse wheel is rolled
			if (e.Type.ToString() == "ThumbPosition")
			{
				SiteBoxSnapToIndex();
			}
		}

		#endregion

		#region SiteMap Code

		private void UIForm_KeyDown(object sender, KeyEventArgs e)
		{
			// If the CTRL key is held down, set value to true
			if (e.Control)
			{
				_ctrlKeyPressed = true;
			}
		}

		private void UIForm_KeyUp(object sender, KeyEventArgs e)
		{
			// Undo CTRL state when key released
			_ctrlKeyPressed = false;
		}

		private void siteMapPanel_Paint(object sender, PaintEventArgs e)
		{
			if (_siteListInfo.Sites != null)
			{
				// Create a bitmap buffer
				Bitmap bm = new Bitmap((int)siteMapPanel.Width, (int)siteMapPanel.Height);

				// Draw graphics to the buffer
				bm = _siteListInfo.Draw(bm, 15.0F /*fontSizeScale*/);

				// Set graphics view settings
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				// Send the buffer to the screen
				e.Graphics.DrawImage(bm, 0, 0);
			}
		}

		private void siteMapPanel_MouseClick(object sender, MouseEventArgs e)
		{
			// Hit Test the graphics to do things like
			// click to select or include a site
			if (_siteListInfo.Sites == null)
				return;
			// Get mouse position in pixels within the panel container
			Point p = new Point(e.X, e.Y);

			// Get state of mouse button
			if (_ctrlKeyPressed)
			{   // Include mode
				bool b1 = (ProgramSettings.ActiveConfiguration.DisallowInclExclSiteIfAnyNotPublished == 1);
				bool b2 = StateVars.Any_Data_Not_Published(_siteListInfo.Sites);
				if (!b1 || !b2)
				{
					int i = _siteListInfo.HitTest_Sites(p, "Include");
					// If clicked on a site...
					if (i > -1)
					{
						// Refresh Graphics
						Refresh_Site_View_Cmd();
					}
				}
			}
			else
			{   // Select mode
				int i = _siteListInfo.HitTest_Sites(p, "Select");

				Refresh_Site_View_Cmd();

				// if clicked on a site...
				if (i > -1)
				{
					// Refresh Graphics
					Refresh_Site_View_Cmd();

					// Clicking on a site scrolls the SiteBox into view
					SiteBoxSnapToIndex(i);
				}
			}
		}

		private void siteMapPanel_Resize(object sender, EventArgs e)
		{
			siteMapPanel.Invalidate();
		}

		#endregion

		#region UI Misc Items

		private void InitUITasks()
		{
			string msg = "";
			Show_DateTime();

			// Start timer
			timer.Start();

			try
			{
				// Get program settings
				Refresh_Program_Settings_Cmd(Program.SettingsFile);
			}
			catch
			{
				throw;
			}
			try
			{
				// Create ACP log file
				Program.LogFile = FileSystemFuncs.GetAcpLogPath();
				FileSystemFuncs.CreateLogFile(Program.LogFile);
			}
			catch (Exception ex)
			{
				msg = "";
				msg += "Error creating log file " + Program.LogFile + "\n\n";
				msg += ex.Message + "\n\n";
				msg += "The Automation Control Program cannot run until this problem is corrected.";
				throw new Exception(msg);
			}

			FileSystemFuncs.AppendToLogFile("SecureDesktop = " + (Program.SecureDesktop ? "true" : "false"));
			FileSystemFuncs.AppendToLogFile("AutoStartMx = " + (Program.AutoStartMx ? "true" : "false"));

			FileSystemFuncs.AppendToLogFile("InitUITasks");
			FileSystemFuncs.AppendToLogFile2(ProgramSettings.DumpSettings());

			if (ProgramSettings.AlwaysOnTop)
			{
				if (!Debugger.IsAttached)
				{
					FileSystemFuncs.AppendToLogFile("AlwaysOnTop: TopMost = true");
					TopMost = true;
				}
				WindowState = FormWindowState.Maximized;
				FormBorderStyle = FormBorderStyle.FixedSingle;
				MaximizeBox = false;
				MinimizeBox = false;
			}
			else if (Program.SecureDesktop)
			{
				if (!Debugger.IsAttached)
				{
					FileSystemFuncs.AppendToLogFile("SecureDesktop: TopMost = true");
					TopMost = true;
				}
				WindowState = FormWindowState.Maximized;
				MaximizeBox = false;
				MinimizeBox = false;
			}
			else
			{
				TopMost = false;
				WindowState = FormWindowState.Normal;
				MaximizeBox = true;
				MinimizeBox = true;
			}

			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.AsyncMgrEvent += new AsyncMgrEventHandler(HandleAsyncMgrEvent);

			// Register for events from StateVariables
			StateVars.StateChanged += new StateVarEventHandler(StateVarChanged);

			_siteListInfo = new SiteListInfo();

			// Get Login credentials to determine user mode
			Set_UIMode_Cmd();

			// Initialize Site Map graphics
			GraphicsFuncs.InitMatrix();
			GraphicsFuncs.ClientSizeWidth = siteMapPanel.Width;
			GraphicsFuncs.ClientSizeHeight = siteMapPanel.Height;

			tt = new System.Windows.Forms.ToolTip();

			// Refresh the form location and size
			try
			{
				// Get bounds of current screen
				Screen screen = Screen.FromControl(this);

				StreamReader sr = new StreamReader(Application.StartupPath + "\\VFAACP_Persistence.txt");
				string fl = sr.ReadLine();
				sr.Close();
				sr.Dispose();

				string[] pfl = fl.Split(new string[] { "," }, StringSplitOptions.None);

				int left = int.Parse(pfl[0]);
				int top = int.Parse(pfl[1]);
				int wid = int.Parse(pfl[2]);
				int hgt = int.Parse(pfl[3]);
				Rectangle pLoc = new Rectangle(left, top, wid, hgt);

				// Adjust screen if persisted location is out-of-bounds.  If so, it will appear full-screen.
				if (left < screen.Bounds.Left ||
					left + wid > screen.Bounds.Width ||
					top < screen.Bounds.Top ||
					top + hgt > screen.Bounds.Height)
				{
					left = screen.Bounds.Left;
					top = screen.Bounds.Top;
					wid = screen.Bounds.Width;
					hgt = screen.Bounds.Height;
				}

				Location = new Point(left, top);
				Size = new Size(wid, hgt);

			}
			catch { }

			StateVars.Design_Control_Set = false;
			StateVars.Tray_Control_Set = false;
			StateVars.Measure_Tray_Incomplete = false;

			// Clear all data from Interim Folder
			FileSystemFuncs.DeleteDirContents(ProgramSettings.InterimFileRootDirectory);

			AsyncPolling.Setup();

			Show_Mx_State();
			Show_CP_State();
		}   // InitUITasks()

		private void timer_Tick(object sender, EventArgs e)
		{
			Show_DateTime();
		}
		void Show_DateTime()
		{
			// Update Date and Time on Statusbar
			DateTime dt = DateTime.Now;
			toolStripStatusDateTime.Text = dt.ToString(ProgramSettings.StatusBarDateTimeFormat).ToUpper();
		}

		private void toolStripStatusItemValue_MouseHover(object sender, EventArgs e)
		{
			ToolStripStatusLabel label = sender as ToolStripStatusLabel;
			if (label == null)
				return;
			if (label.Text == "*")
				return;
			string val = null;
			if (label == toolStripStatusProtocolValueLabel)
				val = MeasSetup.ProtocolNumber;
			else if (label == toolStripStatusLotValueLabel)
				val = MeasSetup.LotNumber;
			else if (label == toolStripStatusMoldPlateValueLabel)
				val = MeasSetup.MoldPlateID;
			else if (label == toolStripStatusOperatorValueLabel)
				val = MeasSetup.OperatorName;
			else if (label == toolStripStatusRecipeValueLabel)
				val = MeasSetup.RecipeFile;
			else if (label == toolStripStatusDesignValueLabel)
				val = MeasSetup.DesignFile;
			else if (label == toolStripStatusTolFileValue)
				val = MeasSetup.ToleranceFile;
			else if (label == toolStripStatusTrayValue)
				val = MeasSetup.TrayFile;
			else
				return;
			Point p = new Point(label.Bounds.Left, label.Bounds.Top - 30);
			tt.Show(val, statusStrip1, p);
		}

		private void toolStripStatusItemValue_MouseLeave(object sender, EventArgs e)
		{
			tt.Hide(statusStrip1);
		}

		private void UIForm_Load(object sender, EventArgs e)
		{
			Icon = Properties.Resources.VFA_ACP;
		}

		private void UIForm_Shown(object sender, EventArgs e)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			MxMgr mxMgr = MxMgr.Instance();
			if (Program.SecureDesktop)
			{
				FileSystemFuncs.AppendToLogFile("SecureDesktop");
				asyncMgr.PerformAsyncOp(new AsyncOpStartMx(true,false), requireMxConnection: false);
			}
			else if (Program.AutoStartMx)
			{
				FileSystemFuncs.AppendToLogFile("AutoStartMx");
				asyncMgr.PerformAsyncOp(new AsyncOpStartMx(false,true), requireMxConnection: false);
			}
			else if (ProgramSettings.AutoConnectToMx && mxMgr.IsMxRunning())
			{
				FileSystemFuncs.AppendToLogFile("AutoConnectToMx");
				mxMgr.ConnectToMx();
			}
		}

		private void UIForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Confirm Exit
			string msg = "Exit the Automation Control Program?";
			bool wasTopMost = TopMost;
			if (!Debugger.IsAttached)
				TopMost = true;
			if (MessageBox.Show(this, msg, "Confirm Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
			{
				e.Cancel = true;
				TopMost = wasTopMost;
				return;
			}
			if (!Check_External_Database_File_Before_Exit())
			{
				e.Cancel = true;
				TopMost = wasTopMost;
				return;
			}
			// Test for UnPublished Data
			if (StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
			{
				msg = "Measured data exists that has not been published.\n\n";
				msg += "Do you want to close the program without publishing the data?";
				if (MessageBox.Show(this, msg, "Data Not Published", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
				{
					e.Cancel = true;
					TopMost = wasTopMost;
					return;
				}
			}
			TopMost = wasTopMost;
			FileSystemFuncs.AppendToLogFile("UIForm_FormClosing");

			StopPollingAndEraseCrosshair();

			if (Program.SecureDesktop)
			{
				FileSystemFuncs.AppendToLogFile("SecureDesktop KillMx");
				MxMgr mxMgr = MxMgr.Instance();
				mxMgr.KillMx(verbose: false);
			}

			// Unregister Events
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.AsyncMgrEvent -= new AsyncMgrEventHandler(HandleAsyncMgrEvent);
			StateVars.StateChanged -= new StateVarEventHandler(StateVarChanged);

			using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\VFAACP_Persistence.txt"))
			{
				// Persist the current location and size of the UI form
				Rectangle fp = new Rectangle(Location, Size);
				sw.WriteLine(fp.X.ToString() + "," + fp.Y.ToString() + "," + fp.Width.ToString() + "," + fp.Height.ToString());
			}
		}   // UIForm_FormClosing()

		#endregion

		#region Menu Functions

		// File Menu

		private void loadUnloadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Load_Unload_Cmd();
		}

		private void measureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Measure_Tray_Cmd();
		}

		private void publishToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Publish_Data_Cmd();
		}

		private void measurePublishRepeatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Measure_Publish_Repeat_Cmd();
		}

		private void startMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			MxMgr mxMgr = MxMgr.Instance();
			if (mxMgr.IsMxRunning())
			{
				ReportError("Mx is already running", showDialog: true);
				return;
			}
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpStartMx(false, false), requireMxConnection: false);
		}
		private void closeMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.CloseMx();
		}
		private void killMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.KillMx(verbose: true);
		}
		private void hideMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.HideMxWindow();
		}
		private void showMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.ShowMxWindow();
		}
		private void minimizeMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.MinimizeMxWindow();
		}
		private void maximizeMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.MaximizeMxWindow();
		}

		private void showMxInTaskbarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.ShowMxInTaskbar(true);
		}

		private void dontShowMxInTaskbarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.ShowMxInTaskbar(false);
		}

		private void setMxStyleAppWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.SetMxStyleAppOrToolWindow(true);
		}
		private void setMxStyleToolWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.SetMxStyleAppOrToolWindow(false);
		}

		private void hideMxWindowOffScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.MoveMxWindow(false);
		}
		private void connectToMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			Clear_CP_Status();
			Clear_Mx_Status();
			mxMgr.ConnectToMx();
		}

		private void disconnectFromMxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MxMgr mxMgr = MxMgr.Instance();
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			mxMgr.DisconnectFromMx(verbose: true);
			StateVars.Mx_State = Mx_States.Disconnected;
			StateVars.CP_State = CP_States.Error;
		}

		private void resetMCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Reset_Motion_Control_Cmd();
		}

		private void enableJoystickToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Enable_Joystick_Cmd();
		}

		private void homeTiltAxesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Home_Tilt_Axes_Cmd();
		}

		private void moveTiltAxesLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Move_Tilt_Axes_Level_Cmd();
		}

		private void showCrosshairToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			bool enabled = showCrosshairToolStripMenuItem.Checked;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.CrosshairEnabled = enabled;
			if (!enabled)
			{
				EraseCrosshair();
			}
		}

		private void clearAllSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_All_Sites_Cmd();
		}

		private void clearSelectedSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_Selected_Sites_Cmd();
		}

		private void updateTrayFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Update_Tray_File_Cmd();
		}

		private void updateTrayLevelCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Update_Tray_Level_Coords_Cmd();
		}

		private void setCatseyeOffsetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Set_Catseye_Offset_Cmd();
		}

		private void refreshTrayToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			if (MeasSetup.TrayFile != null)
			{
				Read_Tray_File(MeasSetup.TrayFile);
			}
		}

		private void refreshProgramSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			bool settingsOk = false;
			bool exitApp = false;
			string msg;
			while (settingsOk == false && exitApp == false)
			{
				try
				{
					Refresh_Program_Settings_Cmd(Program.SettingsFile);
					msg = "Program settings loaded from " + Program.SettingsFile;
					Set_CP_Status(msg);
					settingsOk = true;
				}
				catch (Exception ex)
				{
					msg = ex.Message + "\n\n";
					msg += "Edit file " + Program.SettingsFile + " to correct the problem.\n\n";
					msg += "Then click OK to try again, or click Cancel to quit.";
					DialogResult dr = MessageBox.Show(this, msg, "Program Settings Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr != DialogResult.OK)
					{
						exitApp = true;
					}
				}
			}
			if (exitApp)
			{
				FormClosing -= new System.Windows.Forms.FormClosingEventHandler(UIForm_FormClosing);
				Application.Exit();
			}
			FileSystemFuncs.AppendToLogFile("Refresh_Program_Settings_Cmd");
			FileSystemFuncs.AppendToLogFile2(ProgramSettings.DumpSettings());
			StartPolling();
		}   // refreshProgramSettingsToolStripMenuItem_Click()

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Terminate the program
			Application.Exit();
		}

		// Edit Menu
		private void includeAllSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Include_All_Sites_Cmd();
		}

		private void includeNSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			int N = 0;
			if (sender == include4SitesToolStripMenuItem)
				N = 4;
			else if (sender == include8SitesToolStripMenuItem)
				N = 8;
			else if (sender == include12SitesToolStripMenuItem)
				N = 12;
			else if (sender == include16SitesToolStripMenuItem)
				N = 16;
			else if (sender == include32SitesToolStripMenuItem)
				N = 32;
			Include_N_Sites_Cmd(N);
		}

		private void excludeAllSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Exclude_All_Sites_Cmd();
		}

		private void includeSelectedSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Include_Selected_Sites_Cmd();
		}

		private void excludeSelectedSitesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			Exclude_Selected_Sites_Cmd();
		}

		// Configuration Menu

		private void configurationMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
			string msg = "";
			string configpath = (string)menuItem.Tag;
			// Do not allow change if any data not published
			if (StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
			{
				msg = "Data has not been published.\n\n";
				msg += "Publish or clear data before changing configuration.";
				MessageBox.Show(this, msg, "Data Not Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (StateVars.Design_Control_Set)
			{
				msg = "Changing the configuration will reset the design setup.\n\n";
				msg += "Click OK to continue, or click Cancel.";
				DialogResult dr = MessageBox.Show(this, msg, "Reset Design Setup", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr != DialogResult.OK)
					return;
				MeasSetup.ResetDesign();
				StateVars.Design_Control_Set = false;
			}
			// Test if password is required
			if (ProgramSettings.ConfigurationPasswordRequired && ProgramSettings.ConfigurationPassword != null)
			{
				// Request password
				PasswordDialog pd = new PasswordDialog();

				if (pd.ShowDialog() == DialogResult.OK)
				{
					if (pd.EnteredPassword != ProgramSettings.ConfigurationPassword)
					{
						msg = "The password entered did not pass authentication";
						MessageBox.Show(this, msg, "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
				}
			}
			Activate_Configuration_Cmd(configpath);
		} // configurationMenuItem_Click()

		// Help Menu
		private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			AboutVFAACP about = new AboutVFAACP();
			about.TopMost = TopMost;
			about.ShowDialog();
		}

		#endregion

		#region User Commands

		private void Recover_From_Estop()
		{
			StopPollingAndEraseCrosshair();
			FileSystemFuncs.AppendToLogFile("Recover_From_Estop");
			if (StateVars.CP_Busy)
			{
				FileSystemFuncs.AppendToLogFile("CP_Busy");
				return;
			}
			string msg = "";
			msg += "Emergency Stop\n\n";
			msg += "To recover, the system must re-home the stage axes.\n\n";
			msg += "Ensure that the window and door are closed, and emergency stop buttons are released.\n\n";
			msg += "Click OK to recover or click Cancel to halt.";
			DialogResult dr = MessageBox.Show(this, msg, "Emergency Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
			if (dr != DialogResult.OK)
			{
				return;
			}
			Clear_All_Sites(displayWarning: false);
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpResetMotion());
		}   // Recover_From_Estop()

		private void Recover_From_Motion_Error()
		{
			StopPollingAndEraseCrosshair();
			FileSystemFuncs.AppendToLogFile("Recover_From_Motion_Error");
			if (StateVars.CP_Busy)
			{
				FileSystemFuncs.AppendToLogFile("CP_Busy");
				return;
			}
			string msg = "";
			msg += "A motion error occurred.\n\n";
			msg += "To recover, the system must re-home the stage axes.\n\n";
			msg += "Click OK to recover or click Cancel to halt.";
			if (MessageBox.Show(this, msg, "Motion Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) != DialogResult.OK)
			{
				StartPolling();
				return;
			}
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpRecoverFromMotionError());
		}   // Recover_From_Motion_Error()


		private void Refresh_Program_Settings_Cmd(string path)
		{
			string msg = ProgramSettings.ReadSettingsFromFile(path);
			if (!string.IsNullOrEmpty(msg))
			{
				// If an AutoPublishForPower error, show a warning and continue
				if (msg.Contains("AutoPublishForPowerDirectory"))
				{
					msg = "Error in program settings file " + path + "\n\n" + msg + "\n\n";
					msg += "The Automation Control Program can run but AutoPublishForPower will be disabled.";
					MessageBox.Show(this, msg, "VFAACP Program Settings Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					// If there were any other problems with the settings, the application cannot run.
					msg = "Error in program settings file " + path + "\n\n" + msg + "\n\n";
					msg += "The Automation Control Program cannot run until this problem is corrected.";
					throw new Exception(msg);
				}
			}
			string Title = "Zygo VeriFire Asphere Automation Control Program";

			// Add Instrument Name to the UI Header
			if (ProgramSettings.InstrumentName.Length > 0)
			{
				Title += " - " + ProgramSettings.InstrumentName;
			}
			Text = Title;

			VerificationParts.Reset();
		}   // Refresh_Program_Settings_Cmd()

		private void Refresh_Site_View_Cmd()
		{
			// Update SiteMap
			siteMapPanel.Invalidate();

			// Begin update SiteList
			siteBoxPanel.SuspendLayout();

			int indx = 0;
			int nIncl = 0;

			// Update state of SiteBox list to match state of SiteListInfo
			if (_siteListInfo.Sites != null)
			{
				foreach (Site s in _siteListInfo.Sites)
				{
					if (siteBoxPanel.Controls.Count == _siteListInfo.Sites.Count())
					{
						VFAACP.SiteBox sb = (VFAACP.SiteBox)siteBoxPanel.Controls[indx];
						sb.SiteIncluded = s.IsIncludedUserState;

						if (s.IsMeasuring) sb.State = SiteState.Measuring;
						if (s.IsMeasuredOK) sb.State = SiteState.MeasuredOK;
						if (s.IsMeasuredErr) sb.State = SiteState.MeasuredERR;
						if (!s.IsMeasuring && !s.IsMeasuredOK && !s.IsMeasuredErr)
							sb.State = SiteState.Idle;
						sb.SitePublished = s.IsPublished;
						sb.SiteResultText = s.ResultText;
						sb.SiteBitmap = s.ResultBitmap1;
						sb.SiteBitmap2 = s.ResultBitmap2;
						indx++;
					}

					if (s.IsIncludedUserState) nIncl++;
				}
			}

			StateVars.UpdateState();

			siteBoxPanel.ResumeLayout();
		}   // Refresh_Site_View_Cmd()

		private void Reset_Motion_Control_Cmd()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				Set_CP_Status("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return;
			}
			FileSystemFuncs.AppendToLogFile("Reset_Motion_Control_Cmd");
			StopPollingAndEraseCrosshair();
			Clear_All_Sites(displayWarning: false);
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpResetMotion());
		}

		private void Enable_Joystick_Cmd()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				Set_CP_Status("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return;
			}
			FileSystemFuncs.AppendToLogFile("Enable_Joystick_Cmd");
			StopPollingAndEraseCrosshair();
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpEnableJoystick());
		}

		private void Home_Tilt_Axes_Cmd()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				Set_CP_Status("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return;
			}
			FileSystemFuncs.AppendToLogFile("Home_Tilt_Axes_Cmd");
			StopPollingAndEraseCrosshair();
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpHomeTiltAxes());
		}

		private void Move_Tilt_Axes_Level_Cmd()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				Set_CP_Status("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return;
			}
			FileSystemFuncs.AppendToLogFile("Move_Tilt_Axes_Level_Cmd");
			StopPollingAndEraseCrosshair();
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpMoveTiltAxesLevel());
		}

		private void Show_Mx_State()
		{
			string text = "UNKNOWN";
			Color backColor = Color.Red;
			switch (StateVars.Mx_State)
			{
				case Mx_States.Connected:
					text = "CONNECTED";
					backColor = Color.Green;
					break;
				case Mx_States.Disconnected:
					text = "DISCONNECTED";
					backColor = Color.Red;
					break;
				case Mx_States.MStop:
					text = "MSTOP";
					backColor = Color.Red;
					break;
				case Mx_States.EStop:
					text = "ESTOP";
					backColor = Color.Red;
					break;
				case Mx_States.SafetyFault:
					text = "SAFETY FAULT";
					backColor = Color.Red;
					break;
				case Mx_States.StageNotHomed:
					text = "STAGE NOT HOMED";
					backColor = Color.Red;
					break;
			}
			toolStripStatusMxRemoteLabel.Text = text;
			toolStripStatusMxRemoteLabel.BackColor = backColor;
			toolStripStatusMxRemoteLabel.ForeColor = Color.White;
			FileSystemFuncs.AppendToLogFile("Mx State = " + text);
		}   // Show_Mx_State()

		private void Show_CP_State()
		{
			string text;
			Color backColor;
			switch (StateVars.CP_State)
			{
				case CP_States.AtLoadUnload:
					text = "LOAD/UNLOAD";
					backColor = Color.Green;
					break;
				case CP_States.Error:
					text = "ERROR";
					backColor = Color.Red;
					break;
				case CP_States.HomingStage:
					text = "HOMING STAGE";
					backColor = Color.Green;
					break;
				case CP_States.Ready:
				default:
					text = "READY";
					backColor = Color.Green;
					break;
				case CP_States.Setup:
					text = "SETUP";
					backColor = Color.Green;
					break;
				case CP_States.Measuring:
					text = "MEASURING";
					backColor = Color.Green;
					break;
				case CP_States.MovingToIdle:
				case CP_States.MovingToLoadUnload:
				case CP_States.MovingToSite:
					text = "MOVING";
					backColor = Color.Green;
					break;
				case CP_States.Publishing:
					text = "PUBLISHING";
					backColor = Color.Green;
					break;
				case CP_States.ResetMotion:
					text = "BUSY";
					backColor = Color.Green;
					break;
				case CP_States.StartingMx:
					text = "STARTING MX";
					backColor = Color.Red;
					break;
				case CP_States.WaitingForStop:
					text = "BUSY";
					backColor = Color.Green;
					break;
			}
			toolStripStatusCPStateLabel.Text = text;
			toolStripStatusCPStateLabel.BackColor = backColor;
			toolStripStatusCPStateLabel.ForeColor = Color.White;
			FileSystemFuncs.AppendToLogFile("CP State = " + text);
		}   // Show_Async_State()

		public static bool Browse_For_Tray_File(out string siteCoordsFile)
		{
			siteCoordsFile = null;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".csv";
			ofd.Filter = "Tray Files (*.csv)|*.csv";
			if (_lastDirTrayFiles == "")
				_lastDirTrayFiles = ProgramSettings.TrayFileDirectory;
			ofd.InitialDirectory = _lastDirTrayFiles;
			ofd.Multiselect = false;
			ofd.Title = "Select a Tray File";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				siteCoordsFile = ofd.FileName;
				_lastDirTrayFiles = Path.GetDirectoryName(siteCoordsFile);
				return true;
			}
			return false;
		}

		private void Read_Tray_File(string path)
		{
			string msg;
			msg = "Reading tray file " + path;
			FileSystemFuncs.AppendToLogFile("Read_Tray_File: " + msg);
			List<Site> siteList = null;
			try
			{
				siteList = SiteCoordFile.Read(path);
			}
			catch (Exception ex)
			{
				Set_CP_Status(ex.Message);
				MessageBox.Show(this, ex.Message, "Tray File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			_siteListInfo.Reset();
			_siteListInfo.NewSiteList(siteList);
			ResetSiteBoxDisplay();
			siteMapPanel.Invalidate();

			StateVars.Tray_Control_Set = true;
			MeasSetup.TrayFile = path;
			MeasSetup.TrayName = Path.GetFileNameWithoutExtension(path);

			FillSiteBoxDisplay();

			int n = 0;
			msg = "Tray file " + MeasSetup.TrayName + ": ";
			n = _siteListInfo.NumSites;
			msg += n.ToString() + " site";
			if (n > 1)
				msg += "s";
			msg += " defined, ";
			n = _siteListInfo.NumSitesIncluded;
			msg += n.ToString() + " site";
			if (n > 1)
				msg += "s";
			msg += " included";
			n = _siteListInfo.NumCalibrationSitesIncluded;
			if (n > 0)
			{
				msg += ", " + n.ToString() + " calibration site";
				if (n > 1)
					msg += "s";
				msg += " included";
			}
			Set_CP_Status(msg);

			siteBoxPanel.Focus();
		}   // Read_Tray_File()

		private void btnSelectTray_Click(object sender, EventArgs e)
		{
			StopPollingAndEraseCrosshair();
			Clear_CP_Status();
			Clear_Mx_Status();
			if (StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
			{
				string msg = "";
				msg += "Data has not been published.\n\n";
				msg += "Publish or clear data before selecting a new tray.";
				MessageBox.Show(msg, "Data Not Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
				StartPolling();
				return;
			}
			string path;
			if (!Browse_For_Tray_File(out path))
			{
				StartPolling();
				return;
			}
			Read_Tray_File(path);
			StartPolling();
		}

		private void Clear_Design_Info()
		{
			FileSystemFuncs.AppendToLogFile("Clear_Design_Info");
			Clear_CP_Status();
			Clear_Mx_Status();
			MeasSetup.ResetDesign();
			StateVars.Design_Control_Set = false;
		}

		private bool Check_External_Database_File_Before_Clear_Data()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (File.Exists(path))
			{
				if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "The file must be uploaded to the factory system\n";
					msg += "before clearing data.";
					MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "Clearing data will delete the file.\n\n";
					msg += "Click OK to continue and delete the file or click Cancel to stop.";
					DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr == DialogResult.OK)
					{
						FileSystemFuncs.DeleteExternalDatabaseFile();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return true;
		} // Check_External_Database_File_Before_Clear_Data()

		private bool Check_External_Database_File_Before_Load_Tray()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (File.Exists(path))
			{
				if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "The file must be uploaded to the factory system\n";
					msg += "before changing trays.";
					MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "Changing trays will delete the file.\n\n";
					msg += "Click OK to continue and delete the file or click Cancel to stop.";
					DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr == DialogResult.OK)
					{
						FileSystemFuncs.DeleteExternalDatabaseFile();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return true;
		} // Check_External_Database_File_Before_Load_Tray()

		private bool Check_External_Database_File_Before_Setup()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (File.Exists(path))
			{
				if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "The file must be uploaded to the factory system\n";
					msg += "before setting up a new measurement.";
					MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "Setting up for a new measurement will delete the file.\n\n";
					msg += "Click OK to continue and delete the file or click Cancel to stop.";
					DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr == DialogResult.OK)
					{
						FileSystemFuncs.DeleteExternalDatabaseFile();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return true;
		} // Check_External_Database_File_Before_Setup()

		private bool Check_External_Database_File_Before_Measure()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (File.Exists(path))
			{
				if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "The file must be uploaded to the factory system\n";
					msg += "before performing new measurements.";
					MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "Performing new measurements will delete the file.\n\n";
					msg += "Click OK to continue and delete the file or click Cancel to stop.";
					DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr == DialogResult.OK)
					{
						FileSystemFuncs.DeleteExternalDatabaseFile();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return true;
		} // Check_External_Database_File_Before_Measure()

		private bool Check_External_Database_File_Before_Publish()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (File.Exists(path))
			{
				if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "The file must be uploaded to the factory system\n";
					msg += "before publishing data.";
					MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				else
				{
					string msg = "The external database XML file exists.\n\n";
					msg += "Publishing data will delete the file.\n\n";
					msg += "Click OK to continue and delete the file or click Cancel to stop.";
					DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (dr == DialogResult.OK)
					{
						FileSystemFuncs.DeleteExternalDatabaseFile();
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return true;
		} // Check_External_Database_File_Before_Publish()

		private bool Check_External_Database_File_Before_Exit()
		{
			string path = FileSystemFuncs.ExternalDatabaseFilePath;
			if (!File.Exists(path))
				return true;
			bool wasTopMost = TopMost;
			if (ProgramSettings.ActiveConfiguration.ForceExternalDatabaseFileUpload == 1)
			{
				if (!Debugger.IsAttached)
					TopMost = true;
				string msg = "The external database XML file exists.\n\n";
				msg += "The file must be uploaded to the factory system\n";
				msg += "before exiting the program.";
				MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				TopMost = wasTopMost;
				return false;
			}
			else
			{
				if (!Debugger.IsAttached)
					TopMost = true;
				string msg = "The external database XML file exists.\n\n";
				msg += "Exiting the program will delete the file.\n\n";
				msg += "Click OK to continue and delete the file or click Cancel to stop.";
				DialogResult dr = MessageBox.Show(this, msg, "External Database File Exists", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
				TopMost = wasTopMost;
				if (dr == DialogResult.OK)
				{
					FileSystemFuncs.DeleteExternalDatabaseFile();
					return true;
				}
				else
				{
					return false;
				}
			}
		} // Check_External_Database_File_Before_Exit()

		private void Load_Unload_Cmd()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				Set_CP_Status("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return;
			}
			StopPollingAndEraseCrosshair();
			Clear_CP_Status();
			Clear_Mx_Status();
			ConfigurationFile config = ProgramSettings.ActiveConfiguration;
			if (config.LoadTrayResetsData == 1)
			{
				bool ok;
				ok = Check_External_Database_File_Before_Load_Tray();
				if (!ok)
				{
					StartPolling();
					return;
				}
				if (StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
				{
					string msg = "Measured data exists that has not been published.\n\n";
					msg += "Do you want to change trays without publishing the data?";
					DialogResult dr = MessageBox.Show(this, msg, "Data Not Published", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
					if (dr == DialogResult.Cancel)
					{
						StartPolling();
						return;
					}
				}
				ok = Clear_All_Sites(displayWarning: false);
				if (!ok)
				{
					StartPolling();
					return;
				}
			}
			if (config.LoadTrayResetsDesign == 1)
			{
    			Clear_Design_Info();
			}
			if (ProgramSettings.LoadTrayCountdown_sec > 10)
			{
				Perform_Legacy_Load_Unload();
			}
			else
			{
				AsyncMgr asyncMgr = AsyncMgr.Instance();
				asyncMgr.PerformAsyncOp(new AsyncOpLoadUnload());
			}
		}   // Load_Unload_Cmd()

		private void Perform_Legacy_Load_Unload()
		{
			StopPollingAndEraseCrosshair();
			LoadTimer dlg = new LoadTimer(ProgramSettings.LoadTrayCountdown_sec);
			while (true)
			{
				AsyncOpStatus status = null;
				StateVars.CP_State = CP_States.MovingToLoadUnload;
				Set_CP_Status("Moving to Load/Unload position");
				Application.DoEvents();
				status = RunLoadTrayScript();
				if (status != null)
					break;
				StateVars.CP_State = CP_States.AtLoadUnload;
				Set_CP_Status("Moved to Load/Unload position");
				Application.DoEvents();
				DialogResult dr = dlg.ShowDialog();
				StateVars.CP_State = CP_States.MovingToIdle;
				Set_CP_Status("Moving to Idle position");
				Application.DoEvents();
				status = RunRetractTrayScript();
				if (status != null)
					break;
				StateVars.CP_State = CP_States.Ready;
				Set_CP_Status("Moved to Idle position", 500);
				if (dr != DialogResult.Retry)
					break;
			}
			StartPolling();
		}

		private AsyncOpStatus RunLoadTrayScript()
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			AsyncOpStatus status = null;
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				status = asyncMgr.RunScript(ProgramSettings.LoadTrayScript);
			}
			else
			{
				Thread.Sleep(ProgramSettings.TestWithoutInstrumentMoveTime_msec);
			}
			if ((status != null) && status.HasError)
			{
				ReportError("Error running script " + ProgramSettings.LoadTrayScript + ": " + status.ErrorMessage, showDialog: status.ShowDialog);
			}
			return status;
		}

		private AsyncOpStatus RunRetractTrayScript()
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			AsyncOpStatus status = null;
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				status = asyncMgr.RunScript(ProgramSettings.RetractTrayScript);
			}
			else
			{
				Thread.Sleep(ProgramSettings.TestWithoutInstrumentMoveTime_msec);
			}
			if ((status != null) && status.HasError)
			{
				ReportError("Error running script " + ProgramSettings.RetractTrayScript + ": " + status.ErrorMessage, showDialog: status.ShowDialog);
			}
			return status;
		}

		private bool ValidateTrayForRecipe()
		{
			string msg = "";
			int numSitesIncluded = _siteListInfo.NumSitesIncluded;
			if (numSitesIncluded == 0)
			{
				msg = "There are no included tray sites.";
				MessageBox.Show(this, msg, "No Included Sites", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			int numCalibrationSitesIncluded = _siteListInfo.NumCalibrationSitesIncluded;
			if (numCalibrationSitesIncluded > 1)
			{
				msg = "Tray must not include more than one calibration site.";
				MessageBox.Show(this, msg, "More Than One Calibration Site Included", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			Recipe recipe = MeasSetup.CurrentRecipe;

			// Any calibration site(s) will be ignored.
			// If the included sites are calibration sites, there is nothing to do.
			if (numSitesIncluded == numCalibrationSitesIncluded)
			{
				msg = "There are no included design sites.\n\n";
				msg += "This design does not require a calibration site.";
				MessageBox.Show(this, msg, "No Included Design Sites", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}	// ValidateTrayForRecipe()

		private void Measure_Tray_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Measure_Tray_Cmd");
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			string msg = null;
			DialogResult dr;
			bool ok;
			ok = Check_External_Database_File_Before_Measure();
			if (!ok)
			{
				StartPolling();
				return;
			}

			int initialSite = 1;
			if (StateVars.Measure_Tray_Incomplete)
			{
				initialSite = _siteListInfo.LastSiteMeasuredOK + 1;
				if (StateVars.Included_Site_Data_Not_Published(initialSite, _siteListInfo.Sites))
				{
					if (ProgramSettings.ActiveConfiguration.DisallowRemeasureUnpublishedSite == 1)
					{
						msg = "Measured data exists at the remeasure site that\n";
						msg += "has not been published.\n\n";
						msg += "Data must be published before measuring.";
						MessageBox.Show(this, msg, "Resuming Measure Included Sites - Data Not Published", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						StartPolling();
						return;
					}
					else
					{
						msg = "Measured data exists at the remeasure site that\n";
						msg += "has not been published.\n\n";
						msg += "The data will be overwritten.\n\n";
						msg += "Do you want to continue?";
						dr = MessageBox.Show(this, msg, "Resuming Measure Included Sites - Confirm Overwrite Measured Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
						if (dr == DialogResult.Cancel)
						{
							StartPolling();
							return;
						}
					}
				}
			}
			else
			{
				initialSite = 1;
				if (StateVars.Any_Included_Data_Not_Published(_siteListInfo.Sites))
				{
					if (ProgramSettings.ActiveConfiguration.DisallowRemeasureUnpublishedSite == 1)
					{
						msg = "Measured data exists for one or more included sites that\n";
						msg += "has not been published.\n\n";
						msg += "Data must be published before measuring.";
						MessageBox.Show(this, msg, "Measure Included Sites - Data Not Published", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						StartPolling();
						return;
					}
					else
					{
						msg = "Measured data exists for one or more included sites that\n";
						msg += "has not been published.\n\n";
						msg += "The data will be overwritten.\n\n";
						msg += "Do you want to continue?";
						dr = MessageBox.Show(this, msg, "Measure Included Sites - Confirm Overwrite Measured Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
						if (dr == DialogResult.Cancel)
						{
							StartPolling();
							return;
						}
					}
				}
			}

			if (ValidateTrayForRecipe() == false)
			{
				StartPolling();
				return;
			}
			
			AsyncOpMeasureSites ms = new AsyncOpMeasureSites();
			ms.SiteList = _siteListInfo.Sites;
			ms.AllowStageAdj = false;
			ms.RemeasureSite = 0;
			ms.InitialSite = initialSite; // TODO KK
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(ms);
		}   // Measure_Tray_Cmd()

		private void Measure_Publish_Repeat_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Measure_Publish_Repeat_Cmd");
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			MeasurePublishRepeatDialog dlg = new MeasurePublishRepeatDialog();
			dlg.TopMost = true;
			bool wasTopMost = TopMost;
			if (!Debugger.IsAttached)
				TopMost = true;
			DialogResult dr = dlg.ShowDialog();
			TopMost = wasTopMost;
			if (dr != DialogResult.OK)
			{
				StartPolling();
				return;
			}
			if (ValidateTrayForRecipe() == false)
			{
				StartPolling();
				return;
			}
			List<Site> siteList = _siteListInfo.Sites;
			if (siteList == null)
			{
				StartPolling();
				return;
			}
			AsyncOpMeasurePublishRepeat mpr = new AsyncOpMeasurePublishRepeat();
			mpr.SiteList = siteList;
			mpr.NumIterations = dlg.NumIterations;
			mpr.PublishFullData = dlg.PublishFullData;
			mpr.PublishSummaryCsvFile = dlg.PublishSummaryCsvFile;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(mpr);
		}	// Measure_Publish_Repeat_Cmd()

		private void Measure_Site_Cmd(int siteNum)
		{
			FileSystemFuncs.AppendToLogFile("Measure_Site_Cmd: " + siteNum.ToString());
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			bool ok;
			ok = Check_External_Database_File_Before_Measure();
			if (!ok)
			{
				StartPolling();
				return;
			}
			if (StateVars.Included_Site_Data_Not_Published(siteNum, _siteListInfo.Sites))
			{
				string msg = "Measured data for site " + siteNum.ToString() + " already exists.\n\n";
				msg += "The data will be overwritten.\n\n";
				msg += "Do you want to continue?";
				if (MessageBox.Show(this, msg, "Confirm Overwrite Measured Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
				{
					StartPolling();
					return;
				}
			}
			AsyncOpMeasureSites ms = new AsyncOpMeasureSites();
			ms.SiteList = _siteListInfo.Sites;
			ms.AllowStageAdj = true;
			ms.RemeasureSite = siteNum;
			ms.InitialSite = siteNum; // TODO KK
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(ms);
		}   // Measure_Site_Cmd()

		private void Move_To_Site_Cmd(int siteNum)
		{
			FileSystemFuncs.AppendToLogFile("Move_To_Site_Cmd: " + siteNum.ToString());
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			AsyncOpMoveToSite mts = new AsyncOpMoveToSite();
			mts.DestinationSite = _siteListInfo.Sites[siteNum - 1];
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(mts);
		}   // Move_To_Site_Cmd()

		private void Get_New_Coords_For_Site_Cmd(int siteNum)
		{
			FileSystemFuncs.AppendToLogFile("Get_New_Coords_For_Site_Cmd: " + siteNum.ToString());
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			AsyncOpNewCoordsForSite ncfs = new AsyncOpNewCoordsForSite();
			ncfs.SiteNum = siteNum;
			ncfs.Prompt = true;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(ncfs);
		}   // Get_New_Coords_For_Site_Cmd()

		private void Store_New_Coords_For_Site(int siteNum, StageCoords newCoords, bool prompt)
		{
			double deltaXYLimit_mm = Math.Abs(ProgramSettings.DeltaXYLimit_mm);
			double deltaZLimit_mm = Math.Abs(ProgramSettings.DeltaZLimit_mm);
			double deltaRPLimit_deg = Math.Abs(ProgramSettings.DeltaRPLimit_deg);
			Site site = _siteListInfo.Sites[siteNum - 1];
			Recipe recipe = MeasSetup.CurrentRecipe;
			StageCoords oldCoords = site.GetAbsoluteCoords(recipe);
			if (ProgramSettings.TestWithoutInstrument == 1)
			{   // Ignore passed values
				newCoords = oldCoords;
			}
			else if (ProgramSettings.TestWithoutInstrument == 2)
			{   // The passed values are random numbers in the range (-1,+1).
				// Use those to calculate offsets.
				newCoords.X_mm = oldCoords.X_mm + (newCoords.X_mm * deltaXYLimit_mm);
				newCoords.Y_mm = oldCoords.Y_mm + (newCoords.Y_mm * deltaXYLimit_mm);
				newCoords.Z_mm = oldCoords.Z_mm + (newCoords.Z_mm * deltaZLimit_mm);
				newCoords.R_deg = oldCoords.R_deg + (newCoords.R_deg * deltaRPLimit_deg);
				newCoords.P_deg = oldCoords.P_deg + (newCoords.P_deg * deltaRPLimit_deg);
			}
			double deltaX_mm = newCoords.X_mm - oldCoords.X_mm;
			double deltaY_mm = newCoords.Y_mm - oldCoords.Y_mm;
			double deltaZ_mm = newCoords.Z_mm - oldCoords.Z_mm;
			double deltaR_deg = newCoords.R_deg - oldCoords.R_deg;
			double deltaP_deg = newCoords.P_deg - oldCoords.P_deg;
			if (ProgramSettings.Store_Z == 0)
			{
				deltaZ_mm = 0.0;
			}
			if (ProgramSettings.Store_RP == 0)
			{
				deltaR_deg = 0.0;
				deltaP_deg = 0.0;
			}
			if (prompt)
			{
				string title = "Store New Coordinates for Site";
				string msg = "Do you want to store new X/Y coordinates for site " + siteNum.ToString() + "?\n\n";
				System.Windows.Forms.MessageBoxIcon icon = MessageBoxIcon.Question;
				if ((Math.Abs(deltaX_mm) > deltaXYLimit_mm) || (Math.Abs(deltaY_mm) > deltaXYLimit_mm))
				{
					msg += "Delta X or Y is greater than " + deltaXYLimit_mm.ToString("F0") + " mm.\n\n";
					icon = MessageBoxIcon.Exclamation;
				}
				if ((ProgramSettings.Store_Z == 1) && (Math.Abs(deltaZ_mm) > deltaZLimit_mm))
				{
					msg += "Delta Z is greater than " + deltaZLimit_mm.ToString("F0") + " mm.\n\n";
					icon = MessageBoxIcon.Exclamation;
				}
				if ((ProgramSettings.Store_RP == 1) && ((Math.Abs(deltaR_deg) > deltaRPLimit_deg) || (Math.Abs(deltaP_deg) > deltaRPLimit_deg)))
				{
					msg += "Delta R or P is greater than " + deltaRPLimit_deg.ToString("F0") + " deg.\n\n";
					icon = MessageBoxIcon.Exclamation;
				}
				msg += "          New X   = " + newCoords.X_mm.ToString("F4") + " mm\n";
				msg += "          Old X   = " + oldCoords.X_mm.ToString("F4") + " mm\n";
				msg += "          Delta X = " + deltaX_mm.ToString("F4") + " mm\n\n";
				msg += "          New Y   = " + newCoords.Y_mm.ToString("F4") + " mm\n";
				msg += "          Old Y   = " + oldCoords.Y_mm.ToString("F4") + " mm\n";
				msg += "          Delta Y = " + deltaY_mm.ToString("F4") + " mm\n\n";
				if (ProgramSettings.Store_Z == 1)
				{
					msg += "          New Z   = " + newCoords.Z_mm.ToString("F4") + " mm\n";
					msg += "          Old Z   = " + oldCoords.Z_mm.ToString("F4") + " mm\n";
					msg += "          Delta Z = " + deltaZ_mm.ToString("F4") + " mm\n\n";
				}
				if (ProgramSettings.Store_RP == 1)
				{
					msg += "          New R   = " + newCoords.R_deg.ToString("F4") + " deg\n";
					msg += "          Old R   = " + oldCoords.R_deg.ToString("F4") + " deg\n";
					msg += "          Delta R = " + deltaR_deg.ToString("F4") + " deg\n\n";
					msg += "          New P   = " + newCoords.P_deg.ToString("F4") + " deg\n";
					msg += "          Old P   = " + oldCoords.P_deg.ToString("F4") + " deg\n";
					msg += "          Delta P = " + deltaP_deg.ToString("F4") + " deg\n\n";
				}
				if (MessageBox.Show(this, msg, title, MessageBoxButtons.YesNo, icon) != DialogResult.Yes)
				{
					return;
				}
			}
			site.ApplyDeltaStageCoords(deltaX_mm, deltaY_mm, deltaZ_mm, deltaR_deg, deltaP_deg);
			site.IsModified = true;
			site.SiteBox.HighlightStoreButton = true;
			_siteListInfo.UpdateMap();
		}   // Store_New_Coords_For_Site()

		private void Apply_Tray_Level_Coords(double newR, double newP)
		{
			string title = "Update Tray Level Coordinates";
			double X, Y, Z, oldR, oldP;
			double deltaR, deltaP;
			DialogResult dr;
			string errmsg = null;
			string msg = null;
			errmsg = FileSystemFuncs.ReadSystemLoadPosFile(out X, out Y, out Z, out oldR, out oldP);
			if (errmsg != null)
			{
				MessageBox.Show(this, errmsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			deltaR = newR - oldR;
			deltaP = newP - oldP;
			msg = "";
			msg += "New tray level coordinates:\n\n";
			msg += "          New R   = " + newR.ToString("F4") + " deg\n";
			msg += "          Old R   = " + oldR.ToString("F4") + " deg\n";
			msg += "          Delta R = " + deltaR.ToString("F4") + " deg\n\n";
			msg += "          New P   = " + newP.ToString("F4") + " deg\n";
			msg += "          Old P   = " + oldP.ToString("F4") + " deg\n";
			msg += "          Delta P = " + deltaP.ToString("F4") + " deg\n\n";
			msg += "Click OK to save the new values, or click Cancel.";
			dr = MessageBox.Show(this, msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (dr != DialogResult.OK)
			{
				return;
			}
			FileSystemFuncs.AppendToLogFile("Update Tray Level Coordinates");

			errmsg = FileSystemFuncs.WriteSystemLoadPosFile(X, Y, Z, newR, newP);
			if (errmsg != null)
			{
				MessageBox.Show(this, errmsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ProgramSettings.RCoord_deg = newR;
			ProgramSettings.PCoord_deg = newP;
			errmsg = ProgramSettings.WriteSettingsToFile(Program.SettingsFile);
			if (errmsg != null)
			{
				MessageBox.Show(this, errmsg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				Set_CP_Status("Tray level coordinates updated");
			}
		} // Apply_Tray_Level_Coords()

		private void Apply_Catseye_Offset(double curX, double curY, double curZ)
		{
			List<Site> sites = _siteListInfo.Sites;
			if (sites == null)
			{
				return;
			}
			Recipe recipe = MeasSetup.CurrentRecipe;
			if (recipe == null)
			{
				return;
			}
			string title;
			string msg;
			// Look for a site near (curX,curY)
			Site curSite = null;
			foreach (Site s in sites)
			{
				if (s.NearXY(curX, curY))
				{
					curSite = s;
					break;
				}
			}
			if (curSite == null)
			{
				title = "Set Catseye Offset Error";
				msg = " No site matches the current X/Y coordinates. ";
				MessageBox.Show(this, msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				{
					return;
				}
			}
			double newZ = curZ - ProgramSettings.ZCoord_mm - recipe.ZCoord_mm;
			double oldZ = curSite.Z_Offset;
			if (curSite.IsCalibrationSite)
			{
				title = "Store New Z Coordinate for Calibration Site";
				msg = String.Format("Do you want to store a new Z coordinate for calibration site {0}C?\n\n", curSite.Index);
			}
			else
			{
				title = "Store New Z Coordinate for Measurement Sites";
				msg = "Do you want to store a new Z coordinate for all measurement sites?\n\n";
			}
			double deltaZ_mm = newZ - oldZ;
			const double deltaLim = 3.0;
			System.Windows.Forms.MessageBoxIcon icon;
			if (Math.Abs(deltaZ_mm) > deltaLim)
			{
				msg += "Delta Z is greater than " + deltaLim.ToString("F0") + " mm.\n\n";
				icon = MessageBoxIcon.Exclamation;
			}
			else
			{
				icon = MessageBoxIcon.Question;
			}
			msg += "          New Z = " + newZ.ToString("F4") + " mm\n";
			msg += "          Old Z = " + oldZ.ToString("F4") + " mm\n";
			msg += "          Delta Z = " + deltaZ_mm.ToString("F4") + " mm\n\n";
			if (MessageBox.Show(this, msg, title, MessageBoxButtons.OKCancel, icon) == DialogResult.Cancel)
			{
				return;
			}
			if (curSite.IsCalibrationSite)
			{
				curSite.Z_Offset = newZ;
				curSite.IsModified = true;
				curSite.SiteBox.HighlightStoreButton = true;
			}
			else
			{
				foreach (Site s in _siteListInfo.Sites)
				{
					if (!s.IsCalibrationSite)
					{
						s.Z_Offset = newZ;
						s.IsModified = true;
						s.SiteBox.HighlightStoreButton = true;
					}
				}
			}
			Set_CP_Status("Set catseye offset done");
		}	// Apply_Catseye_Offset()

		private void Publish_Data_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Publish_Data_Cmd");
			Clear_CP_Status();
			Clear_Mx_Status();
 			StopPollingAndEraseCrosshair();
			Recipe recipe = MeasSetup.CurrentRecipe;
			if (recipe == null)
			{
				StartPolling();
				return;
			}
			if (!Check_External_Database_File_Before_Publish())
			{
				StartPolling();
				return;
			}

			// Setup the Publish dialog
			PublishDialog dlg = new PublishDialog();
			DateTime dt = DateTime.Now;
			string rootFolder = null;
			if (MeasSetup.IsToolingCalibrationSetup)
			{
				if (_lastDirToolingCalPublishFileRoot == "")
					_lastDirToolingCalPublishFileRoot = ProgramSettings.ToolingCalPublishFileRootDirectory;
				rootFolder = _lastDirToolingCalPublishFileRoot;
			}
			else
			{
				if (_lastDirPublishFileRoot == "")
					_lastDirPublishFileRoot = ProgramSettings.PublishFileRootDirectory;
				rootFolder = _lastDirPublishFileRoot;
			}
			string subFolder = MeasSetup.PublishName + "_" + dt.ToString(ProgramSettings.PublishFolderDateTimeFormat).ToUpper();
			dlg.PublishRootFolder = rootFolder;
			dlg.PublishSubFolder = subFolder;

			int numSites = 0;
			int numSitesToPublish = 0;
			List<Site> siteList = _siteListInfo.Sites;
			if (siteList != null)
			{
				numSites = siteList.Count;
				foreach (Site s in siteList)
				{
					if (s.IsIncludedUserState && !s.IsCalibrationSite && (s.IsMeasuredOK || s.IsMeasuredErr))
						++numSitesToPublish;
				}
			}
			dlg.NumSitesToPublishLabel = numSitesToPublish.ToString() + " of " + numSites.ToString() + " Sites will be Published";
			bool wasTopMost = TopMost;
			dlg.TopMost = true;
			if (!Debugger.IsAttached)
				TopMost = true;
			// Show the Publish dialog
			DialogResult dr = dlg.ShowDialog();
			TopMost = wasTopMost;
			if (dr != DialogResult.OK)
			{
				StartPolling();
				return;
			}
			if ((siteList == null) || (numSitesToPublish == 0))
			{
				StartPolling();
				return;
			}
			if (MeasSetup.IsToolingCalibrationSetup)
			{
				_lastDirToolingCalPublishFileRoot = dlg.PublishRootFolder;
			}
			else
			{
				_lastDirPublishFileRoot = dlg.PublishRootFolder;
			}

			AsyncOpPublishFiles aopf = new AsyncOpPublishFiles();
			aopf.SiteList = siteList;
			aopf.PublishFullData = dlg.PublishFullData;
			aopf.PublishSummaryCsvFile = dlg.PublishSummaryCsvFile;
			aopf.PublishToExtDatabase = dlg.PublishToExtDatabase;
			aopf.PublishToPower = ((ProgramSettings.ActiveConfiguration.AutoPublishForPower == 1)
								&& (ProgramSettings.AutoPublishForPowerDirectory != null));
			aopf.FileFilter = recipe.PublishFilesGroup;
			aopf.DestinationFolder = dlg.PublishPath;
			aopf.RaisePublishDoneEvent = true;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(aopf);
		}   // Publish_Data_Cmd()

		private void Publish_Response_Cmd()
		{
			MessageBox.Show(this, "Publish Data Complete", "Publish Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
			ConfigurationFile config = ProgramSettings.ActiveConfiguration;
			if (config.ResetDataAfterPublish == 1)
			{
				bool ok = Clear_All_Sites(displayWarning: false);
				if (!ok)
				{
					return;
				}
			}
			if (config.ResetDesignAndTrayAfterPublish == 1)
			{
				MeasSetup.ResetDesign();
				MeasSetup.ResetTray();
				_siteListInfo.Reset();
				StateVars.Design_Control_Set = false;
				StateVars.Tray_Control_Set = false;
			}
		} // Publish_Response_Cmd()

		private void Stop_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Stop_Cmd");
			Set_CP_Status("Stop requested");
			Clear_Mx_Status();
			StateVars.Waiting_For_Stop = true;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.RequestStop();
		}

		private void Abort_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Abort_Cmd");
			Set_CP_Status("Abort requested");
			Clear_Mx_Status();
			StateVars.Waiting_For_Stop = true;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.RequestAbort();
		}

		private void Print_Screen_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Print_Screen_Cmd");
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			PrintScreenDialog dlg = new PrintScreenDialog(this);
			bool wasTopMost = TopMost;
			dlg.TopMost = true;
			if (!Debugger.IsAttached)
				TopMost = true;
			dlg.ShowDialog();
			TopMost = wasTopMost;
			StartPolling();
		}

		private void Print_Report_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Print_Report_Cmd");
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			if (_siteListInfo.NumSites == 0)
			{
				StartPolling();
				return;
			}
			PrintTrayReportDialog dlg = new PrintTrayReportDialog(this, _siteListInfo, MeasSetup.PresetComment);
			bool wasTopMost = TopMost;
			dlg.TopMost = true;
			if (!Debugger.IsAttached)
				TopMost = true;
			dlg.ShowDialog();
			TopMost = wasTopMost;
			StartPolling();
		}

		private void Live_Display_Cmd(bool close_then_open)
		{
			FileSystemFuncs.AppendToLogFile("Live_Display_Cmd");
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.ShowLiveDisplay(close_then_open);
			Clear_CP_Status();
			Clear_Mx_Status();
		}

		private void Include_All_Sites_Cmd()
		{
			if (!StateVars.Tray_Control_Set || StateVars.CP_Busy)
				return;
			foreach (Site s in _siteListInfo.Sites)
			{
				s.IsIncludedUserState = true;
				s.IsSelected = false;
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Included all sites");
		}   // Include_All_Sites_Cmd()

		private void Include_N_Sites_Cmd(int N)
		{
			if (!StateVars.Tray_Control_Set || StateVars.CP_Busy)
				return;
			foreach (Site s in _siteListInfo.Sites)
			{
				if (s.Index <= N)
					s.IsIncludedUserState = true;
				else
					s.IsIncludedUserState = false;
				s.IsSelected = false;
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Included sites 1-" + N.ToString());
		}   // Include_N_Sites_Cmd()

		private void Exclude_All_Sites_Cmd()
		{
			if (!StateVars.Tray_Control_Set || StateVars.CP_Busy)
				return;
			foreach (Site s in _siteListInfo.Sites)
			{
				s.IsIncludedUserState = false;
				s.IsSelected = false;
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Excluded all sites");
		}   // Exclude_All_Sites_Cmd()

		private void Include_Selected_Sites_Cmd()
		{
			int N = 0;
			foreach (Site s in _siteListInfo.Sites)
			{
				if (s.IsSelected)
				{
					s.IsIncludedUserState = true;
					++N;
				}
				s.IsSelected = false;
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Included " + N.ToString() + ((N > 1) ? " sites" : " site"));
		}   // Include_Selected_Sites_Cmd()

		private void Exclude_Selected_Sites_Cmd()
		{
			int N = 0;
			foreach (Site s in _siteListInfo.Sites)
			{
				if (s.IsSelected)
				{
					s.IsIncludedUserState = false;
					++N;
				}
				s.IsSelected = false;
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Excluded " + N.ToString() + ((N > 1) ? " sites" : " site"));
		}   // Exclude_Selected_Sites_Cmd()

		private bool Clear_All_Sites(bool displayWarning = false)
		{
			if (displayWarning && StateVars.Any_Data_Not_Published(_siteListInfo.Sites))
			{
				string msg = "Measured data exists that has not been published.\n\n";
				msg += "Do you want to clear the data without publishing?";
				DialogResult dr = MessageBox.Show(this, msg, "Confirm Clear Data For All Sites", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					return false;
				}
			}
			string ret = FileSystemFuncs.DeleteDirContents(ProgramSettings.InterimFileRootDirectory);
			if (!string.IsNullOrEmpty(ret))
			{
				Set_CP_Status("File System Error: " + ret);
				return false;
			}

			// Set all sites status to unmeasured state
			if (_siteListInfo.Sites != null)
			{
				foreach (Site s in _siteListInfo.Sites)
				{
					s.IsMeasuredOK = false;
					s.IsMeasuredErr = false;
					s.ResultBitmap1 = null;
					s.ResultBitmap2 = null;
					s.ResultText = "";
					s.IsPublished = false;
				}
				// If measure tray was incomplete, reset to initial state
				StateVars.Measure_Tray_Incomplete = false;
				_siteListInfo.LastSiteMeasuredOK = -1;
				Refresh_Site_View_Cmd();
			}

			Set_CP_Status("Cleared all site data");
			StateVars.UpdateState();
			return true;
		}   // Clear_All_Sites()

		private void Clear_All_Sites_Cmd()
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			if (Check_External_Database_File_Before_Clear_Data())
				Clear_All_Sites(displayWarning: true);
			StartPolling();
		}

		private void Clear_Selected_Sites_Cmd()
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			if (!Check_External_Database_File_Before_Clear_Data())
			{
				StartPolling();
				return;
			}

			if (StateVars.Any_Selected_Data_Not_Published(_siteListInfo.Sites))
			{
				string msg = "Measured data exists for one or more selected sites that has not been published.\n\n";
				msg += "Do you want to clear the data without publishing?";
				if (MessageBox.Show(this, msg, "Confirm Clear Data For Selected Sites", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
				{
					StartPolling();
					return;
				}
			}

			if (_siteListInfo.Sites != null)
			{
				int siteNum = 0;
				foreach (Site s in _siteListInfo.Sites)
				{
					siteNum++;

					if (s.IsSelected)
					{
						FileSystemFuncs.ClearInterimSiteData(siteNum);
						s.IsMeasuredOK = false;
						s.IsMeasuredErr = false;
						s.ResultBitmap1 = null;
						s.ResultBitmap2 = null;
						s.ResultText = "";
						s.IsPublished = false;
					}
				}
			}
			Refresh_Site_View_Cmd();
			Set_CP_Status("Cleared selected site data");
			StartPolling();
		}   // Clear_Selected_Sites_Cmd()

		private void Save_Tray_File_Cmd()
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".csv";
			sfd.Filter = "Tray Files (*.csv)|*.csv|All files (*.*)|*.*";
			sfd.InitialDirectory = ProgramSettings.TrayFileDirectory;
			sfd.Title = "Select a Tray File to Save";

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				FileSystemFuncs.AppendToLogFile("Save_Tray_File_Cmd:" + sfd.FileName);
				try
				{
					SiteCoordFile.Write(_siteListInfo.Sites, sfd.FileName);
					_siteListInfo.SavedSiteCoords();
					StateVars.UpdateState();
				}
				catch (Exception ex)
				{
					string title = "Error Writing Tray File";
					MessageBox.Show(this, ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}   // Save_Tray_File_Cmd()

		private void Update_Tray_File_Cmd()
		{
			string path = MeasSetup.TrayFile;
			if (path == null)
				return;
			StopPollingAndEraseCrosshair();
			Clear_CP_Status();
			Clear_Mx_Status();
			int numSitesModified = _siteListInfo.NumSitesModified();
			string title = "Update Tray File";
			string msg = "";
			if (numSitesModified == 0)
			{
				msg = "No site has modified coordinates.\n\n";
			}
			else if (numSitesModified == 1)
			{
				msg = "One site has modified coordinates.\n\n";
			}
			else
			{
				msg = numSitesModified.ToString() + " sites have modified coordinates.\n\n";
			}
			msg += "Are you sure that you want to overwrite this tray file?\n\n";
			msg += path + "\n";
			DialogResult dr = MessageBox.Show(this, msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (dr == DialogResult.Cancel)
			{
				StartPolling();
				return;
			}
			FileSystemFuncs.AppendToLogFile("Update_Tray_File_Cmd:" + path);
			try
			{
				SiteCoordFile.Write(_siteListInfo.Sites, path);
				_siteListInfo.SavedSiteCoords();
				StateVars.UpdateState();
			}
			catch (Exception ex)
			{
				title = "Error Writing Tray File";
				MessageBox.Show(this, ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			StartPolling();
		}   // Update_Tray_File_Cmd()

		private void Update_Tray_Level_Coords_Cmd()
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			string title = "Update Tray Level Coordinates";
			string msg = "";
			msg += "This saves new stage Roll and Pitch coordinates\n";
			msg += "for the tray level position.\n\n";
			msg += "Click OK to continue, or click Cancel.";
			DialogResult dr = MessageBox.Show(this, msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dr != DialogResult.OK)
			{
				StartPolling();
				return;
			}
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpUpdateTrayLevelCoords());
		} // Update_Tray_Level_Coords_Cmd()

		private void Set_Catseye_Offset_Cmd()
		{
			Clear_CP_Status();
			Clear_Mx_Status();
			StopPollingAndEraseCrosshair();
			string title = "Set Catseye Offset";
			string msg = "";
			msg += "This sets a Z axis offset for measuring sites on this tray.\n\n";
			msg += "The stage must be positioned at catseye for a part.\n\n";
			msg += "Click OK to continue, or click Cancel.";
			DialogResult dr = MessageBox.Show(this, msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dr != DialogResult.OK)
			{
				StartPolling();
				return;
			}
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.PerformAsyncOp(new AsyncOpSetCatseyeOffset());
		} // Set_Catseye_Offset_Cmd()

		private void Set_UIMode_Cmd()
		{
			// Get Windows UserName
			string uName = Environment.UserName;

			// File Menu
			loadUnloadToolStripMenuItem.Visible = true;
			measureToolStripMenuItem.Visible = true;
			publishToolStripMenuItem.Visible = true;
			measurePublishRepeatToolStripMenuItem.Visible = true;
			toolStripMenuSep1.Visible = true;

			connectToMxToolStripMenuItem.Visible = true;
			disconnectFromMxToolStripMenuItem.Visible = true;
			showCrosshairToolStripMenuItem.Visible = true;
			toolStripMenuSep2.Visible = true;

			clearAllSitesToolStripMenuItem.Visible = true;
			clearSelectedSitesToolStripMenuItem.Visible = true;
			updateTrayFileToolStripMenuItem.Visible = true;
			updateTrayLevelCoordinatesToolStripMenuItem.Visible = true;
			toolStripMenuSep4.Visible = true;

			refreshTrayToolStripMenuItem.Visible = true;
			refreshProgramSettingsToolStripMenuItem.Visible = true;
			toolStripMenuSep5.Visible = true;

			exitToolStripMenuItem.Visible = true;

			// Prep Configuration Menu
			bool isSuperUser = false;

			foreach (string superuser in ProgramSettings.SuperUserNames)
			{
				if (superuser.Trim().ToUpper() == uName.ToUpper()) // Case-insensitive
				{
					isSuperUser = true;
					break;
				}
			}
			if (isSuperUser)
			{
				Populate_Configurations_Menu_Cmd(); 
			}
			else
			{
				configurationToolStripMenuItem.Visible = false;
			}

			// Regardless of User, Set default configuration
			Activate_Configuration_Cmd(Path.Combine(ProgramSettings.ConfigurationFileDirectory, ProgramSettings.DefaultConfigurationFile));

			string msg = "Welcome " + uName;
			if (isSuperUser)
				msg += " (super user)";
			Set_CP_Status(msg);
		} // Set_UIMode_Cmd()

		private void Populate_Configurations_Menu_Cmd()
		{
			try
			{
				string[] configFiles = Directory.GetFiles(ProgramSettings.ConfigurationFileDirectory, "*.acpconfig", SearchOption.AllDirectories);

				foreach (string file in configFiles)
				{
					try
					{
						// Attempt to read as ConfigurationFile.  If any exceptions, do not show it
						ConfigurationFile cf = new ConfigurationFile(file);

						ToolStripMenuItem newMenuItem = new ToolStripMenuItem();
						newMenuItem.Text = cf.Name;
						newMenuItem.Click += new EventHandler(configurationMenuItem_Click);
						newMenuItem.Tag = file;

						configurationToolStripMenuItem.DropDownItems.Insert(configurationToolStripMenuItem.DropDownItems.Count, newMenuItem);
					}
					catch (Exception ex)
					{
						// Log the error but do not throw exception
						FileSystemFuncs.AppendToLogFile("Populate_Configurations_Menu_Cmd: Error reading config file " + file + ": " + ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				FileSystemFuncs.AppendToLogFile("Error in Populate_Configurations_Menu_Cmd: " + ex.Message);
			}
		}

		private void Activate_Configuration_Cmd(string ConfigFilePath)
		{
			try
			{
				// Read the file
				ConfigurationFile cf = new ConfigurationFile(ConfigFilePath);

				// Indicate the config file chosen on the menu with a check
				foreach (ToolStripMenuItem tsmi in configurationToolStripMenuItem.DropDownItems)
				{
					if (tsmi.Text == Path.GetFileNameWithoutExtension(ConfigFilePath))
					{
						tsmi.Checked = true;
					}
					else
					{
						tsmi.Checked = false;
					}
				}

				// Store active configuration for State
				ProgramSettings.ActiveConfiguration = cf;

				Set_CP_Status("Configuration " + cf.Name + " loaded");

				FileSystemFuncs.AppendToLogFile2(cf.DumpSettings());

				// Set state variables for instant update
				StateVars.UpdateState();
			}
			catch (Exception ex)
			{
				throw new Exception("Error setting Active Configuration " + Path.GetFileName(ConfigFilePath) + ": " + ex.Message);
			}
		}

		#endregion

	}
}
