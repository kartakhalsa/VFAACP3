using DevExpress.CodeParser.Diagnostics;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Diagnostics.GUIResources;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

// Some references
// https://stackoverflow.com/questions/67058960/getting-hwnd-of-a-hidden-window
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
// https://stackoverflow.com/questions/2719756/find-window-with-specific-text-for-a-process
// https://stackoverflow.com/questions/67058960/getting-hwnd-of-a-hidden-window
// https://stackoverflow.com/questions/17887211/c-sharp-get-process-window-titles/17890354#17890354
// https://stackoverflow.com/questions/5712930/get-handle-of-a-specific-window-using-user32-dll
// https://stackoverflow.com/questions/13547639/return-window-handle-by-its-name-title/45566499#45566499
// https://bytes.com/topic/net/answers/417241-find-instances-form-running-c-using-findwindowex

namespace VFAACP
{
	public class ProcUtils
	{
		public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private extern static bool EnumThreadWindows(int threadId, EnumWindowsProc callback, IntPtr lParam);

		[DllImport("user32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll")]
		static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
		private extern static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, Int32 lParam);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


		public const int WM_SYSCOMMAND = 0x0112;
		public const int SC_CLOSE = 0xF060;


		public static AsyncOpStatus StartProgram(string executable)
		{
			if (string.IsNullOrEmpty(executable))
			{
				string errorMessage = "ERROR: executable is null";
				FileSystemFuncs.AppendToLogFile(errorMessage);
				return new AsyncOpStatus(abortFlag: true, errorMessage: errorMessage);
			}
			try
			{
				FileSystemFuncs.AppendToLogFile("Starting " + executable);
				ProcessStartInfo pinfo = new ProcessStartInfo();
				pinfo.FileName = executable;
				//pinfo.Arguments = scriptFile;
				//pinfo.WorkingDirectory = ProgramSettings.ScriptFileDirectory;
				pinfo.CreateNoWindow = false;
				pinfo.UseShellExecute = false;
				pinfo.LoadUserProfile = true;
				pinfo.RedirectStandardOutput = false;
				pinfo.RedirectStandardError = false;
				using (Process p = new Process())
				{
					p.StartInfo = pinfo;
					p.Start();
				}
			}
			catch (Exception ex)
			{
				string errorMessage = "ERROR: " + ex.Message;
				FileSystemFuncs.AppendToLogFile(errorMessage);
				return new AsyncOpStatus(abortFlag: true, errorMessage: errorMessage);
			}
			return null;
		}

