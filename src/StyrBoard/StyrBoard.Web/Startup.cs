using Microsoft.Owin;
using Owin;
using StyrBoard.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace StyrBoard.Web
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
