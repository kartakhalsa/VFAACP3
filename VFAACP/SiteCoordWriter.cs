using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VFAACP
{
    class SiteCoordWriter
    {
        // Create/write a site coords file.

        public static void WriteSiteCoordsFile(List<Site> siteList, string path)
        {
            try
            {
                FileInfo fi = new FileInfo(path);
                StreamWriter sw;
                FileStream fs = fi.Create();
                fs.Close();
                fs.Dispose();
                sw = fi.AppendText();
                foreach (Site s in siteList)
                {
                    double x = s.X;
                    double y = s.Y;
                    double z_offset = s.Z_Offset;
                    double r_offset = s.R_Offset;
                    double p_offset = s.P_Offset;
                    int included = s.IsIncluded ? 1 : 0;
                    string line = "";
                    line = line + x.ToString("F4") + ",";
                    line = line + y.ToString("F4") + ",";
                    line = line + z_offset.ToString("F4") + ",";
                    line = line + r_offset.ToString("F4") + ",";
                    line = line + p_offset.ToString("F4") + ",";
                    line = line + included.ToString();
                    sw.WriteLine(line);
                }
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error writing site coordinates file: " + ex.Message);
            }
        }   // WriteSiteCoordsFile()
 
    }   // Class SiteCoordWriter
}

