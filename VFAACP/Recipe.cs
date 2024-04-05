using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace VFAACP
{
	public class Recipe
	{
		// A class to manage recipe values read from a file

		public string PartType { get; protected set; }
		public string JobFile { get; protected set; }
		public string JobPath { get; protected set; }

		public string DesignFile { get; protected set; }
		public string DesignPath { get; protected set; }

		public string PromptFile { get; protected set; }
		public string SetupScript { get; protected set; }
		public string MeasureScript { get; protected set; }

		public double ZCoord_mm { get; protected set; }
		public double RCoord_deg { get; protected set; }
		public double PCoord_deg { get; protected set; }

		public string ResultTxtFile { get; protected set; }
		public string ResultCsv1File { get; protected set; }
		public string ResultCsv2File { get; protected set; }
		public string ResultBmp1File { get; protected set; }
		public string ResultBmp2File { get; protected set; }
		
		public string PublishFilesGroup { get; protected set; }
		
		public Recipe()
		{
			PartType = null;
			JobFile = null;
			JobPath = null;

			DesignFile = null;
			DesignPath = null;

			PromptFile = null;

			SetupScript = null;
			MeasureScript = null;

			ZCoord_mm = 0.0;
			RCoord_deg = 0.0;
			PCoord_deg = 0.0;

			ResultTxtFile = null;
			ResultCsv1File = null;
			ResultCsv2File = null;
			ResultBmp1File = null;
			ResultBmp2File = null;
				
			PublishFilesGroup = null;
		}

		public static Recipe ReadFromFile(string path)
		{
			FileInfo fi = new FileInfo(path);
			if (!fi.Exists)
			{
				throw new Exception("Cannot access recipe file " + path);
			}
			try
			{
				Recipe recipe = new Recipe();
				IniFileReader ini = new IniFileReader(path);

				recipe.PartType = ini.GetValue("Recipe", "PartType");
				recipe.ZCoord_mm = ini.GetDouble("Recipe", "ZCoord_mm");
				recipe.RCoord_deg = ini.GetDouble("Recipe", "RCoord_deg");
				recipe.PCoord_deg = ini.GetDouble("Recipe", "PCoord_deg");
				recipe.MeasureScript = ini.GetValue("Recipe", "MeasureScript");
				recipe.PublishFilesGroup = ini.GetValue("Recipe", "PublishFilesGroup");
				try { recipe.JobFile = ini.GetValue("Recipe", "JobFile"); }
				catch { recipe.JobFile = null; }
				try { recipe.DesignFile = ini.GetValue("Recipe", "DesignFile"); }
				catch { recipe.DesignFile = null; }
				try { recipe.PromptFile = ini.GetValue("Recipe", "PromptFile"); }
				catch { recipe.PromptFile = null; }
				try { recipe.SetupScript = ini.GetValue("Recipe", "SetupScript"); }
				catch { recipe.SetupScript = null; }
				try { recipe.ResultTxtFile = ini.GetValue("Recipe", "ResultTxtFile"); }
				catch { recipe.ResultTxtFile = null; }
				try { recipe.ResultCsv1File = ini.GetValue("Recipe", "ResultCsv1File"); }
				catch { recipe.ResultCsv1File = null; }
				try { recipe.ResultCsv2File = ini.GetValue("Recipe", "ResultCsv2File"); }
				catch { recipe.ResultCsv2File = null; }
				try { recipe.ResultBmp1File = ini.GetValue("Recipe", "ResultBmp1File"); }
				catch { recipe.ResultBmp1File = null; }
				try { recipe.ResultBmp2File = ini.GetValue("Recipe", "ResultBmp2File"); }
				catch { recipe.ResultBmp2File = null; }

				return recipe;
			}
			catch (Exception ex)
			{
				throw new Exception("Error reading recipe file: " + ex.Message);
			}
		} // ReadFromFile()

		public static Recipe ReadFromFileAndValidate(string path, bool designIsKnown)
		{
			Recipe recipe = ReadFromFile(path);
			string msg;
			if (recipe.Validate(out msg, designIsKnown) == false)
			{
				throw new Exception(msg);
			}
			return recipe;
		} // ReadFromFileAndValidate()

		public bool Validate(out string msg, bool designIsKnown = false)
		{
			msg = null;
			FileInfo fi;
			if (string.IsNullOrEmpty(PartType))
			{
				msg = "The PartType value cannot be null";
				return false;
			}
			if (string.IsNullOrEmpty(MeasureScript))
			{
				msg = "The MeasureScript value cannot be null";
				return false;
			}
			if (string.IsNullOrEmpty(PublishFilesGroup))
			{
				msg = "The PublishFilesGroup value cannot be null";
				return false;
			}
			fi = new FileInfo(ProgramSettings.ScriptFileDirectory + "\\" + MeasureScript);
			if (!fi.Exists)
			{
				msg = "Script file " + MeasureScript + " does not exist in folder " + ProgramSettings.ScriptFileDirectory;
				return false;
			}
			if (!string.IsNullOrEmpty(JobFile))
			{
				if (designIsKnown)
				{
					msg = "Recipe should not specify a JobFile";
					return false;
				}
				JobPath = FileSystemFuncs.FindFile(ProgramSettings.JobFileDirectory, JobFile);
				if (JobPath.Length == 0)
				{
					msg = "Cannot find Mx job file " + JobFile + " in folder hierarchy beginning at " + ProgramSettings.JobFileDirectory;
					return false;
				}
			}
			if (!string.IsNullOrEmpty(DesignFile))
			{
				if (designIsKnown)
				{
					msg = "Recipe should not specify a DesignFile";
					return false;
				}
				string dirName = Path.GetDirectoryName(DesignFile);
				if (!string.IsNullOrEmpty(dirName))
				{	// designFile is relative or absolute path
					DesignPath = DesignFile;
					if (!Path.IsPathRooted(DesignPath))
					{
						DesignPath = Path.Combine(ProgramSettings.MxWorkingDirectory, DesignPath);
					}
					if (File.Exists(DesignPath) == false)
					{
						msg = "Cannot find design file " + DesignPath;
						return false;
					}
					DesignFile = Path.GetFileName(DesignPath);
				}
				else
				{	// DesignFile is just a filename
                    msg = "Recipe DesignFile must be a relative or absolute path";
                    return false;
				}
			}
			if (!string.IsNullOrEmpty(PromptFile))
			{
				if (designIsKnown)
				{
					msg = "Recipe should not specify a PromptFile";
					return false;
				}
				fi = new FileInfo(ProgramSettings.RecipeFileDirectory + "\\" + PromptFile);
				if (!fi.Exists)
				{
					msg = "Mx prompt file " + PromptFile + " does not exist in folder " + ProgramSettings.RecipeFileDirectory;
					return false;
				}
			}
			if (!designIsKnown && string.IsNullOrEmpty(JobFile) && string.IsNullOrEmpty(DesignFile) && string.IsNullOrEmpty(PromptFile))
			{
				msg = "Recipe should specify a JobFile, DesignFile or PromptFile";
				return false;
			}
			if (!string.IsNullOrEmpty(SetupScript))
			{
				fi = new FileInfo(ProgramSettings.ScriptFileDirectory + "\\" + SetupScript);
				if (!fi.Exists)
				{
					msg = "Mx script file " + SetupScript + " does not exist in folder " + ProgramSettings.ScriptFileDirectory;
					return false;
				}
			}
			return true;
		} // Validate()

	}   // Class Recipe
}
