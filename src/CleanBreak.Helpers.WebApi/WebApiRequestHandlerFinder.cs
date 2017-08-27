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
using System.Web.Http.Routing;

namespace CleanBreak.Helpers.WebApi
{
	public static class WebApiRequestHandlerFinder
	{
		public static WebApiRequestHandler GetRequestHandler(string httpMethod, Uri uri, HttpConfiguration httpConfiguration = null)
		{
			httpConfiguration = httpConfiguration ?? GlobalConfiguration.Configuration;
			httpConfiguration.EnsureInitialized();

			var requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), uri);
			IHttpRouteData routeData = httpConfiguration.Routes.GetRouteData(requestMessage);
			requestMessage.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

			HttpControllerDescriptor controllerDescriptor = null;
			try
			{
				controllerDescriptor = httpConfiguration.Services.GetHttpControllerSelector().SelectController(requestMessage);
			}
			catch (HttpResponseException ex)
			{
				if (ex.Response.StatusCode == HttpStatusCode.NotFound)
				{
					// Controller doesn't found
					return null;
				}
				throw;
			}

			var controllerContext = new HttpControllerContext(httpConfiguration, routeData, requestMessage)
			{
				ControllerDescriptor = controllerDescriptor
			};
			HttpActionDescriptor actionDescriptor = httpConfiguration.Services.GetActionSelector().SelectAction(controllerContext);
			return new WebApiRequestHandler()
			{
				ControllerType = controllerDescriptor.ControllerType,
				ActionName = actionDescriptor.ActionName,
				Method = httpMethod
			};
		}
	}
}
