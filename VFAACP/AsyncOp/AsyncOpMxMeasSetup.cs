using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
    public class AsyncOpMxMeasSetup : AsyncOp
    {
		private AsyncMgr _asyncMgr;
        private Recipe _recipe;
        private string _trayFile;

        public AsyncOpMxMeasSetup(Recipe recipe, string trayFile)
        {
            _asyncMgr = AsyncMgr.Instance();
            _recipe = recipe;
            _trayFile = trayFile;
        }

        public override AsyncOpStatus DoAsyncOp()
        {
            _asyncMgr.ReportStatus("Mx measurement setup");

            if (!string.IsNullOrEmpty(_recipe.SettingsFile))
            {
                _asyncMgr.ReportStatus("Loading settings file " + _recipe.SettingsFile);
                // TODO
            }

            if (!string.IsNullOrEmpty(_recipe.SetupScript))
            {
                _asyncMgr.ReportStatus("Running setup script");
                AsyncOpStatus status = _asyncMgr.RunScript(_recipe.SetupScript);
                if ((status != null) && status.Error)
                {
                    _asyncMgr.ReportError("Error running setup script: " + status.ErrorMessage, showDialog: status.ShowDialog);
                    return status;
                }
            }
            AsyncEventArgs_MxMeasSetupDone e = new AsyncEventArgs_MxMeasSetupDone();
            e.trayFile = _trayFile;
			_asyncMgr.RaiseAsyncEvent(e);
            return null;
        }
    }
}
