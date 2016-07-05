namespace AJN.Jonesy.Model {
    using System.Collections.ObjectModel;

    public class Question {
        public int Id { get; set; }

        public string Text { get; set; }

        public Answer Answer { get; set; }

        public string Details { get; set; }

        public Collection<Question> SimilarQuestions { get; set; }

        public Question() {
            SimilarQuestions = new Collection<Question>();
        }
    }
}