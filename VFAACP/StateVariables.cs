using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VFAACP
{
    public enum MxStates
    {
        Ready,
        Disconnected,
        EStop,
        MStop,
        StageNotHomed
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

    class StateVariables
    {
        // This class holds static state variables

        public static event StateVarEventHandler StateChanged;

        private static MxStates _mxState = MxStates.Disconnected;
        private static bool _asyncBusy = false;
        private static bool _designControlSet = false;
        private static bool _trayControlSet = false;
        private static bool _loadTrayInProgress = false;
        private static bool _measureTrayInProgress = false;
        private static bool _measureTrayIncomplete = false;
        private static bool _moveToSiteInProgress = false;
        private static bool _measureSiteInProgress = false;
        private static bool _publishInProgress = false;
        private static bool _waitingForStop = false;

        private static void StateValChanged(string StateItem)
        {
            StateChanged(null, new StateVarEventArgs(StateItem));
        }

        public static MxStates MxState
        {
            get { return _mxState; }
            set 
            {
                if (_mxState != value)
                {
                    _mxState = value;
                    StateValChanged("MxState");
                }
            }
        }

        public static bool Async_Busy
        {
            get { return _asyncBusy; }
            set 
            {
                if (_asyncBusy != value)
                {
                    _asyncBusy = value;
                    StateValChanged("Async_Busy");
                }
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

        public static bool Load_Tray_In_Progress
        {
            get { return _loadTrayInProgress; }
            set
            {
                if (_loadTrayInProgress != value)
                {
					_loadTrayInProgress = value;
                    StateValChanged("Load_Tray_In_Progress");
                }
            }
        }

        public static bool Measure_Tray_In_Progress
        {
            get { return _measureTrayInProgress; }
            set 
            {
                if (_measureTrayInProgress != value)
                {
                    _measureTrayInProgress = value;
                    StateValChanged("Measure_Tray_In_Progress");
                }
            }
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

        public static bool Move_To_Site_In_Progress
        {
            get { return _moveToSiteInProgress; }
            set
            {
                if (_moveToSiteInProgress != value)
                {
                    _moveToSiteInProgress = value;
                    StateValChanged("Move_To_Site_In_Progress");
                }
            }
        }

        public static bool Measure_Site_In_Progress
        {
            get { return _measureSiteInProgress; }
            set
            {
                if (_measureSiteInProgress != value)
                {
                    _measureSiteInProgress = value;
                    StateValChanged("Measure_Site_In_Progress");
                }
            }
        }

        public static bool Publish_In_Progress
        {
            get { return _publishInProgress; }
            set
            {
                if (_publishInProgress != value)
                {
                    _publishInProgress = value;
                    StateValChanged("Publish_In_Progress");
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

        public static bool CP_Is_Busy
        {
            get { return _asyncBusy || _loadTrayInProgress || _measureTrayInProgress || _moveToSiteInProgress || _measureSiteInProgress || _publishInProgress; }
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
