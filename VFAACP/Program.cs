using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace VFAACP
{
	public static class Program
	{
		public static string SettingsFilename = "VFAACP_Settings.ini";
		public static string SettingsFile { get; set; }
		public static string LogFile { get; set; }


		public static bool SecureDesktop { get; set; }
		public static bool AutoStartMx { get; set; }
		public static DateTime StartTime { get; set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				foreach (string arg in args)
				{
					if (string.Compare(arg, "SecureDesktop", ignoreCase: true) == 0)
					{
						SecureDesktop = true;
					}
					else if (string.Compare(arg, "AutoStartMx", ignoreCase: true) == 0)
					{
						AutoStartMx = true;
					}
				}
				SettingsFile = Path.Combine(Application.StartupPath, SettingsFilename);
				StartTime = DateTime.Now;
				Application.Run(new UIForm());
			}
			catch (Exception ex)
			{
				string msg = "";
				msg += GetAssemblyProduct();
				msg += Environment.NewLine;
				msg += "Version " + GetAssemblyVersion();
				msg += Environment.NewLine;
				msg += Environment.NewLine;
				msg += ex.Message;
				MessageBox.Show(msg, "Global Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		public static string GetAssemblyVersion()
		{
			return String.Format("{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
		}

		public static string GetAssemblyTitle()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
			if (attributes.Length > 0)
			{
				AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
				if (titleAttribute.Title != "")
				{
					return titleAttribute.Title;
				}
			}
			return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
		}

		public static string GetAssemblyDescription()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
			if (attributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyDescriptionAttribute)attributes[0]).Description;
		}

		public static string GetAssemblyProduct()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			if (attributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyProductAttribute)attributes[0]).Product;
		}

		public static string GetAssemblyCopyright()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
			if (attributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
		}

		public static string GetAssemblyCompany()
		{
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
			if (attributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCompanyAttribute)attributes[0]).Company;
		}

	} // class Program
}
