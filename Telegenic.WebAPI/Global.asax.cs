using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http;
using Telegenic.WebAPI.Plumbing.CastleWindsor;

namespace Telegenic.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BootstrapContainer(GlobalConfiguration.Configuration);
        }

        protected void Application_End()
        {
            container.Dispose();
        }

        private static void BootstrapContainer(HttpConfiguration configuration)
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            var dependencyResolver = new WindsorDependencyResolver(container);
            configuration.DependencyResolver = dependencyResolver;

        }
    }
}
