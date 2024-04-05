using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;


namespace VFAACP
{
	class FileSystemFuncs
	{
		public static string ExternalDatabaseFilePath
		{
			get
			{
				return Path.Combine(ProgramSettings.PublishExternalDatabaseDirectory, ProgramSettings.PublishExternalDatabaseFile);
			}
		}

		public static bool ExternalDatabaseFileExists
		{
			get
			{
				string path = ExternalDatabaseFilePath;
				return File.Exists(path);
			}
		}

		public static void WriteMeasSetupFile(Recipe recipe)
		{
			string path = Path.Combine(ProgramSettings.MeasSetupDirectory, ProgramSettings.MeasSetupFile);
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.WriteLine("[MeasurementSetup]");
					if (!string.IsNullOrEmpty(MeasSetup.RecipeFile))
						sw.WriteLine("RecipeFile=" + MeasSetup.RecipeFile);
					if (!string.IsNullOrEmpty(MeasSetup.PromptOutputFile))
						sw.WriteLine("PromptOutputFile=" + MeasSetup.PromptOutputFile);
					if (!string.IsNullOrEmpty(MeasSetup.DesignFile))
						sw.WriteLine("DesignFile=" + MeasSetup.DesignFile);
					if (!string.IsNullOrEmpty(MeasSetup.DesignName))
						sw.WriteLine("DesignName=" + MeasSetup.DesignName);
					if (!string.IsNullOrEmpty(MeasSetup.ProtocolNumber))
						sw.WriteLine("ProtocolNumber=" + MeasSetup.ProtocolNumber);
					if (!string.IsNullOrEmpty(MeasSetup.LotNumber))
						sw.WriteLine("LotNumber=" + MeasSetup.LotNumber);
					if (!string.IsNullOrEmpty(MeasSetup.MoldPlateID))
						sw.WriteLine("MoldPlateID=" + MeasSetup.MoldPlateID);
					if (!string.IsNullOrEmpty(MeasSetup.OperatorName))
						sw.WriteLine("OperatorName=" + MeasSetup.OperatorName);
					if (!string.IsNullOrEmpty(MeasSetup.ToleranceFile))
						sw.WriteLine("ToleranceFile=" + MeasSetup.ToleranceFile);
					if (!string.IsNullOrEmpty(recipe.ResultTxtFile))
						sw.WriteLine("ResultTxtFile=" + recipe.ResultTxtFile);
					if (!string.IsNullOrEmpty(recipe.ResultCsv1File))
						sw.WriteLine("ResultCsv1File=" + recipe.ResultCsv1File);
					if (!string.IsNullOrEmpty(recipe.ResultCsv2File))
						sw.WriteLine("ResultCsv2File=" + recipe.ResultCsv2File);
					if (!string.IsNullOrEmpty(recipe.ResultBmp1File))
						sw.WriteLine("ResultBmp1File=" + recipe.ResultBmp1File);
					if (!string.IsNullOrEmpty(recipe.ResultBmp2File))
						sw.WriteLine("ResultBmp2File=" + recipe.ResultBmp2File);
					sw.WriteLine("TestWithoutInstrument=" + ProgramSettings.TestWithoutInstrument.ToString());
					AppendToLogFile("Wrote MeasurementSetupFile " + path);
				}
			}
			catch (Exception ex)
			{
				string msg = "Error writing MeasSetupFile " + path + ": " + ex.Message;
				AppendToLogFile(msg);
			}
		}   // WriteMeasSetupFile()

		public static string WriteSystemLoadPosFile(double x, double y, double z, double r, double p)
		{
			string errmsg = null;
			//string fn = ProgramSettings.SystemLoadPosFile;
			//try
			//{
			//    using (StreamWriter sw = new StreamWriter(fn))
			//    {
			//        sw.WriteLine(x.ToString("F4"));
			//        sw.WriteLine(y.ToString("F4"));
			//        sw.WriteLine(z.ToString("F4"));
			//        sw.WriteLine(r.ToString("F4"));
			//        sw.WriteLine(p.ToString("F4"));
			//        AppendToLogFile("Wrote SystemLoadPosFile " + fn);
			//    }
			//}
			//catch (Exception ex)
			//{
			//    AppendToLogFile("Error writing SystemLoadPosFile " + fn + ": " + ex.Message);
			//}
			return errmsg;
		}   // WriteSystemLoadPosFile()

		public static string ReadSystemLoadPosFile(out double x, out double y, out double z, out double r, out double p)
		{
			string errmsg = null;
			x = y = z = r = p = 0.0;
			//string fn = ProgramSettings.SystemLoadPosFile;
			//StreamReader sr = null;
			//try
			//{
			//    sr = new StreamReader(fn);
			//    x = double.Parse(sr.ReadLine());
			//    y = double.Parse(sr.ReadLine());
			//    z = double.Parse(sr.ReadLine());
			//    r = double.Parse(sr.ReadLine());
			//    p = double.Parse(sr.ReadLine());
			//}
			//catch (Exception ex)
			//{
			//    errmsg = "Error reading file " + fn;
			//    AppendToLogFile(errmsg + ": " + ex.Message);
			//}
			//finally
			//{
			//    if (sr != null)
			//    {
			//        sr.Close();
			//        sr.Dispose();
			//    }
			//}
			return errmsg;
		} // ReadSystemLoadPosFile()

		public static string CopyToInterim(int SiteNum)
		{
			try
			{
				string src = ProgramSettings.SiteDataDirectory;
				string subDir = "Site" + SiteNum.ToString("00");
				string dst = Path.Combine(ProgramSettings.InterimFileRootDirectory, subDir);
				CopyFolder(src, dst);
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}   // CopyToInterim()

		public static void CopyFolder(string SourceFolder, string DestFolder)
		{
			// This is a recursive method that will copy a directory and 
			// all subfolders and files
			// Note that this method will throw an exception, so make sure to handle
			// exceptions with the calling code

			if (!Directory.Exists(DestFolder)) Directory.CreateDirectory(DestFolder);
			string[] files = Directory.GetFiles(SourceFolder);
			foreach (string file in files)
			{
				string name = Path.GetFileName(file);
				string dest = Path.Combine(DestFolder, name);
				File.Copy(file, dest, true);
			}
			string[] folders = Directory.GetDirectories(SourceFolder);
			foreach (string folder in folders)
			{
				string name = Path.GetFileName(folder);
				string dest = Path.Combine(DestFolder, name);
				CopyFolder(folder, dest);
			}
		}   // CopyFolder()

		public static int CopyFolderUsingFileFilter(string SourceFolder, string DestFolder, string FileFilter)
		{
			// This is a recursive method that will copy a directory and 
			// all subfolders and files that match the FileFilter parameter, ie *.txt
			// Note that this method will throw an exception, so make sure to handle
			// exceptions with the calling code
			if (!Directory.Exists(DestFolder)) Directory.CreateDirectory(DestFolder);
			string[] files = Directory.GetFiles(SourceFolder, FileFilter);
			foreach (string file in files)
			{
				string name = Path.GetFileName(file);
				string dest = Path.Combine(DestFolder, name);
				File.Copy(file, dest, true);
			}
			string[] folders = Directory.GetDirectories(SourceFolder);
			foreach (string folder in folders)
			{
				string name = Path.GetFileName(folder);
				string dest = Path.Combine(DestFolder, name);
				CopyFolderUsingFileFilter(folder, dest, FileFilter);
			}
			return Directory.GetFiles(DestFolder,FileFilter,SearchOption.AllDirectories).Count();
		}   // CopyFolderUsingFileFilter()

		public static string DeleteDirContents(string dir)
		{
			try
			{
				foreach (string d in Directory.GetDirectories(dir))
				{
					Directory.Delete(d, true);
				}
				foreach (string f in Directory.GetFiles(dir))
				{
					File.Delete(f);
				}
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			return null;
		}   // DeleteDirContents()

		public static string ClearInterimSiteData(int SiteNum)
		{
			try
			{
				// Look for folder matching SiteNum
				string subDir = "Site" + SiteNum.ToString("00");
				string fileAndPath = ProgramSettings.InterimFileRootDirectory + "\\" + subDir;
				if (Directory.Exists(fileAndPath))
				{
					foreach (string f in Directory.GetFiles(fileAndPath))
					{
						File.Delete(f);
					}
					Directory.Delete(fileAndPath);
				}
				return "";
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}   // ClearSite()

		public static void DeleteExternalDatabaseFile()
		{
			// Does not throw an exception if file does not exist
			try
			{
				File.Delete(ExternalDatabaseFilePath);
			}
			catch { }
		} // DeleteExternalDatabaseFile()

		// Returns error message
		public static string MakeSummaryFile(List<Site> sites, string csvFile, out int numSitesInSummary)
		{
			string version = Program.GetAssemblyVersion();
			string vfa = ProgramSettings.InstrumentName;
			string protocolNumber = null;
			string lotNumber = MeasSetup.LotNumber;
			string moldPlateID = MeasSetup.MoldPlateID;
			string operatorName = MeasSetup.OperatorName;
			string recipeName = MeasSetup.RecipeFile;
			string designFile = MeasSetup.DesignFile;
			string designName = MeasSetup.DesignName;
			string jobFile = MeasSetup.CurrentRecipe.JobFile;
			if (MeasSetup.IsToolingCalibrationSetup)
			{
				protocolNumber = MeasSetup.ProtocolNumber;
			}
			if (string.IsNullOrEmpty(protocolNumber))
				protocolNumber = "*";
			if (string.IsNullOrEmpty(lotNumber))
				lotNumber = "*";
			if (string.IsNullOrEmpty(moldPlateID))
				moldPlateID = "*";
			if (string.IsNullOrEmpty(operatorName))
				operatorName = "*";
			if (string.IsNullOrEmpty(designName))
				designName = "*";
			if (string.IsNullOrEmpty(jobFile))
				jobFile = "*";

			numSitesInSummary = 0;
			if (sites == null)
				return "";
			int numSites = sites.Count;
			if (numSites == 0)
				return "";
			if (csvFile == "")
				return "";
			try
			{
				string hdrLine1 = "";
				List<string> dataLineList = new List<string>();
				foreach (Site s in sites)
				{
					int siteNum = s.Index;
					string subDir = "Site" + siteNum.ToString("00");
					string subDirPath = ProgramSettings.InterimFileRootDirectory + "\\" + subDir;
					if (Directory.Exists(subDirPath))
					{
 						string csvFilePath = subDirPath + "\\" + csvFile;
						FileInfo fi = new FileInfo(csvFilePath);
						if (fi.Exists)
						{
							DateTime ftimestamp = fi.LastWriteTime;
							string fdate = ftimestamp.ToString(ProgramSettings.SummaryFileDateFormat);
							string ftime = ftimestamp.ToString(ProgramSettings.SummaryFileTimeFormat);
							string dataLine1 = "";
							try
							{
								using (StreamReader sr = new StreamReader(csvFilePath))
								{
									// Read hdrLine1 from first CSV file
									if (hdrLine1.Length == 0)
										hdrLine1 = sr.ReadLine();
									else
										sr.ReadLine();
									dataLine1 = sr.ReadLine();
								}
							}
							catch (Exception ex)
							{
								return ex.Message;
							}
 							// Insert other column values
							// (must match with column headings)
							string dataLine2 = "";
							dataLine2 += version + ",";
							dataLine2 += vfa + ",";
							dataLine2 += protocolNumber + ",";
							dataLine2 += lotNumber + ",";
							dataLine2 += moldPlateID + ",";
							dataLine2 += operatorName + ",";
							dataLine2 += recipeName + ",";
							if (!string.IsNullOrEmpty(designFile))
								dataLine2 += designFile + ",";
							else
								dataLine2 += designName + ",";
							dataLine2 += jobFile + ",";
							dataLine2 += siteNum.ToString() + ",";
							dataLine2 += fdate + ",";
							dataLine2 += ftime + ",";
							dataLine2 += dataLine1;
							dataLineList.Add(dataLine2);
							++numSitesInSummary;
						}
					}
				}
				// ncia = number of columns inserted by ACP
				int ncia = 0;
				// Insert other column headings
				// (must match with column values)
				string hdrLine2 = "";
				hdrLine2 += "\"Version\","; ++ncia;
				hdrLine2 += "\"VFA\","; ++ncia;
				hdrLine2 += "\"Protocol\","; ++ncia;
				hdrLine2 += "\"Lot\","; ++ncia;
				hdrLine2 += "\"MoldPlate\","; ++ncia;
				hdrLine2 += "\"Operator\","; ++ncia;
				hdrLine2 += "\"Recipe\","; ++ncia;
				hdrLine2 += "\"DesignFile\","; ++ncia;
				hdrLine2 += "\"JobFile\","; ++ncia;
				hdrLine2 += "\"Site\","; ++ncia;
				hdrLine2 += "\"Date\","; ++ncia;
				hdrLine2 += "\"Time\","; ++ncia;
				hdrLine2 += hdrLine1;

				// Make summary CSV file
				string summaryCsvFilePath = ProgramSettings.InterimFileRootDirectory + "\\" + "Summary.csv";
				using (StreamWriter sw = new StreamWriter(summaryCsvFilePath))
				{
					sw.WriteLine(hdrLine2);
					foreach (string line in dataLineList)
					{
						sw.WriteLine(line);
					}
					// [OPTIONAL] Add Excel statistics calculation formulas
					if ((ProgramSettings.SummaryFileAddStats) && (numSitesInSummary > 1))
					{
						int numCols = (hdrLine2.Split(new string[] { "," }, StringSplitOptions.None)).Length;
						int numRows = numSitesInSummary + 1;
						// c1 and c2 are 0-based column indexes for the range of columns needing stats.
						// Skip columns inserted by ACP (var "ncia"),
						// and also these columns created by scripts:
						// "Instr Axis Offset", "Site Axis Offset", "Design Code", "Shape"
						const int nscs = 4; // Number of Script Columns to Skip
						// Skip trailing column "Status"
						const int ntcs = 1; // Number of Trailing Columns to Skip
						int c1 = ncia + nscs;
						int c2 = numCols - ntcs - 1;
						string statsLine = "";
						statsLine = FormatStatsLine("MAX", c1, c2, numCols, numRows);
						sw.WriteLine(statsLine);
						statsLine = FormatStatsLine("MIN", c1, c2, numCols, numRows);
						sw.WriteLine(statsLine);
						statsLine = FormatStatsLine("AVERAGE", c1, c2, numCols, numRows);
						sw.WriteLine(statsLine);
						statsLine = FormatStatsLine("STDEV", c1, c2, numCols, numRows);
						sw.WriteLine(statsLine);
					}
				}
			}
			catch(Exception ex)
			{
				return ex.Message;
			}
			return null;
		}   // MakeSummaryFile()

		private static string GetColName(int colIndex)
		{
			// Returns a letter or letters to refer to an Excel column.
			// colIndex is the 0-based index.
			// 0..25 => "A".."Z"
			// 26..51 => "AA".."AZ"
			// etc.
			string colName = "";
			if (colIndex <= 25)
			{   // One letter
				colName = Convert.ToChar(colIndex + 97).ToString().ToUpper();
			}
			else if (colIndex <= (26 * 26))
			{   // Two letters
				int i = (colIndex / 26) - 1;
				int j = colIndex % 26;
				colName = Convert.ToChar(i + 97).ToString().ToUpper() +
							Convert.ToChar(j + 97).ToString().ToUpper();
			}
			else
			{
				colName = "";
			}
			return colName;
		}   // GetColName()

		private static string GetRowRangeForColIndex(int colIndex, int r1, int r2)
		{
			// Returns an Excel row range for a column index, e.g. "(D2,D5)"
			// colIndex is the 0-based column index
			// r1 and r2 are 0-based row indexes for the range of rows
			string colName = GetColName(colIndex);
			// To 1-based rows
			r1++;
			r2++;
			string rowRange = "(" + colName + r1.ToString() + ":" + colName + r2.ToString() + ")";
			return rowRange;
		}   // GetRowRangeForColIndex()

		private static string FormatStatsLine(string funcName, int c1, int c2, int numCols, int numRows)
		{
			// Format a line of Excel formulas to calculate statistics.
			// funcName is the Excel recognized function name, ie. MAX, MIN, STDEV
			// c1 and c2 are 0-based column indexes for the range of columns needing stats
			// numCols is the number of columns of data
			// numRows is the number of rows of data

			string statsLine = "";
			for (int colIndex = 0; colIndex < numCols; colIndex++)
			{
				string value = "";
				if (colIndex == 0)
				{
					value = "\"" + funcName + "\"";
				}
				else if ((colIndex >= c1) & (colIndex <= c2))
				{
					string range = GetRowRangeForColIndex(colIndex, 1, numRows - 1);
					value = "\"=" + funcName + range + "\"";
				}
				statsLine += value;
				if (colIndex < (numCols - 1))
					statsLine += ",";
			}
			return statsLine;
		}   // FormatStatsLine()


		// PublishFiles
		// If successful, returns number of files copied as a string.
		// Otherwise, returns an error message.
		public static string PublishFiles(int SiteNum, string DstRootDir, string FilterString)
		{
			string srcDir; 
			string dstDir;
			string subDir;

			int filesCopied = 0;

			try
			{
				// Copy files and folders from InterimRootFile directory to Dir, using FilterString to control which files are copied
				// If SiteNum is -1, then copy files in root dir (e.g. summary CSV file)
				if (SiteNum == -1)
				{
					// Copy files non-recursively; just files in the root source dir
					subDir = "";
					dstDir = DstRootDir;

					srcDir = Path.Combine(ProgramSettings.InterimFileRootDirectory, subDir);

					DirectoryInfo srcDirInfo = new DirectoryInfo(srcDir);

					if (srcDirInfo.Exists)
					{
						if (!Directory.Exists(dstDir))
							Directory.CreateDirectory(dstDir);
						DirectoryInfo dstDirInfo = new DirectoryInfo(dstDir);

						FileInfo[] files = srcDirInfo.GetFiles(FilterString);

						// Num files published
						int nfp = 0;
						foreach (FileInfo fi in files)
						{
							string destFile = "";
							if (fi.Name.CompareTo("Summary.csv") == 0)
							{
								// Summary.csv file is renamed so root matches destination folder name
								destFile = dstDir + "\\" + dstDirInfo.Name + ".csv";
							}
							else
							{
								destFile = dstDir + "\\" + fi.Name;
							}
							File.Copy(fi.FullName, destFile, true);
							nfp++;
						}

						return nfp.ToString();
					}
					else
					{
						return "0";
					}
				}
				else
				{
					// Recursively copy subfolders
					subDir = "Site" + SiteNum.ToString("00");
					dstDir = Path.Combine(DstRootDir, subDir);

					srcDir = Path.Combine(ProgramSettings.InterimFileRootDirectory, subDir);

					filesCopied = CopyFolderUsingFileFilter(srcDir, dstDir, FilterString);

					return filesCopied.ToString();
				}

			}
			catch(Exception ex)
			{
				return ex.Message;
			}
		}   // PublishFiles()

		public static bool AnyFilesInSiteIterimFolder(int siteNum)
		{
			try
			{
				bool result = false;
				string subDir = "Site" + siteNum.ToString("00");
				string path = Path.Combine(ProgramSettings.InterimFileRootDirectory, subDir);
				if (Directory.Exists(path))
				{
					DirectoryInfo di = new DirectoryInfo(path);
					if (di.GetFiles().Count() > 0)
						result = true;
				}
				return result;
			}
			catch
			{
				return false;
			}
		}   // AnyFilesInSiteIterimFolder()

		public static string MakeLogFileName()
		{
			string prefix = "VFAACP_LOG_";
			string timestamp = "";
			DateTime dt = DateTime.Now;
			timestamp = dt.ToString(ProgramSettings.LogFilenameDateTimeFormat);
			return prefix + timestamp + ".txt";
		}

		public static string MakeMxLogFileName()
		{
			string prefix = "VFAACP_LOG_";
			string timestamp = "";
			DateTime dt = DateTime.Now;
			timestamp = dt.ToString(ProgramSettings.LogFilenameDateTimeFormat);
			return prefix + timestamp + ".txt";
		}

		public static string CreateLogFile(string path)
		{
			try
			{
				FileInfo fi = new FileInfo(path);
				FileStream fs = fi.Create();
				fs.Close();
				fs.Dispose();
				return path;
			}
			catch
			{
				throw;
			}
		}
		public static string GetAcpLogPath()
		{
			const string prefix = "VFAACP_LOG_";
			string timestamp = "";
			timestamp = Program.StartTime.ToString(ProgramSettings.LogFilenameDateTimeFormat);
			string fn = prefix + timestamp + ".txt";
			string path = Path.Combine(ProgramSettings.AcpLogFileDirectory, fn);
			return path;
		}

		public static string GetMxLogPath()
		{
			const string prefix = "MX_LOG_";
			string timestamp = "";
			timestamp = Program.StartTime.ToString(ProgramSettings.LogFilenameDateTimeFormat);
			string fn = prefix + timestamp + ".txt";
			string path = Path.Combine(ProgramSettings.MxLogFileDirectory, fn);
			return path;
		}


		public static string AppendToLogFile(string line)
		{
			// Append to program log file. Create the file if it does not exist.
			try
			{
				DateTime dt;
				string timestamp = "";
				dt = DateTime.Now;
				timestamp = dt.ToString(ProgramSettings.LogEntryDateTimeFormat).ToUpper();

				FileInfo fi = new FileInfo(Program.LogFile);
				if (!fi.Exists)
				{
					FileStream fs = fi.Create();
					fs.Close();
					fs.Dispose();
					using (StreamWriter sw = fi.AppendText())
					{
						sw.WriteLine(timestamp + "Created log file");
						sw.WriteLine(timestamp + line);
					}
				}
				else
				{
					using (StreamWriter sw = fi.AppendText())
					{
						sw.WriteLine(timestamp + line);
					}
				}
				return null;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}   // AppendToLogFile()

		public static void AppendToLogFile2(string buf)
		{
			// Append to program log file.
			FileInfo fi = new FileInfo(Program.LogFile);
			if (fi.Exists)
			{
				using (StreamWriter sw = fi.AppendText())
				{
					sw.Write(buf);
				}
			}
		}   // AppendToLogFile2()
		
		private static void CopyFile(string fn, string src_dir, string dst_dir)
		{
			string msg;
			string src = src_dir + "\\" + fn;
			string dst = dst_dir + "\\" + fn;
			FileInfo fi = new FileInfo(src);
			if (fi.Exists)
			{
				try
				{
					System.IO.File.Copy(src, dst, true);
				}
				catch (Exception ex)
				{
					msg = "Failed to copy file " + fn + " from " + src_dir + " to " + dst_dir + "\n" + ex.Message;
					MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				msg = "File " + src + " does not exist";
				MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}   // CopyFile()


		public static bool IsDirectoryWritable(string path)
		{
			try
			{
				using (FileStream fs = File.Create(
					Path.Combine(
						path,
						Path.GetRandomFileName()
					),
					1,
					FileOptions.DeleteOnClose)
				)
				{ }
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool DirectoryExistsAndIsNotEmpty(string path)
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo di = new DirectoryInfo(path);
				FileInfo[] files = di.GetFiles();
				DirectoryInfo[] subdirs = di.GetDirectories();
				return (files.Length > 0) || (subdirs.Length > 0);
			}
			else
			{
				return false;
			}
		}   // DirectoryExistsAndIsNotEmpty()

			// Recursive function.
		public static string FindFile(string dir, string filename)
		{
			string path = dir + "\\" + filename;
			if (File.Exists(path))
			{
				return path;
			}
			try
			{
				foreach (string d in Directory.GetDirectories(dir))
				{
					path = FindFile(d, filename);
					if (path != "")
					{
						return path;
					}
				}
			}
			catch (System.Exception ex)
			{
				AppendToLogFile("Error in FindFile: " + ex.Message);
			}
			return "";
		}   // FindFile()

		public static void WriteSiteSetupFile(Site site)
		{
			string path = Path.Combine(ProgramSettings.SiteDataDirectory, ProgramSettings.SiteSetupFile);
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.WriteLine("[SiteSetup]");
					sw.WriteLine("SiteNum=" + site.Index.ToString());
					sw.WriteLine("AxisOffset=" + site.Axis.ToString());
					sw.WriteLine("TestWithoutInstrument=" + ProgramSettings.TestWithoutInstrument.ToString());
				}
				AppendToLogFile("Wrote SiteSetupFile " + path);
			}
			catch (Exception ex)
			{
				AppendToLogFile("Error writing SiteSetupFile " + path + ": " + ex.Message);
			}
		} // WriteSiteSetupFile()

		public static void MakeWritable(string path)
		{
			FileInfo info = new FileInfo(path);
			info.Attributes = FileAttributes.Normal;
			//DirectoryInfo info = new DirectoryInfo(path);
			//DirectorySecurity sec = info.GetAccessControl();
			//sec.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
			//info.SetAccessControl(sec);
		}

	} // Class FileSystemFuncs
}
