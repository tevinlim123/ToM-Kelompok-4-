using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToM.Startup))]
namespace ToM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
