using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpResetMotion : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpResetMotion()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			AsyncOpStatus status = null;
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			if (mxClient != null)
			{
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_ResettingMotion());
				try
				{
					mxClient.Instrument.ClearEmergencyStop();
					mxClient.Instrument.ClearMotionStop();
					mxClient.Instrument.ClearSafetyFault();
				}
				catch (ZygoError ex)
				{
					_asyncMgr.ReportError(ex.Message, showDialog: true);
					status = new AsyncOpStatus(abortFlag: true);
					return status;
				}
				Thread.Sleep(1000 * ProgramSettings.ResetMotionDelay_sec);
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_HomingStage());
				// Allow for two tries homing axes
				int num_tries = 0;
				while (true)
				{
					try
					{
						num_tries++;
						mxClient.Motion.HomeAll(true);
						break;
					}
					catch (ZygoError ex)
					{
						if (num_tries >= 2)
						{
							_asyncMgr.ReportError(ex.Message, showDialog: true);
							status = new AsyncOpStatus(abortFlag: true);
							return status;
						}
					}
				}
				_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_ResetMotionDone());
			}
			return status;
		}
	}
}
