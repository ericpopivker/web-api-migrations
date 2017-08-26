using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ApiVersion.Owin;
using ApiVersion.Sample.MigrationModule;
using ApiVersion.Web.Sample.ApiMigrations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ApiVersion.Web.Sample.Startup))]

namespace ApiVersion.Web.Sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);
            HttpConfiguration configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.Use<ApiVersionMiddleware>(new MigrationLoader(typeof(v20170817_Migration).Namespace), new DefaultVersionProvider());
            app.UseWebApi(configuration);
        }
    }
}
