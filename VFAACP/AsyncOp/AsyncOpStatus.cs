using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncOpStatus
	{
		public bool AbortFlag { get; set; }
		public string ErrorMessage { get; set; }
		public bool ShowDialog { get; set; }
		
		public bool HasError
		{
			get
			{
				return !string.IsNullOrEmpty(ErrorMessage);
			}
		}

		public bool HasAbortOrError
		{
			get
			{
				return AbortFlag || !string.IsNullOrEmpty(ErrorMessage);
			}
		}

		public AsyncOpStatus()
		{
			AbortFlag = false;
			ErrorMessage = null;
			ShowDialog = false;
		}

		public AsyncOpStatus(bool abortFlag = false, string errorMessage = null, bool showDialog = false)
		{
			AbortFlag = abortFlag;
			ErrorMessage = errorMessage;
			ShowDialog = showDialog;
		}
	}
}
