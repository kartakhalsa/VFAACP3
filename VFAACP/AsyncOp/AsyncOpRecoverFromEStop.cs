using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpRecoverFromEStop : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpRecoverFromEStop()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			try
			{
				mxClient.Instrument.ClearEmergencyStop();
				const int clear_estop_sleep_ms = 3000;
				Thread.Sleep(clear_estop_sleep_ms);
				mxClient.Motion.HomeAll(true);
			}
			catch (ZygoError ex)
			{
				_asyncMgr.ReportError(ex.Message, showDialog: true);
				return new AsyncOpStatus(abortFlag: true);
			}
			return null;
		}
	}
}
