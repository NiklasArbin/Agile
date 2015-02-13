using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StyrBoard.Domain.Model;

namespace StyrBoard.Web.IoC
{
    public class PriorityInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPriority>().ImplementedBy<Priority>());
        }
    }
}