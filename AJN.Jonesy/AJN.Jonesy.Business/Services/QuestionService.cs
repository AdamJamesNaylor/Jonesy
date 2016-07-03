namespace AJN.Jonesy.Business.Services {
    using System.Linq;
    using System.Xml.Linq;
    using Model;

    public class QuestionService
        : IQuestionService {

        public Question Get(int id) {
            string strId = id.ToString();
            var questions = XElement.Load("questions.xml");

            var question = questions.Descendants("question")
                .FirstOrDefault(q => q.Attribute("id").Value == strId);

            if (question == null)
                return null; //should log this or throw

            return ParseQuestion(question);
        }

        private Question ParseQuestion(XElement question) {

            var id = question.Attribute("id").Value;
            var text = question.Attribute("text").Value;
            var answer = ParseAnswer(question.Element("Answer"));
            return new Question {
                Id = int.Parse(id),
                Text = text,
                Answer = answer
            };
        }

        private Answer ParseAnswer(XElement answer) {
            var text = answer.Attribute("text").Value;
            return new Answer {
                Text = text
            };
        }
    }
}