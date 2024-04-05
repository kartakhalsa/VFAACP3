using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class AsyncEventArgs_StatusMessage : EventArgs
	{
		public string statusMessage;
		public AsyncEventArgs_StatusMessage(string msg)
		{
			statusMessage = msg;
		}
	}

	public class AsyncEventArgs_MxStatusMessage : EventArgs
	{
		public string statusMessage;
		public AsyncEventArgs_MxStatusMessage(string msg)
		{
			statusMessage = msg;
		}
	}

	public class AsyncEventArgs_PromptMessage : EventArgs
	{
		public string promptMessage;
		public AsyncEventArgs_PromptMessage(string msg)
		{
			promptMessage = msg;
		}
	}

	public class AsyncEventArgs_ReportError : EventArgs
	{
		public string message;
		public bool showDialog;
		public AsyncEventArgs_ReportError(string message, bool showDialog = false)
		{
			this.message = message;
			this.showDialog = showDialog;
		}
	}

	public class AsyncEventArgs_StartingMx : EventArgs
	{
	}
	public class AsyncEventArgs_WaitForMxStart : EventArgs
	{
		public int remaining_sec;
	}
	public class AsyncEventArgs_WaitForMxReady : EventArgs
	{
		public int remaining_sec;
	}
	public class AsyncEventArgs_MxIsReady : EventArgs
	{
	}
	public class AsyncEventArgs_StartMxError : EventArgs
	{
		public string errorMessage;
	}
	public class AsyncEventArgs_ConnectingToMx : EventArgs
	{
	}
	public class AsyncEventArgs_DisconnectingFromMx : EventArgs
	{
	}
	public class AsyncEventArgs_MxConnected : EventArgs
	{
	}
	public class AsyncEventArgs_MxDisconnected : EventArgs
	{
	}

	public class AsyncEventArgs_ResettingMotion : EventArgs
	{
	}
	public class AsyncEventArgs_ResetMotionDone : EventArgs
	{
	}

	public class AsyncEventArgs_LoadTrayDone : EventArgs
	{
	}

	public class AsyncEventArgs_MovingToSite : EventArgs
	{
		public int siteIndex;
	}
	public class AsyncEventArgs_MoveToSiteDone : EventArgs
	{
		public int siteIndex;
	}

	public class AsyncEventArgs_MeasureSites : EventArgs
	{
	}
	public class AsyncEventArgs_MeasureSitesDone : EventArgs
	{
	}

	public class AsyncEventArgs_MeasSetup : EventArgs
	{
	}
	public class AsyncEventArgs_MeasSetupError : EventArgs
	{
		public string errorMessage;
	}
	public class AsyncEventArgs_MeasSetupDone : EventArgs
	{
		public string trayFile;
	}

	public class AsyncEventArgs_MeasuredSites : EventArgs
	{
		public List<Site> siteList;
	}

	public class AsyncEventArgs_PerformAsyncOps : EventArgs
	{
	}
	public class AsyncEventArgs_PerformAsyncOpsDone : EventArgs
	{
	}

	public class AsyncEventArgs_Publish : EventArgs
	{
	}
	public class AsyncEventArgs_PublishDone : EventArgs
	{
	}

	public class AsyncEventArgs_EStop : EventArgs
	{
	}
	public class AsyncEventArgs_MStop : EventArgs
	{
	}
	public class AsyncEventArgs_SafetyFault : EventArgs
	{
	}
	public class AsyncEventArgs_StageNotHomed : EventArgs
	{
	}
	public class AsyncEventArgs_HomingStage : EventArgs
	{
	}
	public class AsyncEventArgs_HomingTiltAxes : EventArgs
	{
	}
	public class AsyncEventArgs_HomeTiltAxesDone : EventArgs
	{
	}
	public class AsyncEventArgs_MovingToLoadUnload : EventArgs
	{
	}
	public class AsyncEventArgs_MoveToLoadUnloadDone : EventArgs
	{
	}
	public class AsyncEventArgs_AtLoadUnload : EventArgs
	{
	}
	public class AsyncEventArgs_NotAtLoadUnload : EventArgs
	{
	}

	public class AsyncEventArgs_CrosshairPos : EventArgs
	{
		public bool show;
		public PointF pos_xy_mm; 
	}

	public class AsyncEventArgs_NewCoordsForSite : EventArgs
	{
		public int siteNum;
		public StageCoords stageCoords; 
		public bool prompt;
	}

	public class AsyncEventArgs_CatseyeOffset : EventArgs
	{
		public StageCoords stageCoords; 
	}

	public class AsyncEventArgs_TrayLevelCoords : EventArgs
	{
		public StageCoords stageCoords; 
	}
}
