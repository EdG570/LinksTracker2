using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinksTracker2.Startup))]
namespace LinksTracker2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
