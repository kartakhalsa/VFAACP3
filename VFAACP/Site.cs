using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using VFAACP;

namespace VFAACP
{
	public class Site
	{
		// Encapsulate properties and behaviors of a measurement site

		// Site Icon Files
		string curSiteIconFile;
		string siteNotIncludedAndNotSelected = "NotIncludedAndNotSelected";
		string siteIncludedAndNotSelected = "IncludedAndNotSelected";
		string siteNotIncludedAndSelected = "NotIncludedAndSelected";
		string siteIncludedAndSelected = "IncludedAndSelected";
		string siteIncludedAndMeasuring = "IncludedAndMeasuring";
		string siteIncludedAndMeasuredOK = "IncludedAndMeasuredOK";
		string siteIncludedAndMeasuredERR = "IncludedAndMeasuredERR";
		string siteSelectedAndMeasuredOK = "SelectedAndMeasuredOK";
		string siteSelectedAndMeasuredERR = "SelectedAndMeasuredERR";

		private int index;

		// Position info
		private double x;
		private double y;
		private double z_offset;
		private double r_offset;
		private double p_offset;

		// Orientation (angle)
		private double axis;

		// Measured position info
		private double meas_x;
		private double meas_y;

		// State info
		private bool isIncludedFileState;
		private bool isIncludedUserState;
		private bool isCalibrationSite;
		private bool isSelected;
		private bool isMeasuring;
		private bool isMeasuredOK;
		private bool isMeasuredErr;
		private bool isPublished;
		private bool isModified;

		// Display info
		private string resultText;
		private Image resultBitmap1;
		private Image resultBitmap2;
		private SiteBox siteBox;

		public Site(int Index, double X, double Y, double Z_Offset, double R_Offset, double P_Offset, int Included, int ForCalibration, double Axis)
		{
			index = Index;
			x = X;
			y = Y;
			z_offset = Z_Offset;
			r_offset = R_Offset;
			p_offset = P_Offset;
			axis = Axis;
			isIncludedFileState = (Included == 1);
			isIncludedUserState = (Included == 1);
			isCalibrationSite = (ForCalibration == 1);
			isSelected = false;
			isMeasuring = false;
			isMeasuredOK = false;
			isMeasuredErr = false;
			isPublished = false;
			isModified = false;
			resultText = "";
			if (isIncludedFileState)
			{
				curSiteIconFile = siteIncludedAndNotSelected;
			}
			else
			{
				curSiteIconFile = siteNotIncludedAndNotSelected;
			}
		} // Site()

		public int Index
		{
			get { return index; }
		}

		public double[] Position
		{
			get { return new double[] { x, y, z_offset, r_offset, p_offset }; }
			set
			{
				try
				{
					x = value[0];
					y = value[1];
					z_offset = value[2];
					r_offset = value[3];
					p_offset = value[4];
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
		}

		public double X
		{
			get { return x; }
			set { x = value; }
		}

		public double Y
		{
			get { return y; }
			set { y = value; }
		}

		public double Z_Offset
		{
			get { return z_offset; }
			set { z_offset = value; }
		}

		public double R_Offset
		{
			get { return r_offset; }
			set { r_offset = value; }
		}

		public double P_Offset
		{
			get { return p_offset; }
			set { p_offset = value; }
		}

		public double Axis
		{
			get { return axis; }
			set { axis = value; }
		}

		public SiteBox SiteBox
		{
			get { return siteBox; }
			set { siteBox = value; }
		}

		public bool IsIncludedFileState
		{
			get { return isIncludedFileState; }
			set 
			{ 
				isIncludedFileState = value;
				SetIconForState();
			}
		}

		public bool IsIncludedUserState
		{
			get { return isIncludedUserState; }
			set
			{
				isIncludedUserState = value;
				SetIconForState();
			}
		}

		public bool IsCalibrationSite
		{
			get { return isCalibrationSite; }
			set
			{
				isCalibrationSite = value;
			}
		}

		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				isSelected = value;
				SetIconForState();
			}
		}

