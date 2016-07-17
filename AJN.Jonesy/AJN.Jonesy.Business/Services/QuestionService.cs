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
            result.EquivalentQuestions = GetEquivalentQuestions(result);
            return result;
        }

        public Collection<Question> GetPopularQuestions() {
            var allQuestions = List();
            return allQuestions.Take(5).ToCollection();
        }

        public Collection<Question> List(bool excludeNonCanonical = true) {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var popularQuestions = questions.Descendants("question");

            if (excludeNonCanonical)
                popularQuestions = popularQuestions.Where(q => q.IsCanonical());

            return popularQuestions.Select(_questionXmlParser.Parse).ToCollection();
        }

        public Collection<Question> GetSimilarQuestions(Question question) {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var similarQuestions =
                questions.Descendants("question").Where(q => q.Attribute("id").Value != question.Id.ToString() && q.IsCanonical());

            return similarQuestions.TakeRandomExclusive(5).Select(_questionXmlParser.Parse).ToCollection();
        }

        public Collection<Question> GetEquivalentQuestions(Question question) {
            string file = Path.Combine(_appDataPath, "questions.xml");
            var questions = XElement.Load(file);

            var xmlQuestion = questions.Descendants("question").First(q => q.Attribute("id").Value == question.Id.ToString());

            var equivalentQuestionAttribute = xmlQuestion.Attribute("equivalentQuestions");
            if (equivalentQuestionAttribute == null)
                return new Collection<Question>();

            var equivalentQuestionIds = equivalentQuestionAttribute.Value.Split(',');
            var result = questions.Descendants("question").Where(q => equivalentQuestionIds.Contains(q.Attribute("id").Value));

            return result.Select(_questionXmlParser.Parse).ToCollection();
        }

        private readonly string _appDataPath;
        private readonly IQuestionXmlParser _questionXmlParser;
    }

}