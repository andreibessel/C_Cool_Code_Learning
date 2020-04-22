using System;
using System.Collections.Generic;
using System.Linq;

namespace NUlableIntroduction
{
    public enum QuestionType
    {
        YesNo,
        Number,
        Text
    }
    public class SurveyQuestion
    {
        public string QuestionText { get; }
        public QuestionType QuestionType { get; }

        public SurveyQuestion(QuestionType questionType, string text) =>
            (QuestionType, QuestionText) = (questionType, text);
    }
    public class SurveyRun
    {


        private List<SurveyQuestion> surveyQuestionsList =
            new List<SurveyQuestion>();

        public void AddQuestion(QuestionType questionType, string question) =>
            AddQuestion(new SurveyQuestion(questionType, question));

        public void AddQuestion(SurveyQuestion surveyQuestion) =>
            surveyQuestionsList.Add(surveyQuestion);

        private List<SurveyResponse>? respondents;
        public void PerformSurvey(int numberOfRespondents)
        {
            int repondentsConsenting = 0;
            respondents = new List<SurveyResponse>();
            while (repondentsConsenting < numberOfRespondents)
            {
                var respondent = SurveyResponse.GetRandomId();
                if (respondent.AnswerSurvey(surveyQuestionsList))
                    repondentsConsenting++;
                respondents.Add(respondent);
            }
        }
        public IEnumerable<SurveyResponse> AllParticipants => (respondents ?? Enumerable.Empty<SurveyResponse>());
        public ICollection<SurveyQuestion> Questions => surveyQuestionsList;
        public SurveyQuestion GetQuestion(int index) => surveyQuestionsList[index];
    }
}
