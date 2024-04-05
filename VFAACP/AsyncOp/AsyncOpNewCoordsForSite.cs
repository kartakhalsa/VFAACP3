using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpNewCoordsForSite : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private int _siteNum;
		private bool _prompt;

		public int SiteNum
		{
			set
			{
				_siteNum = value;
			}
		}

		public bool Prompt
		{
			set
			{
				_prompt = value;
			}
		}

		public AsyncOpNewCoordsForSite()
		{
			_asyncMgr = AsyncMgr.Instance();
			_siteNum = 0;
			_prompt = false;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			_asyncMgr.DoGetNewCoordsForSite(_siteNum, _prompt);
			return null;
		}
	}
}
