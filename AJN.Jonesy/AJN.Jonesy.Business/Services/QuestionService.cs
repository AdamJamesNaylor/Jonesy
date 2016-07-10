namespace AJN.Jonesy.Business.Services {
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Common;
    using Model;

    public class QuestionService
        : IQuestionService {

        public QuestionService(string appDataPath, IQuestionXmlParser questionXmlParser) {
            _appDataPath = appDataPath;
            _questionXmlParser = questionXmlParser;
        }

        public Question Get(int id) {

            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            string strId = id.ToString();
            var question = questions.Descendants("question")
                .FirstOrDefault(q => q.Attribute("id").Value == strId);

            if (question == null)
                return null; //todo should log this or throw

            var result = _questionXmlParser.Parse(question);
            result.SimilarQuestions = GetSimilarQuestions(result);
            return result;
        }

        public Collection<Question> GetPopularQuestions() {
            var allQuestions = List();
            return new Collection<Question>(allQuestions.Take(5).ToList());
        }

        public Collection<Question> List() {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var popularQuestions = questions.Descendants("question");

            return new Collection<Question>(popularQuestions.Select(_questionXmlParser.Parse).ToList());
        }

        public Collection<Question> GetSimilarQuestions(Question question) {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var similarQuestions =
                questions.Descendants("question").Where(q => q.Attribute("id").Value != question.Id.ToString());

            return new Collection<Question>(similarQuestions.TakeRandom(5).Select(_questionXmlParser.Parse).ToList());
        }

        private readonly string _appDataPath;
        private readonly IQuestionXmlParser _questionXmlParser;
    }

}