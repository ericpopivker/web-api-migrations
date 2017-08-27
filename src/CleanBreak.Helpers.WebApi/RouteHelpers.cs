using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

namespace CleanBreak.Helpers.WebApi
{
	public static class RouteHelpers
	{
		public static WebApiRequestHandler GetRequestHandler(HttpRequest request)
		{
			var message = new HttpRequestMessage(new HttpMethod(request.HttpMethod), request.Url);
			var routeData = GlobalConfiguration.Configuration.Routes.GetRouteData(message);

			message.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
			HttpControllerDescriptor descriptor = null;
			try
			{
				descriptor = GlobalConfiguration.Configuration.Services.GetHttpControllerSelector().SelectController(message);
			}
			catch (HttpResponseException ex)
			{
				if (ex.Response.StatusCode == HttpStatusCode.NotFound)
				{
					return null;
				}
				throw;
			}


			var controllerContext = new HttpControllerContext(GlobalConfiguration.Configuration, routeData, message)
			{
				ControllerDescriptor = descriptor
			};
			var action = GlobalConfiguration.Configuration.Services.GetActionSelector().SelectAction(controllerContext);
			return new WebApiRequestHandler()
			{
				ControllerType = descriptor.ControllerType,
				ActionName = action.ActionName
			};
		}

		//private HttpMethod ParseHttpMethod(string method)
		//{
		//	switch (method.ToUpper())
		//	{
		//		case "POST":
		//			return HttpMethod.Post;
		//		case "GET":
		//			return HttpMethod.Get;
		//		case "PUT":
		//			return HttpMethod.Put;
		//		case "DELETE":
		//			return HttpMethod.Delete;
		//		case "HEAD":
		//			return HttpMethod.Head;
		//		case "OPTIONS":
		//			return HttpMethod.Options;
		//		case "TRACE":
		//			return HttpMethod.Trace;
		//		default:
		//			throw new ArgumentException("Unsupported HTTP Method", nameof(method));
					
		//	}
		//}
	}
}
