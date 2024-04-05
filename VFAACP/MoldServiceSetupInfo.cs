using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VFAACP
{
	public class MoldServiceSetupInfo
	{
		public string MoldPlateID { get; set; }
		public string DesignCode { get; set; }
		public string DesignFile { get; set; }
		public string RecipeFile { get; set; }
		public string TrayFile { get; set; }
		public string OperatorName { get; set; }
		public string ErrorMessage { get; set; }

		public MoldServiceSetupInfo()
		{
			MoldPlateID = null;
			DesignCode = null;
			DesignFile = null;
			RecipeFile = null;
			TrayFile = null;
			OperatorName = null;
			ErrorMessage = null;
		}

		public static MoldServiceSetupInfo ReadFromFile(string path)
		{
			MoldServiceSetupInfo info = new MoldServiceSetupInfo();
			try
			{
				IniFileReader ini = new IniFileReader();
				ini.IgnoreCase = true;
				ini.ReadFile(path);
				info.MoldPlateID = ini.GetValue("", "MoldPlateID", required: true);
				info.DesignCode = ini.GetValue("", "DesignCode", required: true);
				info.DesignFile = ini.GetValue("", "DesignFile", required: true);
				info.RecipeFile = ini.GetValue("", "RecipeFile", required: true);
				info.TrayFile = ini.GetValue("", "TrayFile", required: true);
				info.OperatorName = ini.GetValue("", "OperatorName", required: false);
				info.ErrorMessage = ini.GetValue("", "ErrorMessage", required: false);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error reading file {0}: {1}", path, ex.Message));
			}
			return info;
		} // ReadFromFile()

		public void Validate()
		{
			const int MoldPlateID_MaxLen = 40;
			const int DesignCode_MaxLen = 40;
			const int OperatorName_MaxLen = 40;

			if (string.IsNullOrEmpty(MoldPlateID))
				throw new Exception("MoldPlateID must not be null.");
			if (MoldPlateID.Length > MoldPlateID_MaxLen)
				throw new Exception("MoldPlateID must not be longer than " + MoldPlateID_MaxLen + " characters.");

			if (string.IsNullOrEmpty(DesignCode))
				throw new Exception("DesignCode must not be null.");
			if (DesignCode.Length > DesignCode_MaxLen)
				throw new Exception("DesignCode must not be longer than " + DesignCode_MaxLen + " characters.");

			if ((OperatorName != null) && (OperatorName.Length > OperatorName_MaxLen))
				throw new Exception("OperatorName must not be longer than " + OperatorName_MaxLen + " characters.");
		}

		public string AsTextLines
		{
			get
			{
				string lines = "";
				lines += "MoldPlateID = " + MoldPlateID + "\n";
				lines += "DesignCode = " + DesignCode + "\n";
				lines += "DesignFile = " + DesignFile + "\n";
				lines += "RecipeFile = " + RecipeFile + "\n";
				lines += "TrayFile = " + TrayFile + "\n";
				lines += "OperatorName = " + OperatorName + "\n";
				lines += "ErrorMessage = " + ErrorMessage + "\n";
				return lines;
			}
		}
	}
}
