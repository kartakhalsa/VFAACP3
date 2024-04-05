using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpInstrumentCheck : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpInstrumentCheck()
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
			//if (!_asyncMgr.Mx_Connected)
			//	return new AsyncOpStatus(abortFlag: true, errorMessage: "Mx is disconnected");
			try
			{
				if (mxClient.Instrument.IsEmergencyStopActive())
					return new AsyncOpStatus(abortFlag: true, errorMessage: "Emergency Stop");
				if (mxClient.Instrument.IsMotionStopActive())
					return new AsyncOpStatus(abortFlag: true, errorMessage: "Motion Stop");
				if (mxClient.Instrument.IsSafetyFaultActive())
					return new AsyncOpStatus(abortFlag: true, errorMessage: "Safety Fault");
				AxisType[] axisAry = { AxisType.X, AxisType.Y, AxisType.Z, AxisType.RY, AxisType.RX };
				foreach (AxisType axis in axisAry)
					if (!mxClient.Motion.IsHomed(axis))
						return new AsyncOpStatus(abortFlag: true, errorMessage: "A stage axis is not homed");
			}
			catch (ZygoError ex)
			{
				return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
			}
			return null;
		}
	}
}
