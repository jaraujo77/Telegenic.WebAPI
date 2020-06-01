using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Telegenic.WebAPI.Plumbing.nHibernate;

namespace Telegenic.WebAPI.Plumbing.CastleWindsor.Installers
{
    public class PersistenceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<PersistenceFacility>();
        }
    }
}