using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {

    /// <summary>
    /// JSON representation of geofenced survey
    /// 20150326 Andrew Arace
    /// </summary>
    public class jLocation {

        public string Id { get; set; }
        public string Name { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Radius { get; set; }

        public bool EndingSoon { get; set; }

        public List<jSurvey> Surveys { get; set; }

        public jLocation(SurveyEntities ctx, Location l) {
            Id = l.Id.ToString();
            Lat = (double)l.Lat;
            Lng = (double)l.Lng;
            Radius = (double)l.Radius;
            Name = l.Name;

            if (l.ToDate.HasValue && l.ToDate.Value < (System.DateTime.UtcNow.AddHours(12))) {
                EndingSoon = true;
            }
            else {
                EndingSoon = false;
            }

            Surveys = new List<jSurvey>();
            var qs = from s in l.Surveys
                     select s;
            foreach (var s in qs) {
                Surveys.Add(new jSurvey(ctx, s, true));
            }

        }
    }

}