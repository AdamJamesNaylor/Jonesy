namespace AJN.Jonesy.Model {
    using System;
    using System.Collections.ObjectModel;

    public class Question {
        public int Id { get; set; }

        public string Text { get; set; }

        public Answer Answer { get; set; }

        public Audit Audit { get; set; }

        public Collection<Question> SimilarQuestions { get; set; }

        public Question() {
            SimilarQuestions = new Collection<Question>();
        }
    }

    public class Audit {
        public AuditEvent Created { get; set; }
        public AuditEvent Modified { get; set; }
        public VerifiedEvent Verified { get; set; }
    }

    public class VerifiedEvent
        : AuditEvent {
        public GameVersion Version { get; set; }
    }

    public class GameVersion {
        public string ReleaseNumber { get; set; } //1.10 etc.
        public GameEdition Edition { get; set; }
    }

    public class GameEdition {
        public string Name { get; set; }

        public static GameEdition Computer = new GameEdition { Name = "Computer" };
        public static GameEdition Console = new GameEdition { Name = "Console" };
        public static GameEdition Pocket = new GameEdition { Name = "Pocket" };
    }

    public class AuditEvent {
        public DateTime On { get; set; }
        public int By { get; set; }
    }
}