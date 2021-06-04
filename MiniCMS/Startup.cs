using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniCMS.Startup))]
namespace MiniCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
