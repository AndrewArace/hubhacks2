using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gov.cityofboston.hubhacks2.web.Controllers.v1 {


    /// <summary>
    /// Handles posting of survey question answers
    /// 20150323 Andrew Arace
    /// </summary>
    public class AnswerController : ApiController {

        [HttpPost]
        [Route("api/v1/answers")]
        public result GetLocationSurveys(string aId, [FromBody]List<jQuestionAnswer> answers) {
            using (var ctx = new SurveyEntities()) {
                if (Activation.Validate(ctx, aId)) { //validate it is a valid person
                    Guid g = Guid.Parse(aId);
                    int count = 0;
                    if (answers != null) 
                        count = answers.Count;

                    string debug = "";
                    Survey s = null;

                    foreach(jQuestionAnswer qa in answers) {
                        Question q = Question.GetById(ctx, qa.questionId);
                        if (s == null) {
                            s = Survey.GetById(ctx, q.SurveyId);
                        }

                        Answer a = new Answer();
                        a.ActivationId = g;
                        a.QuestionId = q.Id;
                        
                        //anytime surveys capture lat/long
                        if (s.SurveyTypeId == (int)SurveyTypes.Anytime
                            && qa.answerLat != 0
                            && qa.answerLng != 0) {
                            a.AnswerLat = (decimal)qa.answerLat;
                            a.AnswerLng = (decimal)qa.answerLng;
                        }
                        
                        if (q != null) {
                            if (q.QuestionTypeId == (int)QuestionTypes.YesNo) {
                                debug += "YESNOQUESTION=";
                                debug += qa.answerYesNo.ToString() + "/";
                                a.AnswerYesNo = qa.answerYesNo;
                            }
                            else if (q.QuestionTypeId == (int)QuestionTypes.Scale1to10) {
                                debug += "SCALE1_10QUESTION=";
                                debug += qa.answerNumeric.ToString() + "/";
                                a.AnswerNumeric = (decimal)qa.answerNumeric; ;
                            }
                            else {
                                debug += "DEADQUESTION";
                            }
                            ctx.Answers.Add(a);

                            //global & location-based surveys can only be taken once
                            if (s.SurveyTypeId == (int)SurveyTypes.Global
                                || s.SurveyTypeId == (int)SurveyTypes.Location) {

                                    var already = (from t in ctx.Takens
                                                   where t.ActivationId == g && t.SurveyId == s.Id
                                                   select t).FirstOrDefault();

                                    if (already == null) {
                                        Taken t = new Taken();
                                        t.TakenDate = System.DateTime.UtcNow;
                                        t.Skipped = false;
                                        t.ActivationId = g;
                                        t.SurveyId = s.Id;
                                        ctx.Takens.Add(t);
                                    }
                            }


                            ctx.SaveChanges();
                        }
                        else {
                            debug += "NOQUESITON/";
                        }
                    }

                    return new result(true, string.Format("thanks for {0} answers {1}", count, debug));
                }
                return result.FAILED;
            }
        }
    }

}
