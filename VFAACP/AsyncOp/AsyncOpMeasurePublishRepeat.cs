using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
	{
	public class AsyncOpMeasurePublishRepeat : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private List<Site> _siteList;
		private int _numIterations;
		private bool _publishFullData;
		private bool _publishSummaryCsvFile;
		
		public List<Site> SiteList
		{
			set
			{
				_siteList = value;
			}
		}

		public int NumIterations
		{
			set
			{
				_numIterations = value;
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

		public AsyncOpMeasurePublishRepeat()
		{
			_asyncMgr = AsyncMgr.Instance();
			_siteList = null;
			_numIterations = 0;
			_publishFullData = false;
			_publishSummaryCsvFile = false;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			bool publishToPower = false;
			bool pubToExtDatabase = false;

			string protocolNumber = MeasSetup.ProtocolNumber;
			Recipe recipe = MeasSetup.CurrentRecipe;
			string designName = MeasSetup.DesignName;
			string rootFolder = null;
			if (!string.IsNullOrEmpty(protocolNumber))
				rootFolder = ProgramSettings.ToolingCalPublishFileRootDirectory;
			else
				rootFolder = ProgramSettings.PublishFileRootDirectory;

			AsyncOpMeasureSites aoms = new AsyncOpMeasureSites();
			aoms.SiteList = _siteList;

			for (int i = 1; i <= _numIterations; i++)
			{
				_asyncMgr.ReportStatus("Measure/Publish/Repeat " + i.ToString() + " of " + _numIterations.ToString());
				AsyncOpStatus status;
				status = aoms.DoAsyncOp();
				if (status != null)
					return status;
				if (_asyncMgr.StopOrAbortRequested)
					return new AsyncOpStatus(abortFlag: true);
				if (publishToPower || pubToExtDatabase || _publishFullData || _publishSummaryCsvFile)
				{
					DateTime dt = DateTime.Now;
					string subFolder = designName + "_" + dt.ToString(ProgramSettings.PublishFolderDateTimeFormat).ToUpper();
					subFolder = PathSanitizer.SanitizeFilename(subFolder, '_');
					AsyncOpPublishFiles aopf = new AsyncOpPublishFiles();
					aopf.SiteList = _siteList;
					aopf.PublishFullData = _publishFullData;
					aopf.PublishSummaryCsvFile = _publishSummaryCsvFile;
					aopf.DestinationFolder = rootFolder + "\\" + subFolder;;
					aopf.FileFilter = recipe.PublishFilesGroup;
					aopf.RaisePublishDoneEvent = false;
					try
					{
						status = aopf.DoAsyncOp();
					}
					catch (Exception ex)
					{
						_asyncMgr.ReportError(ex.Message);
						return new AsyncOpStatus(abortFlag: true);
					}
					if (_asyncMgr.StopOrAbortRequested)
						return new AsyncOpStatus(abortFlag: true);
				}
			}
			_asyncMgr.ReportStatus("Measure/Publish/Repeat " + _numIterations.ToString() + " iterations complete");
			return null;
		}
	}
}
