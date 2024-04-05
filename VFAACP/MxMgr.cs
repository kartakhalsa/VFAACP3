using DevExpress.CodeParser;
using DevExpress.Utils.Drawing.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Zygo.MetroProXP.RemoteAccessClient;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace VFAACP
{
	// Class to manage Mx startup, hiding and closing
	public class MxMgr
	{
		static MxMgr _instance;

		private int _mxStartTimer_sec;

		private MxClient _mxClient;
		protected MxMgr()
		{
		}

		public static MxMgr Instance()
		{
			if (_instance == null)
			{
				_instance = new MxMgr();
			}
			return _instance;
		}

		public MxClient MxClient
		{
			get { return _mxClient; }
		}

		public bool MxConnected
		{
			get
			{
				if ((_mxClient != null) && _mxClient.Connected)
					return true;
				else
					return false;
			}
		}

		public int GetMxProcessId()
		{
			string mxProcessName = "MainUI";
			Process[] processes = Process.GetProcessesByName(mxProcessName);
			if (processes.Length > 0)
			{
				return processes[0].Id;
			}
			else
			{
				return 0;
			}
		}
		public bool IsMxRunning()
		{
			int processId = GetMxProcessId();
			if (processId != 0)
				return true;
			else
				return false;
		}

		public IntPtr GetMxMainWindowHandle()
		{
			IntPtr hwnd = MyEnumWindows.GetWindowHandle("Mx","WindowsForms10");
			return hwnd;
		}
		public IntPtr GetMxLiveDisplayWindowHandle()
		{
			IntPtr hwnd = MyEnumWindows.GetWindowHandle("Live Display", "WindowsForms10");
			return hwnd;
		}
		public void CloseMx(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportStatus("Closing Mx");
				DisconnectFromMx(verbose: verbose);
				ProcUtils.CloseWindow(hwnd);
				if (verbose)
					asyncMgr.ReportStatus("Closed Mx");
			}
			else
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
			}
		}

		public void KillMx(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			int processId = GetMxProcessId();
			if (processId == 0)
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			else
			{
				Process process = Process.GetProcessById(processId);
				if (process != null)
				{
					DisconnectFromMx(verbose: verbose);
					process.Kill();
					if (verbose)
						asyncMgr.ReportStatus("Mx process killed");
					return;
				}
			}
			if (verbose)
				asyncMgr.ReportError("Cannot kill Mx process");
		}
		public void ShowMxWindow(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			ProcUtils.ShowWindow(hwnd);
			ProcUtils.SetWindowPosOnScreen(hwnd, verbose);
			if (verbose)
				asyncMgr.ReportStatus("Mx window shown");
		}
		public void HideMxWindow(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			ProcUtils.HideWindow(hwnd);
			if (verbose)
				asyncMgr.ReportStatus("Mx window hidden");
		}
		public bool MinimizeMxWindow(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return false;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return false;
			}
			ProcUtils.MinimizeWindow(hwnd);
			if (verbose)
				asyncMgr.ReportStatus("Mx window minimized");
			return true;
		}
		public void MaximizeMxWindow(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			ProcUtils.MaximizeWindow(hwnd);
			//ProcUtils.SetWindowPosOnScreen(hwnd, verbose);
			if (verbose)
				asyncMgr.ReportStatus("Mx window maximized");
		}

		public void ShowMxInTaskbar(bool show, bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			ProcUtils.ShowWindowInTaskbar(hwnd, show);
			if (verbose)
			{
				if (show)
					asyncMgr.ReportStatus("Mx shown in taskbar");
				else
					asyncMgr.ReportStatus("Mx not shown in taskbar");
			}
		}
		public void SetMxStyleAppOrToolWindow(bool appStyle, bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			if (appStyle)
			{
				ProcUtils.SetWindowStyleApp(hwnd);
				if (verbose)
					asyncMgr.ReportStatus("Mx window style set to app");
			}
			else
			{
				ProcUtils.SetWindowStyleTool(hwnd);
				if (verbose)
					asyncMgr.ReportStatus("Mx window style set to tool");
			}
		}
		public void MoveMxWindow(bool onScreen, bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
				return;
			}
			ProcUtils.SetWindowPosOnScreen(hwnd, onScreen);
			if (verbose)
			{
				if (onScreen)
					asyncMgr.ReportStatus("Moved Mx window on screen");
				else
					asyncMgr.ReportStatus("Moved Mx window off screen");
			}
		}

		public bool FinalizeSecureDesktop(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
				{
					asyncMgr.ReportError("Mx is not running");
				}
				return false;
			}
			IntPtr hwnd = GetMxMainWindowHandle();
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (verbose)
				{
					asyncMgr.ReportError("Cannot access Mx window");
				}
				return false;
			}
			ProcUtils.SetWindowPosOnScreen(hwnd, false);
			if (verbose)
			{
				asyncMgr.ReportStatus("Moved Mx window off screen");
			}
			return true;
		}
		public bool ConnectToMx(bool reportErrors = true, bool raiseEvents = true)
		{
			string msg;
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (reportErrors)
				{
					asyncMgr.ReportError("Mx is not running");
				}
				return false;
			}
			try
			{
				if (raiseEvents)
				{
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_ConnectingToMx());
				}
				if (_mxClient != null)
				{
					//_mxClient.Terminate();
					_mxClient = null;
				}
				_mxClient = new MxClient("localhost");
				_mxClient.Connect(true /*force*/);
				if (!_mxClient.Connected)
				{
					msg = "ConnectToMx failed";
					_mxClient.Terminate();
					_mxClient = null;
					if (reportErrors)
					{
						asyncMgr.ReportError(msg);
					}
					else
					{
						FileSystemFuncs.AppendToLogFile(msg);
					}
					if (raiseEvents)
					{
						asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxDisconnected());
					}
					return false;
				}
				else
				{
					if (raiseEvents)
					{
						asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxConnected());
					}
					return true;
				}
			}
			catch (ZygoError ex)
			{
				msg = "ConnectToMx failed:" + ex.Message;
				_mxClient = null;
				if (reportErrors)
				{
					asyncMgr.ReportError(msg);
				}
				else
				{
					FileSystemFuncs.AppendToLogFile(msg);
				}
				if (raiseEvents)
				{
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxDisconnected());
				}
				return false;
			}
		}   // ConnectToMx()

		public void DisconnectFromMx(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (IsMxRunning() && (_mxClient != null))
			{
				if (verbose)
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_DisconnectingFromMx());
				try
				{
					_mxClient.Terminate();
				}
				catch (ZygoError ex)
				{
					if (verbose)
						asyncMgr.ReportError(ex.Message);
				}
			}
			_mxClient = null;
			if (verbose)
				asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxDisconnected());
		}   // DisconnectFromMx()

		public void ExerciseMxGUI()
		{
			// Use remote access to exercise the Mx GUI
			// Delay after each tab is shown
			const int delay_sec = 10;
			if (_mxClient == null)
				return;
			string msg;
			UserInterfaceService ui = _mxClient.UserInterface;
			List<string> tabNames = new List<string> {"Calibration", "Automation", "Analysis", "Measurement" };
			foreach (string tabName in tabNames)
			{
				var tab = ui.GetTab(tabName);
				if (tab != null)
				{
					msg = "ExerciseMxGUI: show tab " + tabName;
					FileSystemFuncs.AppendToLogFile(msg);
					tab.Show();
					Thread.Sleep(delay_sec * 1000);
				}
				else
				{
					msg = "ExerciseMxGUI error: GetTab(\"" + tabName + "\") returned null";
					FileSystemFuncs.AppendToLogFile(msg);
				}
			}
		}

		public bool MxShowLiveDisplay()
		{
			// Use remote access to cause Mx to show live display
			if (_mxClient == null)
				return false;
			UserInterfaceService ui = _mxClient.UserInterface;
			var tab = ui.GetTab("Analysis");
			if (tab == null)
			{
				FileSystemFuncs.AppendToLogFile("MxShowLiveDisplay error: GetTab(\"Analysis\") returned null");
				return false;
			}
			var group = tab.GetGroup("Measurement");
			if (group == null)
			{
				FileSystemFuncs.AppendToLogFile("MxShowLiveDisplay error: GetGroup(\"Measurement\") returned null");
				return false;
			}
			var container = group.GetContainer("Live Display");
			if (container == null)
			{
				FileSystemFuncs.AppendToLogFile("MxShowLiveDisplay error: GetContainer(\"Live Display\") returned null");
				return false;
			}
			container.Show();
			return true;
		}
		public void ShowLiveDisplay(bool close_then_open)
		{
			if (_mxClient == null)
				return;
			IntPtr hwnd;
			hwnd = GetMxLiveDisplayWindowHandle();
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{	// window already exists
				if (close_then_open)
				{
					ProcUtils.CloseWindow(hwnd);
					hwnd = IntPtr.Zero;
				}
				else
				{
					ProcUtils.ShowWindow(hwnd);
					ProcUtils.ShowWindowTopMost(hwnd);
					ProcUtils.SetWindowStyleTool(hwnd);
					return;
				}
			}
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				if (MxShowLiveDisplay())
				{
					Thread.Sleep(1000);
					hwnd = GetMxLiveDisplayWindowHandle();
					Thread.Sleep(1000);
				}
			}
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				AsyncMgr asyncMgr = AsyncMgr.Instance();
				asyncMgr.ReportError("Cannot show Live Display");
				return;
			}
			ProcUtils.ShowWindowTopMost(hwnd);
			ProcUtils.SetWindowStyleTool(hwnd);
		}

		private void OnStartMxTimerEvent(Object sender, EventArgs e)
		{
			_mxStartTimer_sec += 1;
			int ndots = 1 + _mxStartTimer_sec % 10;
			string msg = "Please wait " + new string('.', ndots);
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.ReportMxStatus(msg);
		}

		public bool StartMx(bool secureDesktop, bool autoStartMx)
		{
			// This runs a python script to setup and launch Mx
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			System.Timers.Timer timer = null;
			bool mx_is_running = false;
			mx_is_running = IsMxRunning();
			if (!mx_is_running)
			{
				_mxStartTimer_sec = 0;
				timer = new System.Timers.Timer(1000);
				timer.Elapsed += OnStartMxTimerEvent;
				timer.AutoReset = true;
				timer.Enabled = true;
				timer.Start();
				AsyncEventArgs_StartingMx e1 = new AsyncEventArgs_StartingMx();
				asyncMgr.RaiseAsyncEvent(e1);
				string scriptArgs = "-mx_logfile " + FileSystemFuncs.GetMxLogPath();
				if (secureDesktop)
				{
					scriptArgs += " -securedesktop";
				}
				AsyncOpStatus status = asyncMgr.RunScript(ProgramSettings.StartMxScript, scriptArgs: scriptArgs, allowKill: true);
				timer.Stop();
				asyncMgr.ReportStatus(null);
				if ((status != null) && status.HasError)
				{
					AsyncEventArgs_StartMxError e2 = new AsyncEventArgs_StartMxError();
					e2.errorMessage = status.ErrorMessage;
					asyncMgr.RaiseAsyncEvent(e2);
					timer.Stop();
					return false;
				}
				mx_is_running = IsMxRunning();
				if (!mx_is_running)
				{
					AsyncEventArgs_StartMxError e6 = new AsyncEventArgs_StartMxError();
					e6.errorMessage = "Error starting Mx";
					asyncMgr.RaiseAsyncEvent(e6);
					return false;
				}
			}
			if (secureDesktop)
			{
				if (!ConnectToMx(raiseEvents: true))
				{
					AsyncEventArgs_StartMxError e6 = new AsyncEventArgs_StartMxError();
					e6.errorMessage = "Failed to connect to Mx";
					asyncMgr.RaiseAsyncEvent(e6);
					return false;
				}
			}
			AsyncEventArgs_MxIsReady e7 = new AsyncEventArgs_MxIsReady();
			asyncMgr.RaiseAsyncEvent(e7);
			return true;
		}

	}
}
