using gov.cityofboston.hubhacks2.api;
using gov.cityofboston.hubhacks2.api.json;
using gov.cityofboston.hubhacks2.api.util;
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
        [HttpGet]
        [Route("api/v1/answers/debug")]
        public result debugAnswers(string aId) {
            double lat = 42.3394;
            double lng = -71.0365;
            string err = "";
            double[] sp = CoordinateConversion.LatLongToMAStatePlane(Math.Abs(lng), Math.Abs(lat), ref err);
            double[] llShift = CoordinateConversion.StatePlaneUnityShift(sp);

            return result.NO_RESULTS;
        }


        [HttpPost]
        [Route("api/v1/answers")]
        public result PostQuestionAnswers(string aId, [FromBody]List<jQuestionAnswer> answers) {
            try {
                using (var ctx = new SurveyEntities()) {
                    if (Activation.Validate(ctx, aId)) { //validate it is a valid person
                        Guid g = Guid.Parse(aId);
                        int count = 0;
                        if (answers != null)
                            count = answers.Count;

                        Survey s = null;

                        foreach (jQuestionAnswer qa in answers) {
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
                                string cat, desc;
                                double isHappy;

                                desc = q.QuestionText.Length > 100 ? q.QuestionText.Substring(0, 50) : q.QuestionText;
                                cat = s.Category;

                                if (q.QuestionTypeId == (int)QuestionTypes.YesNo) {
                                    a.AnswerYesNo = qa.answerYesNo;
                                    isHappy = (qa.answerYesNo ^ q.YesAnswerBad ? 10.0 : 0.0);
                                }
                                else if (q.QuestionTypeId == (int)QuestionTypes.Scale1to10) {
                                    a.AnswerNumeric = (decimal)qa.answerNumeric;
                                    if (q.LabelHighGreen) {
                                        isHappy = qa.answerNumeric;
                                    }
                                    else {
                                        isHappy = 10.0-qa.answerNumeric;
                                    }
                                }
                                else {
                                    throw new ApplicationException("Unknown Question");
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

                                if (s.SurveyTypeId == (int)SurveyTypes.Anytime) {
                                    //capture coordinates for realtime cloud
                                    double lat = qa.answerLat;
                                    double lng = qa.answerLng;
                                    string err = "";
                                    double[] sp = CoordinateConversion.LatLongToMAStatePlane(Math.Abs(lng), Math.Abs(lat), ref err);
                                    double[] llShift = CoordinateConversion.StatePlaneUnityShift(sp);

                                    CloudData cd = new CloudData();
                                    cd.XCoord = (decimal)llShift[0];
                                    cd.YCoord = (decimal)llShift[1];
                                    cd.Category = cat;
                                    cd.Description = desc;
                                    cd.HappyFactor = (decimal)isHappy;
                                    cd.IsRealtime = true;
                                    cd.ItemTime = System.DateTime.Now;

                                    ctx.CloudDatas.Add(cd);

                                }


                                ctx.SaveChanges();
                            }
                            else {
                                throw new ApplicationException("No Question");
                            }
                        }

                        return new result(true, string.Format("thanks for {0} answers", count));
                    }
                    return result.FAILED;
                }
            }
            catch (Exception ex) {
                return new result(false, ex.ToString());
            }
        }
    }

}
