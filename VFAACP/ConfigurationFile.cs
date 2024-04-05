using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DevExpress.XtraReports.Native;

namespace VFAACP
{
	public class ConfigurationFile
	{
		private string configFile;

		public ConfigurationFile(string path)
		{
			try
			{
				FileInfo fi = new FileInfo(path);
				if (!fi.Exists)
				{
					throw new Exception("Configuration file " + path + " does not exist");
				}
				ReadSettingsFromFile(path);
				configFile = path;
			}
			catch
			{
				throw;
			}
		}

		// Public properties
		public string Name
		{
			get
			{
				return Path.GetFileNameWithoutExtension(configFile);
			}
		}

		// Individual Site Boxes
		public int MoveSiteButton { get; set; }
		public int StoreSitePositionButton { get; set; }
		public int MeasureSiteButton { get; set; }
		public int PrintSiteButton { get; set; }
		// Menu Bar
		public int MxControl { get; set; }
		public int EnableJoystick { get; set; }
		public int MeasurePublishRepeat { get; set; }
		public int MxRemoteStartStop { get; set; }
		public int RefreshProgramSettings { get; set; }
		public int RefreshTray { get; set; }
		public int SetCatseyeOffset { get; set; }
		public int ShowCrosshair { get; set; }
		public int HomeTiltAxes { get; set; }
		public int MoveTiltAxesLevel { get; set; }
		public int UpdateTrayFile { get; set; }
		public int UpdateTrayLevelCoordinates { get; set; }
		public int ClearSelectedSites { get; set; }
		// Stop Button
		public int StopMeasurementSequenceAfterCurrentSite { get; set; }

		// Setup New Measurement
		public int ToolingCalibrationProtocolRun { get; set; }
		public int MoldServiceSetup { get; set; }
		public int BrowseForEngineeringDesignFile { get; set; }
		public int BrowseForProductionDesignFile { get; set; }
		public int BrowseForRecipeFile { get; set; }
		public int MeasureVerificationParts { get; set; }
		public int UseLotNumberToLoadDesignFile { get; set; }
		// Publish Data
		public int AutoPublishForPower { get; set; }
		public int PublishFullData { get; set; }
		public int PublishToExternalDatabase { get; set; }
		public int PublishSummaryCsvFile { get; set; }
		public int ResetDataAfterPublish { get; set; }
		public int ResetDesignAndTrayAfterPublish { get; set; }
		public int DisallowInclExclSiteIfAnyNotPublished { get; set; }
		public int DisallowRemeasureUnpublishedSite { get; set; }
		public int ForceExternalDatabaseFileUpload { get; set; }
		// Load Tray
		public int LoadTrayResetsData { get; set; }
		public int LoadTrayResetsDesign { get; set; }
		// Operations Banner
		public int PrintReportButton { get; set; }
		public int PrintScreenButton { get; set; }
		// Monitoring
		public int MonitoringMeasurementResultsAndTolerances { get; set; }

		public int NumSetupNewMeasurementOptions
		{
			get
			{
				int n = 0;
				if (ToolingCalibrationProtocolRun > 0)
					++n;
				if (MoldServiceSetup > 0)
					++n;
				if (BrowseForEngineeringDesignFile > 0)
					++n;
				if (BrowseForProductionDesignFile > 0)
					++n;
				if (BrowseForRecipeFile > 0)
					++n;
				if (MeasureVerificationParts > 0)
					++n;
				if (UseLotNumberToLoadDesignFile > 0)
					++n;
				return n;
			}
		}

		public bool OnlyOneSetupNewMeasurementOption
		{
			get
			{
				return (NumSetupNewMeasurementOptions == 1);
			}
		}

