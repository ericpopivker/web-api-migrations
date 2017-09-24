using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace CleanBreak.WebApi.Core
{
	public static class WebApiRequestHandlerFinder
	{
		public static WebApiRequestHandler GetRequestHandler(string httpMethod, Uri uri, HttpConfiguration httpConfiguration = null)
		{
			httpConfiguration = httpConfiguration ?? GlobalConfiguration.Configuration;
			httpConfiguration.EnsureInitialized();

			var requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), uri);
			IHttpRouteData routeData = httpConfiguration.Routes.GetRouteData(requestMessage);
			if (routeData == null)
			{
				return null;
			}
			RemoveOptionalRoutingParameters(routeData.Values);

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
			HttpActionContext actionContext = new HttpActionContext(controllerContext, actionDescriptor);

			return new WebApiRequestHandler()
			{
				ControllerType = controllerDescriptor.ControllerType,
				ActionName = actionDescriptor.ActionName,
				ActionReturnType = actionDescriptor.ReturnType,
				ActionParatemersTypes = actionDescriptor.GetParameters().Select(p => p.ParameterType),
				Method = httpMethod
			};
		}


		/// <summary>
		/// It was taken as is 
		/// from System.Web.Http.Dispatcher.HttpRoutingDispatcher.RemoveOptionalRoutingParameters(IDictionary{string, object}) source codes
		/// </summary>
		private static void RemoveOptionalRoutingParameters(IDictionary<string, object> routeValueDictionary)
		{

			// Get all keys for which the corresponding value is 'Optional'.
			// Having a separate array is necessary so that we don't manipulate the dictionary while enumerating.
			// This is on a hot-path and linq expressions are showing up on the profile, so do array manipulation.
			int max = routeValueDictionary.Count;
			int i = 0;
			string[] matching = new string[max];
			foreach (KeyValuePair<string, object> kv in routeValueDictionary)
			{
				if (kv.Value == RouteParameter.Optional)
				{
					matching[i] = kv.Key;
					i++;
				}
			}
			for (int j = 0; j < i; j++)
			{
				string key = matching[j];
				routeValueDictionary.Remove(key);
			}
		}

	}
}
