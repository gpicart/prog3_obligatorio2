using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Obligatorio2.Startup))]
namespace Obligatorio2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
