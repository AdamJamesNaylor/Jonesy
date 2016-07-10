
namespace AJN.Jonesy.Website {
    using System;
    using System.Runtime.Remoting.Channels;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Business.Services;
    using Controllers;

    public class AutofacConfig {
        public static void RegisterDependencies() {
            var builder = new ContainerBuilder();

            builder.Register<IQuestionXmlParser>(c => new QuestionXmlParser()).InstancePerRequest();

            var appDataPath = HostingEnvironment.MapPath("~/app_data");
            builder.Register<IQuestionService>(c => new QuestionService(appDataPath, c.Resolve<IQuestionXmlParser>())).InstancePerRequest();

            builder.RegisterControllers(typeof(QuestionController).Assembly);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}