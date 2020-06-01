using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using static Telegenic.WebAPI.Plumbing.nHibernate.Maps.BaseMaps;

namespace Telegenic.WebAPI.Plumbing.nHibernate
{
    public class PersistenceFacility : AbstractFacility
    {
        protected override void Init()
        {
            var config = BuildDatabaseConfiguration();

            Kernel.Register(
                Component.For<ISessionFactory>()
                .UsingFactoryMethod(x => config.BuildSessionFactory()),
                Component.For<ISession>()
                .UsingFactoryMethod(m => m.Resolve<ISessionFactory>().OpenSession())
                .LifestyleTransient()
                );
        }

        protected virtual IPersistenceConfigurer SetupDatabase()
        {
            return MsSqlConfiguration.MsSql2012
                .UseOuterJoin()
                .ConnectionString(c => c.FromConnectionStringWithKey("default"))
                .ShowSql();
        }

        private Configuration BuildDatabaseConfiguration()
        {
            return Fluently.Configure()
                .Database(SetupDatabase)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MovieMap>())
                .BuildConfiguration();
        }
    }
}