using gov.cityofboston.hubhacks2.api.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api {

    public enum SurveyTypes : int {
        UNKNOWN = 0,
        Global = 1,
        Anytime = 2,
        Location = 3
    }


    /// <summary>
    /// Extensions to the Survey object
    /// 20150316 Andrew Arace
    /// </summary>
    public partial class Survey {

        public static Survey GetById(SurveyEntities ctx, Guid surveyId) {
            return (from s in ctx.Surveys
                    where s.Id == surveyId
                    select s).FirstOrDefault();

        }


        /// <summary>
        /// Pull surveys for the given type, that have not yet been taken by the given activationid id
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="activationId"></param>
        /// <param name="surveyTypeId"></param>
        /// <returns></returns>
        public static IQueryable<Survey> GetSurveysForActivation(SurveyEntities ctx,
                                                    Guid activationId,
                                                    int surveyTypeId) {

            //get all global surveys or anytime surveys
            var q = from s in ctx.Surveys
                    where (s.SurveyTypeId == surveyTypeId)
                    && s.Enabled == true
                    && (!s.ToDate.HasValue || s.ToDate.Value >= System.DateTime.UtcNow)
                    && (!s.FromDate.HasValue || s.FromDate.Value <= System.DateTime.UtcNow)
                    select s;

            //remove surveys that have been taken or skipped
            var taken = from t in ctx.Takens
                        where t.ActivationId == activationId
                        select t.SurveyId;

            var finalq = from all in q
                         where !taken.Contains(all.Id)
                         select all;

            return finalq;
        }
    }

}
