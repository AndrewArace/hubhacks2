using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {

    /// <summary>
    /// JSON representation of question answers, sent in by phone
    /// 20150316 Andrew Arace
    /// </summary>
    public class jQuestionAnswer {

        public string questionId { get; set; }
        public double answerNumeric { get; set; }
        public bool answerYesNo { get; set; }

        public double answerLat { get; set; }
        public double answerLng { get; set; }


        public jQuestionAnswer() {

        }

    }
}
