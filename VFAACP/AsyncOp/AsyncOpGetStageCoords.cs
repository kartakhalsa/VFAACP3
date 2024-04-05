using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpGetStageCoords : AsyncOp
	{
		private AsyncMgr _asyncMgr;

		public AsyncOpGetStageCoords()
		{
			_asyncMgr = AsyncMgr.Instance();
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			return null;
		}
	}
}
