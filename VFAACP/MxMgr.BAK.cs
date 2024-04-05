using DevExpress.CodeParser;
using DevExpress.Utils.Drawing.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Zygo.MetroProXP.RemoteAccessClient;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace VFAACP
{
	// Class to manage Mx startup, hiding and closing
	public class MxMgr
	{
		private const string mxWindowClassName1 = "WindowsForms10.Window.8.app.0.10d5fb2_r7_ad2";
		private const string mxWindowClassName2 = "WindowsForms10.Window.8.app.0.ae65b0_r8_ad2";

		static MxMgr _instance;

		private bool _useMxHwndCache = false;

		// Handle for Mx main window, cached
		private IntPtr _mxHwnd = IntPtr.Zero;

		//  Mx Process ID
		private int _mxProcessId = 0;

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

		public void Diag()
		{
			List<IntPtr> handles = ProcUtils.GetModuleWindowHandles("MainUI.exe");
			if (handles != null)
			{
				;
			}
			else
			{
				;
			}
		}

		public void Diag2()
		{
			IntPtr hwnd;

			//hwnd = MyEnumWindows.GetMxMainWindowHandle();

			return;

			//foreach (string title in titles)
			//{
			//	if (title.StartsWith("Live Display"))
			//		;
			//	if (title.StartsWith("Mx"))
			//		;
			//}

			//hwnd = FindWindow1.FindWindow1A("Live Display");
			//if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			//{
			//	string className = ProcUtils.GetWindowClassName(hwnd);
			//	ProcUtils.HideWindow(hwnd);
			//	ProcUtils.ShowWindow(hwnd);
			//	return;
			//}


			//if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			//{
			//	string className;
			//	//className = ProcUtils.GetWindowClassName(hwnd);
			//	className = "WindowsForms10.Window.8.app.0.10d5fb2_r7_ad2";
			//	hwnd = FindWindow1.FindWindow1B(className, "Mx");
			//	ProcUtils.HideWindow(hwnd);
			//	ProcUtils.ShowWindow(hwnd);
			//}

			//hwnd = FindWindow1.FindWindow1A("Live Display");
			//if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			//{
			//	ProcUtils.HideWindow(hwnd);
			//	ProcUtils.ShowWindow(hwnd);
			//}

			//hwnd = FindWindow2.GetHandleByTitle("Mx");
			//if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			//{
			//	ProcUtils.HideWindow(hwnd);
			//	ProcUtils.ShowWindow(hwnd);
			//}

			//hwnd = FindWindow2.GetHandleByTitle("Live Display");
			//if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			//{
			//	ProcUtils.HideWindow(hwnd);
			//	ProcUtils.ShowWindow(hwnd);
			//}
		}
		public void Diag3()
		{
			IntPtr hwnd;
			hwnd = FindWindow1.FindWindow1A("Mx");
			hwnd = FindWindow2.GetHandleByTitle("Mx");
		}

		public int GetMxProcessId()
		{
			string mxProcessName = "MainUI";
			Process[] processes = Process.GetProcessesByName(mxProcessName);
			if (processes.Length > 0)
			{
				_mxProcessId = processes[0].Id;
				return _mxProcessId;
			}
			else
			{
				return 0;
			}
		}
		public IntPtr GetMxMainWindowHandle()
		{
			IntPtr hwnd = MyEnumWindows.GetWindowHandle("Mx","WindowsForms10");
			return hwnd;
		}
		public IntPtr GetMxMainWindowHandle_OLD()
		{
			IntPtr hwnd;
			if (_useMxHwndCache && !IntPtr.Equals(_mxHwnd, IntPtr.Zero))
			{
				return _mxHwnd;
			}
			else
			{
				// Mx Main window can have two different titles: "Mx" or "Mx: Remote Client Active"
				const string title1 = "Mx";
				const string title2 = "Mx: Remote Client Active";
				hwnd = ProcUtils.GetWindowHandle(mxWindowClassName1, title1);
				if (IntPtr.Equals(hwnd, IntPtr.Zero))
				{
					hwnd = ProcUtils.GetWindowHandle(mxWindowClassName2, title1);
					if (IntPtr.Equals(hwnd, IntPtr.Zero))
					{
						hwnd = ProcUtils.GetWindowHandle(mxWindowClassName1, title2);
						if (IntPtr.Equals(hwnd, IntPtr.Zero))
						{
							hwnd = ProcUtils.GetWindowHandle(mxWindowClassName2, title2);
						}
					}
				}
				_mxHwnd = hwnd;
				return hwnd;
			}
		}
		public IntPtr GetMxLiveDisplayWindowHandle()
		{
			IntPtr hwnd = MyEnumWindows.GetWindowHandle("Live Display", "WindowsForms10");
			return hwnd;
		}
		public IntPtr GetMxLiveDisplayWindowHandle_OLD()
		{
			const string title = "Live Display";
			IntPtr hwnd;
			hwnd = ProcUtils.GetWindowHandle(mxWindowClassName1, title);
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				hwnd = ProcUtils.GetWindowHandle(mxWindowClassName2, title);
			}
			return hwnd;
		}
		public bool IsMxRunning()
		{
			int processId = GetMxProcessId();
			if (processId != 0)
				return true;
			else
				return false;
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
					asyncMgr.ReportMxStatus("Closing Mx");
				DisconnectFromMx(verbose: verbose);
				ProcUtils.CloseWindow(hwnd);
				_mxProcessId = 0;
				_mxHwnd = IntPtr.Zero;
				if (verbose)
					asyncMgr.ReportMxStatus("Closed Mx");
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
					asyncMgr.ReportError("Mx is not running", showDialog: false);
				return;
			}
			else
			{
				Process process = Process.GetProcessById(processId);
				if (process != null)
				{
					DisconnectFromMx(verbose: verbose);
					_mxProcessId = 0;
					_mxHwnd = IntPtr.Zero;
					process.Kill();
					if (verbose)
						asyncMgr.ReportMxStatus("Mx process killed");
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
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				ProcUtils.ShowWindow(hwnd);
				if (verbose)
					asyncMgr.ReportMxStatus("Mx window shown");
			}
			else
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
			}
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
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				ProcUtils.HideWindow(hwnd);
				if (verbose)
					asyncMgr.ReportMxStatus("Mx window hidden");
			}
			else
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
			}
		}
		public void MinimizeMxWindow(bool verbose = true)
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
				ProcUtils.MinimizeWindow(hwnd);
				if (verbose)
					asyncMgr.ReportMxStatus("Mx window minimized");
			}
			else
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
			}
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
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				ProcUtils.MaximizeWindow(hwnd);
				if (verbose)
					asyncMgr.ReportMxStatus("Mx window maximized");
			}
			else
			{
				if (verbose)
					asyncMgr.ReportError("Cannot access Mx window");
			}
		}
		public bool ConnectToMx(bool verbose = true)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				if (verbose)
					asyncMgr.ReportError("Mx is not running");
				return false;
			}
			try
			{
				if (verbose)
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_ConnectingToMx());
				if (_mxClient != null)
				{
					//_mxClient.Terminate();
					_mxClient = null;
				}
				_mxClient = new MxClient("localhost");
				_mxClient.Connect(true /*force*/);
				if (!_mxClient.Connected)
				{
					_mxClient.Terminate();
					_mxClient = null;
					if (verbose)
					{
						asyncMgr.ReportError("Cannot connect to Mx");
						asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxDisconnected());
					}
					return false;
				}
				else
				{
					if (verbose)
						asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MxConnected());
					return true;
				}
			}
			catch (ZygoError ex)
			{
				_mxClient = null;
				if (verbose)
				{
					asyncMgr.ReportError(ex.Message);
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

		public void ShowLiveDisplay()
		{
			if (_mxClient == null)
				return;
			IntPtr hwnd;
			hwnd = GetMxLiveDisplayWindowHandle();
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				// Window already exists
				// Behavior appears more reliable if we close, then re-open
				ProcUtils.CloseWindow(hwnd);
			}
			UserInterfaceService ui = _mxClient.UserInterface;
			var tab = ui.GetTab("Analysis");
			if (tab != null)
			{
				var group = tab.GetGroup("Measurement");
				if (group != null)
				{
					var container = group.GetContainer("Live Display");
					if (container != null)
					{
						container.Show();
						hwnd = GetMxLiveDisplayWindowHandle();
					}
				}
			}
			if (IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				AsyncMgr asyncMgr = AsyncMgr.Instance();
				asyncMgr.ReportError("Cannot show Live Display");
				return;
			}
			ProcUtils.ShowWindowTopMost(hwnd);
		}
		public void ShowLiveDisplayWindow_OLD()
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (!IsMxRunning())
			{
				asyncMgr.ReportError("Mx is not running");
				return;
			}
			IntPtr hwnd = GetMxLiveDisplayWindowHandle();
			if (!IntPtr.Equals(hwnd, IntPtr.Zero))
			{
				ProcUtils.ShowWindow(hwnd);
			}
			else
			{
				asyncMgr.ReportError("Cannot access Mx window");
			}
		}

		private void OnStartMxTimerEvent(Object sender, EventArgs e)
		{
			_mxStartTimer_sec -= 1;
			//int ndots = 1 + _mxStartTimer_sec % 10;
			int n_sec;
			if (_mxStartTimer_sec > 0)
				n_sec = _mxStartTimer_sec;
			else
				n_sec = 0;
			string msg = "Please wait " + n_sec.ToString() + " sec";
				
			//+ new string('.', ndots);
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			asyncMgr.ReportMxStatus(msg);
		}

		public bool StartMx1(int wait_for_ready_sec = 0)
		{
			// This version of StartMx may be OBSOLETE
			// This version of StartMx uses a StartMxScript that:
			// 1. Launches Mx
			// 2. Allows a long time for Mx to initialize
			// 3. Connects to Mx
			// 4. Exercises the GUI
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (IsMxRunning())
			{
				return true;
			}
			AsyncEventArgs_StartingMx e1 = new AsyncEventArgs_StartingMx();
			asyncMgr.RaiseAsyncEvent(e1);
			_mxStartTimer_sec = wait_for_ready_sec;
			System.Timers.Timer timer = new System.Timers.Timer(1000);
			timer.Elapsed += OnStartMxTimerEvent;
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Start();
			AsyncOpStatus status = asyncMgr.RunScript(ProgramSettings.StartMxScript, allowKill: true);
			if ((status != null) && status.HasError)
			{
				timer.Stop();
				AsyncEventArgs_StartMxError e2 = new AsyncEventArgs_StartMxError();
				e2.errorMessage = status.ErrorMessage;
				asyncMgr.RaiseAsyncEvent(e2);
				return false;
			}

			if (timer != null)
			{
				timer.Stop();
				asyncMgr.ReportMxStatus(null);
			}
			return true;
		}
		public bool StartMx2(int wait_for_ready_sec = 0)
		{
			// This version of StartMx runs a StartMxScript that just launches Mx.
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			if (IsMxRunning())
			{
				return true;
			}
			AsyncEventArgs_StartingMx e1 = new AsyncEventArgs_StartingMx();
			asyncMgr.RaiseAsyncEvent(e1);

			AsyncOpStatus status = asyncMgr.RunScript(ProgramSettings.StartMxScript, allowKill: true);
			if ((status != null) && status.HasError)
			{
				AsyncEventArgs_StartMxError e2 = new AsyncEventArgs_StartMxError();
				e2.errorMessage = status.ErrorMessage;
				asyncMgr.RaiseAsyncEvent(e2);
				return false;
			}
			bool mx_is_running = false;
			int remaining_sec = wait_for_ready_sec;
			while (remaining_sec >= 0)
			{
				Thread.Sleep(1000);
				mx_is_running = IsMxRunning();
				if (!mx_is_running)
				{
					AsyncEventArgs_WaitForMxStart e3 = new AsyncEventArgs_WaitForMxStart();
					e3.remaining_sec = remaining_sec;
					asyncMgr.RaiseAsyncEvent(e3);
				}
				else
				{
					AsyncEventArgs_WaitForMxReady e3 = new AsyncEventArgs_WaitForMxReady();
					e3.remaining_sec = remaining_sec;
					asyncMgr.RaiseAsyncEvent(e3);
				}
				remaining_sec -= 1;
			}
			if (!mx_is_running)
			{
				AsyncEventArgs_StartMxError e2 = new AsyncEventArgs_StartMxError();
				e2.errorMessage = "Error starting Mx";
				asyncMgr.RaiseAsyncEvent(e2);
				return false;
			}

			//remaining_sec = wait_for_ready_sec;
			//while (remaining_sec >= 0)
			//{
			//	Thread.Sleep(1000);
			//	AsyncEventArgs_WaitForMxReady e3 = new AsyncEventArgs_WaitForMxReady();
			//	e3.remaining_sec = remaining_sec;
			//	asyncMgr.RaiseAsyncEvent(e3);
			//	remaining_sec -= 1;
			//}
			return true;
		}

		//public AsyncOpStatus StartMxDirect()
		//{
		//	IntPtr hwnd = IntPtr.Zero;
		//	hwnd = GetMxMainWindowHandle();
		//	if (!IntPtr.Equals(hwnd, IntPtr.Zero))
		//	{
		//		return new AsyncOpStatus(abortFlag: true, errorMessage: "Mx is already running");
		//	}
		//	AsyncOpStatus status = ProcUtils.StartProgram(ProgramSettings.MxExecutable);
		//	if (status != null)
		//		return status;
		//	return null;

		//}

	}
}
