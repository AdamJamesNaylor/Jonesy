
namespace AJN.Jonesy.Business {
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Model;

    public static class QuestionUrlParser {
        public static string Generate(Question question) {

            var title = GenerateTitle(question);

            return string.Format("/questions/{0}/{1}", question.Id, title);
        }

        public static string Latinise(string text) {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString) {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveUnsupportedCharacters(string text) {
            //taken from http://stackoverflow.com/questions/1856785/characters-allowed-in-a-url
            var unreserved = new[] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

            var result = new StringBuilder();
            foreach (var c in text) {
                if (c == ' ') {
                    result.Append('-');
                    continue;
                }

                if (unreserved.Contains(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string GenerateTitle(Question question) {
            if (question == null)
                return null;

            var result = question.Text.ToLower();
            result = Latinise(result);

            return RemoveUnsupportedCharacters(result);
        }
    }
}