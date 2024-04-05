using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpRecoverFromMotionError : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpRecoverFromMotionError()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			//_asyncMgr.DoResetMotionControl("motion error");
			return null;
		}
	}
}