		private void ReadSettingsFromFile(string path)
		{
			try
			{
				IniFileReader ini = new IniFileReader(path);

				try
				{
					MoveSiteButton = ini.GetInteger("Individual Site Boxes", "MoveSiteButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MoveSiteButton: " + ex.Message);
				}
				if (!(MoveSiteButton == 0 || MoveSiteButton == 1))
					throw new Exception("Error reading item MoveSiteButton: Value must be 0 or 1");

				try
				{
					StoreSitePositionButton = ini.GetInteger("Individual Site Boxes", "StoreSitePositionButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item StoreSitePositionButton: " + ex.Message);
				}
				if (!(StoreSitePositionButton == 0 || StoreSitePositionButton == 1))
					throw new Exception("Error reading item StoreSitePositionButton: Value must be 0 or 1");

				try
				{
					MeasureSiteButton = ini.GetInteger("Individual Site Boxes", "MeasureSiteButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MeasureSiteButton: " + ex.Message);
				}
				if (!(MeasureSiteButton == 0 || MeasureSiteButton == 1))
					throw new Exception("Error reading item MeasureSiteButton: Value must be 0 or 1");

				try
				{
					PrintSiteButton = ini.GetInteger("Individual Site Boxes", "PrintSiteButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PrintSiteButton: " + ex.Message);
				}
				if (!(PrintSiteButton == 0 || PrintSiteButton == 1))
					throw new Exception("Error reading item PrintSiteButton: Value must be 0 or 1");

				try
				{
					MeasurePublishRepeat = ini.GetInteger("Menu Bar", "MeasurePublishRepeat");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MeasurePublishRepeat: " + ex.Message);
				}
				if (!(MeasurePublishRepeat == 0 || MeasurePublishRepeat == 1))
					throw new Exception("Error reading item MeasurePublishRepeat: Value must be 0 or 1");

				try
				{
					HomeTiltAxes = ini.GetInteger("Menu Bar", "HomeTiltAxes");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item HomeTiltAxes: " + ex.Message);
				}
				if (!(HomeTiltAxes == 0 || HomeTiltAxes == 1))
					throw new Exception("Error reading item HomeTiltAxes: Value must be 0 or 1");

				try
				{
					MoveTiltAxesLevel = ini.GetInteger("Menu Bar", "MoveTiltAxesLevel");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MoveTiltAxesLevel: " + ex.Message);
				}
				if (!(MoveTiltAxesLevel == 0 || MoveTiltAxesLevel == 1))
					throw new Exception("Error reading item MoveTiltAxesLevel: Value must be 0 or 1");

				try
				{
					UpdateTrayFile = ini.GetInteger("Menu Bar", "UpdateTrayFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item UpdateTrayFile: " + ex.Message);
				}
				if (!(UpdateTrayFile == 0 || UpdateTrayFile == 1))
					throw new Exception("Error reading item UpdateTrayFile: Value must be 0 or 1");

				try
				{
					UpdateTrayLevelCoordinates = ini.GetInteger("Menu Bar", "UpdateTrayLevelCoordinates");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item UpdateTrayLevelCoordinates: " + ex.Message);
				}
				if (!(UpdateTrayLevelCoordinates == 0 || UpdateTrayLevelCoordinates == 1))
					throw new Exception("Error reading item UpdateTrayLevelCoordinates: Value must be 0 or 1");

				try
				{
					ClearSelectedSites = ini.GetInteger("Menu Bar", "ClearSelectedSites");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ClearSelectedSites: " + ex.Message);
				}
				if (!(ClearSelectedSites == 0 || ClearSelectedSites == 1))
					throw new Exception("Error reading item ClearSelectedSites: Value must be 0 or 1");

				try
				{
					SetCatseyeOffset = ini.GetInteger("Menu Bar", "SetCatseyeOffset");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item SetCatseyeOffset: " + ex.Message);
				}
				if (!(SetCatseyeOffset == 0 || SetCatseyeOffset == 1))
					throw new Exception("Error reading item SetCatseyeOffset: Value must be 0 or 1");

				try
				{
					RefreshTray = ini.GetInteger("Menu Bar", "RefreshTray");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item RefreshTray: " + ex.Message);
				}
				if (!(RefreshTray == 0 || RefreshTray == 1))
					throw new Exception("Error reading item RefreshTray: Value must be 0 or 1");

				try
				{
					ShowCrosshair = ini.GetInteger("Menu Bar", "ShowCrosshair");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ShowCrosshair: " + ex.Message);
				}
				if (!(ShowCrosshair == 0 || ShowCrosshair == 1))
					throw new Exception("Error reading item ShowCrosshair: Value must be 0 or 1");

				try
				{
					MxControl = ini.GetInteger("Menu Bar", "MxControl");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MxControl: " + ex.Message);
				}
				if (!(MxControl == 0 || MxControl == 1))
					throw new Exception("Error reading item MxControl: Value must be 0 or 1");

				try
				{
					EnableJoystick = ini.GetInteger("Menu Bar", "EnableJoystick");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item EnableJoystick: " + ex.Message);
				}
				if (!(EnableJoystick == 0 || EnableJoystick == 1))
					throw new Exception("Error reading item EnableJoystick: Value must be 0 or 1");

				try
				{
					RefreshProgramSettings = ini.GetInteger("Menu Bar", "RefreshProgramSettings");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item RefreshProgramSettings: " + ex.Message);
				}
				if (!(RefreshProgramSettings == 0 || RefreshProgramSettings == 1))
					throw new Exception("Error reading item RefreshProgramSettings: Value must be 0 or 1");

				try
				{
					MxRemoteStartStop = ini.GetInteger("Menu Bar", "MxRemoteStartStop");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MxRemoteStartStop: " + ex.Message);
				}
				if (!(MxRemoteStartStop == 0 || MxRemoteStartStop == 1))
					throw new Exception("Error reading item MxRemoteStartStop: Value must be 0 or 1");

				try
				{
					StopMeasurementSequenceAfterCurrentSite = ini.GetInteger("Stop Button", "StopMeasurementSequenceAfterCurrentSite");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item StopMeasurementSequenceAfterCurrentSite: " + ex.Message);
				}
				if (!(StopMeasurementSequenceAfterCurrentSite == 0 || StopMeasurementSequenceAfterCurrentSite == 1))
					throw new Exception("Error reading item StopMeasurementSequenceAfterCurrentSite: Value must be 0 or 1");

				try
				{
					UseLotNumberToLoadDesignFile = ini.GetInteger("Setup New Measurement", "UseLotNumberToLoadDesignFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item UseLotNumberToLoadDesignFile: " + ex.Message);
				}
				if (!(UseLotNumberToLoadDesignFile == 0 || UseLotNumberToLoadDesignFile == 1))
					throw new Exception("Error reading item UseLotNumberToLoadDesignFile: Value must be 0 or 1");

				try
				{
					ToolingCalibrationProtocolRun = ini.GetInteger("Setup New Measurement", "ToolingCalibrationProtocolRun");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ToolingCalibrationProtocolRun: " + ex.Message);
				}
				if (!(ToolingCalibrationProtocolRun == 0 || ToolingCalibrationProtocolRun == 1))
					throw new Exception("Error reading item ToolingCalibrationProtocolRun: Value must be 0 or 1");

				try
				{
					MoldServiceSetup = ini.GetInteger("Setup New Measurement", "MoldServiceSetup");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MoldServiceSetup: " + ex.Message);
				}
				if (!(MoldServiceSetup == 0 || MoldServiceSetup == 1 || MoldServiceSetup == 2))
					throw new Exception("Error reading item MoldServiceSetup: Value must be 0, 1 or 2");

				try
				{
					BrowseForEngineeringDesignFile = ini.GetInteger("Setup New Measurement", "BrowseForEngineeringDesignFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item BrowseForEngineeringDesignFile: " + ex.Message);
				}
				if (!(BrowseForEngineeringDesignFile == 0 || BrowseForEngineeringDesignFile == 1))
					throw new Exception("Error reading item BrowseForEngineeringDesignFile: Value must be 0 or 1");

				try
				{
					BrowseForProductionDesignFile = ini.GetInteger("Setup New Measurement", "BrowseForProductionDesignFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item BrowseForProductionDesignFile: " + ex.Message);
				}
				if (!(BrowseForProductionDesignFile == 0 || BrowseForProductionDesignFile == 1))
					throw new Exception("Error reading item BrowseForProductionDesignFile: Value must be 0 or 1");

				try
				{
					BrowseForRecipeFile = ini.GetInteger("Setup New Measurement", "BrowseForRecipeFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item BrowseForRecipeFile: " + ex.Message);
				}
				if (!(BrowseForRecipeFile == 0 || BrowseForRecipeFile == 1))
					throw new Exception("Error reading item BrowseForRecipeFile: Value must be 0 or 1");

				try
				{
					MeasureVerificationParts = ini.GetInteger("Setup New Measurement", "MeasureVerificationParts");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MeasureVerificationParts: " + ex.Message);
				}
				if (!(MeasureVerificationParts == 0 || MeasureVerificationParts == 1))
					throw new Exception("Error reading item MeasureVerificationParts: Value must be 0 or 1");

				int numSetupNewMeasurementOptions = 0;
				if (ToolingCalibrationProtocolRun != 0)
					++numSetupNewMeasurementOptions;
				if (MoldServiceSetup != 0)
					++numSetupNewMeasurementOptions;
				if (UseLotNumberToLoadDesignFile != 0)
					++numSetupNewMeasurementOptions;
				if (BrowseForEngineeringDesignFile != 0)
					++numSetupNewMeasurementOptions;
				if (BrowseForProductionDesignFile != 0)
					++numSetupNewMeasurementOptions;
				if (BrowseForRecipeFile != 0)
					++numSetupNewMeasurementOptions;
				if (MeasureVerificationParts != 0)
					++numSetupNewMeasurementOptions;

				if (numSetupNewMeasurementOptions == 0)
				{
					throw new Exception("At least one Setup New Measurement option must be enabled");
				}

				try
				{
					AutoPublishForPower = ini.GetInteger("Publish Data", "AutoPublishForPower");
				}
				catch
				{
					AutoPublishForPower = 0;
				}
				if (!(AutoPublishForPower == 0 || AutoPublishForPower == 1))
					throw new Exception("Error reading item AutoPublishForPower: Value must be 0 or 1");

				try
				{
					PublishFullData = ini.GetInteger("Publish Data", "PublishFullData");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PublishFullData: " + ex.Message);
				}
				if (!(PublishFullData == 0 || PublishFullData == 1))
					throw new Exception("Error reading item PublishFullData: Value must be 0 or 1");

				try
				{
					PublishToExternalDatabase = ini.GetInteger("Publish Data", "PublishToExternalDatabase");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PublishToExternalDatabase: " + ex.Message);
				}
				if (!(PublishToExternalDatabase == 0 || PublishToExternalDatabase == 1))
					throw new Exception("Error reading item PublishToExternalDatabase: Value must be 0 or 1");

				try
				{
					PublishSummaryCsvFile = ini.GetInteger("Publish Data", "PublishSummaryCsvFile");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PublishSummaryCsvFile: " + ex.Message);
				}
				if (!(PublishSummaryCsvFile == 0 || PublishSummaryCsvFile == 1))
					throw new Exception("Error reading item PublishSummaryCsvFile: Value must be 0 or 1");


				if ((PublishToExternalDatabase == 0) && (PublishFullData == 0) && (PublishSummaryCsvFile == 0))
				{
					throw new Exception("At least one Publish Data destination option must be enabled");
				}

				try
				{
					ResetDataAfterPublish = ini.GetInteger("Publish Data", "ResetDataAfterPublish");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ResetDataAfterPublish: " + ex.Message);
				}
				if (!(ResetDataAfterPublish == 0 || ResetDataAfterPublish == 1))
					throw new Exception("Error reading item ResetDataAfterPublish: Value must be 0 or 1");

				try
				{
					ResetDesignAndTrayAfterPublish = ini.GetInteger("Publish Data", "ResetDesignAndTrayAfterPublish");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ResetDesignAndTrayAfterPublish: " + ex.Message);
				}
				if (!(ResetDesignAndTrayAfterPublish == 0 || ResetDesignAndTrayAfterPublish == 1))
					throw new Exception("Error reading item ResetDesignAndTrayAfterPublish: Value must be 0 or 1");
                
				try
				{
					DisallowInclExclSiteIfAnyNotPublished = ini.GetInteger("Publish Data", "DisallowInclExclSiteIfAnyNotPublished");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item DisallowInclExclSiteIfAnyNotPublished: " + ex.Message);
				}
				if (!(DisallowInclExclSiteIfAnyNotPublished == 0 || DisallowInclExclSiteIfAnyNotPublished == 1))
					throw new Exception("Error reading item DisallowInclExclSiteIfAnyNotPublished: Value must be 0 or 1");

				try
				{
					DisallowRemeasureUnpublishedSite = ini.GetInteger("Publish Data", "DisallowRemeasureUnpublishedSite");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item DisallowRemeasureUnpublishedSite: " + ex.Message);
				}
				if (!(DisallowRemeasureUnpublishedSite == 0 || DisallowRemeasureUnpublishedSite == 1))
					throw new Exception("Error reading item DisallowRemeasureUnpublishedSite: Value must be 0 or 1");

				try
				{
					ForceExternalDatabaseFileUpload = ini.GetInteger("Publish Data", "ForceExternalDatabaseFileUpload");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item ForceExternalDatabaseFileUpload: " + ex.Message);
				}
				if (!(ForceExternalDatabaseFileUpload == 0 || ForceExternalDatabaseFileUpload == 1))
					throw new Exception("Error reading item ForceExternalDatabaseFileUpload: Value must be 0 or 1");

				try
				{
					LoadTrayResetsData = ini.GetInteger("Load Tray", "LoadTrayResetsData");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item LoadTrayResetsData: " + ex.Message);
				}
				if (!(LoadTrayResetsData == 0 || LoadTrayResetsData == 1))
					throw new Exception("Error reading item LoadTrayResetsData: Value must be 0 or 1");

				try
				{
					LoadTrayResetsDesign = ini.GetInteger("Load Tray", "LoadTrayResetsDesign");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item LoadTrayResetsDesign: " + ex.Message);
				}
				if (!(LoadTrayResetsDesign == 0 || LoadTrayResetsDesign == 1))
					throw new Exception("Error reading item LoadTrayResetsDesign: Value must be 0 or 1");

				try
				{
					PrintReportButton = ini.GetInteger("Operation Banner", "PrintReportButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PrintReportButton: " + ex.Message);
				}
				if (!(PrintReportButton == 0 || PrintReportButton == 1))
					throw new Exception("Error reading item PrintReportButton: Value must be 0 or 1");

				try
				{
					PrintScreenButton = ini.GetInteger("Operation Banner", "PrintScreenButton");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item PrintScreenButton: " + ex.Message);
				}
				if (!(PrintScreenButton == 0 || PrintScreenButton == 1))
					throw new Exception("Error reading item PrintScreenButton: Value must be 0 or 1");

				try
				{
					MonitoringMeasurementResultsAndTolerances = ini.GetInteger("Monitoring", "MonitoringMeasurementResultsAndTolerances");
				}
				catch (Exception ex)
				{
					throw new Exception("Error reading item MonitoringMeasurementResultsAndTolerances: " + ex.Message);
				}
				if (!(MonitoringMeasurementResultsAndTolerances == 0 || MonitoringMeasurementResultsAndTolerances == 1))
					throw new Exception("Error reading item MonitoringMeasurementResultsAndTolerances: Value must be 0 or 1");
			}
			catch
			{
				throw;
			}
		} // ReadSettingsFromFile()

