using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {


    public class CloudDataController : ApiController {

        /// <summary>
        /// Pull data for unity clouds
        /// 20150325 Andrew Arace
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/cloud")]
        public result GetClouds(string cat = null) {
            using (var ctx = new SurveyEntities()) {
                List<jCloudData> ret = new List<jCloudData>();

                var q = (from cd in ctx.CloudDatas
                         where cd.IsRealtime == false
                         orderby cd.ItemTime
                         select cd);

                if (!string.IsNullOrEmpty(cat)) {
                    q = from qq in q
                        where qq.Category == cat
                        orderby qq.ItemTime
                        select qq;
                }

                if (q.Count() > 0) {
                    foreach (CloudData c in q) {
                        ret.Add(new jCloudData(c));
                    }
                }

                if (ret.Count > 0) {
                    return new result(true) { data = ret };
                }
                else {
                    return result.NO_RESULTS;
                }
            }
        }


        /// <summary>
        /// Pull realtime data for unity clouds
        /// </summary>
        /// <param name="aId">activation identifier for client</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/realtimecloud")]
        public result GetRealtimeClouds(string lastPull = null) {
            if (string.IsNullOrEmpty(lastPull)) {
                return new result(false, "lastPull parse failed.");
            }

            DateTime dt;

            if (DateTime.TryParseExact(lastPull, jCloudData.TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
                using (var ctx = new SurveyEntities()) {
                    List<jCloudData> ret = new List<jCloudData>();

                    var q = (from cd in ctx.CloudDatas
                             where cd.IsRealtime == true
                             && cd.ItemTime >= dt
                             orderby cd.ItemTime
                             select cd);
                    if (q.Count() > 0) {
                        foreach (CloudData c in q) {
                            ret.Add(new jCloudData(c));
                        }
                    }

                    if (ret.Count > 0) {
                        return new result(true) { data = ret };
                    }
                    else {
                        return result.NO_RESULTS;
                    }

                }
            }
            else {
                return new result(false, "lastPull parse failed. format is " + jCloudData.TIME_FORMAT + ". Current time is " + DateTime.Now.ToString(jCloudData.TIME_FORMAT));
            }
        }


    }

}
