using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSC_dCC_TrashCollector.Startup))]
namespace MSC_dCC_TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
