using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNet.SignalR.Hubs;
using Raven.Client;
using Raven.Client.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.Domain.Repository.IoC;
using StyrBoard.Web.IoC;

namespace StyrBoard.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IWindsorContainer _container;


        protected void Application_Start()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Install(FromAssembly.Containing<RepositoryInstaller>());
            _container.Register(Classes.FromThisAssembly().BasedOn(typeof(IHub)).LifestyleTransient());
            var signalRDependencyResolver = new SignalRDependencyResolver(_container);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = signalRDependencyResolver;
            InstallInversionOfControlForWebApi();

            using (var session = _container.Resolve<IDocumentStore>().OpenSession())
            {
               var prio = session.Load<Priority>(new Priority().Id) ?? new Priority();
                _container.Register(Component.For<IPriority>().Instance(prio));
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Dispose()
        {
            if (_container != null) _container.Dispose();
            base.Dispose();
        }

        private void InstallInversionOfControlForWebApi()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(_container));
        }
    }
}
