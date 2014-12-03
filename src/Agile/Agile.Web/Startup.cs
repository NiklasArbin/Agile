using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Agile.Web.Startup))]
namespace Agile.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
