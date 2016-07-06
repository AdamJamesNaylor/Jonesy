namespace AJN.Jonesy.Website.Models {
    using System.Collections.ObjectModel;
    using Model;

    public class HomeViewModel {
        public Collection<Question> PopularQuestions { get; set; }
    }
}