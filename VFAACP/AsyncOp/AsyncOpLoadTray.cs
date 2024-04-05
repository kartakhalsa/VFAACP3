using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VFAACP
{
    public class AsyncOpLoadTray : AsyncOp
    {
		private AsyncMgr _asyncMgr;
		private const int TestWithoutInstrumentMoveTime_msec = 3000;

        public AsyncOpLoadTray()
        {
            _asyncMgr = AsyncMgr.Instance();
        }

		private AsyncOpStatus RunLoadTrayScript()
		{
			AsyncOpStatus status = null;
			_asyncMgr.ReportStatus("Moving tray to Load/Unload position");
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				status = _asyncMgr.RunScript(ProgramSettings.LoadTrayScript);
			}
			else
			{
				Thread.Sleep(TestWithoutInstrumentMoveTime_msec);
			}
			if ((status != null) && status.Error)
			{
				_asyncMgr.ReportError("Error running script " + ProgramSettings.LoadTrayScript + ": " + status.ErrorMessage, showDialog: status.ShowDialog);
			}
			return status;
		}

		private AsyncOpStatus RunRetractTrayScript()
		{
			AsyncOpStatus status = null;
			_asyncMgr.ReportStatus("Moving tray to idle position");
			if (ProgramSettings.TestWithoutInstrument == 0)
			{
				status = _asyncMgr.RunScript(ProgramSettings.RetractTrayScript);
			}
			else
			{
				Thread.Sleep(TestWithoutInstrumentMoveTime_msec);
			}
			if ((status != null) && status.Error)
			{
				_asyncMgr.ReportError("Error running script " + ProgramSettings.RetractTrayScript + ": " + status.ErrorMessage, showDialog: status.ShowDialog);
			}
			return status;
		}

		public override AsyncOpStatus DoAsyncOp()
        {
			AsyncOpStatus status = null;
			LoadTimer dlg = new LoadTimer(ProgramSettings.LoadTrayCountdown_sec);
			while (true)
			{
				status = RunLoadTrayScript();
				if (status != null)
					return status;
				_asyncMgr.ReportStatus("Ready to Load/Unload Tray");
				DialogResult dr = dlg.ShowDialog();
				status = RunRetractTrayScript();
				if (status != null)
					return status;
				if (dr != DialogResult.Retry)
					break;
			}
			_asyncMgr.ReportStatus(null);
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_LoadTrayDone());
            return null;
        }
    }
}
