using System.Web;
using CleanBreak.Integration.Owin;

namespace CleanBreak.Helpers.WebApi
{
	public static class WebApiExtenstions
	{
		public static WebApiRequestHandler GetWebApiRequestHandler(this OwinMigration migration)
		{
			return RouteHelpers.GetRequestHandler(HttpContext.Current.Request);
		}
	}
}
