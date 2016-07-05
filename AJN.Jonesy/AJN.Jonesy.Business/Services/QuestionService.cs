namespace AJN.Jonesy.Business.Services {
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Model;

    public class QuestionService
        : IQuestionService {

        public QuestionService(string appDataPath) {
            _appDataPath = appDataPath;
        }

        public Question Get(int id) {

            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            string strId = id.ToString();
            var question = questions.Descendants("question")
                .FirstOrDefault(q => q.Attribute("id").Value == strId);

            if (question == null)
                return null; //should log this or throw

            var result = ParseQuestion(question);
            result.SimilarQuestions = GetSimilarQuestions(result);
            return result;
        }

        public Collection<Question> GetSimilarQuestions(Question question) {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var similarQuestion = questions.Descendants("question").Where(q => q.Attribute("id").Value != question.Id.ToString());

            return new Collection<Question>(similarQuestion.Select(ParseQuestion).ToList());
        }

        private Question ParseQuestion(XElement question) {

            var id = question.Attribute("id").Value;
            var text = question.Element("text").Value;
            var answer = ParseAnswer(question.Element("answer"));
            return new Question {
                Id = int.Parse(id),
                Text = text,
                Answer = answer,
            };
        }

        private Answer ParseAnswer(XElement answer) {
            var text = answer.Element("text").Value;
            var result = new Answer {
                Text = text
            };

            var details = answer.Element("details");
            if (details != null)
                result.Details = details.Value;

            return result;
        }

        private readonly string _appDataPath;
    }
}