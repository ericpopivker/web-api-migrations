using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Versions;
using CleanBreak.Integration.Owin;

namespace CleanBreak.Helpers.WebApi
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
			//var mappingAttributes = GetMappingAttributes(version);
			WebApiRequestHandler requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(method, uri, _httpConfiguration);
			if (requestHandler == null)
			{
				return false;
			}
			return true;
			//return mappingAttributes.Any(mappingAttr => IsApplied(requestHandler, mappingAttr));
		}

		private WebApiMigrationMapAttribute[] GetMappingAttributes(VersionWrapper version)
		{
			var cacheKey = version.Version.GetType().AssemblyQualifiedName;
			WebApiMigrationMapAttribute[] attributes;
			if (!_cache.TryGet(cacheKey, out attributes))
			{
				attributes =
					version.Version.GetType()
						.GetCustomAttributes(typeof(WebApiMigrationMapAttribute), false)
						.Cast<WebApiMigrationMapAttribute>()
						.ToArray();
				_cache[cacheKey] = attributes;
			}
			return attributes;
		}

		public bool IsApplied(WebApiRequestHandler requestHandler, WebApiMigrationMapAttribute migrationMapping)
		{
			return migrationMapping.Controller == null || migrationMapping.Controller == requestHandler.ControllerType
							 && migrationMapping.HttpMethod == null || string.Compare(migrationMapping.HttpMethod, requestHandler.Method, StringComparison.OrdinalIgnoreCase) == 0
							 && migrationMapping.Action == null || migrationMapping.Action == requestHandler.ActionName;
		}
	}
}