		public static IntPtr GetProcessMainWindowHandle(int processId)
		{   // I don't think this works
			IntPtr hwnd = IntPtr.Zero;
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList)
			{
				string name = process.ProcessName;
				string title = process.MainWindowTitle;
				if (process.Id == processId)
				{
					hwnd = process.MainWindowHandle;
				}
			}
			return hwnd;
		}
		public static IntPtr GetModuleWindowHandle(string moduleName)
		{
			IntPtr hwnd = IntPtr.Zero;
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList)
			{
				string name = process.ProcessName;
				string title = process.MainWindowTitle;
				ProcessModule module = null;
				try
				{
					module = process.MainModule;
					if (module != null)
					{
						if (!string.IsNullOrEmpty(module.ModuleName))
						{
							if (String.Equals(module.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase))
							{
								hwnd = process.MainWindowHandle;
								if (!IntPtr.Equals(hwnd, IntPtr.Zero))
								{
									return hwnd;
								}
							}
						}
					}
				}
				catch
				{
					;
				}
			}
			return IntPtr.Zero;
		}
		delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

		public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
		{
			var handles = new List<IntPtr>();
			Process process = Process.GetProcessById(processId);
			if (process == null)
				return null;
			foreach (ProcessThread thread in process.Threads)
				EnumThreadWindows(thread.Id, (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);

			return handles;
		}

		public static int GetModuleProcessId(string moduleName)
		{   // Note this can be time expensive, e.g. about 5 seconds
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList)
			{
				string name = process.ProcessName;
				string title = process.MainWindowTitle;
				ProcessModule module = null;
				try
				{
					module = process.MainModule;
					if (module != null)
					{
						if (!string.IsNullOrEmpty(module.ModuleName))
						{
							if (String.Equals(module.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase))
							{
								return process.Id;
							}
						}
					}
				}
				catch
				{
					;
				}
			}
			return 0;
		}

		public static List<IntPtr> GetModuleWindowHandles(string moduleName)
		{
			int processId = GetModuleProcessId(moduleName);
			if (processId == 0)
				return null;
			List<IntPtr> handles = (List<IntPtr>)EnumerateProcessWindowHandles(processId);
			return handles;

		}

		public static IntPtr GetWindowHandle(string className, string windowName)
		{
			IntPtr hwnd = FindWindow(className, windowName);
			return hwnd;
		}
		public static string GetWindowClassName(IntPtr hwnd)
		{
			StringBuilder className = new StringBuilder(256);
			GetClassName(hwnd, className, className.Capacity);
			return className.ToString();
		}
		public static void CloseWindow(IntPtr hwnd)
		{
			const uint WM_SYSCOMMAND = 0x0112;
			const int SC_CLOSE = 0xF060;
			PostMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0);
		}

		public static void ShowWindow(IntPtr hwnd)
		{
			const int SW_SHOWNORMAL = 1;
			ShowWindow(hwnd, SW_SHOWNORMAL);
		}
		public static void MinimizeWindow(IntPtr hwnd)
		{
			const int SW_MINIMIZE = 6;
			ShowWindow(hwnd, SW_MINIMIZE);
		}

		public static void MaximizeWindow(IntPtr hwnd)
		{
			const int SW_MAXIMIZE = 3;
			ShowWindow(hwnd, SW_MAXIMIZE);
		}

		public static void HideWindow(IntPtr hwnd)
		{
			ShowWindow(hwnd, 0);
		}

		public static void ShowWindowTopMost(IntPtr hwnd)
		{
			// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
			IntPtr HWND_TOPMOST = new IntPtr(-1);
			const UInt32 SWP_NOSIZE = 0x0001;
			const UInt32 SWP_NOMOVE = 0x0002;
			const UInt32 SWP_SHOWWINDOW = 0x0040;
			SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
		}

		public static void foo()
		{
			int pid = 10684; // from taskmanager

			Process[] processList = Process.GetProcesses();
			int num_processes = 0;
			foreach (Process process in processList)
			{
				++num_processes;
				string name = process.ProcessName;
				string title = process.MainWindowTitle;
				if (pid == process.Id)
				{
					System.IntPtr hwnd = process.MainWindowHandle;
					ProcessModule module = null;
					try
					{
						module = process.MainModule;
						if (module != null)
						{
							if (!string.IsNullOrEmpty(module.ModuleName))
							{
								if (module.ModuleName.StartsWith("NotePad"))
								{
									;
								}
							}
						}
					}
					catch { }
				}
			}
		}
	}

	public static class MyEnumWindows
	{
		private delegate bool EnumWindowsProc(IntPtr windowHandle, IntPtr lParam);

		[DllImport("user32")]
		private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

		[DllImport("user32.dll")]
		private static extern bool EnumChildWindows(IntPtr hWndStart, EnumWindowsProc callback, IntPtr lParam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessage1(IntPtr hWnd, int Msg, int wparam, int lparam);

		[DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SendMessage2(IntPtr hWnd, uint Msg, int wparam, StringBuilder lparam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public unsafe static extern int SendMessageTimeout(
										IntPtr hWnd,
										uint uMsg,
										uint wParam,
										StringBuilder lParam,
										uint fuFlags,
										uint uTimeout,
										void* lpdwResult);
		//public static extern int SendMessageTimeout(
		//								IntPtr hWnd,
		//								uint uMsg,
		//								uint wParam,
		//								StringBuilder lParam,
		//								uint fuFlags,
		//								uint uTimeout,
		//								int* lpdwResult);
		//public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

		private static string _titleStartsWith;
		private static List<string> _windowTitles = new List<string>();
		private static List<IntPtr> _windowHandles = new List<IntPtr>();
		private static int _windowCount = 0;

		public static IntPtr GetWindowHandle(string caption, string classNameStartsWith)
		{
			int n = MyEnumWindows.EnumerateWindows(caption, false);
			if (n != 0)
			{
				for (int i = 0; i < n; i++)
				{
					IntPtr hwnd = _windowHandles[i];
					string className = GetClassName(hwnd);
					if (className.StartsWith(classNameStartsWith))
						return hwnd;
				}
			}
			return IntPtr.Zero;
		}
		public static int EnumerateWindows(string titleStartsWith, bool includeChildren)
		{
			_titleStartsWith = titleStartsWith;
			_windowTitles = new List<string>();
			_windowHandles = new List<IntPtr>();
			EnumWindows(MyEnumWindows.EnumWindowsCallback, includeChildren ? (IntPtr) 1 : IntPtr.Zero);
			return _windowTitles.Count();
		}

		private static bool EnumWindowsCallback(IntPtr hwnd, IntPtr includeChildren)
		{
			string title = MyEnumWindows.GetWindowTitle(hwnd);
			_windowCount++;
			if (string.IsNullOrEmpty(title))
				return true;
			if (title.StartsWith(_titleStartsWith))
			{
				_windowTitles.Add(title);
				_windowHandles.Add(hwnd);
			}
			if (!includeChildren.Equals(IntPtr.Zero))
			{
				MyEnumWindows.EnumChildWindows(hwnd, MyEnumWindows.EnumWindowsCallback, IntPtr.Zero);
			}
			return true;
		}
		private static unsafe string GetWindowTitle(IntPtr hwnd)
		{
			const int WM_GETTEXT = 0x000D;
			const int WM_GETTEXTLENGTH = 0x000E;
			const int timeout_ms = 100;
			int len;
			if (SendMessageTimeout(hwnd, WM_GETTEXTLENGTH, 0, null, 2, timeout_ms, &len) == 0)
			{
				return null;
			}
			if (len == 0)
			{
				return null;
			}

			StringBuilder sb = new StringBuilder(len + 1);
			if (SendMessageTimeout(hwnd, WM_GETTEXT, (uint)sb.Capacity, sb, 2, timeout_ms, null) == 0)
			{
				return null;
			}

			return sb.ToString();
		}
		private static string GetWindowTitle_NG3(IntPtr hwnd)
		{
			const int WM_GETTEXT = 0x000D;
			const int WM_GETTEXTLENGTH = 0x000E;
			//const int MAX_STRING_SIZE = 9999;
			Int32 len = SendMessage1(hwnd, WM_GETTEXTLENGTH, 0, 0).ToInt32();
			if (len == 0)
				return String.Empty;
			StringBuilder sb = new StringBuilder(len + 1);
			SendMessage2(hwnd, WM_GETTEXT, sb.Capacity, sb);
			string title = sb.ToString();
			//IntPtr buffer = Marshal.AllocHGlobal((len + 1) * 2);
			//SendMessage(hwnd, WM_GETTEXT, len, buffer);
			//string title = Marshal.PtrToStringUni(buffer);
			//Marshal.FreeHGlobal(buffer);
			return title;
		}

		private static string GetWindowTitle_NG2(IntPtr hwnd)
		{
			//const int WM_GETTEXT = 0xD;
			//const int MAX_STRING_SIZE = 32768;
			//StringBuilder sb = new StringBuilder(MAX_STRING_SIZE);

			////SendMessage(hwnd, WM_GETTEXT, (IntPtr) sb.Length, sb);

			//string title = sb.ToString();
			//return title;
			return null;
		}

		private static string GetWindowTitle_NG1(IntPtr windowHandle)
		{   // I did not get this to work
			//uint SMTO_ABORTIFHUNG = 0x0002;
			//uint WM_GETTEXT = 0xD;
			//int MAX_STRING_SIZE = 32768;
			//IntPtr result;
			//string title = string.Empty;
			//IntPtr memoryHandle = Marshal.AllocCoTaskMem(MAX_STRING_SIZE);
			//Marshal.Copy(title.ToCharArray(), 0, memoryHandle, title.Length);
			//MyEnumWindows.SendMessageTimeout(windowHandle, WM_GETTEXT, (IntPtr)MAX_STRING_SIZE, memoryHandle, SMTO_ABORTIFHUNG, (uint)1000, out result);
			//title = Marshal.PtrToStringAuto(memoryHandle);
			//Marshal.FreeCoTaskMem(memoryHandle);
			//return title;
			return null;
		}

	}

	public static class FindWindow1
	{
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.

		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Auto)]
		static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

		// You can also call FindWindow(default(string), lpWindowName) or FindWindow((string)null, lpWindowName)

		public static IntPtr FindWindow1A(string caption)
		{
			IntPtr hwnd = FindWindowByCaption(IntPtr.Zero, caption);
			return hwnd;
		}
		public static IntPtr FindWindow1B(string className, string caption)
		{
			IntPtr hwnd = FindWindow(className, caption);
			return hwnd;
		}
	}
	public static class FindWindow2
	{
		[DllImport("USER32.DLL")]
		static extern IntPtr GetShellWindow();

		[DllImport("USER32.DLL")]
		static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

		[DllImport("user32.dll")]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		public static IntPtr GetHandleByTitle(string windowTitle)
		{	// I did not get this to work
			const int nChars = 256;

			IntPtr shellWindow = GetShellWindow();
			IntPtr found = IntPtr.Zero;

			EnumWindows(
				delegate (IntPtr hWnd, int lParam)
				{
					//ignore shell window
					if (hWnd == shellWindow) return true;

					//get Window Title
					StringBuilder sb = new StringBuilder(nChars);

					if (GetWindowText(hWnd, sb, nChars) > 0)
					{
						//Case insensitive match
						string text = sb.ToString();
						if (text.StartsWith("Mx"))
						{
							found = hWnd;
							return true;
						}
					}
					return true;

				}, 0
			);

			return found;
		}

		delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
	}
}

