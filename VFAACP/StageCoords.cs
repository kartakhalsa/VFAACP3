using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	// Class to encapsulate stage coordinates
	public class StageCoords
	{
		public double X_mm { get; set; }
		public double Y_mm { get; set; }
		public double Z_mm { get; set; }
		public double R_deg { get; set; }
		public double P_deg { get; set; }

		public StageCoords()
		{
			X_mm = 0.0;
			Y_mm = 0.0;
			Z_mm = 0.0;
			R_deg = 0.0;
			P_deg = 0.0;
		}
	}
}
