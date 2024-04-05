using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpMeasureSites : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private List<Site> _inputSiteList;
		private bool _allowStageAdj;
		private int _remeasureSite;
		private int _initialSite; // KK figure out if initialSite is handled correctly
		
		public List<Site> SiteList
		{
			set
			{
				_inputSiteList = value;
			}
		}

		public bool AllowStageAdj
		{
			set
			{
				_allowStageAdj = value;
			}
		}

		public int RemeasureSite
		{
			set
			{
				_remeasureSite = value;
			}
		}

		public int InitialSite
		{
			set
			{
				_initialSite = value;
			}
		}

		public AsyncOpMeasureSites()
		{
			_asyncMgr = AsyncMgr.Instance();
			_inputSiteList = null;
			_allowStageAdj = false;
			_remeasureSite = 0;
			_initialSite = 0;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MeasureSites());
			List<Site> measSiteList = new List<Site>();
			Recipe recipe = MeasSetup.CurrentRecipe;
			int numSitesMoved = 0;
			int numSitesMeasuredOK = 0;
			AsyncOpStatus status = null;

			FileSystemFuncs.AppendToLogFile("MeasureSites begin");

			foreach (Site site in _inputSiteList)
			{
				site.IsSelected = false; // Clear selected state
			}
			_asyncMgr.UpdateSiteViews(_inputSiteList);

			// Count sites
			int numSitesIncluded = 0;
			int numCalibrationSitesIncluded = 0;
			foreach (Site site in _inputSiteList)
			{
				if (site.IsIncludedUserState)
				{
					if (site.IsCalibrationSite)
					{
						site.ResultText = "Skipped";
						_asyncMgr.ReportStatus("Skipping calibration site " + site.Index.ToString());
					}
					else
					{
						measSiteList.Add(site);
					}
					++numSitesIncluded;
					if (site.IsCalibrationSite)
						++numCalibrationSitesIncluded;
				}
			}

			_asyncMgr.UpdateSiteViews(_inputSiteList);

			if (measSiteList.Count == 0)
			{
				return null;
			}

			if ((_allowStageAdj == false) &&
				(ProgramSettings.Home_Tilt_Axes_At_First_Site == 1) && 
				(ProgramSettings.Home_Tilt_Axes_At_Each_Site == 0))
			{
				// KK TODO
				//if (_asyncMgr.DoHomeTiltAxes() == false)
				//    return new AsyncOpStatus(abortFlag: true);
			}

			foreach (Site site in measSiteList)
			{
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MeasureSites());
				int siteNum = site.Index;

				_asyncMgr.ReportMxStatus(null); 

				// Test for Cancel
				if (_asyncMgr.StopOrAbortRequested)
				{
					if (_asyncMgr.StopRequested)
						_asyncMgr.ReportError("Measure Included Sites canceled by user", showDialog: true);
					StateVars.Measure_Tray_Incomplete = true;
					StateVars.Waiting_For_Stop = false;
					site.ResultText = "";
					site.ResultBitmap1 = null;
					site.ResultBitmap2 = null;
					site.IsMeasuredErr = false;
					site.IsMeasuredOK = false;
					site.IsMeasuring = false;
					_asyncMgr.UpdateSiteViews(_inputSiteList);
					FileSystemFuncs.AppendToLogFile("MeasureSites return Abort");
					return new AsyncOpStatus(abortFlag: true);
				}

				if (_remeasureSite == 0 || (siteNum == _remeasureSite))
				{
					site.ResultText = "";
					site.ResultBitmap1 = null;
					site.ResultBitmap2 = null;
					site.IsMeasuredErr = false;
					site.IsMeasuredOK = false;
					site.IsPublished = false;
					site.IsMeasuring = true;
					_asyncMgr.UpdateSiteViews(_inputSiteList);
					StageCoords siteCoords = site.GetAbsoluteCoords(recipe);
					if (ProgramSettings.TestWithoutInstrument == 0)
					{
						if (_allowStageAdj)
						{
							StageCoords stageCoords;
							status = _asyncMgr.GetStageCoords(out stageCoords);
							if ((status != null) && status.HasError)
							{
								_asyncMgr.ReportError("Error getting stage coords: " + status.ErrorMessage, showDialog: status.ShowDialog);
								return status;
							}
							double dx = stageCoords.X_mm - siteCoords.X_mm;
							double dy = stageCoords.Y_mm - siteCoords.Y_mm;
							double dist = Math.Sqrt((dx * dx) + (dy * dy));

							if (dist > ProgramSettings.StageAdjustDeltaRadius_mm)
							{
								_asyncMgr.ReportStatus("Moving to site " + site.Index.ToString());
								status = _asyncMgr.MoveToSiteXYZRP(siteCoords);
								if ((status != null) && status.HasAbortOrError)
									return status;
							}
							else
							{
								_asyncMgr.ReportStatus("Moving Z to site " + site.Index.ToString());
								status = _asyncMgr.MoveToSiteZ(siteCoords);
								if ((status != null) && status.HasAbortOrError)
									return status;
							}
						}
						else
						{   // _allowStageAdj==false
							bool homeTiltAxes = false;
							if ((ProgramSettings.Home_Tilt_Axes_At_Each_Site == 1) ||
								((ProgramSettings.Home_Tilt_Axes_At_First_Site == 1) && (numSitesMoved == 0)))
								homeTiltAxes = true;
							if (homeTiltAxes)
								_asyncMgr.ReportStatus("Homing RP axes and moving stage (X,Y,Z,R,P) to site " + site.Index.ToString());
							else
								_asyncMgr.ReportStatus("Moving to site " + site.Index.ToString());

							status = _asyncMgr.MoveToSiteXYZRP(siteCoords, homeTiltAxes);
							if ((status != null) && status.HasAbortOrError)
								return status;
							++numSitesMoved;
						}
					}
					else // ProgramSettings.TestWithoutInstrument > 0
					{
						bool simulateMoveError = false;
						if (simulateMoveError)
						{
							// Simulate move error
							site.IsMeasuredErr = true;
							site.ResultText = "Simulated move error";
							StateVars.Measure_Tray_Incomplete = true;
							_asyncMgr.UpdateSiteViews(_inputSiteList);
							_asyncMgr.ReportError("Site " + siteNum.ToString() + " simulated move ERROR", showDialog: status.ShowDialog);
						}
						// KK work out how to handle fake stage coords
						//this.fakeX = X;
						//this.fakeY = Y;
						//this.fakeZ = Z;
						//this.fakeR = R;
						//this.fakeP = P;
					}

					// Test for Cancel
					if (_asyncMgr.StopOrAbortRequested)
					{
						if (_asyncMgr.StopRequested)
							_asyncMgr.ReportError("Measure site(s) canceled by user", showDialog: status.ShowDialog);
						StateVars.Measure_Tray_Incomplete = true;
						StateVars.Waiting_For_Stop = false;
						site.ResultText = "";
						site.ResultBitmap1 = null;
						site.ResultBitmap2 = null;
						site.IsMeasuredErr = false;
						site.IsMeasuredOK = false;
						site.IsMeasuring = false;
						_asyncMgr.UpdateSiteViews(_inputSiteList);
						FileSystemFuncs.AppendToLogFile("MeasureSites return abort");
						return new AsyncOpStatus(abortFlag: true);
					}

					FileSystemFuncs.ClearInterimSiteData(siteNum);

					FileSystemFuncs.WriteSiteSetupFile(site);
					_asyncMgr.ReportStatus("Measuring site " + siteNum.ToString());
					status = _asyncMgr.RunScript(recipe.MeasureScript);
					if ((status != null) && status.HasError)
					{
						_asyncMgr.ReportError("Error measuring site " + siteNum.ToString() + ": " + status.ErrorMessage, showDialog: status.ShowDialog);
						site.IsMeasuredErr = true;
						site.ResultText = "ERROR: " + status.ErrorMessage;
						if (status.AbortFlag)
							return status;
					}
					if (!site.IsMeasuredErr && site.SiteBox.AutoStoreEnabled)
					{
						status = _asyncMgr.DoGetNewCoordsForSite(siteNum, prompt: false);
						if ((status != null) && status.HasError)
							return status;
					}

					FileSystemFuncs.CopyToInterim(siteNum);
					string subDir = "Site" + siteNum.ToString("00");
					string subPath = Path.Combine(ProgramSettings.InterimFileRootDirectory, subDir);

					if (!site.IsMeasuredErr)
					{
						// Read result text file from Interim Directory
						string resultTxtPath = Path.Combine(subPath, recipe.ResultTxtFile);
						string resultText = "";
						if (File.Exists(resultTxtPath))
						{
							resultText = _asyncMgr.Read_Display_Results_File_Cmd(resultTxtPath);
						}
						else
						{
							resultText = "ERROR: Result text file not found";
							site.IsMeasuredErr = true;
						}
						site.ResultText = resultText;
					}

					// Recipe should specify one or two bitmap files for display
					if (!site.IsMeasuredErr)
					{
						if (!string.IsNullOrEmpty(recipe.ResultBmp1File))
						{
							string path = Path.Combine(subPath, recipe.ResultBmp1File);
							try
							{
								using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
								{
									site.ResultBitmap1 = Image.FromStream(fs);
								}
							}
							catch
							{
								string msg = "Error reading bitmap file " + path;
								FileSystemFuncs.AppendToLogFile(msg);
								site.ResultText += Environment.NewLine + "ERROR: " + msg;
								site.IsMeasuredErr = true;
							}
						}
					}
					if (!site.IsMeasuredErr)
					{
						if (!string.IsNullOrEmpty(recipe.ResultBmp2File))
						{
							string path = Path.Combine(subPath, recipe.ResultBmp2File);
							try
							{
								using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
								{
									site.ResultBitmap2 = Image.FromStream(fs);
								}
							}
							catch
							{
								string msg = "Error reading bitmap file " + path;
								FileSystemFuncs.AppendToLogFile(msg);
								site.ResultText += Environment.NewLine + "ERROR: " + msg;
								site.IsMeasuredErr = true;
							}
						}
					}
					if (!site.IsMeasuredErr && site.ResultText.Contains("ERROR"))
					{
						site.IsMeasuredErr = true;
					}
					if (!site.IsMeasuredErr)
					{
						site.IsMeasuredOK = true;
						numSitesMeasuredOK++;
					}
					_asyncMgr.UpdateSiteViews(_inputSiteList);
				}
			} // foreach (Site site in measSiteList)
            
			StateVars.Measure_Tray_Incomplete = false;
			StateVars.Waiting_For_Stop = false;

			_asyncMgr.ReportStatus("Measure sites done");

			if (numSitesMeasuredOK == 0)
			{
				FileSystemFuncs.AppendToLogFile("No site measured OK");
				FileSystemFuncs.AppendToLogFile("MeasureSites return Abort");
				return new AsyncOpStatus(abortFlag: true);
			}
			FileSystemFuncs.AppendToLogFile("MeasureSites done");
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MeasureSitesDone());
			return null;
		}   // DoAsyncOp()

	}
}
