using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Drawing;

namespace VFAACP
{
	public enum Mx_States
	{
		Connected,
		Disconnected,
		EStop,
		MStop,
		SafetyFault,
		StageNotHomed
	}

	public enum CP_States
	{
		AtLoadUnload,
		Error,
		HomingStage,
		Measuring,
		MovingToIdle,
		MovingToLoadUnload,
		MovingToSite,
		Publishing,
		ResetMotion,
		Ready,
		Setup,
		StartingMx,
		WaitingForStop
	}

	public class StateVarEventArgs : EventArgs
	{
		private readonly string _stateValChanged;

		// Constructor
		public StateVarEventArgs(string stateValChanged)
		{
			_stateValChanged = stateValChanged;
		}

		public string StateValChanged
		{
			get { return _stateValChanged; }
		}

	}   // Class StateVarEventArgs

	public delegate void StateVarEventHandler(object sender, StateVarEventArgs e);

	class StateVars
	{
		// This class holds static state variables

		public static event StateVarEventHandler StateChanged;

		private static Mx_States _Mx_State = Mx_States.Disconnected;
		private static CP_States _CP_State = CP_States.Ready;

		private static bool _designControlSet = false;
		private static bool _trayControlSet = false;

		private static bool _measureTrayIncomplete = false;

		private static bool _waitingForStop = false;

		private static void StateValChanged(string stateItem)
		{
			StateChanged(null, new StateVarEventArgs(stateItem));
		}

		public static Mx_States Mx_State
		{
			get { return _Mx_State; }
			set 
			{
				if (_Mx_State != value)
				{
					_Mx_State = value;
					StateValChanged("Mx_State");
				}
			}
		}
		public static CP_States CP_State
		{
			get { return _CP_State; }
			set
			{
				if (_CP_State != value)
				{
					_CP_State = value;
					StateValChanged("CP_State");
				}
			}
		}

		public static bool CP_StartingMx
		{
			get
			{
				switch (_CP_State)
				{
					case CP_States.StartingMx:
						return true;
				}
				return false;
			}
		}
		public static bool CP_Busy
		{
			get
			{
				switch (_CP_State)
				{
					case CP_States.HomingStage:
					case CP_States.Measuring:
					case CP_States.MovingToLoadUnload:
					case CP_States.MovingToSite:
					case CP_States.Publishing:
					case CP_States.ResetMotion:
					case CP_States.Setup:
					case CP_States.StartingMx:
					case CP_States.WaitingForStop:
						return true;
				}
				return false;
			}
		}

		public static bool CP_Moving_Or_Measuring
		{
			get
			{
				switch (_CP_State)
				{
					case CP_States.Measuring:
					case CP_States.MovingToLoadUnload:
					case CP_States.MovingToSite:
						return true;
				}
				return false;
			}
		}

		public static bool Design_Control_Set
		{
			get { return _designControlSet; }
			set 
			{
				if (_designControlSet != value)
				{
					_designControlSet = value;
					StateValChanged("Design_Control_Set");
				}
			}
		}

		public static bool Tray_Control_Set
		{
			get { return _trayControlSet; }
			set 
			{
				if (_trayControlSet != value)
				{
					_trayControlSet = value;
					StateValChanged("Tray_Control_Set");
				}
			}
		}

		public static bool Design_And_Tray_Controls_Set
		{
			get { return _designControlSet && _trayControlSet; }
		}

		public static bool Measure_Tray_Incomplete
		{
			get { return _measureTrayIncomplete; }
			set
			{
				if (_measureTrayIncomplete != value)
				{
					_measureTrayIncomplete = value;
					StateValChanged("Measure_Tray_Incomplete");
				}
			}
		}

		public static bool Waiting_For_Stop
		{
			get { return _waitingForStop; }
			set
			{
				if (_waitingForStop != value)
				{
					_waitingForStop = value;
					StateValChanged("Waiting_For_Stop");
				}
			}
		}

		public static bool Any_Included_Data_Not_Published(List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (s.IsIncludedUserState && !s.IsCalibrationSite && !s.IsPublished && (s.IsMeasuredOK || s.IsMeasuredErr))
				{
					result = true;
					break;
				}
			}
			return result;
		}   // Any_Included_Data_Not_Published()

		public static bool Any_Selected_Data_Not_Published(List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (s.IsSelected && !s.IsCalibrationSite && !s.IsPublished && (s.IsMeasuredOK || s.IsMeasuredErr))
				{
					result = true;
					break;
				}
			}
			return result;
		}   // Any_Selected_Data_Not_Published()

		public static bool Included_Site_Data_Not_Published(int siteNum, List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (s.Index == siteNum)
				{
					if (s.IsIncludedUserState && !s.IsCalibrationSite && !s.IsPublished && (s.IsMeasuredOK || s.IsMeasuredErr))
					{
						string subDir = "Site" + siteNum.ToString("00");
						string path = ProgramSettings.InterimFileRootDirectory + "\\" + subDir;
						if (FileSystemFuncs.DirectoryExistsAndIsNotEmpty(path))
						{
							result = true;
						}
					}
					break;
				}
			}
			return result;
		}   // Included_Site_Data_Not_Published()

		public static bool Any_Data_Not_Published(List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (!s.IsCalibrationSite && !s.IsPublished && FileSystemFuncs.AnyFilesInSiteIterimFolder(s.Index))
				{
					result = true;
					break;
				}
			}
			return result;
		}   // Any_Data_Not_Published()

		public static bool Any_Included_Sites(List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (s.IsIncludedUserState)
				{
					result = true;
					break;
				}
			}
			return result;
		}   // Any_Included_Sites()

		public static bool Any_Selected_Sites(List<Site> sites)
		{
			if (sites == null)
				return false;
			bool result = false;
			foreach (Site s in sites)
			{
				if (s.IsSelected)
				{
					result = true;
					break;
				}
			}
			return result;
		}   // Any_Selected_Sites()

		public static void UpdateState()
		{
			StateValChanged("Update");
		}

	}   // Class StateVariables
}
