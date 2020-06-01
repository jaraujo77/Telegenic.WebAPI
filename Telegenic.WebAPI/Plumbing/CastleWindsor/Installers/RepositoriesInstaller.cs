using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Telegenic.Repository;

namespace Telegenic.WebAPI.Plumbing.CastleWindsor.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Telegenic.Repository")
                .Where(Component.IsInSameNamespaceAs<SeriesRepository>())
                .WithService.DefaultInterfaces()
                .LifestyleTransient()
                );
        }
    }
}