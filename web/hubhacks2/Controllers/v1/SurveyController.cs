using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {

    /// <summary>
    /// Controls the retrieval of surveys
    /// 20150316 Andrew Arace
    /// </summary>
    public class SurveyController : ApiController {

        /// <summary>
        /// Pull global surveys given activation
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/surveys/global")]
        public result GetGlobalSurveys(string aId) {
            using (var ctx = new SurveyEntities()) {
                if (Activation.Validate(ctx, aId)) { //validate it is a valid person
                    Guid g = Guid.Parse(aId);

                    IQueryable<Survey> ss = Survey.GetSurveysForActivation(ctx, g, 1);
                    if (ss != null && ss.Count() > 0) {
                        result ret = new result(true);
                        List<object> retD = new List<object>();
                        foreach (Survey s in ss) {
                            retD.Add(new jSurvey(ctx, s));
                        }
                        if (retD.Count > 0) {
                            ret.data = retD;
                            return ret;
                        }
                    }
                    return result.NO_RESULTS;
                }
            }
            return result.FAILED;
        }


        /// <summary>
        /// Pull anytime surveys given activation
        /// </summary>
        /// <param name="aId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/surveys/anytime")]
        public result GetAnytimeSurveys(string aId) {
            using (var ctx = new SurveyEntities()) {
                if (Activation.Validate(ctx, aId)) { //validate it is a valid person
                    Guid g = Guid.Parse(aId);

                    IQueryable<Survey> ss = Survey.GetSurveysForActivation(ctx, g, 2);
                    if (ss != null && ss.Count() > 0) {
                        result ret = new result(true);
                        List<object> retD = new List<object>();
                        foreach (Survey s in ss) {
                            retD.Add(new jSurvey(ctx, s));
                        }
                        if (retD.Count > 0) {
                            ret.data = retD;
                            return ret;
                        }
                    }
                    return result.NO_RESULTS;
                }
            }
            return result.FAILED;
        }



    }

}