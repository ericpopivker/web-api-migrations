using System;
using System.Web.Http;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Versions;
using CleanBreak.Owin;

namespace CleanBreak.WebApi.Core
{
	public class WebApiVersionFilter : IVersionFilter
	{
		private readonly HttpConfiguration _httpConfiguration;
		private readonly ICache _cache = new StaticClassCache();

		public WebApiVersionFilter(HttpConfiguration httpConfiguration)
		{
			if (httpConfiguration == null)
			{
				throw new ArgumentNullException(nameof(httpConfiguration));
			}
			_httpConfiguration = httpConfiguration;
		}

		public bool FilterDowngrade(object data, VersionWrapper version)
		{
			Response response = (Response)data;
			return Filter(response.RequestMethod, response.RequestUri, version);
		}

		public bool FilterUpgrade(object data, VersionWrapper version)
		{
			Request request = (Request)data;
			return Filter(request.Method, request.Uri, version);
		}

		private bool Filter(string method, Uri uri, VersionWrapper version)
		{
			WebApiRequestHandler requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(method, uri, _httpConfiguration);
			if (requestHandler == null)
			{
				return false;
			}
			return true;
		}
	}
}
