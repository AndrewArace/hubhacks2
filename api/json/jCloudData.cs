using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {

    /// <summary>
    /// JSON representation of cloud data pulled by Unity
    /// 20150326 Andrew Arace
    /// </summary>
    public class jCloudData {

        public const String TIME_FORMAT = "yyyyMMddHHmmss";

        public int i { get; set; }
        public double h { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string t { get; set; }

        public jCloudData(CloudData cd) {
            i = cd.Id;
            h = (double)cd.HappyFactor;
            x = (double)cd.XCoord;
            y = (double)cd.YCoord;
            d = cd.Description;
            c = cd.Category;
            t = cd.ItemTime.ToString(TIME_FORMAT);
        }

    }
}
