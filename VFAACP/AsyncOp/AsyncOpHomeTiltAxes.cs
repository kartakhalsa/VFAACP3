using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpHomeTiltAxes : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpHomeTiltAxes()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				_asyncMgr.ReportStatus("ProgramSettings.TestWithoutInstrument = " + ProgramSettings.TestWithoutInstrument.ToString());
				return null;
			}
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			try
			{
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_HomingTiltAxes());
				mxClient.Motion.HomeRP(true);
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_HomeTiltAxesDone());
			}
			catch (Exception ex)
			{
				return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
			}

			return null;
		}
	}
}
