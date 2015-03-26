using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {


    /// <summary>
    /// Handles negotiation of locations
    /// 20150316 Andrew Arace
    /// </summary>
    public class LocationController : ApiController {


        /// <summary>
        /// Pull active location items with a valid activation
        /// 20150316 Andrew Arace
        /// </summary>
        /// <param name="aId">activation identifier for client</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/surveys/location")]
        public result GetLocationSurveys(string aId) {
            using (var ctx = new SurveyEntities()) {
                if (Activation.Validate(ctx, aId)) { //validate it is a valid person
                    Guid g = Guid.Parse(aId);

                    IQueryable<Location> ls = Location.GetLocationSurveysForActivation(ctx, g);
                    if (ls != null && ls.Count() > 0) {
                        result ret = new result(true);
                        List<object> retD = new List<object>();
                        foreach (Location l in ls) {
                            retD.Add(new jLocation(ctx, l));
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