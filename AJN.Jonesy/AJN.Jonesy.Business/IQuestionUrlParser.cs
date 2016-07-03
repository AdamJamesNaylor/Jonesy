using AJN.Jonesy.Model;

namespace AJN.Jonesy.Business
{
    public interface IQuestionUrlParser
    {
        string Generate(Question question);
        string Latinise(string text);
        string RemoveUnsupportedCharacters(string text);
    }
}