namespace AJN.Jonesy.Business.Services {
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;
    using Model;

    public class QuestionXmlParser : IQuestionXmlParser {
        public Question Parse(XElement question) {

            var id = question.Attribute("id").Value;
            var text = question.Element("text").Value;
            var answer = ParseAnswer(question.Element("answer"));
            var audit = ParseAudit(question.Element("audit"));
            return new Question {
                Id = int.Parse(id),
                Text = text,
                Answer = answer,
                Audit = audit
            };
        }

        private Audit ParseAudit(XElement audit) {
            var result = new Audit {
                Created = ParseAuditEvent(audit.Element("created"))
            };

            var modified = audit.Element("modified");
            if (modified != null)
                result.Modified = ParseAuditEvent(modified);

            var verified = audit.Element("verified");
            if (verified == null)
                return result;

            result.Verified = new VerifiedEvent {
                On = DateTime.Parse(verified.Attribute("datetime").Value),
                By = Convert.ToInt32(verified.Attribute("by").Value),
                Version = ParseGameVersion(verified.Element("version"))
            };

            return result;
        }

        private GameVersion ParseGameVersion(XElement version) {
            return new GameVersion {
                ReleaseNumber = version.Attribute("releaseNumber").Value,
                Edition = new GameEdition(version.Attribute("edition").Value)
            };
        }

        private AuditEvent ParseAuditEvent(XElement question) {
            return new AuditEvent {
                On = DateTime.Parse(question.Attribute("datetime").Value),
                By = Convert.ToInt32(question.Attribute("by").Value)
            };
        }

        private Answer ParseAnswer(XElement answer) {
            var text = answer.Element("text").Value;
            var result = new Answer {
                Text = text
            };

            var details = answer.Element("details");
            if (details != null)
                result.Details = details.Value;

            var sources = answer.Element("sources");
            if (sources != null) {
                var sourcesCollection = new Collection<Source>();
                foreach (var source in sources.Elements("source")) {
                    sourcesCollection.Add(new Source {
                        Url = new Uri(source.Element("url").Value),
                        Comment = source.Element("comment").Value
                    });
                }
                result.Sources = sourcesCollection;
            }

            return result;
        }
    }
}