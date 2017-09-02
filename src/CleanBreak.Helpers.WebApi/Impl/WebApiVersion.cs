using System.Net.Http;
using System.Web.Http;
using CleanBreak.Common.Caches;
using CleanBreak.Helpers.WebApi.Contract;
using CleanBreak.Integration.Owin;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Helpers.WebApi.Impl
{
	public class WebApiVersion : OwinVersion
	{
		private readonly HttpConfiguration _httpConfiguration;
		private readonly ICache _cache;

		public WebApiVersion(HttpConfiguration httpConfiguration, ICache cache)
		{
			_httpConfiguration = httpConfiguration;
			_cache = cache;
		}

		public IApiVersion ApiVersion { get; set; }

		public override bool Upgrade(Request request)
		{
			var apiRequest = new ApiRequest(request);
			var changes = ApiVersion.Changes;
			var targetContext = new TargetContext()
			{
				HttpConfiguration = _httpConfiguration,
				Cache = _cache
			};

			bool changed = false;
			foreach (var change in changes)
			{				
				setUp(change as ApiChangeBase);
				if (change.Target.IsMap(apiRequest, targetContext))
				{
					change.UpgradeRequest(apiRequest);
					changed = true;
				}
			}
			return changed;
		}

		private void setUp(ApiChangeBase change)
		{
			if (change == null)
			{
				return;
			}
			change.HttpConfiguration = _httpConfiguration;
		}

		public override bool Downgrade(Response response)
		{
			var apiResponse = new ApiResponse(response);
			var changes = ApiVersion.Changes;
			var targetContext = new TargetContext()
			{
				HttpConfiguration = _httpConfiguration,
				Cache = _cache
			};

			bool changed = false;
			foreach (var change in changes)
			{
				setUp(change as ApiChangeBase);
				if (change.Target.IsMap(apiResponse, targetContext))
				{
					change.DowngradeResponse(apiResponse);
					changed = true;
				}
			}
			return changed;
		}
	}
}
