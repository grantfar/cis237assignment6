using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asignment6.Startup))]
namespace Asignment6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
