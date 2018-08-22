using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(textAnalizer.Startup))]
namespace textAnalizer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
