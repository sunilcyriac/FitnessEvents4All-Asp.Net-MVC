using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessEvents.Startup))]
namespace FitnessEvents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
