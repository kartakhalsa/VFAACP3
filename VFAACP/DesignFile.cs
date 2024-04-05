using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace VFAACP
{
	public class DesignFile
	{
		private string _partName;

		public string GetPartName()
		{
			return _partName;
		}

		public DesignFile()
		{
			_partName = null;
		}

		public void ReadDesignCsvFile(string path)
		{
			// Try to read _partName from design file
			bool foundHeader = false;
			int linenum = 0;
			_partName = null;
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						linenum++;
						if (line.Length == 0)
							continue;
						if (line.ToUpper().Contains("[HEADER]"))
							foundHeader = true; // Simple validation
						string[] parseAry;
						parseAry = line.Split(new string[] { ",", "=" }, StringSplitOptions.None);
						if (parseAry.Length >= 2)
						{
							string tmp = parseAry[0].ToUpper();
							if ((tmp == "NAME") || (tmp == "JOB_PART_NUM"))
							{
								_partName = parseAry[1];
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error reading design file " + path + ": " + ex.Message);
			}
			if (linenum == 0)
				throw new Exception("Error reading design file " + path);
			if (!foundHeader)
				throw new Exception("Error reading design file " + path + ": [HEADER] not found");
		} // ReadDesignFile()

	} // class DesignFile

} // namespace VFAACP