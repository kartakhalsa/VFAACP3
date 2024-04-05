using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpSetCatseyeOffset : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpSetCatseyeOffset()
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
				_asyncMgr.ReportError("SetCatseyeOffset error: " + status.ErrorMessage, showDialog: status.ShowDialog);
				return status;
			}
			AsyncEventArgs_CatseyeOffset e = new AsyncEventArgs_CatseyeOffset();
			e.stageCoords = stageCoords;
			_asyncMgr.RaiseAsyncEvent(e);
			return null;
		}
	}
}
