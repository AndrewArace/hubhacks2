using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {

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
