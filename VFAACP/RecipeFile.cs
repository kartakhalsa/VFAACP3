using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace VFAACP
{
    class RecipeFile
    {
        // A class to contain values for a Recipe File
        private string recipePath;

        private string appFile;                 // *Required*
        private string settingsFile;            // Optional
        private string jobFile;                 // Optional
        private string jobPath;                 // Optional
		private string designFile;              // Optional
		private string designPath;              // Optional
		private string promptFile;              // Optional

		private string setupScript;             // Optional
		private string calibrationScript;       // Optional
        private string transSphere;             // Optional
        private double zCoord = 0.0;            // *Required*
        private double rCoord = 0.0;            // *Required*
        private double pCoord = 0.0;            // *Required*
        private string measureScript;           // *Required*

        private string resultBitmapFileDisplay; // Optional
        private string resultBitmapFilePublish; // Optional
        private string resultTextFile;          // Optional
        private string resultCsvFile;           // Optional
        private string publishFilesGroup;      // *Required*

        public RecipeFile(string path, bool designIsKnown)
        {
            try
            {
				recipePath = path;
                FileInfo fi = new FileInfo(recipePath);
                if (!fi.Exists)
                {
                    throw new Exception("Recipe file " + recipePath + " does not exist");
                }
                Reset();
                ReadAndPopulateRecipeFile(designIsKnown);
            }
            catch
            {
                throw;
            }
        }

        // Properties

        public string AppFile
        {
            get { return appFile; }
        }

        public string SettingsFile
        {
            get { return settingsFile; }
        }

        public string JobFile
        {
            get { return jobFile; }
        }

        public string JobPath
        {
            get { return jobPath; }
        }

		public string DesignFile
		{
			get { return designFile; }
		}

		public string DesignPath
		{
			get { return designPath; }
		}

		public string PromptFile
        {
            get { return promptFile; }
        }

        public string SetupScript
        {
            get { return setupScript; }
        }

		public string CalibrationScript
		{
			get { return calibrationScript; }
		}

		public string TransSphere
        {
            get { return transSphere; }
        }

        public string MeasureScript
        {
            get { return measureScript; }
        }

        public double Z_Coord
        {
            get { return zCoord; }
        }

        public double R_Coord
        {
            get { return rCoord; }
        }

       public double P_Coord
        {
            get { return pCoord; }
        }

        public string BitmapFileDisplay
        {
            get { return resultBitmapFileDisplay; }
        }

        public string BitmapFilePublish
        {
            get { return resultBitmapFilePublish; }
        }

        public string ResultTextFile
        {
            get { return resultTextFile; }
        }

        public string ResultCsvFile
        {
            get { return resultCsvFile; }
        }

        public string PublishFilesGroup
        {
            get { return publishFilesGroup; }
        }

        public void Reset()
        {
            appFile = "";
            settingsFile = "";
            jobFile = "";
            jobPath = "";
			designFile = "";
			designPath = "";
			promptFile = "";
            setupScript = "";
			calibrationScript = "";
			transSphere = "";
            zCoord = 0.0;
            rCoord = 0.0;
            pCoord = 0.0;
            measureScript = "";
            resultBitmapFileDisplay = "";
            resultBitmapFilePublish = "";
            resultTextFile = "";
            resultCsvFile = "";
            publishFilesGroup = "";
        }   // Reset()

        private void ReadAndPopulateRecipeFile(bool designIsKnown)
        {
            if (recipePath == "")
            {
                return;
            }

            INIReader pf = new INIReader(recipePath);

            // Read file and validate certain fields
            try
            {
                appFile = pf.GetValueForSectionAndName("Recipe", "AppFile");

                try
                {
                    zCoord = double.Parse(pf.GetValueForSectionAndName("Recipe", "ZCoord"));
                }
                catch(Exception ex)
                {
                    throw new Exception("ZCoord Parse Error: " + ex.Message);
                }
                try
                {
                    pCoord = double.Parse(pf.GetValueForSectionAndName("Recipe", "PCoord"));
                }
                catch (Exception ex)
                {
                    throw new Exception("PCoord Parse Error: " + ex.Message);
                }
                try
                {
                    rCoord = double.Parse(pf.GetValueForSectionAndName("Recipe", "RCoord"));
                }
                catch (Exception ex)
                {
                    throw new Exception("RCoord Parse Error: " + ex.Message);
                }
                measureScript = pf.GetValueForSectionAndName("Recipe", "MeasureScript");
                publishFilesGroup = pf.GetValueForSectionAndName("Recipe", "PublishFilesGroup");

                // Validation code
                if (appFile == "")
                {
                    throw new Exception("The AppFile value cannot be null");
                }
                if (measureScript == "")
                {
                    throw new Exception("The MeasureScript value cannot be null");
                }
                if (publishFilesGroup == "")
                {
                    throw new Exception("The PublishFilesGroup value cannot be null");
                }

                // Make sure referenced files exist
                FileInfo fi1 = new FileInfo(ProgramSettings.DirMetroProWorking + "\\" + appFile);
                if (!fi1.Exists)
                {
                    throw new Exception("MetroPro application file " + appFile + " does not exist in folder " + ProgramSettings.DirMetroProWorking);
                }

                FileInfo fi2 = new FileInfo(ProgramSettings.DirMetroProWorking + "\\" + measureScript);
                if (!fi2.Exists)
                {
                    throw new Exception("MetroScript file " + measureScript + " does not exist in folder " + ProgramSettings.DirMetroProWorking);
                }

                // Get optional values; validate if set

                try { transSphere = pf.GetValueForSectionAndName("Recipe", "TransSphere"); }
                catch { transSphere = ""; }

                try { settingsFile = pf.GetValueForSectionAndName("Recipe", "SettingsFile"); }
                catch { settingsFile = ""; }
                if (settingsFile.Length > 0)
                {
                    FileInfo fi3 = new FileInfo(ProgramSettings.DirMetroProWorking + "\\" + settingsFile);
                    if (!fi3.Exists)
                    {
                        throw new Exception("MetroPro settings file " + settingsFile + " does not exist in folder " + ProgramSettings.DirMetroProWorking);
                    }
                }

                try { jobFile = pf.GetValueForSectionAndName("Recipe", "JobFile"); }
                catch { jobFile = ""; }
                if (jobFile.Length > 0)
                {
					if (designIsKnown)
					{
						throw new Exception("Recipe should not specify a JobFile");
					}
                    jobPath = FileSystemFuncs.FindFile(ProgramSettings.DirJobFiles, jobFile);
                    if (jobPath == "")
                    {
                        throw new Exception("Cannot find MetroPro job file " + jobFile + " in folder hierarchy beginning at " + ProgramSettings.DirJobFiles);
                    }
                }

				try { designFile = pf.GetValueForSectionAndName("Recipe", "DesignFile"); }
				catch { designFile = ""; }
				if (designFile.Length > 0)
				{
					if (designIsKnown)
					{
						throw new Exception("Recipe should not specify a DesignFile");
					}
					designPath = FileSystemFuncs.FindFile(ProgramSettings.DirDesignFiles, designFile);
					if (designPath == "")
					{
						throw new Exception("Cannot find design file " + designFile + " in folder hierarchy beginning at " + ProgramSettings.DirDesignFiles);
					}
				}
				try { promptFile = pf.GetValueForSectionAndName("Recipe", "PromptFile"); }
                catch { promptFile = ""; }
                if (promptFile.Length > 0)
                {
					if (designIsKnown)
					{
						throw new Exception("Recipe should not specify a PromptFile");
					}
					FileInfo fi5 = new FileInfo(ProgramSettings.DirRecipeFiles + "\\" + promptFile);
                    if (!fi5.Exists)
                    {
                        throw new Exception("MetroPro prompt file " + promptFile + " does not exist in folder " + ProgramSettings.DirRecipeFiles);
                    }
                }

                try { setupScript = pf.GetValueForSectionAndName("Recipe", "SetupScript "); }
                catch { setupScript = ""; }
                if (setupScript.Length > 0)
                {
                    FileInfo fi7 = new FileInfo(ProgramSettings.DirMetroProWorking + "\\" + setupScript);
                    if (!fi7.Exists)
                    {
                        throw new Exception("MetroPro script file " + setupScript + " does not exist in folder " + ProgramSettings.DirMetroProWorking);
                    }
                }

				try { calibrationScript = pf.GetValueForSectionAndName("Recipe", "CalibrationScript "); }
				catch { calibrationScript = ""; }
				if (calibrationScript.Length > 0)
				{
					FileInfo fi8 = new FileInfo(ProgramSettings.DirMetroProWorking + "\\" + calibrationScript);
					if (!fi8.Exists)
					{
						throw new Exception("MetroPro script file " + calibrationScript + " does not exist in folder " + ProgramSettings.DirMetroProWorking);
					}
				}

				try { resultBitmapFileDisplay = pf.GetValueForSectionAndName("Recipe", "ResultBitmapFileDisplay"); }
                catch { resultBitmapFileDisplay = ""; }
                try { resultBitmapFilePublish = pf.GetValueForSectionAndName("Recipe", "ResultBitmapFilePublish"); }
                catch { resultBitmapFilePublish = ""; }
                try { resultTextFile = pf.GetValueForSectionAndName("Recipe", "ResultTextFile"); }
                catch { resultTextFile = ""; }
                try { resultCsvFile = pf.GetValueForSectionAndName("Recipe", "ResultCsvFile"); }
                catch { resultCsvFile = ""; }

            }
            catch (Exception ex)
            {
                throw new Exception("Recipe file error:\n" + ex.Message);
            }

        }   // ReadAndPopulateRecipeFile()

    }   // Class RecipeFile
}
