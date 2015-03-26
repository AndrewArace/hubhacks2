using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api {

    public enum QuestionTypes : int {
        UNKNOWN = 0,
        YesNo = 1,
        Scale1to10 = 2
    }

    public partial class Question {

        public static Question GetById(SurveyEntities ctx, String questionId) {
            Guid g;
            if (Guid.TryParse(questionId, out g)) {

                return (from ques in ctx.Questions
                        where ques.Id == g
                        select ques).FirstOrDefault();

            }
            return null;
        }

    }
}