		public bool IsMeasuring
		{
			get { return isMeasuring; }
			set
			{
				isMeasuring = value;
				if (isMeasuring)
				{
					isMeasuredOK = false;
					isMeasuredErr = false;
				}
				SetIconForState();
			}
		}

		public bool IsMeasuredOK
		{
			get { return isMeasuredOK; }
			set
			{
				isMeasuredOK = value;
				if (isMeasuredOK)
				{
					isMeasuring = false;
					isMeasuredErr = false;
				}
				SetIconForState();
			}
		}

		public bool IsMeasuredErr
		{
			get { return isMeasuredErr; }
			set
			{
				isMeasuredErr = value;
				if (isMeasuredErr)
				{
					isMeasuring = false;
					isMeasuredOK = false;
				}
				SetIconForState();
			}
		}

		public bool IsPublished
		{
			get { return isPublished; }
			set { isPublished = value; }
		}

		public bool IsModified
		{
			get { return isModified; }
			set { isModified = value; }
		}

		public string IconFile
		{
			get { return curSiteIconFile; }
		}

		public string ResultText
		{
			get { return resultText; }
			set { resultText = value; }
		}

		public Image ResultBitmap1
		{
			get { return resultBitmap1; }
			set { resultBitmap1 = value; }
		}

		public Image ResultBitmap2
		{
			get { return resultBitmap2; }
			set { resultBitmap2 = value; }
		}

		public void SetMeasuredXY(double X, double Y)
		{
			meas_x = X;
			meas_y = Y;
		}

		private void SetIconForState()
		{
			if (!isIncludedUserState && !isSelected) curSiteIconFile = siteNotIncludedAndNotSelected;
			if (isIncludedUserState && !isSelected) curSiteIconFile = siteIncludedAndNotSelected;
			if (!isIncludedUserState && isSelected) curSiteIconFile = siteNotIncludedAndSelected;
			if (isIncludedUserState && isSelected) curSiteIconFile = siteIncludedAndSelected;
			if (isIncludedUserState && isMeasuring) curSiteIconFile = siteIncludedAndMeasuring;
			if (isIncludedUserState && isMeasuredOK) curSiteIconFile = siteIncludedAndMeasuredOK;
			if (isIncludedUserState && isMeasuredErr) curSiteIconFile = siteIncludedAndMeasuredERR;
			if (!isIncludedUserState && isMeasuredOK) curSiteIconFile = siteIncludedAndMeasuredOK;
			if (!isIncludedUserState && isMeasuredErr) curSiteIconFile = siteIncludedAndMeasuredERR;
			if (isSelected && isMeasuredOK) curSiteIconFile = siteSelectedAndMeasuredOK;
			if (isSelected && isMeasuredErr) curSiteIconFile = siteSelectedAndMeasuredERR;
		}   // SetIconForState()

		public StageCoords GetAbsoluteCoords(Recipe recipe)
		{
			StageCoords c = new StageCoords();
			c.X_mm = this.X;
			c.Y_mm = this.Y;
			c.Z_mm = this.Z_Offset + ProgramSettings.ZCoord_mm;
			c.R_deg = this.R_Offset + ProgramSettings.RCoord_deg;
			c.P_deg = this.P_Offset + ProgramSettings.PCoord_deg;
			if (recipe != null)
			{
				c.Z_mm += recipe.ZCoord_mm;
				c.R_deg += recipe.RCoord_deg;
				c.P_deg += recipe.PCoord_deg;
			}
			return c;
		}   // GetAbsoluteCoords()

		public void ApplyDeltaStageCoords(double deltaX, double deltaY, double deltaZ, double deltaR, double deltaP)
		{
			this.X += deltaX;
			this.Y += deltaY;
			this.Z_Offset += deltaZ;
			this.R_Offset += deltaR;
			this.P_Offset += deltaP;
		}   // ApplyDeltas()

		public bool NearXY(double testX, double testY)
		{
			double dx = Math.Abs(this.X - testX);
			double dy = Math.Abs(this.Y - testY);
			double lim = Math.Abs(ProgramSettings.DeltaXYLimit_mm);
			if ((dx <= lim) && (dy <= lim))
				return true;
			else
				return false;
		}
	}   // Class Site
}