		public string DumpSettings()
		{
			string nl = Environment.NewLine;
			string buf = "";
			string divider = new string('=', 80) + nl;
			buf += divider;
			buf += "Configuration file " + configFile + nl;

			buf += "[Individual Site Boxes]" + nl;
			buf += "MoveSiteButton = " + MoveSiteButton.ToString() + nl;
			buf += "StoreSitePositionButton = " + StoreSitePositionButton.ToString() + nl;
			buf += "MeasureSiteButton = " + MeasureSiteButton.ToString() + nl;
			buf += "PrintSiteButton = " + PrintSiteButton.ToString() + nl;

			buf += "[Menu Bar]" + nl;
			buf += "MxControl = " + MxControl.ToString() + nl;
			buf += "EnableJoystick = " + EnableJoystick.ToString() + nl;
			buf += "MeasurePublishRepeat = " + MeasurePublishRepeat.ToString() + nl;
			buf += "MxRemoteStartStop = " + MxRemoteStartStop.ToString() + nl;
			buf += "RefreshProgramSettings = " + RefreshProgramSettings.ToString() + nl;
			buf += "RefreshTray = " + RefreshTray.ToString() + nl;
			buf += "SetCatseyeOffset = " + SetCatseyeOffset.ToString() + nl;
			buf += "ShowCrosshair = " + ShowCrosshair.ToString() + nl;
			buf += "HomeTiltAxes = " + HomeTiltAxes.ToString() + nl;
			buf += "MoveTiltAxesLevel = " + MoveTiltAxesLevel.ToString() + nl;
			buf += "UpdateTrayFile = " + UpdateTrayFile.ToString() + nl;
			buf += "UpdateTrayLevelCoordinates = " + UpdateTrayLevelCoordinates.ToString() + nl;
			buf += "ClearSelectedSites = " + ClearSelectedSites.ToString() + nl;

			buf += "[Stop Button]" + nl;
			buf += "StopMeasurementSequenceAfterCurrentSite = " + StopMeasurementSequenceAfterCurrentSite.ToString() + nl;

			buf += "[Setup New Measurement]" + nl;
			buf += "ToolingCalibrationProtocolRun = " + ToolingCalibrationProtocolRun.ToString() + nl;
			buf += "MoldServiceSetup = " + MoldServiceSetup.ToString() + nl;
			buf += "BrowseForEngineeringDesignFile = " + BrowseForEngineeringDesignFile.ToString() + nl;
			buf += "BrowseForProductionDesignFile = " + BrowseForProductionDesignFile.ToString() + nl;
			buf += "BrowseForRecipeFile = " + BrowseForRecipeFile.ToString() + nl;
			buf += "UseLotNumberToLoadDesignFile = " + UseLotNumberToLoadDesignFile.ToString() + nl;
			buf += "MeasureVerificationParts = " + MeasureVerificationParts.ToString() + nl;

			buf += "[Publish Data]" + nl;
			buf += "AutoPublishForPower = " + AutoPublishForPower.ToString() + nl;
			buf += "PublishFullData = " + PublishFullData.ToString() + nl;
			buf += "PublishToExternalDatabase = " + PublishToExternalDatabase.ToString() + nl;
			buf += "PublishSummaryCsvFile = " + PublishSummaryCsvFile.ToString() + nl;
			buf += "ResetDataAfterPublish = " + ResetDataAfterPublish.ToString() + nl;
			buf += "ResetDesignAndTrayAfterPublish = " + ResetDesignAndTrayAfterPublish.ToString() + nl;
			buf += "DisallowInclExclSiteIfAnyNotPublished = " + DisallowInclExclSiteIfAnyNotPublished.ToString() + nl;
			buf += "DisallowRemeasureUnpublishedSite = " + DisallowRemeasureUnpublishedSite.ToString() + nl;
			buf += "ForceExternalDatabaseFileUpload = " + ForceExternalDatabaseFileUpload.ToString() + nl;

			buf += "[Load Tray]" + nl;
			buf += "LoadTrayResetsData = " + LoadTrayResetsData.ToString() + nl;
			buf += "LoadTrayResetsDesign = " + LoadTrayResetsDesign.ToString() + nl;

			buf += "[Operation Banner]" + nl;
			buf += "PrintReportButton = " + PrintReportButton.ToString() + nl;
			buf += "PrintScreenButton = " + PrintScreenButton.ToString() + nl;

			buf += "[Monitoring]" + nl;
			buf += "MonitoringMeasurementResultsAndTolerances = " + MonitoringMeasurementResultsAndTolerances.ToString() + nl;

			buf += divider;
			return buf;
		} // DumpSettings()
	
	} // DumpSettings()
}
