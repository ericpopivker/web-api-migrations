using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CleanBreak.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using WebApiSample;
using WebApiSample.ApiVersions;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApiSample
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration configuration = new HttpConfiguration();

			WebApiConfig.Register(configuration);

			app.UseCleanBreakForWebApi(new CleanBreakApiConfig(), configuration);
			app.UseWebApi(configuration);

		}
	}
}
