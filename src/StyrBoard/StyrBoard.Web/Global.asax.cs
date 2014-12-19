using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNet.SignalR.Hubs;
using StyrBoard.Web.IoC;

namespace StyrBoard.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer _container;

        public WebApiApplication()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
        }
        protected void Application_Start()
        {
            _container.Register(Classes.FromThisAssembly().BasedOn(typeof(IHub)).LifestyleTransient());
            var signalRDependencyResolver = new SignalRDependencyResolver(_container);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = signalRDependencyResolver;   
            InstallInversionOfControlForWebApi();
            

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Dispose()
        {
            _container.Dispose();
            base.Dispose();
        }

        private void InstallInversionOfControlForWebApi()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(_container));
        }
    }
}
