using System.Configuration;
using System.Data.Entity;
using JPGPizza.Data;
using JPGPizza.MVC;
using Microsoft.Owin;
using Owin;
using JPGPizza.Data.Migrations;

[assembly: OwinStartup(typeof(Startup))]
namespace JPGPizza.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
