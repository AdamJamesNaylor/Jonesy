
namespace AJN.Jonesy.Common
{
    using System.Linq;

    public static class StringExtensions
    {
        public static string ToTitleCase(this string operand) {
            if (string.IsNullOrEmpty(operand))
                return null;

            var first = operand.ToLower().First().ToUpper();

            return first + operand.Substring(1);
        }
    }
}
