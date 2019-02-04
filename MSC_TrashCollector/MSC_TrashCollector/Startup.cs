using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MSC_TrashCollector.Startup))]
namespace MSC_TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
        }


    }
}
