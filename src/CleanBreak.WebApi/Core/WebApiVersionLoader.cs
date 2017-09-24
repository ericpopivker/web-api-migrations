using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Versions;

namespace CleanBreak.WebApi.Core
{
	public class WebApiVersionLoader : IVersionLoader
	{
		private readonly ICleanBreakApiConfig _apiConfig;
		private readonly HttpConfiguration _httpConfiguration;
		private readonly ICache _cache = new DefaultInMemoryCache();

		public WebApiVersionLoader(ICleanBreakApiConfig apiConfig, HttpConfiguration httpConfiguration)
		{
			_apiConfig = apiConfig;
			_httpConfiguration = httpConfiguration;
		}

		public IEnumerable<VersionWrapper> Load()
		{
			return _apiConfig.Versions.Select(v => new VersionWrapper()
			{
				Number = getVersion(v.GetType().Namespace.Split('.').Last()),
				Version = new WebApiVersion(_httpConfiguration, _cache)
				{
					ApiVersion = v										
				}
			});
		}

		private string getVersion(string versionStr)
		{
			return Regex.Match(versionStr, @"v(.*)", RegexOptions.IgnoreCase).Groups[1].Value;            		}
	}
}
