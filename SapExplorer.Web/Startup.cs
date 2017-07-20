using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SapExplorer.Web.Startup))]
namespace SapExplorer.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
