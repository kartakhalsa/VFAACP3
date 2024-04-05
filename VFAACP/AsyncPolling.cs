using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using Zygo.MetroProXP.RemoteAccessClient;

namespace VFAACP
{
	public static class AsyncPolling
	{
		static System.Timers.Timer _timer;
		static bool _enabled;
		static EventWaitHandle _sync;
		static DateTime _time;

		public static void Setup()
		{
			const int period_ms = 100;
			_timer = new System.Timers.Timer(period_ms);
			//_pollingTimer.Elapsed += async (sender, e) => await OnPollingTimerEvent;
			_timer.Elapsed += OnPollingTimerEvent;
			_timer.AutoReset = false;
			_enabled = false;
			_sync = new AutoResetEvent(false);
			_time = DateTime.Now;
		}

		public static void Start()
		{
			if (!_enabled)
			{
				_sync.Reset();
				_enabled = true;
				_timer.Start();
			}
		}

		public static void Stop()
		{
			if (_enabled)
			{
				_enabled = false;
				_timer.Start();
				_sync.WaitOne();
			}
		}

		static void OnPollingTimerEvent(Object source, ElapsedEventArgs ev)
		{
			AsyncMgr asyncMgr = AsyncMgr.Instance();
			MxMgr mxMgr = MxMgr.Instance();
			MxClient mxClient = mxMgr.MxClient;
			if (!_enabled)
			{
				_sync.Set();
				return;
			}
			if (mxClient == null)
			{
				_enabled = false;
				_sync.Set();
				return;
			}
			if (ProgramSettings.TestWithoutInstrument > 0)
			{
				_enabled = false;
				_sync.Set();
				return;
			}
			InstrumentService instrument = mxClient.Instrument;
			if (instrument == null)
			{
				_enabled = false;
				_sync.Set();
				return;
			}
			const int interval_sec = 1;
			if ((ev.SignalTime - _time).TotalSeconds < interval_sec)
			{
				_timer.Start();
				return;
			}
			_time = ev.SignalTime;

			try
			{
				if (instrument.IsEmergencyStopActive())
				{
					_enabled = false;
					_sync.Set();
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_EStop());
					return;
				}
				if (instrument.IsMotionStopActive())
				{
					_enabled = false;
					_sync.Set();
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_MStop());
					return;
				}
				if (instrument.IsSafetyFaultActive())
				{
					_enabled = false;
					_sync.Set();
					asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_SafetyFault());
					return;
				}
				{
					AxisType[] axisAry = { AxisType.X, AxisType.Y, AxisType.Z, AxisType.RY, AxisType.RX };
					foreach (AxisType axis in axisAry)
					{
						if (!mxClient.Motion.IsHomed(axis))
						{
							_enabled = false;
							_sync.Set();
							asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_StageNotHomed());
							return;
						}
					}
				}
				mxClient.Motion.SetPendantEnabled(true);
			}
			catch (ZygoError ex)
			{
				_enabled = false;
				_sync.Set();
				asyncMgr.ReportError(ex.Message, true);
				return;
			}
			AsyncOpStatus status = null;
			StageCoords stageCoords;
			status = asyncMgr.GetStageCoords(out stageCoords);
			if (status != null)
			{
				_enabled = false;
				_sync.Set();
				asyncMgr.ReportError(status.ErrorMessage, true);
				return;
			}
			StageCoords loadCoords;
			status = asyncMgr.GetLoadTrayCoords(out loadCoords, useCache: true);
			if (status != null)
			{
				_enabled = false;
				_sync.Set();
				asyncMgr.ReportError(status.ErrorMessage, true);
				return;
			}
			if (AtLoadUnload(stageCoords, loadCoords))
			{
				asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_AtLoadUnload());
			}
			else
			{
				asyncMgr.RaiseAsyncEvent(new AsyncEventArgs_NotAtLoadUnload());
				if (asyncMgr.CrosshairEnabled && StateVars.Tray_Control_Set)
				{
					AsyncEventArgs_CrosshairPos e = new AsyncEventArgs_CrosshairPos();
					e.pos_xy_mm = new PointF((float)stageCoords.X_mm, (float)stageCoords.Y_mm);
					e.show = true;
					asyncMgr.RaiseAsyncEvent(e);
				}
			}
			_timer.Start();
		}

		private static bool AtLoadUnload(StageCoords stageCoords, StageCoords loadCoords)
		{
			const double epsilon_XY_mm = 1.5;
			const double epsilon_Z_mm = 5.0;
			const double epsilon_RP_deg = 0.1;
			if (Math.Abs(stageCoords.X_mm - loadCoords.X_mm) > epsilon_XY_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.Y_mm - loadCoords.Y_mm) > epsilon_XY_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.Z_mm - loadCoords.Z_mm) > epsilon_Z_mm)
			{
				return false;
			}
			if (Math.Abs(stageCoords.R_deg - loadCoords.R_deg) > epsilon_RP_deg)
			{
				return false;
			}
			if (Math.Abs(stageCoords.P_deg - loadCoords.P_deg) > epsilon_RP_deg)
			{
				return false;
			}
			return true;
		}

	}
}
