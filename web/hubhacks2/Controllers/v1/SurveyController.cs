using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {

    public class SurveyController : ApiController {

        [HttpGet]
        [Route("api/v1/surveys")]
        public result GetSurveys(string aId, double lat, double lng) {
            using (var ctx = new SurveyEntities()) {
                if (Activation.Validate(ctx, aId)) { //validate it is a valid person

                    //get all global surveys
                    var q = from s in ctx.Surveys
                            where s.SurveyTypeId == 1
                            select s;

                    if (lat >= 42.26 && lat <= 42.4 && lng >= -71.22 && lng <= -70.87) {
                        //roughly within boston
                        //add in location-based surveys

                    }

                    //remove surveys that have been taken or skipped



                    return result.NO_RESULTS;
                }
            }
            return result.FAILED;
        }

    }

}