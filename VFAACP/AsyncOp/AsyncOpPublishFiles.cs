using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpPublishFiles : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private List<Site> _siteList;
		private bool _publishFullData;
		private bool _publishSummaryCsvFile;
		private bool _publishToExtDatabase;
		private bool _publishToPower;
		private string _destinationFolder;
		private string _fileFilter;
		private bool _raisePublishDoneEvent;
		
		public List<Site> SiteList
		{
			set
			{
				_siteList = value;
			}
		}

		public bool PublishFullData
		{
			set
			{
				_publishFullData = value;
			}
		}
		
		public bool PublishSummaryCsvFile
		{
			set
			{
				_publishSummaryCsvFile = value;
			}
		}

		public bool PublishToExtDatabase
		{
			set
			{
				_publishToExtDatabase = value;
			}
		}

		public bool PublishToPower
		{
			set
			{
				_publishToPower = value;
			}
		}

		public string DestinationFolder
		{
			set
			{
				_destinationFolder = value;
			}
		}

		public string FileFilter
		{
			set
			{
				_fileFilter= value;
			}
		}
		public bool RaisePublishDoneEvent
		{
			set
			{
				_raisePublishDoneEvent = value;
			}
		}

		public AsyncOpPublishFiles()
		{
			_asyncMgr = AsyncMgr.Instance();
			_siteList = null;
			_publishFullData = false;
			_publishSummaryCsvFile = false;
			_publishToExtDatabase = false;
			_publishToPower = false;
			_destinationFolder = null;
			_fileFilter = null;
			_raisePublishDoneEvent = false;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_Publish());
			string ret;

			_asyncMgr.ReportStatus("Publishing files");

			string msg = "";

			Recipe recipe = MeasSetup.CurrentRecipe;

			List<Site> sitesToPublish = new List<Site>();
			foreach (Site s in _siteList)
			{
				if (s.IsIncludedUserState && (!s.IsCalibrationSite) && (s.IsMeasuredOK || s.IsMeasuredErr))
					sitesToPublish.Add(s);
			}

			// Create Summary File
			int numSitesInSummary = 0;
			ret = FileSystemFuncs.MakeSummaryFile(sitesToPublish, recipe.ResultCsv1File, out numSitesInSummary);
			// ret is error message
			if (!string.IsNullOrEmpty(ret))
			{
				throw new Exception("Error creating summary CSV file: " + ret);
			}

			if (_publishToExtDatabase)
			{
				string inputFile = Path.Combine(ProgramSettings.InterimFileRootDirectory, "Summary.csv");
				string outputFile = FileSystemFuncs.ExternalDatabaseFilePath;
				try
				{
					XMLFuncs.WriteSummaryToXML(inputFile, outputFile);
					FileSystemFuncs.AppendToLogFile("Published external database file " + outputFile);
				}
				catch (Exception ex)
				{
					throw new Exception("Error publishing to external database: " + ex.Message);
				}
			}

			if (_publishFullData)
			{
				// Before publishing, the summary file is named 'Summary.csv'
				// After publishing, the file is renamed

				int FilesPublished = 0;
				int SitesPublished = 0;

				foreach (Site s in sitesToPublish)
				{
					int siteNum = s.Index;
					int curFilesPublished = FilesPublished;

					// A publish group may contain a list of semi-colon separated filter strings
					string[] pgroupitems = _fileFilter.Split(new string[] { ";" }, StringSplitOptions.None);

					foreach (string filtstr in pgroupitems)
					{
						ret = FileSystemFuncs.PublishFiles(siteNum, _destinationFolder, filtstr.Trim());
						// ret is either file count or error message
						int fileCount = 0;
						if (int.TryParse(ret, out fileCount) == false)
						{
							throw new Exception("Error publishing files from site " + siteNum.ToString() + ": " + ret);
						}
						FilesPublished += fileCount;
					}

					if (FilesPublished > curFilesPublished)
					{
						SitesPublished++;
						s.IsPublished = true;
					}
				}

				// Publish any remaining files in the root Interim directory

				// Each publish group may contain a list of semi-colon separated filter strings
				string[] pgroupitems2 = _fileFilter.Split(new string[] { ";" }, StringSplitOptions.None);

				foreach (string filtstr in pgroupitems2)
				{
					ret = FileSystemFuncs.PublishFiles(-1, _destinationFolder, filtstr.Trim());
					// ret is either file count or error message
					int fileCount = 0;
					if (int.TryParse(ret, out fileCount) == false)
					{
						throw new Exception("Error publishing files from root Interim directory: " + ret);
					}
					FilesPublished += fileCount;
				}
				msg = "Full data for " + SitesPublished.ToString();
				if (SitesPublished != 1)
					msg += " sites";
				else
					msg += " site";
				msg += " (" + FilesPublished.ToString() + " files) published to " + _destinationFolder;
				_asyncMgr.ReportStatus(msg);
			}
			else if (_publishSummaryCsvFile)
			{
				ret = FileSystemFuncs.PublishFiles(-1, _destinationFolder, "Summary.csv");
				// ret is either file count or error message
				int fileCount = 0;
				if ((int.TryParse(ret, out fileCount) == false) || (fileCount < 1))
				{
					throw new Exception("Error publishing summary CSV file: " + ret);
				}
				msg = "Summary CSV file for " + numSitesInSummary.ToString();
				if (numSitesInSummary != 1)
					msg += " sites";
				else
					msg += " site";
				msg += " published to " + _destinationFolder;
				_asyncMgr.ReportStatus(msg);
			}

			if (_publishToPower && ProgramSettings.OkAutoPublishForPower)
			{
				// Create Auto Publish Directory
				DateTime dt = DateTime.Now;
				string name = MeasSetup.PublishName;
				if (string.IsNullOrEmpty(name))
					name = "NA";
				string lot = MeasSetup.LotNumber;
				if (string.IsNullOrEmpty(lot))
					lot = "NA";
				string rootFolder = ProgramSettings.AutoPublishForPowerDirectory;
				string subFolder = name + "_" + lot + "_" + dt.ToString(ProgramSettings.PublishFolderDateTimeFormat).ToUpper();
				string pubFolder = Path.Combine(rootFolder, subFolder);

				try
				{
					int FilesPublished = 0;
					int SitesPublished = 0;

					if (rootFolder == null)
					{
						throw new Exception("AutoPublishForPower root folder is null");
					}
					if (!Directory.Exists(rootFolder))
					{
						throw new Exception("AutoPublishForPower root folder " + rootFolder + " does not exist");
					}

					foreach (Site s in sitesToPublish)
					{
							int curFilesPublished = FilesPublished;

						// A publish group may contain a list of semi-colon separated filter strings
						string[] pgroupitems = _fileFilter.Split(new string[] { ";" }, StringSplitOptions.None);

						foreach (string filtstr in pgroupitems)
						{
							int siteNum = s.Index;
							ret = FileSystemFuncs.PublishFiles(siteNum, pubFolder, filtstr.Trim());
							// ret is either file count or error message
							int fileCount = 0;
							if (int.TryParse(ret, out fileCount) == false)
							{
								throw new Exception("Error publishing files from site " + siteNum.ToString() + ": " + ret);
							}
							FilesPublished += fileCount;
						}

						if (FilesPublished > curFilesPublished)
						{
							SitesPublished++;
						}
					}
					msg = "A total of " + SitesPublished.ToString();
					if (SitesPublished != 1)
						msg += " sites";
					else
						msg += " site";
					msg += " (" + FilesPublished.ToString() + " files) were auto-published to " + pubFolder;
					_asyncMgr.ReportStatus(msg);
				}
				catch (Exception ex)
				{
					throw new Exception("AutoPublishForPower error: " + ex.Message);
				}
			}
			foreach (Site s in sitesToPublish)
			{
				s.IsPublished = true;
			}
			if (_raisePublishDoneEvent)
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_PublishDone());
			return null;

		}
	}
}
