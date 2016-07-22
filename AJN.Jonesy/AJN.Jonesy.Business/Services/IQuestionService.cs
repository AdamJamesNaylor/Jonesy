namespace AJN.Jonesy.Business.Services {
    using System.Collections.ObjectModel;
    using Model;

    public interface IQuestionService {
        Question Get(int id);
        Collection<Question> GetSimilarQuestions(Question question);
        Collection<Question> GetPopularQuestions();
        Collection<Question> List(bool excludeNonCanonical = true);
        Question Add(Question model);
    }
}