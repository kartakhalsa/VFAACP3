using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpLoadUnload : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpLoadUnload()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MovingToLoadUnload());
			AsyncOpStatus status = null;
			StageCoords loadCoords = new StageCoords();
			status = _asyncMgr.GetLoadTrayCoords(out loadCoords);
			if ((status != null) && status.HasError)
			{
				_asyncMgr.ReportError("Error getting load position coordinates: " + status.ErrorMessage);
				return status;
			}
			status = _asyncMgr.MoveToCoords(loadCoords);
			if ((status != null) && status.HasError)
			{
				_asyncMgr.ReportError("Error moving to Load/Unload position: " + status.ErrorMessage);
				return status;
			}
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MoveToLoadUnloadDone());
			return status;
		}
	}
}
