
namespace AJN.Common
{
    using System.Web;
    using UAParser;

    public class UserAgentParser
    {
        private string _ua;

        public UserAgentParser(HttpRequestBase request) {
            _ua = request.UserAgent;
        }

        public bool IsMobile {
            get {
                var parser = Parser.GetDefault();
                var info = parser.Parse(_ua);
                return info.UA.Family == "Chrome Mobile" || info.UA.Family == "IE Mobile" || info.UA.Family == "Mobile Safari";
            }
        }
    }
}
