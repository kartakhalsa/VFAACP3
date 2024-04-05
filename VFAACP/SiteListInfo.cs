using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace VFAACP
{
	public class SiteListInfo
	{   // Manage list of tray sites and a site map

		private int lastSiteMeasured = -1;

		private List<Site> siteList = null;

		private SiteMap siteMap = null;

		public void NewSiteList(List<Site> newSiteList)
		{
			siteList = newSiteList;
			UpdateMap();
		}

		public void UpdateMap()
		{
			siteMap = new SiteMap(siteList);
		}

		public void Reset()
		{
			siteList = null;
			siteMap = null;
		}

		public List<Site> Sites
		{
			get { return siteList; }
		}

		public SiteMap SiteMap
		{
			get { return siteMap; }
		}

		public List<Site> IncludedSites
		{
			get
			{
				List<Site> inclSites = new List<Site>();
				foreach (Site s in siteList)
				{
					if (s.IsIncludedUserState)
						inclSites.Add(s);
				}
				return inclSites;
			}
		}

		public int NumSites
		{
			get
			{
				if (siteList != null)
					return siteList.Count();
				else
					return 0;
			}
		}

		public int NumSitesIncluded
		{
			get
			{
				if (siteList == null)
					return 0;
				int n = 0;
				foreach (Site s in siteList)
				{
					if (s.IsIncludedUserState)
						n++;
				}
				return n;
			}
		}

		public int NumCalibrationSitesIncluded
		{
			get
			{
				if (siteList == null)
					return 0;
				int n = 0;
				foreach (Site s in siteList)
				{
					if (s.IsCalibrationSite && s.IsIncludedUserState)
					{
						n++;
					}
				}
				return n;
			}
		}

		public int LastSiteMeasuredOK
		{
			get { return lastSiteMeasured; }
			set { lastSiteMeasured = value; }
		}

		public Bitmap Draw(Bitmap bm, float fontSizeScale)
		{
			if (siteMap == null)
				return null;
			else
				return siteMap.Draw(bm, fontSizeScale);
		}   // Draw()

		public void SavedSiteCoords()
		{
			foreach (Site s in siteList)
			{
				s.IsModified = false;
				s.SiteBox.HighlightStoreButton = false;
			}
		}   // SavedSiteCoords()

		public bool AnySiteModified()
		{
			if (siteList == null)
				return false;
			foreach (Site s in siteList)
			{
				if (s.IsModified)
					return true;
			}
			return false;
		}   // AnySiteModified()

		public int NumSitesModified()
		{
			if (siteList == null)
				return 0;
			int count = 0;
			foreach (Site s in siteList)
			{
				if (s.IsModified)
					++count;
			}
			return count;
		}   // NumSitesModified()

		public int HitTest_Sites(Point p, string SelectOrInclude)
		{
			if (siteMap == null)
				return 0;
			else
				return siteMap.HitTest_Sites(p, SelectOrInclude);
		}   // HitTest_Sites()

	}   // Class SiteListInfo
}
