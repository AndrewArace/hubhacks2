using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api {

    public partial class Survey {

        /// <summary>
        /// Get all active Surveys (enabled, and not date-constrained by current date)
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public List<Survey> GetActiveSurveys(SurveyEntities ctx) {
            return (from s in ctx.Surveys
                     where s.Enabled == true &&
                     (!s.FromDate.HasValue || s.FromDate.Value <= System.DateTime.UtcNow) &&
                     (!s.ToDate.HasValue ||  s.ToDate.Value >= System.DateTime.UtcNow)
                     select s).ToList();
        }

    }

}
