using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFAACP
{
	public static class SiteCoordFile
	{
		public static List<Site> Read(string path)
		{
			StreamReader sr = null;
			List<Site> siteList = null;
			try
			{
				sr = new StreamReader(path);
				string line = "";
				int linenum = 0;
				int siteIndex = 0;
				while ((line = sr.ReadLine()) != null)
				{
					linenum++;
					line = line.Trim();
					if (line.Length == 0)
						continue; // Ignore blank line
					string[] fieldAry;
					fieldAry = line.Split(new string[] { "," }, StringSplitOptions.None);

					if (fieldAry.Length >= 6)
					{
						double X = 0.0;
						double Y = 0.0;
						double Z_Offset = 0.0;
						double R_Offset = 0.0;
						double P_Offset = 0.0;
						int Included = 0;
						int ForCalibration = 0;
						double Axis = 0.0;
						// 6 required fields: 5 doubles and 1 int
						try
						{
							X = double.Parse(fieldAry[0]);
							Y = double.Parse(fieldAry[1]);
							Z_Offset = double.Parse(fieldAry[2]);
							R_Offset = double.Parse(fieldAry[3]);
							P_Offset = double.Parse(fieldAry[4]);
							Included = int.Parse(fieldAry[5]);
						}
						catch (Exception ex)
						{
							if (linenum == 1)
								continue; // First line may be a heading that we quietly ignore
							throw new Exception("Error parsing first 6 values in line " + linenum.ToString() + ": " + ex.Message);
						}
						// Optional 7th value
						if (fieldAry.Length >= 7)
						{
							try
							{
								ForCalibration = int.Parse(fieldAry[6]);
							}
							catch (Exception ex)
							{
								throw new Exception("Error parsing 7th value in line " + linenum.ToString() + ": " + ex.Message);
							}
						}
						// Optional 8th value
						if (fieldAry.Length >= 8)
						{
							try
							{
								Axis = double.Parse(fieldAry[7]);
							}
							catch (Exception ex)
							{
								throw new Exception("Error parsing 8th value in line " + linenum.ToString() + ": " + ex.Message);
							}
						}
						++siteIndex;
						Site s = new Site(siteIndex, X, Y, Z_Offset, R_Offset, P_Offset, Included, ForCalibration, Axis);
						if (siteList == null)
							siteList = new List<Site>();
						siteList.Add(s);
					}
					else
					{
						throw new Exception("Error in line " + linenum.ToString() + ": Expected at least 6 fields, got " + fieldAry.Length.ToString());
					}
				} // while
			}
			catch (Exception ex)
			{
				throw new Exception("Error reading tray file " + path + ": " + ex.Message);
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr.Dispose();
				}
			}
			return siteList;
		}   // Read()

		public static void Write(List<Site> siteList, string path)
		{
			StreamWriter sw = null;
			try
			{
				FileInfo fi = new FileInfo(path);
				FileStream fs = fi.Create();
				fs.Close();
				fs.Dispose();
				sw = fi.AppendText();
				string line;
				// Write heading line
				line = "";
				line += string.Format("{0,10}", "X (mm)") + ",";
				line += string.Format("{0,10}", "Y (mm)") + ",";
				line += string.Format("{0,10}", "Z (mm)") + ",";
				line += string.Format("{0,10}", "R (deg)") + ",";
				line += string.Format("{0,10}", "P (deg)") + ",";
				line += string.Format("{0,5}", "Incl") + ",";
				line += string.Format("{0,4}", "Cal") + ",";
				line += string.Format("{0,11}", "Axis (deg)");
				sw.WriteLine(line);
				foreach (Site s in siteList)
				{
					// Write value line
					double X = s.X;
					double Y = s.Y;
					double Z = s.Z_Offset;
					double R = s.R_Offset;
					double P = s.P_Offset;
					int Included = s.IsIncludedFileState ? 1 : 0;
					int ForCalibration = s.IsCalibrationSite ? 1 : 0;
					double Axis = s.Axis;
					line = "";
					line += string.Format("{0,10:F4}", X) + ",";
					line += string.Format("{0,10:F4}", Y) + ",";
					line += string.Format("{0,10:F4}", Z) + ",";
					line += string.Format("{0,10:F4}", R) + ",";
					line += string.Format("{0,10:F4}", P) + ",";
					line += string.Format("{0,5:D}", Included) + ",";
					line += string.Format("{0,4:D}", ForCalibration) + ",";
					line += string.Format("{0,11:F4}", Axis);
					sw.WriteLine(line);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw.Dispose();
				}
			}
		}   // Write()

	} // Class SiteCoordFile
}
