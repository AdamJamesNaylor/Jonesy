
namespace AJN.Jonesy.Model {
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Answer {
        public string Text { get; set; }
        public string Details { get; set; }

        public Collection<Source> Sources { get; set; }

        public bool HasSources { get { return Sources.Any(); } }

        public Answer() {
            Sources = new Collection<Source>();
        }
    }
}
