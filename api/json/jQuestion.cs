using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {

    /// <summary>
    /// JSON representation of Question
    /// 20150316 Andrew Arace
    /// </summary>
    public class jQuestion {

        public string Id { get; set; }
        public string QuestionText { get; set; }
        public int Order { get; set; }
        public int QuestionTypeId { get; set; }

        public string LabelLow { get; set; }
        public string LabelHigh { get; set; }
        public bool LabelHighGreen { get; set; }

        public jQuestion(Question q) {
            Id = q.Id.ToString();
            QuestionText = q.QuestionText;
            Order = q.QuestionOrder.HasValue ? q.QuestionOrder.Value : 0;
            LabelLow = q.LabelLow;
            LabelHigh = q.LabelHigh;
            LabelHighGreen = q.LabelHighGreen;
            QuestionTypeId = q.QuestionTypeId;
        }
    }


}
