using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpingHand.Startup))]
namespace HelpingHand
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
