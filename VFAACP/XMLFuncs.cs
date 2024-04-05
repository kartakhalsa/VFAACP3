using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace VFAACP
{
	public static class XMLFuncs
	{
		public static void WriteSummaryToXML(string inputFile, string outputFile)
		{
			try
			{
				if (!File.Exists(inputFile))
					throw new FileNotFoundException();
				if (!Directory.Exists(Path.GetDirectoryName(outputFile)))
					throw new DirectoryNotFoundException();

				StreamReader sr = null;
				XmlWriter xw = null;
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;

				try
				{
					sr = new StreamReader(inputFile);
					xw = XmlWriter.Create(outputFile, settings);
					xw.WriteStartDocument();
					xw.WriteStartElement("TrayResults");
					string[] parsedHeader;
					string[] parsedData;
					string dataLine;

					// Read header line
					string headerLine = sr.ReadLine();

					// Remove double-quotes from header line
					headerLine = headerLine.Replace("\"", "");

					// Split header line into components
					parsedHeader = headerLine.Split(new string[] { "," }, StringSplitOptions.None);

					// Read all data lines
					while ((dataLine = sr.ReadLine()) != null)
					{
						string name, value;
						// Remove double-quotes from data line
						dataLine = dataLine.Replace("\"", "");
						// Split data line into components
						parsedData = dataLine.Split(new string[] { "," }, StringSplitOptions.None);
						value = parsedData[0];
						// Test for stats line
						if (value.Contains("MIN") || value.Contains("MAX")
							|| value.Contains("AVERAGE") || value.Contains("STDEV"))
						{	// We're done reading data lines
							break;
						}
						int indx = 0;

						xw.WriteStartElement("SiteResult");

						foreach (string s in parsedHeader)
						{
							name = s.Trim();
							name = SanitizeHeaderName(name);
							value = parsedData[indx].Trim();
							xw.WriteElementString(name, value);
							indx++;
						}
						xw.WriteEndElement();
					}
					xw.WriteEndElement();
					xw.WriteEndDocument();
				}
				catch
				{ 
					throw;
				}
				finally
				{
					if (sr != null)
					{
						sr.Close();
						sr.Dispose();
					}
					if (xw != null)
					{
						xw.Flush();
						xw.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		} // WriteSummaryToXML()

		private static string SanitizeHeaderName(string name)
		{
			name = name.Replace(" ", "_");
			name = name.Replace("(", "");
			name = name.Replace(")", "");
			name = name.Replace("[", "");
			name = name.Replace("]", "");
			name = name.Replace("+", "_");
			return name;
		} // SanitizeHeaderName()
	}
}
