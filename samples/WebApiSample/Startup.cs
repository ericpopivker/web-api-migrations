using System.Web.Http;
using CleanBreak.Helpers.WebApi;
using CleanBreak.Helpers.WebApi.Impl;
using CleanBreak.Integration.Owin;
using Microsoft.Owin;
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
			//         app.Use<CleanBreakOwinMiddleware>(new DefaultVersionLoader(typeof(V20170817_Version).Namespace), new DefaultVersionProvider(), new WebApiVersionFilter(configuration));

			app.Use<CleanBreakOwinMiddleware>(new WebApiVersionLoader(new CleanBreakApiConfig(), configuration), new DefaultVersionProvider(), new WebApiVersionFilter(configuration));
			app.UseWebApi(configuration);

		}
	}
}
