using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sharemycoach.Startup))]
namespace sharemycoach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
