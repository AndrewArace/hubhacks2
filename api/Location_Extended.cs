using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api {

    public partial class Location {

        /// <summary>
        /// Pull surveys for the given type, that have not yet been taken by the given activationid id
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="activationId"></param>
        /// <param name="surveyTypeId"></param>
        /// <returns></returns>
        public static IQueryable<Location> GetLocationSurveysForActivation(SurveyEntities ctx,
                                                    Guid activationId) {

            //get all global surveys or anytime surveys
            var q = from l in ctx.Locations
                    where l.Enabled == true
                    && (!l.ToDate.HasValue || l.ToDate.Value >= System.DateTime.UtcNow)
                    && (!l.FromDate.HasValue || l.FromDate.Value <= System.DateTime.UtcNow)
                    select l;


            //remove surveys that have been taken or skipped
            var taken = from t in ctx.Takens
                        where t.ActivationId == activationId
                        select t.SurveyId;

            var finalq = from all in q
                         where !all.Surveys.All( x => taken.Contains(x.Id))
                         select all;

            return finalq;
        }

    }
}
