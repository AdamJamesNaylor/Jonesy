
namespace AJN.Jonesy.Common {

    using System;
    using System.Xml.Linq;
    using Model;

    public static class QuestionExtensions {

        public static XElement ToXElement(this Question operand) {
            var question = new XElement("question", new XAttribute("id", operand.Id),
                new XElement("text", operand.Text),
                new XElement("audit",
                    new XElement("created", new XAttribute("datetime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm")), new XAttribute("by", "0")),
                    new XElement("modified", new XAttribute("datetime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm")), new XAttribute("by", "0")),
                    new XElement("verified", new XAttribute("datetime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm")), new XAttribute("by", "0"),
                        new XElement("version", new XAttribute("edition", "Computer"),
                            new XAttribute("releaseNumber", "1.10")))));

            if (operand.Answer != null) {
                var answer = new XElement("answer");
                if (operand.Answer.Text != null)
                    answer.Add(new XElement("text", new XCData(operand.Answer.Text)));
                if (operand.Answer.Details!= null)
                    answer.Add(new XElement("details", new XCData(operand.Answer.Details)));

                if (answer.HasElements)
                    question.Add(answer);
            }

            if (operand.CanonicalQuestionId != 0)
                question.Add(new XAttribute("canonicalQuestion", operand.CanonicalQuestionId));

            return question;
        }
    }
}
