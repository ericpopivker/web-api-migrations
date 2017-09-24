using System.Web.Http;
using CleanBreak.Owin.Core;
using CleanBreak.WebApi.Core;
using Owin;

namespace CleanBreak.WebApi
{
	public static class AppBuilderExtensions
	{
		public static void UseCleanBreakForWebApi(this IAppBuilder app, ICleanBreakApiConfig cleanBreakApiConfig,
			HttpConfiguration httpConfiguration)
		{
			app.Use<CleanBreakOwinMiddleware>(
				new WebApiVersionLoader(cleanBreakApiConfig, httpConfiguration), 
				new DefaultVersionProvider(), 
				new WebApiVersionFilter(httpConfiguration));
		}
	}
}
