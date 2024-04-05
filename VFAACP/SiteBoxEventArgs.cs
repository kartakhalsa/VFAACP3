using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFAACP
{
	public class SiteBoxEventArgs
	{
		public SiteBoxEventArgs(object Param, bool State)
		{
			this.Param = Param;
			this.State = State;
		}

		public object Param;
		public bool State;
	}
}
