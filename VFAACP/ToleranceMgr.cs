using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VFAACP
{
	public class ToleranceItem
	{	// This class encapsulates tolerance values for one numeric result
		public string ResultName { get; set; }
		public double LowTol { get; set; }
		public double HighTol { get; set; }

		public ToleranceItem()
		{
			ResultName = "";
			LowTol = Double.NaN;
			HighTol = Double.NaN;
		}

		public ToleranceItem(string resultName, double lowTol, double highTol)
		{
			ResultName = resultName;
			LowTol = lowTol;
			HighTol = highTol;
		}

		public bool ValueInTolerance(double val)
		{
			if (Double.IsNaN(val))
				return false;
			if (!Double.IsNaN(LowTol) && (val < LowTol))
				return false;
			if (!Double.IsNaN(HighTol) && (val > HighTol))
				return false;
			return true;
		}

		public string Range()
		{
			string s = "[";
			if (!Double.IsNaN(LowTol))
				s += LowTol.ToString();
			else
				s += "*";
			s += ",";
			if (!Double.IsNaN(HighTol))
				s += HighTol.ToString();
			else
				s += "*";
			s += "]";
			return s;
		}
	} // class ToleranceItem

	public class ResultItem
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public ResultItem()
		{
			Name = "";
			Value = "";
		}
	} // class ResultItem

	public class ToleranceMgr
	{
		private string _toleranceFile;
		private List<ToleranceItem> _toleranceList;

		public string ToleranceFile
		{
			get { return _toleranceFile; }
			set { _toleranceFile = value; }
		}
		public List<ToleranceItem> ToleranceList
		{
			get { return _toleranceList; }
		}


		public ToleranceMgr()
		{
			_toleranceFile = "";
			_toleranceList = null;
		}

		public ToleranceMgr(string designName, string partType)
		{
			Setup(designName, partType);
		}

		public void Setup(string designName, string partType)
		{
			string path = FindToleranceFile(designName, partType);
			_toleranceList = ReadToleranceFile(path);
			_toleranceFile = path;
		}

		public static string FindToleranceFile(string designName, string partType)
		{
			string topDir = ProgramSettings.ToleranceFileDirectory;
			string filename = null;
			string[] fileArray = null;
			string msg = null;
			// First look for a design-specific tolerance file
			filename = designName + "_tolerance.csv";
			fileArray = Directory.GetFiles(topDir, filename, SearchOption.AllDirectories);
			if (fileArray.Length == 1)
			{
				return fileArray[0];
			}
			else if (fileArray.Length > 1)
			{
				FileSystemFuncs.AppendToLogFile("ERROR: Found multiple tolerance files named " + filename);
				throw new Exception("Found multiple tolerance files for design " + designName);
			}
			// Look for a default tolerance file.
			string prefix = designName.Substring(0, 2);
			string tolType = prefix + "_" + partType;
			filename = tolType + "_tolerance.csv";
			fileArray = Directory.GetFiles(topDir, filename, SearchOption.AllDirectories);
			if (fileArray.Length == 1)
			{
				return fileArray[0];
			}
			else if (fileArray.Length > 1)
			{
				FileSystemFuncs.AppendToLogFile("ERROR: Found multiple tolerance files named " + filename);
				throw new Exception("Found multiple tolerance files for part type " + partType);
			}
			msg = "Cannot find a tolerance file for design " + designName + " or type " + tolType;
			FileSystemFuncs.AppendToLogFile("ERROR: " + msg);
			throw new Exception(msg);
		} // FindToleranceFile()

		public static List<ToleranceItem> ReadToleranceFile(string path)
		{
			string[] separators = new string[] { "," };
			List<ToleranceItem> toleranceList = new List<ToleranceItem>();
			int lineNum = 0;
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(path);
				string line = "";
				while ((line = sr.ReadLine()) != null)
				{
					++lineNum;
					line = line.Trim();
					if (line.Length == 0)
						continue;
					if (line.StartsWith(";")) // comment
						continue;
					string[] parsedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
					if (parsedLine.Length < 3)
					{
						throw new Exception("At least 3 fields required, found " + parsedLine.Length.ToString());
					}
					string resultName = parsedLine[0].Trim();
					double lowTol = double.Parse(parsedLine[1].Trim());
					double highTol = double.Parse(parsedLine[2].Trim());
					if (lowTol >= highTol)
					{
						throw new Exception("Invalid tolerance values.");
					}
					ToleranceItem tolerance = new ToleranceItem(resultName, lowTol, highTol);
					toleranceList.Add(tolerance);
				}
				lineNum = 0;
				if (toleranceList.Count == 0)
				{
					throw new Exception("No tolerance items found.");
				}
			}
			catch (Exception ex)
			{
				string msg = "ERROR: Tolerance file " + path;
				if (lineNum > 0)
				{
					msg += ": Error in line " + lineNum.ToString();
				}
				msg += ": ";
				msg += ex.Message;
				FileSystemFuncs.AppendToLogFile(msg);
				throw new Exception("Error reading tolerance file");
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr.Dispose();
				}
			}
			return toleranceList;
		} // ReadToleranceFile()

		public static List<ResultItem> ReadResultFile(string path)
		{
			string[] separators = new string[] { "," };
			List<ResultItem> resultList = new List<ResultItem>();
			int lineNum = 0;
			StreamReader sr = null;
			string msg = null;
			try
			{
				string[] parsedLine = null;
				sr = new StreamReader(path);
				string line = "";
				// Read header line containing result names
				line = sr.ReadLine();
				if (line == null)
				{
					throw new Exception("Invalid result file");
				}
				++lineNum;
				line = line.Trim();
				// Delete double quote chars
				line = line.Replace("\"", "");
				parsedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				int numHeaderFields = parsedLine.Length;
				if (numHeaderFields < 1)
				{
					throw new Exception("Invalid header line");
				}
				for (int i = 0; i < numHeaderFields; i++)
				{
					ResultItem resultItem = new ResultItem();
					resultItem.Name = parsedLine[i].Trim();
					resultList.Add(resultItem);
				}
				// Read data line containing result values
				line = sr.ReadLine();
				if (line == null)
				{
					throw new Exception("Invalid result file");
				}
				++lineNum;
				line = line.Trim();
				parsedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
				int numDataFields = parsedLine.Length;
				if (numDataFields < 1)
				{
					throw new Exception("Invalid data line");
				}
				if (numDataFields != numHeaderFields)
				{
					msg = "Number of data fields (" + numDataFields.ToString() + ")";
					msg += " does not match number of header fields (" + numHeaderFields.ToString() + ")";
					throw new Exception(msg);
				}
				for (int i = 0; i < numDataFields; i++)
				{
					ResultItem resultItem = resultList.ElementAt(i);
					resultItem.Value = parsedLine[i].Trim();
				}
			}
			catch (Exception ex)
			{
				msg = "ERROR: Result file " + path;
				if (lineNum > 0)
				{
					msg += ": Error in line " + lineNum.ToString();
				}
				msg += ": ";
				msg += ex.Message;
				FileSystemFuncs.AppendToLogFile(msg);
				throw new Exception("Error reading CSV file");
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr.Dispose();
				}
			}
			return resultList;
		} // ReadResultFile()

		public List<string> ApplyTolerances(string resultFile)
		{
			List<string> errorList = new List<string>();
			List<ResultItem> resultList = null;
			try
			{
				resultList = ReadResultFile(resultFile);
			}
			catch (Exception ex)
			{
				errorList.Add(ex.Message);
				return errorList;
			}
			foreach (ToleranceItem toleranceItem in ToleranceList)
			{
				string resultName = toleranceItem.ResultName;
				string resultNameWithoutUnit = resultName;
				string resultUnit = "";
				// If result name ends with a right paren
				if (resultName.EndsWith(")"))
				{	// Expect a unit abbreviation in parentheses
					int i = resultName.IndexOf('(');
					if (i > 0)
					{
						int j = resultName.IndexOf(')');
						resultUnit = resultName.Substring(i + 1, j - i - 1);
						resultNameWithoutUnit = resultName.Substring(0, i - 1).Trim();
					}
				}
				bool found = false;
				// Look in resultList for resultItem matching resultName
				foreach (ResultItem resultItem in resultList)
				{   // ignore blanks and case
					string n1 = resultName.Replace(" ", "").ToUpper();
					string n2 = resultItem.Name.Replace(" ", "").ToUpper();
					if (String.Compare(n1, n2) == 0)
					{
						found = true;
						double resultValue = 0.0;
						try
						{
							resultValue = double.Parse(resultItem.Value);
						}
						catch
						{
							errorList.Add(resultName + " has an incorrect format in CSV file");
							resultValue = Double.NaN;
						}
						if (!Double.IsNaN(resultValue) && (toleranceItem.ValueInTolerance(resultValue) == false))
						{
							string msg = "";
							if (resultUnit.Length > 0)
							{
								msg = resultNameWithoutUnit + " OOR " + toleranceItem.Range() + " " + resultUnit;
							}
							else
							{
								msg = resultName + " OOR " + toleranceItem.Range();
							}
							errorList.Add(msg);
						}
						break;
					}
				}
				if (found == false)
				{
					errorList.Add(resultName + " not found in CSV file");
				}
			}
			return errorList;
		} // ApplyTolerances()

	}
}
