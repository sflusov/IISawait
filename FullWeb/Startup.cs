using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullWeb.Startup))]
namespace FullWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
