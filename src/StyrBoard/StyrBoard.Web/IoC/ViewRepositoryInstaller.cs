using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.IoC
{
    public class ViewRepositoryInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBoardRepository>().ImplementedBy<View.Repository.BoardRepository>());
            container.Register(Component.For<IRepository<Sprint>>().ImplementedBy<SprintRepository>());
            container.Register(Component.For<IRepository<Board>>().ImplementedBy<Domain.Repository.BoardRepository>());
            container.Register(Component.For<IListRepository>().ImplementedBy<ListRepository>());
            container.Register(Component.For<IRepository<UserStory>>().ImplementedBy<UserStoryRepository>());
            container.Register(Component.For<ICardRepository>().ImplementedBy<CardRepository>());
        }
    }
}