using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpEnableJoystick : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpEnableJoystick()
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
				mxClient.Motion.SetPendantEnabled(true);
			}
			catch (ZygoError ex)
			{
				return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
			}

			return null;
		}
	}
}
