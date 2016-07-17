namespace AJN.Jonesy.Model {
    using System;
    using System.Collections.ObjectModel;
    using Common;

    public class Question {
        public int Id { get; set; }

        public string Text { get; set; }

        public Answer Answer { get; set; }

        public Audit Audit { get; set; }

        public Collection<Question> SimilarQuestions { get; set; }

        public Collection<Question> EquivalentQuestions { get; set; }

        public int CanonicalQuestionId { get; set; }

        public bool IsCanonical {
            get { return CanonicalQuestionId == 0; }
        }

        public Question() {
            SimilarQuestions = new Collection<Question>();
            EquivalentQuestions = new Collection<Question>();
        }
    }

    public class Audit {
        public AuditEvent Created { get; set; }
        public AuditEvent Modified { get; set; }
        public VerifiedEvent Verified { get; set; }

        public bool HasBeenModified { get { return Modified != null; } }
        public bool IsVerified { get { return Verified != null; } }
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

        public static GameEdition Computer = new GameEdition(computerEditionName);
        public static GameEdition Console = new GameEdition(consoleEditionName);
        public static GameEdition Pocket = new GameEdition(pocketEditionName);

        public GameEdition(string edition) {
            if (string.IsNullOrEmpty(edition))
                throw new ArgumentNullException(nameof(edition), "Edition cannot be null or empty.");

            switch (edition.ToTitleCase()) {
                case computerEditionName:
                    Name = computerEditionName;
                    break;
                case consoleEditionName:
                    Name = consoleEditionName;
                    break;
                case pocketEditionName:
                    Name = pocketEditionName;
                    break;
                default:
                    throw new NotSupportedException("Unsupported game edition " + edition);
            }
        }

        private const string computerEditionName = "Computer";
        private const string consoleEditionName = "Console";
        private const string pocketEditionName = "Pocket";
    }

    public class AuditEvent {
        public DateTime On { get; set; }
        public int By { get; set; }
    }
}