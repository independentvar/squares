using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KongOrange.Squares.WebInterface.Startup))]
namespace KongOrange.Squares.WebInterface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
