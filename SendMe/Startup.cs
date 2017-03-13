using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendMe.Startup))]
namespace SendMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
