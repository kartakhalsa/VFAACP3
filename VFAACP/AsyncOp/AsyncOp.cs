using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VFAACP
{
	// A class to encapsulate an operation to be executed asynchronously by a background thread.
	public abstract class AsyncOp
	{
		public AsyncOp()
		{
		}

		public abstract AsyncOpStatus DoAsyncOp();
	}
}
