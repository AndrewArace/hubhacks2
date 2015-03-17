using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {

    public class ActivationController : ApiController {

        [HttpGet]
        [Route("api/v1/activation/create")]
        public result GetNewActivation() {
            using(var ctx = new SurveyEntities()) {
                var a = Activation.CreateActivation();
                ctx.Activations.Add(a);
                ctx.SaveChanges();
                var r = new result() { data = a.Id.ToString(), error=null, success=true };
                return r;
            }
        }

    }
}
