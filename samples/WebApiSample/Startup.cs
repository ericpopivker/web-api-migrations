using System.Web.Http;
using ApiVersion.Web.Sample.ApiMigrations;
using CleanBreak.Common.Migrations;
using CleanBreak.Integration.Owin;
using Microsoft.Owin;
using Owin;
using WebApiSample;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApiSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            //HttpConfiguration configuration = new HttpConfiguration();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            app.Use<CleanBreakOwinMiddleware>(new DefaultMigrationLoader(typeof(v20170817_Migration).Namespace), new DefaultVersionProvider());
            app.UseWebApi(GlobalConfiguration.Configuration);
        }
    }
}
