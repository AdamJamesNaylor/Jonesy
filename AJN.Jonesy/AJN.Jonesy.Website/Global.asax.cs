using System.Web.Mvc;
using System.Web.Routing;

namespace AJN.Jonesy.Website {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.RegisterDependencies();
        }
    }
}