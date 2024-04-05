using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpUpdateTrayLevelCoords : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpUpdateTrayLevelCoords()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			StageCoords stageCoords;
			AsyncOpStatus status;
			status = _asyncMgr.GetStageCoords(out stageCoords);
			if ((status != null) && status.HasError)
			{
				_asyncMgr.ReportError("Error getting stage coords: " + status.ErrorMessage, showDialog: status.ShowDialog);
				return status;
			}
			AsyncEventArgs_TrayLevelCoords e = new AsyncEventArgs_TrayLevelCoords();
			e.stageCoords = stageCoords;
			_asyncMgr.RaiseAsyncEvent(e);
			return null;
		}
	}
}
