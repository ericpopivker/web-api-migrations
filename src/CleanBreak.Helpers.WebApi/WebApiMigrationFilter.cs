using System;
using System.Linq;
using System.Web;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Migrations;

namespace CleanBreak.Helpers.WebApi
{
	public class WebApiMigrationFilter : IMigrationFilter
	{
		private readonly ICache _cache = new StaticClassCache();

		public bool Filter(object key, MigrationWrapper migration)
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

			WebApiRequestHandler requestHandler = RouteHelpers.GetRequestHandler(HttpContext.Current.Request);
			if (requestHandler == null && attributes.Any())
			{
				return false;
			}
			return attributes.Any(routeMap => IsMap(requestHandler, routeMap));
		}

		public bool IsMap(WebApiRequestHandler key, WebApiMigrationMapAttribute webApiMigrationMap)
		{
			return webApiMigrationMap.ControllerType == null || webApiMigrationMap.ControllerType == key.ControllerType
			         && webApiMigrationMap.HttpMethod == null || string.Compare(webApiMigrationMap.HttpMethod, key.Method, StringComparison.OrdinalIgnoreCase) == 0
			         && webApiMigrationMap.Action == null || webApiMigrationMap.Action == key.ActionName;
		}
	}
}
