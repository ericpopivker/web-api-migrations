using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Migrations;
using CleanBreak.Integration.Owin;

namespace CleanBreak.Helpers.WebApi
{
	public class WebApiMigrationFilter : IMigrationFilter
	{
		private readonly HttpConfiguration _httpConfiguration;
		private readonly ICache _cache = new StaticClassCache();

		public WebApiMigrationFilter(HttpConfiguration httpConfiguration)
		{
			if (httpConfiguration == null)
			{
				throw new ArgumentNullException(nameof(httpConfiguration));
			}
			_httpConfiguration = httpConfiguration;
		}

		public bool Filter(object key, MigrationWrapper migration)
		{
			OwinMigrationKey owinKey = (OwinMigrationKey) key;
			var mappingAttributes = GetMappingAttributes(migration);
			WebApiRequestHandler requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(owinKey.Method, owinKey.Uri, _httpConfiguration);
			if (requestHandler == null && mappingAttributes.Any())
			{
				return false;
			}
			return mappingAttributes.Any(mappingAttr => IsApplied(requestHandler, mappingAttr));
		}

		private WebApiMigrationMapAttribute[] GetMappingAttributes(MigrationWrapper migration)
		{
			var cacheKey = migration.Migration.GetType().AssemblyQualifiedName;
			WebApiMigrationMapAttribute[] attributes;
			if (!_cache.TryGet(cacheKey, out attributes))
			{
				attributes =
					migration.Migration.GetType()
						.GetCustomAttributes(typeof (WebApiMigrationMapAttribute), false)
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
