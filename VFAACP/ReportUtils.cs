using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFAACP
{
	public class ReportUtils
	{
		// Column 1 items
		private const string _designLabel   = "Met File  = ";
		private const string _recipeLabel   = "Recipe    = ";
		private const string _tolFileLabel  = "Tol File  = ";
		private const string _timeDateLabel = "Time/Date = ";
		private const string _trayLabel     = "Tray      = ";
		private const string _vfaLabel      = "VFA       = ";

		// Column 2 items
		//private const string protocolLabel = "Protocol      = ";
		//private const string lotNumLabel   = "Lot Num       = ";
		//private const string testTrayID    = "Test Tray ID  = ";
		//private const string moldPlateID   = "Mold Plate ID = ";

		public static string GetMeasContextDescr(int maxLineLen)
		{
			// Uses these static classes:
			// MeasSetup
			// ProgramSettings

			string recipeName = MeasSetup.RecipeNameRoot;
			string designFile = MeasSetup.DesignFile;
			string designName = MeasSetup.DesignName;
			string tolFile = MeasSetup.ToleranceFileRoot;
			if (string.IsNullOrEmpty(tolFile))
				tolFile = "*";
			string trayName = MeasSetup.TrayName;
			string instrName = ProgramSettings.InstrumentName;

            
			DateTime dt = DateTime.Now;
			string time_date = dt.ToString(ProgramSettings.ReportDateTimeFormat).ToUpper();
			string text;
			text = "MEASUREMENT CONTEXT\n";
			if (!string.IsNullOrEmpty(designFile))
				text += FormatDesignNameAsOneOrTwoLines(designFile, maxLineLen);
			else
				text += FormatDesignNameAsOneOrTwoLines(designName, maxLineLen);
			text += _recipeLabel   + recipeName + "\n";
			text += _tolFileLabel  + tolFile    + "\n";
			text += _timeDateLabel + time_date  + "\n";
			text += _trayLabel     + trayName   + "\n";
			text += _vfaLabel      + instrName  + "\n";

			return text;
		}

		public static string FormatDesignNameAsOneOrTwoLines(string path, int maxLineLen)
		{
			string line1, line2;
			int n = maxLineLen - _designLabel.Length;
			if (path.Length <= n)
			{
				line1 = _designLabel + path;
				line2 = null;
			}
			else
			{   // Path is too long to fit on one line
				line1 = _designLabel + path.Substring(0, n);
				string indent = new string(' ', _designLabel.Length);
				line2 = indent + path.Substring(n);
				if (line2.Length > maxLineLen)
					line2 = line2.Substring(0, maxLineLen);
			}
			if (line2 == null)
				return line1 + "\n";
			else
				return line1 + "\n" + line2 + "\n";
			// FormatDesignNameAsOneOrTwoLines2
		}

	}
}
