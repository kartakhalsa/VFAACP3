using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Diagnostics;

namespace VFAACP
{
	public struct Point3d : ICloneable
	{
		double _x;
		double _y;
		double _z;

		public Point3d(double x, double y, double z)
		{
			_x = x;
			_y = y;
			_z = z;
		}

		public double X
		{
			get { return _x; }
			set { _x = value; }
		}

		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public double Z
		{
			get { return _z; }
			set { _z = value; }
		}

		public override string ToString()
		{
			return "X, " + _x.ToString() + ", Y, " + _y.ToString() + ", Z, " + _z.ToString();
		}

		public object Clone()
		{
			return new Point3d(_x, _y, _z);
		}
	}

	class GraphicsFuncs
	{
		private static float curSF = 1.0f;
		private static int clientSizeW;
		private static int clientSizeH;
		private static Matrix mat;
		private static Bitmap buffer;

		public static Matrix GraphicsMatrix
		{
			get { return mat; }
			set { mat = value; }
		}

		public static int ClientSizeWidth
		{
			get { return clientSizeW; }
			set { clientSizeW = value; }
		}

		public static int ClientSizeHeight
		{
			get { return clientSizeH; }
			set { clientSizeH = value; }
		}

		public static void InitMatrix()
		{
			mat = new Matrix(curSF, 0f, 0f, curSF, 0f, 0f);
		}

		public static float GetScaleFactor(Size clientSize, Size plotExtentsPx)
		{
			// Returns the proper scale factor for the plot

			// Calculate X & Y scale factor in px
			float widthSF = (float)clientSize.Width / (float)plotExtentsPx.Width;
			float heightSF = (float)clientSize.Height / (float)plotExtentsPx.Height;

			// Scale depending on the smaller dimension as the container is resized
			float SF;
			if (widthSF <= heightSF)
			{
				SF = (float)widthSF;
			}
			else
			{
				SF = (float)heightSF;
			}

			return SF;
		}

		public static ImageAttributes MakeTransparency(float NormalizedTransparencyLevel)
		{
			// Create a 5x5 matrix with the transparency value in position (4,4)
			Single[][] values = new Single[][] { new Single[] { 1, 0, 0, 0, 0 }, new Single[] { 0, 1, 0, 0, 0 }, new Single[] { 0, 0, 1, 0, 0 }, new Single[] { 0, 0, 0, (Single)NormalizedTransparencyLevel, 0 }, new Single[] { 0, 0, 0, 0, 1 } };
			// Use the matrix to initialize a new colorMatrix object
			ColorMatrix colMatrix = new ColorMatrix(values);
			// Create an ImageAttributes object, and assign its colorMatrix
			ImageAttributes imageAttr = new ImageAttributes();
			imageAttr.SetColorMatrix(colMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			return imageAttr;
		}

		public static Graphics GetGraphicsTemplate()
		{
			if (buffer == null)
			{
				buffer = new Bitmap(100, 100);
			}

			Graphics g = Graphics.FromImage(buffer);

			return g;
		}

		public static void FitBitmapInRect(Bitmap bitmap, ref Rectangle rect)
		{
			if ((bitmap.Width <= rect.Width) && (bitmap.Height <= rect.Height))
			{   // No scaling needed
				rect.Width = bitmap.Width;
				rect.Height = bitmap.Height;
				return;
			}
			double scaleWidth = ((double)rect.Width / (double)bitmap.Width);
			double scaleHeight = ((double)rect.Height / (double)bitmap.Height);
			double scaleMin = Math.Min(scaleWidth, scaleHeight);
			rect.Width = (int)((scaleMin * bitmap.Width) + 0.5);
			rect.Height = (int)((scaleMin * bitmap.Height) + 0.5);
		} // FitBitmapInRect()

	}

}
