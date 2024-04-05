using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpMoveTiltAxesLevel : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpMoveTiltAxesLevel()
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
			double R = ProgramSettings.RCoord_deg;
			double P = ProgramSettings.PCoord_deg;
			Tuple<AxisType, double, Unit>[] coords = new Tuple<AxisType, double, Unit>[2];
			try
			{
				coords[0] = new Tuple<AxisType, double, Unit>(AxisType.RY, R, Unit.Degrees);
				coords[1] = new Tuple<AxisType, double, Unit>(AxisType.RX, P, Unit.Degrees);
				mxClient.Motion.MoveAbsolute(coords, true);
			}
			catch (Exception ex)
			{
				return new AsyncOpStatus(abortFlag: true, errorMessage: ex.Message);
			}
			_asyncMgr.ReportStatus("Moved tilt axes level");
			return null;
		}
	}
}
