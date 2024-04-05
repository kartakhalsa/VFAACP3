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

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

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

		public static void ShowWindowInTaskbar(IntPtr hwnd, bool show)
		{	// Change style of window to control if it appears in taskbar
			const int GWL_EXSTYLE = -20;
			const int WS_EX_APPWINDOW = 0x00040000;
			const int WS_EX_TOOLWINDOW = 0x00000080;
			const UInt32 SWP_NOSIZE = 0x0001;
			const UInt32 SWP_NOMOVE = 0x0002;
			const UInt32 SWP_NOZORDER = 0x0004;
			const UInt32 SWP_FRAMECHANGED = 0x0020;
			if (show)
			{
				SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_APPWINDOW);
			}
			else
			{
				SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_TOOLWINDOW);
			}
			SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOZORDER | SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);
		}
		public static void SetWindowStyleApp(IntPtr hwnd)
		{   // Change style to app window
			const int GWL_EXSTYLE = -20;
			const int WS_EX_APPWINDOW = 0x00040000;
			const UInt32 SWP_NOSIZE = 0x0001;
			const UInt32 SWP_NOMOVE = 0x0002;
			const UInt32 SWP_NOZORDER = 0x0004;
			const UInt32 SWP_FRAMECHANGED = 0x0020;
			SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_APPWINDOW);
			SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOZORDER | SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);
		}
		public static void SetWindowStyleTool(IntPtr hwnd)
		{   // Change style to tool window
			const int GWL_EXSTYLE = -20;
			const int WS_EX_TOOLWINDOW = 0x00000080;
			const UInt32 SWP_NOSIZE = 0x0001;
			const UInt32 SWP_NOMOVE = 0x0002;
			const UInt32 SWP_NOZORDER = 0x0004;
			const UInt32 SWP_FRAMECHANGED = 0x0020;
			SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_TOOLWINDOW);
			SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOZORDER | SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);
		}

		public static void SetWindowPosOnScreen(IntPtr hwnd, bool onScreen)
		{
			const UInt32 SWP_NOSIZE = 0x0001;
			const UInt32 SWP_NOZORDER = 0x0004;
			int X;
			if (onScreen)
			{
				X = 0;
			}
			else
			{
				System.Drawing.Rectangle rect = Screen.PrimaryScreen.WorkingArea;
				X = rect.Right;
			}
			SetWindowPos(hwnd, IntPtr.Zero, X, 0, 0, 0, SWP_NOZORDER | SWP_NOSIZE);
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

		private static string _titleStartsWith;
		private static List<string> _windowTitles = new List<string>();
		private static List<IntPtr> _windowHandles = new List<IntPtr>();
		private static int _windowCount = 0;

		public static IntPtr GetWindowHandle(string caption, string classNameStartsWith)
		{
			int n = EnumerateWindows(caption, false);
			if (n != 0)
			{
				for (int i = 0; i < n; i++)
				{
					IntPtr hwnd = _windowHandles[i];
					string className = ProcUtils.GetWindowClassName(hwnd);
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

	}
}

