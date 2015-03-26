using System;
using System.Linq;

namespace gov.cityofboston.hubhacks2.api {

    /// <summary>
    /// Extensions to Activation
    /// 20150316 Andrew Arace
    /// </summary>
    public partial class Activation {

        /// <summary>
        /// Create a new activation (activate a new mobile client)
        /// 20150316 Andrew Arace
        /// </summary>
        /// <returns></returns>
        public static Activation CreateActivation() {

            var a = new Activation();
            a.Created = System.DateTime.UtcNow;
            a.LastSeen = System.DateTime.UtcNow;

            return a;
        }


        /// <summary>
        /// Validate that an given activation identifier is real
        /// 20150316 Andrew Arace
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="aId"></param>
        /// <returns></returns>
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