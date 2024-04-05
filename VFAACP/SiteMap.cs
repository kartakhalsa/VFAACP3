using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace VFAACP
{
	// SiteMap class handles a graphic showing tray sites
	public class SiteMap
	{
		private const double coordsDefaultExtMM = 100.0; // Site map plot default extent
		private const double plotBorderFrac = 0.05; // Site map plot border as fraction of max extent of coords
		private const double plotSiteIconMinDiamFrac = 0.10; // Site icon min diameter as fraction of max extent of coords
		private const double plotSiteIconMaxDiamFrac = 0.20; // Site icon max diameter as fraction of max extent of coords

		private double xmin, xmax, ymin, ymax;
		private double xctr, yctr;
		private double xext, yext, maxext;
		private double border;

		private double siteIconDiamMM;
		private double siteIconRadiusMM = 0.0;
		private double graphicsResMMPX = 0.0;

		private bool _crosshairEnabled = false;
		private PointF _crosshairPos_mm = new PointF(0,0);

		private List<Site> siteList;

		public SiteMap(List<Site> newSiteList)
		{
			siteList = newSiteList;
			GetSiteCoordsMinMax();
			xext = xmax - xmin;
			yext = ymax - ymin;
			maxext = Math.Max(xext, yext);
			if (siteList.Count == 1)
			{
				xmin -= (coordsDefaultExtMM / 2.0);
				xmax += (coordsDefaultExtMM / 2.0);
				ymin -= (coordsDefaultExtMM / 2.0);
				ymax += (coordsDefaultExtMM / 2.0);
				xext = xmax - xmin;
				yext = ymax - ymin;
				maxext = Math.Max(xext, yext);
				siteIconDiamMM = plotSiteIconMaxDiamFrac * maxext;
			}
			else
			{
				double minspacing = GetSiteCoordsMinSpacing();
				siteIconDiamMM = 0.8 * minspacing;
				double d1 = plotSiteIconMinDiamFrac * maxext;
				double d2 = plotSiteIconMaxDiamFrac * maxext;
				if (siteIconDiamMM < d1)
					siteIconDiamMM = d1;
				else if (siteIconDiamMM > d2)
					siteIconDiamMM = d2;
			}
			border = plotBorderFrac * maxext;
			siteIconRadiusMM = siteIconDiamMM / 2.0;
			graphicsResMMPX = siteIconDiamMM / 100;
			double adj = siteIconRadiusMM + border;
			xmin -= adj;
			xmax += adj;
			ymin -= adj;
			ymax += adj;
			xext = xmax - xmin;
			yext = ymax - ymin;
			maxext = Math.Max(xext, yext);
			xctr = (xmin + xmax) / 2.0;
			yctr = (ymin + ymax) / 2.0;
		}

		private void GetSiteCoordsMinMax()
		{
			xmax = -999.0;
			xmin = 999.0;
			ymax = -999.0;
			ymin = 999.0;
			foreach (Site s in siteList)
			{
				if (s.X < xmin) { xmin = s.X; }
				if (s.X > xmax) { xmax = s.X; }
				if (s.Y < ymin) { ymin = s.Y; }
				if (s.Y > ymax) { ymax = s.Y; }
			}
		}

		private double GetSiteCoordsMinSpacing()
		{
			double minSpacing = 999.0;
			if ((siteList == null) || (siteList.Count <= 1))
				return 0.0;
			foreach (Site s1 in siteList)
			{
				foreach (Site s2 in siteList)
				{
					if (s1 != s2)
					{
						double dx = s1.X - s2.X;
						double dy = s1.Y - s2.Y;
						double dist = Math.Sqrt((dx * dx) + (dy * dy));
						if (dist < minSpacing)
							minSpacing = dist;
					}
				}
			}
			return minSpacing;
		}

		public bool UdateCrosshair(bool show, PointF pos_mm)
		{
			bool change = false;
			if (_crosshairEnabled != show)
				change = true;
			_crosshairEnabled = show;
			if (_crosshairEnabled)
			{
				const double epsilon_mm = 0.01;
				double dx_mm = _crosshairPos_mm.X - pos_mm.X;
				double dy_mm = _crosshairPos_mm.Y - pos_mm.Y;
				if ((Math.Abs(dx_mm) > epsilon_mm) || (Math.Abs(dy_mm) > epsilon_mm))
					change = true;
				_crosshairPos_mm = pos_mm;
			}
			return change;
		}

		public Bitmap Draw(Bitmap bm, float fontSizeScale)
		{
			// Make graphics from Image
			Graphics g = Graphics.FromImage(bm);
			g.FillRectangle(new SolidBrush(Color.White), 0, 0, bm.Width, bm.Height);
			try
			{
				// Draw measurement sites to a graphics object in world coordinates

				// Zoom by ratio of client extent / plot extent in pixels
				Size plotExtents = new Size((int)(xext / graphicsResMMPX), (int)(yext / graphicsResMMPX));
				float zm = GraphicsFuncs.GetScaleFactor(bm.Size, plotExtents);
				g.ScaleTransform(zm, zm);

				// Translate to center the extents in the viewport
				float tx = -(float)((-xctr / graphicsResMMPX) - ((bm.Size.Width * 0.5) / zm));
				float ty = (float)((-yctr / graphicsResMMPX) + ((bm.Size.Height * 0.5) / zm));
				g.TranslateTransform(tx, ty);

				// Store the transformation matrix for future use
				GraphicsFuncs.GraphicsMatrix = g.Transform;

				// Format font and alignment for site numbers
				float fontSize = fontSizeScale / zm;
				Font numFont = new Font("Courier New", fontSize, FontStyle.Bold, GraphicsUnit.Point);
				StringFormat sF = new StringFormat();
				sF.Alignment = StringAlignment.Center;

				// Loop through each site and plot
				int indx = 0;

				foreach (Site s in siteList)
				{
					indx++;

					try
					{
						// Get the PNG icon file. They are 100px by 100px in size, by spec.
						Bitmap b = null;

						switch (s.IconFile)
						{
							case "IncludedAndMeasuredERR":
								b = Properties.Resources.IncludedAndMeasuredERR;
								break;
							case "IncludedAndMeasuredOK":
								b = Properties.Resources.IncludedAndMeasuredOK;
								break;
							case "IncludedAndMeasuring":
								b = Properties.Resources.IncludedAndMeasuring;
								break;
							case "IncludedAndNotSelected":
								b = Properties.Resources.IncludedAndNotSelected;
								break;
							case "IncludedAndSelected":
								b = Properties.Resources.IncludedAndSelected;
								break;
							case "NotIncludedAndSelected":
								b = Properties.Resources.NotIncludedAndSelected;
								break;
							case "NotIncludedAndNotSelected":
								b = Properties.Resources.NotIncludedAndNotSelected;
								break;
							case "SelectedAndMeasuredERR":
								b = Properties.Resources.SelectedAndMeasuredERR;
								break;
							case "SelectedAndMeasuredOK":
								b = Properties.Resources.SelectedAndMeasuredOK;
								break;
						}

						// Set all white pixels in the icon file to be transparent
						b.MakeTransparent(Color.White);

						// Draw to screen; origin is upper left corner.  Adjust so center is at site location
						// Invert Y due to GDI+ coordinate system

						// Set transparency based on state; if MeasuredOK or MeasuredErr, adjust transparency depending on Included state
						float trans = 1.0f;
						if ((s.IsMeasuredOK || s.IsMeasuredErr) && !s.IsIncludedUserState)
						{
							trans = 0.5f;
						}

						// Zygo Stage Coordinates
						//
						// |-------------------|
						// |         |         |
						// |  (+,-)  |  (-,-)  |
						// |         |         |
						// |-------------------|
						// |         |         |
						// |  (+,+)  |  (-,+)  |
						// |         |         |
						// |-------------------|

						int ix = (int)((-s.X - siteIconRadiusMM) / graphicsResMMPX);
						int iy = -(int)((-s.Y + siteIconRadiusMM) / graphicsResMMPX);
						g.DrawImage(b, new Rectangle(ix, iy, b.Width, b.Height), 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, GraphicsFuncs.MakeTransparency(trans));

					}
					catch
					{
						// Exception will be thrown if icon file not found (not tested)
						// If exception, only plot the site number, do not stop
						// Should post a message to the status bar
					}

					// Add site label
					string label = indx.ToString();
					if (s.IsCalibrationSite)
						label += "C";
					SizeF tSize = g.MeasureString(label, numFont);
					float fx = (float)((-s.X / graphicsResMMPX) - (tSize.Width * 0.5));
					float fy = -(float)((-s.Y / graphicsResMMPX) + (tSize.Height * 0.48));
					RectangleF tRect = new RectangleF(fx, fy, (float)(tSize.Width), (float)(tSize.Height));
					g.DrawString(label, numFont, Brushes.Black, tRect, sF);
				}

				// Draw a crosshair to show FOV position (if applicable)

				// debug
				//crosshairPos_mm = new PointF((float)(xctr+2.5), (float)(yctr-2.5));
				//crosshairPos_mm = new PointF(-8.14f, -48.803f);
				// end debug

				if (_crosshairEnabled)
				{
					if (_crosshairPos_mm.X > xmax - 1f) _crosshairPos_mm.X = (float)xmax - 1f;
					if (_crosshairPos_mm.X < xmin + 1f) _crosshairPos_mm.X = (float)xmin + 1f;
					if (_crosshairPos_mm.Y > ymax - 1f) _crosshairPos_mm.Y = (float)ymax - 1f;
					if (_crosshairPos_mm.Y < ymin + 1f) _crosshairPos_mm.Y = (float)ymin + 1f;

					if (_crosshairPos_mm.X >= xmin && _crosshairPos_mm.X <= xmax && _crosshairPos_mm.Y >= ymin && _crosshairPos_mm.Y <= ymax)
					{
						g.DrawLine(new Pen(Color.RoyalBlue), (float)((-_crosshairPos_mm.X - 5.0f) / graphicsResMMPX), (float)(_crosshairPos_mm.Y / graphicsResMMPX), (float)((-_crosshairPos_mm.X - 1.0f) / graphicsResMMPX), (float)(_crosshairPos_mm.Y / graphicsResMMPX));
						g.DrawLine(new Pen(Color.RoyalBlue), (float)((-_crosshairPos_mm.X + 1.0f) / graphicsResMMPX), (float)(_crosshairPos_mm.Y / graphicsResMMPX), (float)((-_crosshairPos_mm.X + 5.0f) / graphicsResMMPX), (float)(_crosshairPos_mm.Y / graphicsResMMPX));
						g.DrawLine(new Pen(Color.RoyalBlue), (float)(-_crosshairPos_mm.X / graphicsResMMPX), (float)((_crosshairPos_mm.Y - 5.0f) / graphicsResMMPX), (float)(-_crosshairPos_mm.X / graphicsResMMPX), (float)((_crosshairPos_mm.Y - 1.0f) / graphicsResMMPX));
						g.DrawLine(new Pen(Color.RoyalBlue), (float)(-_crosshairPos_mm.X / graphicsResMMPX), (float)((_crosshairPos_mm.Y + 1.0f) / graphicsResMMPX), (float)(-_crosshairPos_mm.X / graphicsResMMPX), (float)((_crosshairPos_mm.Y + 5.0f) / graphicsResMMPX));
					}
				}
				numFont.Dispose();
				sF.Dispose();
			}
			catch { }

			return bm;
		}   // Draw()

		public int HitTest_Sites(Point p, string SelectOrInclude)
		{
			if (siteList == null)
				return 0;
			// Point p is a mouse coordinate in client space
			// A left-click will Select/Deselect a site
			// A ctrl-click will Include/Exclude a site
			// Since we draw scaled images to the graphics area, we don't know the exact
			// size.  Therefore we will approximate the position with a scaled circle.
			// We don't use a circle because the corners may overlap
			int hitIndx = -1;

			if (SelectOrInclude.ToUpper() == "SELECT" || SelectOrInclude.ToUpper() == "INCLUDE")
			{

				int indx = 0;
				// Put point in a point array; needed by TransformPoints method
				Point[] pts = new Point[] { p };
				// Get a temporary graphics template to work with
				Graphics g = GraphicsFuncs.GetGraphicsTemplate();
				// Apply transformations
				g.Transform = GraphicsFuncs.GraphicsMatrix;
				// Send mouse position to world coordinates
				g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, pts);
				g.Dispose();

				// Loop through each site and test for a hit using GraphicsPath.IsVisible
				foreach (Site s in siteList)
				{

					GraphicsPath gp = new GraphicsPath();
					float x = (float)((-s.X - siteIconRadiusMM) / graphicsResMMPX);
					float y = -(float)((-s.Y + siteIconRadiusMM) / graphicsResMMPX);
					gp.AddEllipse(x, y, 100f, 100f);

					if (gp.IsVisible(pts[0]))
					{
						bool i;
						if (SelectOrInclude.ToUpper() == "SELECT")
						{
							i = s.IsSelected;
							s.IsSelected = !i;
							hitIndx = indx;
						}
						else
						{
							i = s.IsIncludedUserState;
							s.IsIncludedUserState = !i;
							hitIndx = indx;
						}
					}

					gp.Dispose();
					indx++;
				}
			}
			return hitIndx;
		}   // HitTest_Sites()
	}
}
