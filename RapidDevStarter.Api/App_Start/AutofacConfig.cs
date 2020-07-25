using Autofac;
using Autofac.Integration.WebApi;
using RapidDevStarter.Core.Autofac;
using System;
using System.Reflection;
using System.Web.Http;

namespace RapidDevStarter.Api.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register services with Injectable attribute
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                          .Where(t => t.GetCustomAttribute<InjectableAttribute>() != null)
                          .AsImplementedInterfaces()
                          .InstancePerRequest();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}