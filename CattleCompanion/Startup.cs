using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CattleCompanion.Startup))]
namespace CattleCompanion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
