using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
    public class AsyncOpResetMotionControl : AsyncOp
    {
		private AsyncMgr _asyncMgr;

        public AsyncOpResetMotionControl()
        {
            _asyncMgr = AsyncMgr.Instance();
        }

        public override AsyncOpStatus DoAsyncOp()
        {
            try
            {
                _asyncMgr.ReportStatus("Resetting moton control");
                _asyncMgr.MxClient.Instrument.ClearEmergencyStop();
                _asyncMgr.MxClient.Instrument.ClearMotionStop();
                _asyncMgr.MxClient.Instrument.ClearSafetyFault();
                const int clear_estop_sleep_ms = 3000;
                Thread.Sleep(clear_estop_sleep_ms);
                _asyncMgr.ReportStatus("Homing stages");
                _asyncMgr.MxClient.Motion.HomeAll(true);
                _asyncMgr.ReportStatus(null);
                StateVariables.MxState = MxStates.Ready;
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
