using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gov.cityofboston.hubhacks2.api.json {
    
    /// <summary>
    /// JSON represetnation of a Survey
    /// 20150316 Andrew Arace
    /// </summary>
    public class jSurvey {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageL { get; set; }
        public string ImageS { get; set; }

        public int SurveyType { get; set; }
        public bool EndingSoon { get; set; }

        public List<jQuestion> Questions { get; set; }

        public jSurvey(SurveyEntities ctx, Survey s, bool loadQuestions = true) {
            Id = s.Id.ToString();
            Name = s.Name;
            Description = s.Description;
            ImageL = s.LargeImageUrl;
            ImageS = s.SmallImageUrl;
            SurveyType = s.SurveyTypeId;

            if (s.ToDate.HasValue && s.ToDate.Value < (System.DateTime.UtcNow.AddDays(3))) {
                EndingSoon = true;
            }
            else {
                EndingSoon = false;
            }

            if (loadQuestions) {
                Questions = new List<jQuestion>();
                var qs = from q in ctx.Questions
                                where q.SurveyId == s.Id
                                && q.Enabled == true
                                && (!q.FromDate.HasValue || q.FromDate.Value <= System.DateTime.UtcNow) 
                                && (!q.ToDate.HasValue || q.ToDate.Value >= System.DateTime.UtcNow)
                                orderby q.QuestionOrder
                                select q;
                foreach (var q in qs) {
                    Questions.Add(new jQuestion(q));
                }
            }
        }

    }

}
