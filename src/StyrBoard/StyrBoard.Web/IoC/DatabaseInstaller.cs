using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.IoC
{
    public class DatabaseInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDocumentStore>().Instance(GetServer())
                );
        }

        private IDocumentStore GetServer()
        {
            var store = new EmbeddableDocumentStore
            {
                DataDirectory = "RavenDB"
            };

            return store.Initialize();
        }
    }
}