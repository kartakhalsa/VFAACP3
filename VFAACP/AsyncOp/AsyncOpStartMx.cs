using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.XtraRichEdit.Fields;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VFAACP
{
	public class AsyncOpStartMx : AsyncOp
	{
		private AsyncMgr _asyncMgr;
		private bool _secureDesktop = false;
		private bool _autoStartMx = false;

		public AsyncOpStartMx(bool secureDesktop, bool autoStartMx)
		{
			_asyncMgr = AsyncMgr.Instance();
			_secureDesktop = secureDesktop;
			_autoStartMx = autoStartMx;
		}

		public override AsyncOpStatus DoAsyncOp()
		{
			MxMgr mxMgr = MxMgr.Instance();
			mxMgr.StartMx(_secureDesktop, _autoStartMx);
			return null;
		}
	}
}
