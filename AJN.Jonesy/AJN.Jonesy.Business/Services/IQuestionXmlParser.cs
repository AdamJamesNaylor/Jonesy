
namespace AJN.Jonesy.Business.Services {
    using System.Xml.Linq;
    using Model;

    public interface IQuestionXmlParser {
        Question Parse(XElement question);
    }
}