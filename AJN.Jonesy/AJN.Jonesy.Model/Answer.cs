
namespace AJN.Jonesy.Model {
    using System.Collections.Generic;
    using System.Linq;

    public class Answer {
        public string Text { get; set; }
        public string Details { get; set; }

        public IEnumerable<Source> Sources { get; set; }

        public bool HasSources { get { return Sources.Any(); } }
    }
}
