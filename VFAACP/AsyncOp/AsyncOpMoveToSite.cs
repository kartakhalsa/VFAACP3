using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpMoveToSite : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private Site _destSite;

		public Site DestinationSite
		{
			set
			{
				_destSite = value;
			}
		}

		public AsyncOpMoveToSite()
		{
			_asyncMgr = AsyncMgr.Instance();
			_destSite = null;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			AsyncOpStatus status = null;
			AsyncEventArgs_MovingToSite e1 = new AsyncEventArgs_MovingToSite();
			e1.siteIndex = _destSite.Index;
			_asyncMgr.RaiseAsyncEvent(e1);
			StageCoords siteCoords = _destSite.GetAbsoluteCoords(MeasSetup.CurrentRecipe);
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				status = _asyncMgr.MoveToSiteXYZRP(siteCoords);
				if ((status != null) && status.HasError)
				{
					_asyncMgr.ReportError("Error moving to site " + _destSite.Index.ToString() + ": " + status.ErrorMessage);
					return status;
				}
			}
			AsyncEventArgs_MoveToSiteDone e2 = new AsyncEventArgs_MoveToSiteDone();
			e2.siteIndex = _destSite.Index;
			_asyncMgr.RaiseAsyncEvent(e2);
			return status;
		}
	}
}
