using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VFAACP
{
    class SiteCoordReader
    {
        // Open and read a site coords file
        // Provide a method to return a list of sites.

        private string pathAndFile;
        private List<Site> siteList;
        private string fileName;

        public SiteCoordReader(string PathAndFile)
        {
            try
            {
                pathAndFile = PathAndFile;
                FileInfo fi = new FileInfo(pathAndFile);
                if (!fi.Exists)
                {
                    throw new Exception("SiteCoordReader: The file '" + pathAndFile + "' does not exist");
                }
                fileName = fi.Name;
            }
            catch
            {
                throw;
            }
        }

        public List<Site> SiteList
        {
            get { return siteList; }
        }

        public int NumSites
        {
            get { return siteList.Count(); }
        }

        public int NumIncludedSites
        {
            get
            {
                int cnt = 0;
                foreach (Site s in siteList)
                {
                    if (s.IsIncluded)
                    {
                        cnt++;
                    }
                }
                return cnt;
            }
        }

        public List<Site> GetSiteList()
        {
            siteList = new List<Site>();

            StreamReader sr = new StreamReader(pathAndFile);
       
            try
            {
                string line;
                int linenum = 0;
                int siteIndex = 0;

                while ((line = sr.ReadLine()) != null && line.Trim() != "")
                {
                    linenum++;

                    string[] parsedStr;
                    parsedStr = line.Split(new string[] { "," }, StringSplitOptions.None);

                    if (parsedStr.Length == 6)
                    {
                        // Validate entries
                        try
                        {
                            double d1 = double.Parse(parsedStr[0]);
                            double d2 = double.Parse(parsedStr[1]);
                            double d3 = double.Parse(parsedStr[2]);
                            double d4 = double.Parse(parsedStr[3]);
                            double d5 = double.Parse(parsedStr[4]);
                            int i1 = int.Parse(parsedStr[5]);
                        }
                        catch(Exception ex)
                        {
                            throw new Exception("Line '" + linenum.ToString() + "', " + ex.Message);
                        }


                        bool enabled;
                        switch (int.Parse(parsedStr[5]))
                        {
                            case 0:
                                enabled = false;
                                break;
                            case 1:
                                enabled = true;
                                break;
                            default:
                                enabled = false;
                                break;
                        }

                        double x = double.Parse(parsedStr[0]);
                        double y = double.Parse(parsedStr[1]);
                        double z_offset = double.Parse(parsedStr[2]);
                        double r_offset = double.Parse(parsedStr[3]);
                        double p_offset = double.Parse(parsedStr[4]);
                        Site s = new Site(++siteIndex, x, y, z_offset, r_offset, p_offset, enabled);

                        siteList.Add(s);
                    }
                    else
                    {
                        throw new Exception("Line '" + linenum.ToString() + "', expecting '6' entries, got '" + parsedStr.Length.ToString() + "'");
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("SiteCoordReader: Error reading file '" + fileName + "'; " + ex.Message);
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }

            return siteList;
        }   // GetSiteList()

    }   // Class SiteCoordReader
}
