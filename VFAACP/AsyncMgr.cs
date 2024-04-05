using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.IO;
using System.Drawing;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public delegate void AsyncMgrEventHandler(object sender, EventArgs e);

	public class AsyncMgr
	{
		static AsyncMgr _instance;

		static StageCoords cached_load_coords = null;

		private List<AsyncOp> _asyncOpList;
		private Thread _asyncOpThread;

		//private System.Timers.Timer _pollingTimer;
		//private bool _pollingEnabled;
		//private EventWaitHandle _pollingSync;
		//private DateTime _pollTime;

		private bool _requestStop = false;
		private bool _requestAbort = false;

		private int _killProcessId = 0;

		private bool _crosshairEnabled = false;

		public event AsyncMgrEventHandler AsyncMgrEvent;

		// Fake stage coordinates for testing without an instrument
		public StageCoords FakeStageCoords = new StageCoords();

		protected AsyncMgr()
		{
		}

		public static AsyncMgr Instance()
		{
			if (_instance == null)
			{
				_instance = new AsyncMgr();
			}
			return _instance;
		}

		public bool CrosshairEnabled
		{
			get { return _crosshairEnabled; }
			set	{ _crosshairEnabled = value; }
		}

		public void ReportError(string message, bool showDialog = true)
		{
			AsyncEventArgs_ReportError e = new AsyncEventArgs_ReportError(message, showDialog);
			AsyncMgrEvent(this, e);
		}

		public void ReportStatus(string statusMessage)
		{
			AsyncEventArgs_StatusMessage e = new AsyncEventArgs_StatusMessage(statusMessage);
			AsyncMgrEvent(this, e);
		}

		public void ReportMxStatus(string statusMessage)
		{
			AsyncEventArgs_MxStatusMessage e = new AsyncEventArgs_MxStatusMessage(statusMessage);
			AsyncMgrEvent(this, e);
		}

		public void ShowAsyncMgrPrompt(string msg)
		{
			//PromptMessage(this, new AsyncMgrEventArgs("Prompt", msg, null));
		}

		public void RaiseAsyncEvent(EventArgs e)
		{
			AsyncMgrEvent(this, e);
		}

		public void RequestStop()
		{
			_requestStop = true;
		}
		public bool StopRequested
		{
			get { return _requestStop; }
		}

		public void RequestAbort()
		{
			_requestAbort = true;
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			if (mxClient != null)
				mxClient.Abort();
			if (_killProcessId != 0)
			{
				Process process = Process.GetProcessById(_killProcessId);
				if (process != null)
				{
					process.Kill();
					ReportStatus("Script process killed");
				}
			}
		}

		public bool AbortRequested
		{
			get { return _requestAbort; }
		}

		public bool StopOrAbortRequested
		{
			get { return _requestStop || _requestAbort; }
		}

		public void PerformAsyncOp(AsyncOp asyncOp, bool requireMxConnection = true)
		{
			List<AsyncOp> asyncOpList = new List<AsyncOp>() { asyncOp };
			PerformAsyncOps(asyncOpList, requireMxConnection);
		}

		public void PerformAsyncOps(List<AsyncOp> asyncOpList, bool requireMxConnection)
		{
			_asyncOpList = asyncOpList;
			_requestStop = false;
			_requestAbort = false;
			_asyncOpThread = new Thread(() => this.DoPerformAsyncOps(requireMxConnection));
			_asyncOpThread.IsBackground = true;
			_asyncOpThread.Start();
		}

		private void DoPerformAsyncOps(bool requireMxConnection)
		{
			if (_asyncOpList == null)
			{
				ReportError("DoPerformAsyncOps: _asyncOpList is null");
				return;
			}
			if (_asyncOpList.Count() == 0)
			{
				ReportError("DoPerformAsyncOps: _asyncOpList is empty");
				return;
			}
			if (requireMxConnection && (StateVars.Mx_State == Mx_States.Disconnected))
			{
				ReportError("Mx is disconnected");
				return;
			}

			RaiseAsyncEvent(new AsyncEventArgs_PerformAsyncOps());
			foreach (AsyncOp asyncOp in _asyncOpList)
			{
				AsyncOpStatus status = asyncOp.DoAsyncOp();
				if (StopOrAbortRequested || ((status != null) && status.AbortFlag))
					break;
			} // foreach
			RaiseAsyncEvent(new AsyncEventArgs_PerformAsyncOpsDone());
		}   // DoPerformAsyncOps()


		public AsyncOpStatus GetStageCoords(out StageCoords coords)
		{
			coords = null;
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				MxMgr mxMgr = MxMgr.Instance();
				MxClient mxClient = mxMgr.MxClient;
				try
				{
					Tuple<AxisType, Unit>[] axes = new Tuple<AxisType, Unit>[5];
					axes[0] = new Tuple<AxisType,Unit>(AxisType.X, Unit.MilliMeters);
					axes[1] = new Tuple<AxisType,Unit>(AxisType.Y, Unit.MilliMeters);
					axes[2] = new Tuple<AxisType,Unit>(AxisType.Z, Unit.MilliMeters);
					axes[3] = new Tuple<AxisType,Unit>(AxisType.RY, Unit.Degrees);
					axes[4] = new Tuple<AxisType,Unit>(AxisType.RX, Unit.Degrees);
					Tuple<AxisType, double, Unit>[] positions;
					positions = mxClient.Motion.GetPositions(axes);
					coords = new StageCoords();
					coords.X_mm = positions[0].Item2;
					coords.Y_mm = positions[1].Item2;
					coords.Z_mm = positions[2].Item2;
					coords.R_deg = positions[3].Item2;
					coords.P_deg = positions[4].Item2;
				}
				catch (ZygoError ex)
				{
					return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
				}
			}
			else if (ProgramSettings.TestWithoutInstrument == 1)
			{
				ReportStatus("ProgramSettings.TestWithoutInstrument == 1");
				coords = FakeStageCoords;
			}
			else // (ProgramSettings.TestWithoutInstrument == 2)
			{
				ReportStatus("ProgramSettings.TestWithoutInstrument == 2");
				var rand = new Random();
				coords = new StageCoords();
				// Random values in range [-1,+1]
				coords.X_mm = (2.0 * rand.NextDouble()) - 1.0;
				coords.Y_mm = (2.0 * rand.NextDouble()) - 1.0;
				coords.Z_mm = (2.0 * rand.NextDouble()) - 1.0;
				coords.R_deg = (2.0 * rand.NextDouble()) - 1.0;
				coords.P_deg = (2.0 * rand.NextDouble()) - 1.0;
			}
			return null;
		}   // GetStageCoords()

		public AsyncOpStatus GetLoadTrayCoords(out StageCoords coords, bool useCache = false)
		{
			coords = null;
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				if (useCache && (cached_load_coords != null))
				{
					coords = cached_load_coords;
				}
				else
				{
					try
					{
						MxMgr mxMgr = MxMgr.Instance();
						MxClient mxClient = mxMgr.MxClient;
						MxService mx = mxClient.Mx;
						coords = new StageCoords();
						coords.X_mm = mx.GetControlNumber(new[] { "Instrument", "Motion", "Load Unload", "Load Position X" }, Unit.MilliMeters);
						coords.Y_mm = mx.GetControlNumber(new[] { "Instrument", "Motion", "Load Unload", "Load Position Y" }, Unit.MilliMeters);
						coords.Z_mm = mx.GetControlNumber(new[] { "Instrument", "Motion", "Load Unload", "Load Position Z" }, Unit.MilliMeters);
						coords.R_deg = mx.GetControlNumber(new[] { "Instrument", "Motion", "Load Unload", "Load Position RY" }, Unit.Degrees);
						coords.P_deg = mx.GetControlNumber(new[] { "Instrument", "Motion", "Load Unload", "Load Position RX" }, Unit.Degrees);
					}
					catch (ZygoError ex)
					{
						return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
					}
					cached_load_coords = coords;
				}
			}
			else
			{
				ReportStatus("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
			}
			return null;
		}   // GetLoadTrayCoords()

		public static bool StageAtLoadPosition(StageCoords stageCoords, StageCoords loadCoords)
		{
			const double epsilon_XY_mm = 1.5;
			const double epsilon_Z_mm = 5.0;
			const double epsilon_RP_deg = 0.1;
			if (Math.Abs(stageCoords.X_mm - loadCoords.X_mm) > epsilon_XY_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.Y_mm - loadCoords.Y_mm) > epsilon_XY_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.Z_mm - loadCoords.Z_mm) > epsilon_Z_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.R_deg - loadCoords.R_deg) > epsilon_RP_deg)
			{
				return false;
			}
			if (Math.Abs(stageCoords.P_deg - loadCoords.P_deg) > epsilon_RP_deg)
			{
				return false;
			}
			return true;
		}

		public AsyncOpStatus MoveToCoords(StageCoords stageCoords)
		{
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			Tuple<AxisType, double, Unit>[] coords = new Tuple<AxisType, double, Unit>[5];
			coords[0] = new Tuple<AxisType, double, Unit>(AxisType.X, stageCoords.X_mm, Unit.MilliMeters);
			coords[1] = new Tuple<AxisType, double, Unit>(AxisType.Y, stageCoords.Y_mm, Unit.MilliMeters);
			coords[2] = new Tuple<AxisType, double, Unit>(AxisType.Z, stageCoords.Z_mm, Unit.MilliMeters);
			coords[3] = new Tuple<AxisType, double, Unit>(AxisType.RY, stageCoords.R_deg, Unit.Degrees);
			coords[4] = new Tuple<AxisType, double, Unit>(AxisType.RX, stageCoords.P_deg, Unit.Degrees);
			try
			{
				mxClient.Motion.MoveAbsolute(coords, true);
			}
			catch (ZygoError ex)
			{
				return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
			}
			return null;
		}

		public AsyncOpStatus MoveToSiteXYZRP(StageCoords siteCoords, bool homeTiltAxes = false)
		{
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			if (homeTiltAxes)
			{
				try
				{
					mxClient.Motion.HomeRP(true);
				}
				catch (ZygoError ex)
				{
					return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
				}
			}
			return MoveToCoords(siteCoords);
		}

		public AsyncOpStatus MoveToSiteZ(StageCoords siteCoords)
		{
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			Tuple<AxisType, double, Unit>[] coords = new Tuple<AxisType, double, Unit>[1];
			coords[0] = new Tuple<AxisType, double, Unit>(AxisType.Z, siteCoords.Z_mm, Unit.MilliMeters);
			try
			{
				mxClient.Motion.MoveAbsolute(coords, true);
			}
			catch (ZygoError ex)
			{
				return new AsyncOpStatus(errorMessage: ex.Message);
			}
			return null;
		}

		// UpdateSiteViews()
		// Called by DoMeasureSites() to update the user interface site map and site list
		public void UpdateSiteViews(List<Site> siteList)
		{
			AsyncEventArgs_MeasuredSites e = new AsyncEventArgs_MeasuredSites();
			e.siteList = siteList;
			RaiseAsyncEvent(e);
		}

		public AsyncOpStatus DoGetNewCoordsForSite(int siteNum, bool prompt)
		{
			StageCoords coords;
			AsyncOpStatus status = GetStageCoords(out coords);
			if ((status != null) && status.HasError)
			{
				ReportError("Error getting stage coords: " + status.ErrorMessage, showDialog: false);
				return status;
			}
			AsyncEventArgs_NewCoordsForSite e = new AsyncEventArgs_NewCoordsForSite();
			e.siteNum = siteNum;
			e.stageCoords = coords;
			e.prompt = prompt;
			RaiseAsyncEvent(e);
			return null;
		}   // DoGetNewCoordsForSite()


		public AsyncOpStatus RunScript(string scriptFile, string scriptArgs = null, bool waitForExit = true, bool allowKill = false)
		{
			string errorMessage = null;
			string standardError = null;
			string scriptPath = Path.Combine(ProgramSettings.ScriptFileDirectory, scriptFile);
			int exitCode = -1;
			_killProcessId = 0;
			try
			{
				FileSystemFuncs.AppendToLogFile("Running script " + scriptPath);
				ProcessStartInfo pinfo = new ProcessStartInfo();
				pinfo.FileName = ProgramSettings.PythonExecutable;
				pinfo.EnvironmentVariables.Add("PYTHONPATH", ProgramSettings.PythonPath);
				pinfo.Arguments = scriptFile;
				if (scriptArgs != null)
				{
					pinfo.Arguments += " " + scriptArgs;
				}
				pinfo.WorkingDirectory = ProgramSettings.ScriptFileDirectory;
				pinfo.CreateNoWindow = true;
				pinfo.UseShellExecute = false;
				pinfo.LoadUserProfile = true;
				pinfo.RedirectStandardOutput = false;
				pinfo.RedirectStandardError = true;
				pinfo.StandardErrorEncoding = Encoding.ASCII;
				using (Process p = new Process())
				{
					p.StartInfo = pinfo;
					p.Start();
					if (waitForExit)
					{
						if (allowKill)
							_killProcessId = p.Id;
						p.WaitForExit();
						standardError = p.StandardError.ReadToEnd();
						standardError = standardError.TrimEnd(new char[] { '\r', '\n' });
						exitCode = p.ExitCode;
					}
					else
					{
						exitCode = 0;
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = "Exception running script " + scriptPath + ": " + ex.Message;
				if (!string.IsNullOrEmpty(standardError))
					errorMessage += "\n" + standardError;
				FileSystemFuncs.AppendToLogFile(errorMessage);
				return new AsyncOpStatus(abortFlag: true, errorMessage: errorMessage, showDialog: true);
			}
			if ((exitCode != 0) || !string.IsNullOrEmpty(standardError))
			{
				string logMsg = "Error running script (exitCode=" + exitCode.ToString() +")";
				if (string.IsNullOrEmpty(standardError))
					logMsg += " (standardError = null)";
				else
					logMsg += " (standardError = \"" + standardError + "\")";
				FileSystemFuncs.AppendToLogFile(logMsg);
				if (string.IsNullOrEmpty(standardError))
					errorMessage = "Error running script " + scriptPath + " (ExitCode=" + exitCode.ToString() + ")";
				else
					errorMessage = standardError;
				bool showDialog;
				if (AbortRequested)
				{
					errorMessage = "Processing aborted";
					showDialog = true;
				}
				else
				{
					showDialog = (exitCode != 0);
				}
				return new AsyncOpStatus(abortFlag: (exitCode != 0), errorMessage: errorMessage, showDialog: showDialog);
			}
			return null;
		}

		public string Read_Display_Results_File_Cmd(string PathAndFile)
		{
			// Read Results Display File
			string displayText = "";
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(PathAndFile);
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					if (displayText.Length > 0)
						displayText += Environment.NewLine;
					displayText += line;
				}
			}
			catch { }
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr.Dispose();
				}
			}

			return displayText;
		}   // Read_Display_Results_File_Cmd()


	}
}
