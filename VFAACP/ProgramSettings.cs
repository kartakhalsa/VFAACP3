using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace VFAACP
{

	class ProgramSettings
	{
		// [General]
		public static string InstrumentName { get; protected set; }
		public static double StageAdjustDeltaRadius_mm { get; protected set; }
		public static int LoadTrayCountdown_sec { get; protected set; }
		public static int ResetMotionDelay_sec { get; protected set; }
		public static int DisplayedSiteBoxes { get; protected set; }
		public static bool AlwaysOnTop { get; protected set; }
		public static bool AutoConnectToMx { get; protected set; }
		public static int TestWithoutInstrument { get; protected set; }
		public static string RecipeLUT { get; protected set; }
		public static string DefaultConfigurationFile { get; protected set; }
		public static string PublishExternalDatabaseFile { get; protected set; }
		public static bool ConfigurationPasswordRequired { get; protected set; }
		public static string SuperUserListFilename { get; protected set; }
		public static bool SummaryFileAddStats { get; protected set; }
		public static string DesignFromLotNumExecutable { get; protected set; }
		public static string MoldServiceSetupExecutable { get; protected set; }
		public static string MoldServiceSetupOutputFile { get; protected set; }
		public static string PythonExecutable { get; protected set; }
		public static string PythonPath { get; protected set; }

		// [StageCoords]
		public static double zCoord_mm = 0.0;
		public static double rCoord_deg = 0.0;
		public static double pCoord_deg = 0.0;
		public static int storeRP = 0;
		public static int storeZ = 0;
		public static double deltaXYLimit_mm = 0.0;
		public static double deltaRPLimit_deg = 0.0;
		public static double deltaZLimit_mm = 0.0;
		public static int homeTiltAxesAtFirstSite = 0;
		public static int homeTiltAxesAtEachSite = 0;

		// [Folders]
		public static string ScriptFileDirectory { get; protected set; }
		public static string MeasSetupDirectory { get; protected set; }
		public static string SiteDataDirectory { get; protected set; }
		public static string InterimFileRootDirectory { get; protected set; }
		public static string PublishFileRootDirectory { get; protected set; }
		public static string ToolingCalPublishFileRootDirectory { get; protected set; }
		public static string PublishExternalDatabaseDirectory { get; protected set; }
		public static string AutoPublishForPowerDirectory { get; protected set; }
		public static string MxWorkingDirectory { get; protected set; }
		public static string JobFileDirectory { get; protected set; }
		public static string RecipeFileDirectory { get; protected set; }
		public static string EngineeringDesignFileDirectory { get; protected set; }
		public static string ProductionDesignFileDirectory { get; protected set; }
		public static string ToolingCalDesignFileDirectory { get; protected set; }
		public static string TrayFileDirectory { get; protected set; }
		public static string VerificationPartDirectory { get; protected set; }
		public static string RecipeLookupTableDirectory { get; protected set; }
		public static string ConfigurationFileDirectory { get; protected set; }
		public static string SuperUserListDirectory { get; protected set; }
		public static string ToleranceFileDirectory { get; protected set; }
		public static string AcpLogFileDirectory { get; protected set; }
		public static string MxLogFileDirectory { get; protected set; }

		// [Files]
		public static string MeasSetupFile { get; protected set; }
		public static string PromptOutputFile { get; protected set; }
		public static string SiteSetupFile { get; protected set; }

		// [Date/Time Formats]
		public static string StatusBarDateTimeFormat { get; protected set; }
		public static string PublishFolderDateTimeFormat { get; protected set; }
		public static string SummaryFileDateFormat { get; protected set; }
		public static string SummaryFileTimeFormat { get; protected set; }
		public static string ReportDateTimeFormat { get; protected set; }
		public static string LogFilenameDateTimeFormat { get; protected set; }
		public static string LogEntryDateTimeFormat { get; protected set; }

		// [Scripts]
		public static string StartMxScript { get; protected set; }
		public static string LoadTrayScript { get; protected set; }
		public static string RetractTrayScript { get; protected set; }

		// hard-coded
		public static int TestWithoutInstrumentMoveTime_msec = 3000;

		public static string[] superUserNames;
		public static ConfigurationFile activeConfiguration;
		public static string configurationPassword = null;

		// Properties
		public static double ZCoord_mm
		{
			get { return zCoord_mm; }
		}

		public static double RCoord_deg
		{
			get { return rCoord_deg; }
			set { rCoord_deg = value; }
		}

		public static double PCoord_deg
		{
			get { return pCoord_deg; }
			set { pCoord_deg = value; }
		}

		public static int Store_RP
		{
			get { return storeRP; }
		}

		public static int Store_Z
		{
			get { return storeZ; }
		}

		public static double DeltaXYLimit_mm
		{
			get { return deltaXYLimit_mm; }
		}

		public static double DeltaRPLimit_deg
		{
			get { return deltaRPLimit_deg; }
		}

		public static double DeltaZLimit_mm
		{
			get { return deltaZLimit_mm; }
		}

		public static int Home_Tilt_Axes_At_First_Site
		{
			get { return homeTiltAxesAtFirstSite; }
		}

		public static int Home_Tilt_Axes_At_Each_Site
		{
			get { return homeTiltAxesAtEachSite; }
		}

		public static bool OkAutoPublishForPower
		{
			get
			{
				if (AutoPublishForPowerDirectory != null)
				{
					DirectoryInfo di = new DirectoryInfo(AutoPublishForPowerDirectory);
					if (di.Exists)
						return true;
				}
				return false;
			}
		}

		public static string SuperUserListFilePath
		{
			get { return Path.Combine(SuperUserListDirectory, SuperUserListFilename); }
		}


		public static string[] SuperUserNames
		{
			get { return superUserNames; }
		}

		public static ConfigurationFile ActiveConfiguration
		{
			get { return activeConfiguration; }
			set { activeConfiguration = value; }
		}

		public static string ConfigurationPassword
		{
			get { return configurationPassword; }
			set { configurationPassword = value; }
		}

		public static string ReadSettingsFromFile(string path)
		{
			DirectoryInfo di;
			FileInfo fi;

			// Read program settings from INI file
			fi = new FileInfo(path);
			if (fi.Exists == false)
			{
				return "Cannot access file.";
			}
			IniFileReader ini = new IniFileReader(path);
			// [General]
			try
			{
				InstrumentName = ini.GetValue("General", "InstrumentName");
			}
			catch (Exception ex)
			{
				return "Error reading item InstrumentName in section [General]\n" + ex.Message;
			}
			try
			{
				StageAdjustDeltaRadius_mm = ini.GetDouble("General", "StageAdjustDeltaRadius_mm");
			}
			catch (Exception ex)
			{
				return "Error reading item StageAdjustDelta_Radius_mm in section [General]\n" + ex.Message;
			}
			try
			{
				LoadTrayCountdown_sec = ini.GetInteger("General", "LoadTrayCountdown_sec");
			}
			catch (Exception ex)
			{
				return "Error reading item LoadTrayCountdown_sec in section [General]\n" + ex.Message;
			}
			try
			{
				ResetMotionDelay_sec = ini.GetInteger("General", "ResetMotionDelay_sec");
			}
			catch (Exception ex)
			{
				return "Error reading item ResetMotionDelay_sec in section [General]\n" + ex.Message;
			}
			try
			{
				DisplayedSiteBoxes = ini.GetInteger("General", "DisplayedSiteBoxes");
			}
			catch (Exception ex)
			{
				return "Error reading item DisplayedSiteBoxes in section [General]\n" + ex.Message;
			}
			if (DisplayedSiteBoxes <= 0 || DisplayedSiteBoxes > 10)
			{
				return "DisplayedSiteBoxes value must be in the range [1,10]";
			}
			try
			{
				AlwaysOnTop = ini.GetBoolean("General", "AlwaysOnTop");
			}
			catch (Exception ex)
			{
				return "Error reading item AlwaysOnTop in section [General]\n" + ex.Message;
			}

			try
			{
				AutoConnectToMx = ini.GetBoolean("General", "AutoConnectToMx");
			}
			catch (Exception ex)
			{
				return "Error reading item AutoConnectToMx in section [General]\n" + ex.Message;
			}
			try
			{
				TestWithoutInstrument = ini.GetInteger("General", "TestWithoutInstrument");
			}
			catch (Exception ex)
			{
				return "Error reading item TestWithoutInstrument in section [General]\n" + ex.Message;
			}
			if (!((TestWithoutInstrument >= 0) && (TestWithoutInstrument <= 2)))
				throw new Exception("Error reading item TestWithoutInstrument in section [General]: Value must be 0, 1 or 2");
			try
			{
				RecipeLUT = ini.GetValue("General", "RecipeLUT");
			}
			catch (Exception ex)
			{
				return "Error reading item RecipeLUT in section [General]\n" + ex.Message;
			}
			try
			{
				DefaultConfigurationFile = ini.GetValue("General", "DefaultConfigurationFile");
			}
			catch (Exception ex)
			{
				return "Error reading item DefaultConfigurationFile in section [General]\n" + ex.Message;
			}
			try
			{
				SuperUserListFilename = ini.GetValue("General", "SuperUserListFilename");
			}
			catch (Exception ex)
			{
				return "Error reading item SuperUserListFilename in section [General]\n" + ex.Message;
			}
			try
			{
				PublishExternalDatabaseFile = ini.GetValue("General", "PublishExternalDatabaseFile");
			}
			catch (Exception ex)
			{
				return "Error reading item PublishExternalDatabaseFile in section [General]\n" + ex.Message;
			}
			try
			{
				ConfigurationPasswordRequired = ini.GetBoolean("General", "ConfigurationPasswordRequired");
			}
			catch (Exception ex)
			{
				return "Error reading item ConfigurationPasswordRequired in section [General]\n" + ex.Message;
			}
			try
			{
				SummaryFileAddStats = ini.GetBoolean("General", "SummaryFileAddStats");
			}
			catch (Exception ex)
			{
				return "Error reading item SummaryFileAddStats in section [General]\n" + ex.Message;
			}

			try
			{
				DesignFromLotNumExecutable = ini.GetValue("General", "DesignFromLotNumExecutable");
			}
			catch (Exception ex)
			{
				return "Error reading item DesignFromLotNumExecutable in section [General]\n" + ex.Message;
			}
			if (DesignFromLotNumExecutable.Length > 0)
			{
				fi = new FileInfo(DesignFromLotNumExecutable);
				if (!fi.Exists)
				{
					return "Cannot access DesignFromLotNumExecutable file " + DesignFromLotNumExecutable;
				}
			}

			try
			{
				MoldServiceSetupExecutable = ini.GetValue("General", "MoldServiceSetupExecutable");
			}
			catch (Exception ex)
			{
				return "Error reading item MoldServiceSetupExecutable in section [General]\n" + ex.Message;
			}
			if (MoldServiceSetupExecutable.Length > 0)
			{
				string ext = Path.GetExtension(MoldServiceSetupExecutable).ToLower();
				if ((ext != ".exe") && (ext != ".bat"))
				{
					return "MoldServiceSetupExecutable file " + MoldServiceSetupExecutable + " must have extension .exe or .bat";
				}
				fi = new FileInfo(MoldServiceSetupExecutable);
				if (!fi.Exists)
				{
					return "Cannot access MoldServiceSetupExecutable file " + MoldServiceSetupExecutable;
				}
			}

			try
			{
				MoldServiceSetupOutputFile = ini.GetValue("General", "MoldServiceSetupOutputFile");
			}
			catch (Exception ex)
			{
				return "Error reading item MoldServiceSetupOutputFile in section [General]\n" + ex.Message;
			}

			try
			{
				PythonExecutable = ini.GetValue("General", "PythonExecutable");
			}
			catch (Exception ex)
			{
				return "Error reading item PythonExecutable in section [General]\n" + ex.Message;
			}
			if (PythonExecutable.Length > 0)
			{
				fi = new FileInfo(PythonExecutable);
				if (!fi.Exists)
				{
					return "Cannot access PythonExecutable file " + PythonExecutable;
				}
			}
			try
			{
				PythonPath = ini.GetValue("General", "PythonPath");
			}
			catch (Exception ex)
			{
				return "Error reading item PythonPath in section [General]\n" + ex.Message;
			}

			// [StageCoords]
			try
			{
				zCoord_mm = ini.GetDouble("StageCoords", "ZCoord_mm");
			}
			catch (Exception ex)
			{
				return "Error reading item ZCoord_mm in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				rCoord_deg = ini.GetDouble("StageCoords", "RCoord_deg");
			}
			catch (Exception ex)
			{
				return "Error reading item RCoord_deg in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				pCoord_deg = ini.GetDouble("StageCoords", "PCoord_deg");
			}
			catch (Exception ex)
			{
				return "Error reading item PCoord_deg in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				storeRP = ini.GetInteger("StageCoords", "StoreRP");
			}
			catch (Exception ex)
			{
				return "Error reading item StoreRP in section [StageCoords]\n" + ex.Message;
			}
			if (!((storeRP == 0) || (storeRP == 1)))
				throw new Exception("Error reading item StoreRP in section [StageCoords]: value must be 0 or 1");
			try
			{
				storeZ = ini.GetInteger("StageCoords", "StoreZ");
			}
			catch (Exception ex)
			{
				return "Error reading item StoreZ in section [StageCoords]\n" + ex.Message;
			}
			if (!((storeZ == 0) || (storeZ == 1)))
				throw new Exception("Error reading item StoreZ in section [StageCoords]: value must be 0 or 1");
			try
			{
				deltaXYLimit_mm = ini.GetDouble("StageCoords", "DeltaXYLimit_mm");
			}
			catch (Exception ex)
			{
				return "Error reading item DeltaXYLimit_mm in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				deltaRPLimit_deg = ini.GetDouble("StageCoords", "DeltaRPLimit_deg");
			}
			catch (Exception ex)
			{
				return "Error reading item DeltaRPLimit_deg in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				deltaZLimit_mm = ini.GetDouble("StageCoords", "DeltaZLimit_mm");
			}
			catch (Exception ex)
			{
				return "Error reading item DeltaZLimit_mm in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				homeTiltAxesAtFirstSite = ini.GetInteger("StageCoords", "HomeTiltAxesAtFirstSite");
			}
			catch (Exception ex)
			{
				return "Error reading item HomeTiltAxesAtFirstSite in section [StageCoords]\n" + ex.Message;
			}
			try
			{
				homeTiltAxesAtEachSite = ini.GetInteger("StageCoords", "HomeTiltAxesAtEachSite");
			}
			catch (Exception ex)
			{
				return "Error reading item HomeTiltAxesAtEachSite in section [StageCoords]\n" + ex.Message;
			}

			// [Folders]
			try
			{
				ScriptFileDirectory = ini.GetValue("Folders", "ScriptFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ScriptFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ScriptFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access ScriptFileDirectory " + ScriptFileDirectory;
			}
			try
			{
				MeasSetupDirectory = ini.GetValue("Folders", "MeasSetupDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item MeasSetupDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(MeasSetupDirectory);
			if (!di.Exists)
			{
				return "Cannot access MeasSetupDirectory " + MeasSetupDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(MeasSetupDirectory))
			{
				return "MeasSetupDirectory " + MeasSetupDirectory + " is not writable";
			}
			try
			{
				SiteDataDirectory = ini.GetValue("Folders", "SiteDataDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item SiteDataDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(SiteDataDirectory);
			if (!di.Exists)
			{
				return "Cannot access SiteDataDirectory " + SiteDataDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(SiteDataDirectory))
			{
				return "SiteDataDirectory " + SiteDataDirectory + " is not writable";
			}
			try
			{
				InterimFileRootDirectory = ini.GetValue("Folders", "InterimFileRootDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item InterimFileRootDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(InterimFileRootDirectory);
			if (!di.Exists)
			{
				return "Cannot access InterimFileRootDirectory " + InterimFileRootDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(InterimFileRootDirectory))
			{
				return "InterimFileRootDirectory " + InterimFileRootDirectory + " is not writable";
			}
			try
			{
				PublishFileRootDirectory = ini.GetValue("Folders", "PublishFileRootDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item PublishFileRootDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(PublishFileRootDirectory);
			if (!di.Exists)
			{
				return "Cannot access PublishFileRootDirectory " + PublishFileRootDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(PublishFileRootDirectory))
			{
				return "PublishFileRootDirectory " + PublishFileRootDirectory + " is not writable";
			}
			try
			{
				ToolingCalPublishFileRootDirectory = ini.GetValue("Folders", "ToolingCalPublishFileRootDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ToolingCalPublishFileRootDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ToolingCalPublishFileRootDirectory);
			if (!di.Exists)
			{
				return "Cannot access ToolingCalPublishFileRootDirectory " + ToolingCalPublishFileRootDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(ToolingCalPublishFileRootDirectory))
			{
				return "ToolingCalPublishFileRootDirectory " + ToolingCalPublishFileRootDirectory + " is not writable";
			}
			try
			{
				PublishExternalDatabaseDirectory = ini.GetValue("Folders", "PublishExternalDatabaseDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item PublishExternalDatabaseDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(PublishExternalDatabaseDirectory);
			if (!di.Exists)
			{
				return "Cannot access PublishExternalDatabaseDirectory " + PublishExternalDatabaseDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(PublishExternalDatabaseDirectory))
			{
				return "PublishExternalDatabaseDirectory " + PublishExternalDatabaseDirectory + " is not writable";
			}
			try
			{
				AutoPublishForPowerDirectory = ini.GetValue("Folders", "AutoPublishForPowerDirectory");
			}
			catch
			{
				AutoPublishForPowerDirectory = null;
			}
			if (AutoPublishForPowerDirectory != null)
			{
				di = new DirectoryInfo(AutoPublishForPowerDirectory);
				if (!di.Exists)
				{
					return "Cannot access AutoPublishForPowerDirectory " + AutoPublishForPowerDirectory;
				}   
				if (!FileSystemFuncs.IsDirectoryWritable(AutoPublishForPowerDirectory))
				{
					return "AutoPublishForPowerDirectory " + AutoPublishForPowerDirectory + " is not writable";
				}
			}

			try
			{
				MxWorkingDirectory = ini.GetValue("Folders", "MxWorkingDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item MxWorkingDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(MxWorkingDirectory);
			if (!di.Exists)
			{
				return "Cannot access MxWorkingDirectory " + MxWorkingDirectory;
			}

			try
			{
				JobFileDirectory = ini.GetValue("Folders", "JobFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item JobFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(JobFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access JobFileDirectory " + JobFileDirectory;
			}

			try
			{
				RecipeFileDirectory = ini.GetValue("Folders", "RecipeFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item RecipeFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(RecipeFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access RecipeFileDirectory " + RecipeFileDirectory;
			}

			try
			{
				EngineeringDesignFileDirectory = ini.GetValue("Folders", "EngineeringDesignFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item EngineeringDesignFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(EngineeringDesignFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access EngineeringDesignFileDirectory " + EngineeringDesignFileDirectory;
			}

			try
			{
				ProductionDesignFileDirectory = ini.GetValue("Folders", "ProductionDesignFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ProductionDesignFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ProductionDesignFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access ProductionDesignFileDirectory " + ProductionDesignFileDirectory;
			}

			try
			{
				ToolingCalDesignFileDirectory = ini.GetValue("Folders", "ToolingCalDesignFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ToolingCalDesignFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ToolingCalDesignFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access ToolingCalDesignFileDirectory " + ToolingCalDesignFileDirectory;
			}

			try
			{
				TrayFileDirectory = ini.GetValue("Folders", "TrayFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item TrayFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(TrayFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access TrayFileDirectory " + TrayFileDirectory;
			}

			try
			{
				VerificationPartDirectory = ini.GetValue("Folders", "VerificationPartDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item VerificationPartDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(VerificationPartDirectory);
			if (!di.Exists)
			{
				return "Cannot access VerificationPartDirectory " + VerificationPartDirectory;
			}

			try
			{
				RecipeLookupTableDirectory = ini.GetValue("Folders", "RecipeLookupTableDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item RecipeLookupTableDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(RecipeLookupTableDirectory);
			if (!di.Exists)
			{
				return "Cannot access RecipeLookupTableDirectory " + RecipeLookupTableDirectory;
			}

			try
			{
				ConfigurationFileDirectory = ini.GetValue("Folders", "ConfigurationFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ConfigurationFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ConfigurationFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access ConfigurationFileDirectory " + ConfigurationFileDirectory;
			}
			string pathtodefaultconfig = Path.Combine(ConfigurationFileDirectory, DefaultConfigurationFile);
			if (!File.Exists(pathtodefaultconfig))
			{
				return "Cannot access Default configuration file " + pathtodefaultconfig;
			}

			try
			{
				SuperUserListDirectory = ini.GetValue("Folders", "SuperUserListDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item SuperUserListDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(SuperUserListDirectory);
			if (!di.Exists)
			{
				return "Cannot access SuperUserListDirectory " + SuperUserListDirectory;
			}

			try
			{
				ToleranceFileDirectory = ini.GetValue("Folders", "ToleranceFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item ToleranceFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(ToleranceFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access ToleranceFileDirectory " + ToleranceFileDirectory;
			}

			try
			{
				AcpLogFileDirectory = ini.GetValue("Folders", "AcpLogFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item AcpLogFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(AcpLogFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access AcpLogFileDirectory " + AcpLogFileDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(AcpLogFileDirectory))
			{
				return "AcpLogFileDirectory " + AcpLogFileDirectory + " is not writable";
			}

			try
			{
				MxLogFileDirectory = ini.GetValue("Folders", "MxLogFileDirectory");
			}
			catch (Exception ex)
			{
				return "Error reading item MxLogFileDirectory in section [Folders]\n" + ex.Message;
			}
			di = new DirectoryInfo(MxLogFileDirectory);
			if (!di.Exists)
			{
				return "Cannot access MxLogFileDirectory " + MxLogFileDirectory;
			}
			if (!FileSystemFuncs.IsDirectoryWritable(MxLogFileDirectory))
			{
				return "MxLogFileDirectory " + MxLogFileDirectory + " is not writable";
			}
			// [Files]
			try
			{
				MeasSetupFile = ini.GetValue("Files", "MeasSetupFile");
			}
			catch (Exception ex)
			{
				return "Error reading item MeasSetupFile in section [Files]\n" + ex.Message;
			}
			try
			{
				PromptOutputFile = ini.GetValue("Files", "PromptOutputFile");
			}
			catch (Exception ex)
			{
				return "Error reading item PromptOutputFile in section [Files]\n" + ex.Message;
			}
			try
			{
				SiteSetupFile = ini.GetValue("Files", "SiteSetupFile");
			}
			catch (Exception ex)
			{
				return "Error reading item SiteSetupFile in section [Files]\n" + ex.Message;
			}

			// If password required, attempt to read the password setting.
			// This is located in the application startup folder in a file named 'p.txt'
			if (ConfigurationPasswordRequired)
			{
				// Test if password file exists
				string passwordFile = Path.Combine(Application.StartupPath, "p.txt");

				if (!File.Exists(passwordFile))
				{
					// If the file does not exist, create one with a default password
					File.WriteAllText(passwordFile, "changeme");
					string msg = "Configuration password file is required but does not exist!\n\nA new password file has been created with a default password";
					MessageBox.Show(msg, "Program Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				// Read the password file
				StreamReader sr = null;
				try
				{
					sr = new StreamReader(passwordFile);

					string tmppwd = sr.ReadLine().Trim();

					if (tmppwd.Length == 0) throw new Exception("Password cannot be blank");

					configurationPassword = tmppwd;
				}
				catch (Exception ex)
				{
					configurationPassword = null;
					return "Error reading configuration password setting: " + ex.Message;
				}
				finally
				{
					if (sr != null)
					{
						sr.Close();
						sr.Dispose();
					}
				}
			}

			try
			{
				StatusBarDateTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "StatusBarDateTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item StatusBarDateTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				PublishFolderDateTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "PublishFolderDateTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item PublishFolderDateTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				SummaryFileDateFormat = Unquote(ini.GetValue("Date&Time Formats", "SummaryFileDateFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item SummaryFileDateFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				SummaryFileTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "SummaryFileTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item SummaryFileTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				ReportDateTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "ReportDateTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item ReportDateTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				LogFilenameDateTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "LogFilenameDateTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item LogFilenameDateTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}
			try
			{
				LogEntryDateTimeFormat = Unquote(ini.GetValue("Date&Time Formats", "LogEntryDateTimeFormat"));
			}
			catch (Exception ex)
			{
				return "Error reading item LogEntryDateTimeFormat in section [Date&Time Formats]\n" + ex.Message;
			}

			try
			{
				StartMxScript = ini.GetValue("Scripts", "StartMxScript");
			}
			catch (Exception ex)
			{
				return "Error reading item StartMxScript in section [General]\n" + ex.Message;
			}

			try
			{
				LoadTrayScript = ini.GetValue("Scripts", "LoadTrayScript");
			}
			catch (Exception ex)
			{
				return "Error reading item LoadTrayScript in section [Scripts]\n" + ex.Message;
			}

			try
			{
				RetractTrayScript = ini.GetValue("Scripts", "RetractTrayScript");
			}
			catch (Exception ex)
			{
				return "Error reading item RetractTrayScript in section [Scripts]\n" + ex.Message;
			}

			// Populate SuperUser list
			try
			{
				if (File.Exists(SuperUserListFilePath))
				{
					IniFileReader ini2 = new IniFileReader();
					ini2.AllowUnnamedValues = true;
					ini2.ReadFile(SuperUserListFilePath);
					superUserNames = ini2.GetSectionValues("[SuperUser List]");
				}
				else
				{
					throw new Exception("Cannot access SuperUserListFilePath " + SuperUserListFilePath);
				}
			}
			catch (Exception ex)
			{
				return "Error populating SuperUser List\n" + ex.Message;
			}
			return null;
		}   // ReadSettingsFromFile()

		public static string WriteSettingsToFile(string path)
		{
			string errmsg = null;
			StreamWriter sw = null;
			try
			{
				sw = new StreamWriter(path);
				sw.WriteLine("[General]");
				sw.WriteLine("InstrumentName = " + InstrumentName);
				sw.WriteLine("StageAdjustDeltaRadius_mm = " + StageAdjustDeltaRadius_mm.ToString());
				sw.WriteLine("LoadTrayCountdown_sec = " + LoadTrayCountdown_sec.ToString());
				sw.WriteLine("ResetMotionDelay_sec = " + ResetMotionDelay_sec.ToString());
				sw.WriteLine("DisplayedSiteBoxes = " + DisplayedSiteBoxes.ToString());
				sw.WriteLine("AlwaysOnTop = " + AlwaysOnTop.ToString());
				sw.WriteLine("AutoConnectToMx = " + AutoConnectToMx.ToString());
				sw.WriteLine("TestWithoutInstrument = " + TestWithoutInstrument.ToString());
				sw.WriteLine("RecipeLUT = " + RecipeLUT);
				sw.WriteLine("DefaultConfigurationFile = " + DefaultConfigurationFile);
				sw.WriteLine("ConfigurationPasswordRequired = " + ConfigurationPasswordRequired.ToString());
				sw.WriteLine("SuperUserListFilename = " + SuperUserListFilename);
				sw.WriteLine("PublishExternalDatabaseFile = " + PublishExternalDatabaseFile);
				sw.WriteLine("SummaryFileAddStats = " + SummaryFileAddStats.ToString());
				sw.WriteLine("DesignFromLotNumExecutable = " + DesignFromLotNumExecutable);
				sw.WriteLine("MoldServiceSetupExecutable = " + MoldServiceSetupExecutable);
				sw.WriteLine("MoldServiceSetupOutputFile = " + MoldServiceSetupOutputFile);
				sw.WriteLine("PythonExecutable = " + PythonExecutable);
				sw.WriteLine("PythonPath = " + PythonPath);
				sw.WriteLine("");
				sw.WriteLine("[StageCoords]");
				sw.WriteLine("ZCoord_mm = " + zCoord_mm.ToString());
				sw.WriteLine("RCoord_deg = " + rCoord_deg.ToString());
				sw.WriteLine("PCoord_deg = " + pCoord_deg.ToString());
				sw.WriteLine("StoreRP = " + storeRP.ToString());
				sw.WriteLine("StoreZ = " + storeZ.ToString());
				sw.WriteLine("DeltaXYLimit_mm = " + deltaXYLimit_mm.ToString());
				sw.WriteLine("DeltaRPLimit_deg = " + deltaRPLimit_deg.ToString());
				sw.WriteLine("DeltaZLimit_mm = " + deltaZLimit_mm.ToString());
				sw.WriteLine("HomeTiltAxesAtFirstSite = " + homeTiltAxesAtFirstSite.ToString());
				sw.WriteLine("HomeTiltAxesAtEachSite = " + homeTiltAxesAtEachSite.ToString());
				sw.WriteLine("");
				sw.WriteLine("[Folders]");
				sw.WriteLine("ScriptFileDirectory = " + ScriptFileDirectory);
				sw.WriteLine("MeasSetupDirectory = " + MeasSetupDirectory);
				sw.WriteLine("SiteDataDirectory = " + SiteDataDirectory);
				sw.WriteLine("InterimFileRootDirectory = " + InterimFileRootDirectory);
				sw.WriteLine("PublishFileRootDirectory = " + PublishFileRootDirectory);
				sw.WriteLine("ToolingCalPublishFileRootDirectory = " + ToolingCalPublishFileRootDirectory);
				sw.WriteLine("PublishExternalDatabaseDirectory = " + PublishExternalDatabaseDirectory);
				sw.WriteLine("AutoPublishForPowerDirectory = " + AutoPublishForPowerDirectory);
				sw.WriteLine("MxWorkingDirectory = " + MxWorkingDirectory);
				sw.WriteLine("JobFileDirectory = " + JobFileDirectory);
				sw.WriteLine("RecipeFileDirectory = " + RecipeFileDirectory);
				sw.WriteLine("EngineeringDesignFileDirectory = " + EngineeringDesignFileDirectory);
				sw.WriteLine("ProductionDesignFileDirectory = " + ProductionDesignFileDirectory);
				sw.WriteLine("ToolingCalDesignFileDirectory = " + ToolingCalDesignFileDirectory);
				sw.WriteLine("TrayFileDirectory = " + TrayFileDirectory);
				sw.WriteLine("VerificationPartDirectory = " + VerificationPartDirectory);
				sw.WriteLine("RecipeLookupTableDirectory = " + RecipeLookupTableDirectory);
				sw.WriteLine("ConfigurationFileDirectory = " + ConfigurationFileDirectory);
				sw.WriteLine("SuperUserListDirectory = " + SuperUserListDirectory);
				sw.WriteLine("ToleranceFileDirectory = " + ToleranceFileDirectory);
				sw.WriteLine("AcpLogFileDirectory = " + AcpLogFileDirectory);
				sw.WriteLine("MxLogFileDirectory = " + MxLogFileDirectory);
				sw.WriteLine("");
				sw.WriteLine("[Files]");
				sw.WriteLine("MeasSetupFile = " + MeasSetupFile);
				sw.WriteLine("PromptOutputFile = " + PromptOutputFile);
				sw.WriteLine("SiteSetupFile = " + SiteSetupFile);
				sw.WriteLine("");
				sw.WriteLine("[Date&Time Formats]");
				sw.WriteLine("StatusBarDateTimeFormat=\"" + StatusBarDateTimeFormat + "\"");
				sw.WriteLine("PublishFolderDateTimeFormat=\"" + PublishFolderDateTimeFormat + "\"");
				sw.WriteLine("SummaryFileDateFormat=\"" + SummaryFileDateFormat + "\"");
				sw.WriteLine("SummaryFileTimeFormat=\"" + SummaryFileTimeFormat + "\"");
				sw.WriteLine("ReportDateTimeFormat=\"" + ReportDateTimeFormat + "\"");
				sw.WriteLine("LogFilenameDateTimeFormat=\"" + LogFilenameDateTimeFormat + "\"");
				sw.WriteLine("LogEntryDateTimeFormat=\"" + LogEntryDateTimeFormat + "\"");
				sw.WriteLine("");
				sw.WriteLine("[Scripts]");
				sw.WriteLine("StartMxScript = " + StartMxScript);
				sw.WriteLine("LoadTrayScript = " + LoadTrayScript);
				sw.WriteLine("RetractTrayScript = " + RetractTrayScript);
				FileSystemFuncs.AppendToLogFile("Wrote file " + path);
			}
			catch (Exception ex)
			{
				errmsg = "Error writing file " + path;
				FileSystemFuncs.AppendToLogFile(errmsg + ": " + ex.Message);
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
					sw.Dispose();
				}
			}
			return errmsg;
		}   // WriteSettingsToFile()

		public static string DumpSettings()
		{
			string nl = Environment.NewLine;
			string buf = "";
			string divider = new string('=', 80) + nl;
			buf += divider;
			buf += "Program settings file " + Program.SettingsFile + nl;
			buf += "[General]" + nl;
			buf += "InstrumentName = " + InstrumentName + nl;
			buf += "StageAdjustDeltaRadius_mm = " + StageAdjustDeltaRadius_mm.ToString() + nl;
			buf += "LoadTrayCountdown_sec = " + LoadTrayCountdown_sec.ToString() + nl;
			buf += "ResetMotionDelay_sec = " + ResetMotionDelay_sec.ToString() + nl;
			buf += "DisplayedSiteBoxes = " + DisplayedSiteBoxes.ToString() + nl;
			buf += "AlwaysOnTop = " + AlwaysOnTop.ToString() + nl;
			buf += "AutoConnectToMx = " + AutoConnectToMx.ToString() + nl; 
			buf += "TestWithoutInstrument = " + TestWithoutInstrument.ToString() + nl;
			buf += "RecipeLUT = " + RecipeLUT + nl;
			buf += "DefaultConfigurationFile = " + DefaultConfigurationFile + nl;
			buf += "SuperUserListFilename = " + SuperUserListFilename + nl;
			buf += "PublishExternalDatabaseFile = " + PublishExternalDatabaseFile + nl;
			buf += "ConfigurationPasswordRequired = " + ConfigurationPasswordRequired.ToString() + nl;
			buf += "SummaryFileAddStats = " + SummaryFileAddStats.ToString() + nl;
			buf += "DesignFromLotNumExecutable = " + DesignFromLotNumExecutable + nl;
			buf += "MoldServiceSetupExecutable = " + MoldServiceSetupExecutable + nl;
			buf += "MoldServiceSetupOutputFile = " + MoldServiceSetupOutputFile + nl;
			buf += "PythonExecutable = " + PythonExecutable + nl;
			buf += "PythonPath = " + PythonPath + nl;

			buf += "[StageCoords]" + nl;
			buf += "ZCoord_mm = " + zCoord_mm.ToString() + nl;
			buf += "RCoord_deg = " + rCoord_deg.ToString() + nl;
			buf += "PCoord_deg = " + pCoord_deg.ToString() + nl;
			buf += "StoreRP = " + storeRP.ToString() + nl;
			buf += "StoreZ = " + storeZ.ToString() + nl;
			buf += "DeltaXYLimit_mm = " + deltaXYLimit_mm.ToString() + nl;
			buf += "DeltaRPLimit_deg = " + deltaRPLimit_deg.ToString() + nl;
			buf += "DeltaZLimit_mm = " + deltaZLimit_mm.ToString() + nl;
			buf += "HomeTiltAxesAtFirstSite = " + homeTiltAxesAtFirstSite.ToString() + nl;
			buf += "HomeTiltAxesAtEachSite = " + homeTiltAxesAtEachSite.ToString() + nl;

			buf += "[Folders]" + nl;
			buf += "ScriptFileDirectory = " + ScriptFileDirectory + nl;
			buf += "MeasSetupDirectory = " + MeasSetupDirectory + nl;
			buf += "SiteDataDirectory = " + SiteDataDirectory + nl;
			buf += "InterimFileRootDirectory = " + InterimFileRootDirectory + nl;
			buf += "PublishFileRootDirectory = " + PublishFileRootDirectory + nl;
			buf += "ToolingCalPublishFileRootDirectory = " + ToolingCalPublishFileRootDirectory + nl;
			buf += "PublishExternalDatabaseDirectory = " + PublishExternalDatabaseDirectory + nl;
			buf += "AutoPublishForPowerDirectory = " + AutoPublishForPowerDirectory + nl;
			buf += "MxWorkingDirectory = " + MxWorkingDirectory + nl;
			buf += "JobFileDirectory = " + JobFileDirectory + nl;
			buf += "RecipeFileDirectory = " + RecipeFileDirectory + nl;
			buf += "EngineeringDesignFileDirectory = " + EngineeringDesignFileDirectory + nl;
			buf += "ProductionDesignFileDirectory = " + ProductionDesignFileDirectory + nl;
			buf += "ToolingCalDesignFileDirectory = " + ToolingCalDesignFileDirectory + nl;
			buf += "TrayFileDirectory = " + TrayFileDirectory + nl;
			buf += "VerificationPartDirectory = " + VerificationPartDirectory + nl;
			buf += "RecipeLookupTableDirectory = " + RecipeLookupTableDirectory + nl;
			buf += "ConfigurationFileDirectory = " + ConfigurationFileDirectory + nl;
			buf += "SuperUserListDirectory = " + SuperUserListDirectory + nl;
			buf += "ToleranceFileDirectory = " + ToleranceFileDirectory + nl;
			buf += "AcpLogFileDirectory = " + AcpLogFileDirectory + nl;
			buf += "MxLogFileDirectory = " + MxLogFileDirectory + nl;

			buf += "[Files]" + nl;
			buf += "MeasSetupFile = " + MeasSetupFile + nl;
			buf += "PromptOutputFile = " + PromptOutputFile + nl;
			buf += "SiteSetupFile = " + SiteSetupFile + nl;

			buf += "[Date&Time Formats]" + nl;
			buf += "StatusBarDateTimeFormat=\"" + StatusBarDateTimeFormat + "\"" + nl;
			buf += "PublishFolderDateTimeFormat=\"" + PublishFolderDateTimeFormat + "\"" + nl;
			buf += "SummaryFileDateFormat=\"" + SummaryFileDateFormat + "\"" + nl;
			buf += "SummaryFileTimeFormat=\"" + SummaryFileTimeFormat + "\"" + nl;
			buf += "ReportDateTimeFormat=\"" + ReportDateTimeFormat + "\"" + nl;
			buf += "LogFilenameDateTimeFormat=\"" + LogFilenameDateTimeFormat + "\"" + nl;
			buf += "LogEntryDateTimeFormat=\"" + LogEntryDateTimeFormat + "\"" + nl;

			buf += "[Scripts]" + nl;
			buf += "StartMxScript = " + StartMxScript + nl;
			buf += "LoadTrayScript = " + LoadTrayScript + nl;
			buf += "RetractTrayScript = " + RetractTrayScript + nl;

			buf += nl;
			buf += "Super User List File " + SuperUserListFilePath + nl;
			buf += "[SuperUser List]" + nl;
			foreach (string s in superUserNames)
				buf += s + nl;
			buf += divider;

			return buf;
		}   // DumpSettings()

		public static string Unquote(string s)
		{
			return s.Replace("\"", "");
		}
	}   // Class ProgramSettings
}
