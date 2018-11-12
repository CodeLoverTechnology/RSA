using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSA.Startup))]
namespace RSA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
