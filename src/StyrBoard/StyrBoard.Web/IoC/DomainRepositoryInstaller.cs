using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;

namespace StyrBoard.Web.IoC
{
    public class DomainRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IRepository<UserStory>)).ImplementedBy<UserStoryRepository>()
                );
        }
    }
}