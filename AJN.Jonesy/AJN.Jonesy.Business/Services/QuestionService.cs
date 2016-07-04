namespace AJN.Jonesy.Business.Services {
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
            string strId = id.ToString();

            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var question = questions.Descendants("question")
                .FirstOrDefault(q => q.Attribute("id").Value == strId);

            if (question == null)
                return null; //should log this or throw

            return ParseQuestion(question);
        }

        private Question ParseQuestion(XElement question) {

            var id = question.Attribute("id").Value;
            var text = question.Element("text").Value;
            var answer = ParseAnswer(question.Element("answer"));
            return new Question {
                Id = int.Parse(id),
                Text = text,
                Answer = answer
            };
        }

        private Answer ParseAnswer(XElement answer) {
            var text = answer.Element("text").Value;
            return new Answer {
                Text = text
            };
        }

        private readonly string _appDataPath;
    }
}