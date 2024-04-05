using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public class AsyncOpMeasSetup : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private Recipe _recipe;
		private string _trayFile;

		public AsyncOpMeasSetup(Recipe recipe, string trayFile)
		{
			_asyncMgr = AsyncMgr.Instance();
			_recipe = recipe;
			_trayFile = trayFile;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			_asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MeasSetup());
			_asyncMgr.ReportStatus("Measurement setup");

			if (!string.IsNullOrEmpty(_recipe.SetupScript))
			{
				_asyncMgr.ReportStatus("Running setup script");
				AsyncOpStatus status = _asyncMgr.RunScript(_recipe.SetupScript);
				if ((status != null) && status.HasError)
				{
					AsyncEventArgs_MeasSetupError e1 = new AsyncEventArgs_MeasSetupError();
					e1.errorMessage = status.ErrorMessage;
					_asyncMgr.RaiseAsyncEvent(e1);
					return status;
				}
			}
			AsyncEventArgs_MeasSetupDone e = new AsyncEventArgs_MeasSetupDone();
			e.trayFile = _trayFile;
			_asyncMgr.RaiseAsyncEvent(e);
			return null;
		}
	}
}
