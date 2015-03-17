using System;
using System.Linq;

namespace gov.cityofboston.hubhacks2.api {

    public partial class Activation {

        public static Activation CreateActivation() {

            var a = new Activation();
            a.Created = System.DateTime.UtcNow;
            a.LastSeen = System.DateTime.UtcNow;

            return a;
        }


        public static bool Validate(SurveyEntities ctx, string aId) {
            Guid g;
            if(Guid.TryParse(aId, out g)) {
                var q = (from a in ctx.Activations
                        where a.Id == g
                        select a);
                if (q != null)
                    return true;
            }
            return false;
        }
    }
}