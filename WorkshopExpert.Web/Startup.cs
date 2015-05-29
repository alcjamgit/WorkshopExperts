using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkshopExpert.Web.Startup))]
namespace WorkshopExpert.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
