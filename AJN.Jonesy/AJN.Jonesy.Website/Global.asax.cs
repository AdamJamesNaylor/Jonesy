using System.Web.Mvc;
using System.Web.Routing;

namespace AJN.Jonesy.Website {
    using System;

    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.RegisterDependencies();
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            if (!Request.Url.Host.StartsWith("www") || Request.Url.IsLoopback)
                return;

            var builder = new UriBuilder(Request.Url) {
                Host = Request.Url.Host.Replace("www.", "")
            };

            Response.RedirectPermanent(builder.ToString(), true);
        }
    }
}