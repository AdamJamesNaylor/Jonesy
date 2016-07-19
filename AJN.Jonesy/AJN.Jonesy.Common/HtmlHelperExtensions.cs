
namespace AJN.Jonesy.Common {
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions {

        public static IHtmlString Render(this HtmlHelper html, string text) {
            text = ReplaceButtons(text);
            text = ReplaceCode(text);
            return html.Raw(text);
        }

        private static string ReplaceCode(string text) {
            return text.Replace("<code>", "<span class=\"code\">")
                .Replace("</code>", "</span>");
        }

        private static string ReplaceButtons(string text) {
            return text.Replace("<button>", "<div class=\"button\"><span>")
                .Replace("</button>", "</span></div>");
        }
    }
}