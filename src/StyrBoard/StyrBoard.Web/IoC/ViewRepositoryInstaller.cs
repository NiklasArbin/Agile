using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StyrBoard.View.Repository;

namespace StyrBoard.Web.IoC
{
    public class ViewRepositoryInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBoardRepository>().ImplementedBy<BoardRepository>()
                );
        }
    }
}