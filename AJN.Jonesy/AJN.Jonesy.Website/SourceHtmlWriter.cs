
namespace AJN.Jonesy.Website
{
    using System;
    using System.Linq;
    using System.Web;
    using Model;

    public static class SourceUrlParser {
        public static SourceUrlType ParseType(string url) {
            url = url.ToLower();
            if (url.StartsWith("https://www.youtube.com/watch?v="))
                return SourceUrlType.YoutubeVideo;

            if (url.EndsWith(".jpg") || url.EndsWith(".gif") || url.EndsWith(".png") || url.EndsWith(".bmp"))
                return SourceUrlType.Image;

            return SourceUrlType.Unsupported;
        }
    }

    public enum SourceUrlType {
        YoutubeVideo,
        Image,
        Unsupported
    }

    public static class SourceHtmlWriter
    {
        public static HtmlString Write(Source model) {
            var type = SourceUrlParser.ParseType(model.Url.AbsoluteUri);

            switch (type) {
                case SourceUrlType.YoutubeVideo:
                    return WriteYoutubeVideoHtml(model);

                    case SourceUrlType.Image:
                    return WriteImageHtml(model);
            }

            return new HtmlString("<!-- unsupported source type " + model.Url.AbsoluteUri + " -->");
        }

        private static HtmlString WriteImageHtml(Source model) {
            return new HtmlString(string.Format("<img src=\"{0}\" />", model.Url.AbsoluteUri));
        }

        private static HtmlString WriteYoutubeVideoHtml(Source model) {
            var videoId = GetYoutubeVideoId(model.Url);
            return new HtmlString(string.Format("<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe>", videoId));
        }

        private static string GetYoutubeVideoId(Uri url) {
            var queryDictionary = HttpUtility.ParseQueryString(url.Query);
            return queryDictionary["v"];
        }
    }
}