using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SensorGateway.Startup))]
namespace SensorGateway
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
