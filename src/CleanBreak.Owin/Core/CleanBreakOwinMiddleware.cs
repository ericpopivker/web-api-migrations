using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanBreak.Common.Caches;
using CleanBreak.Common.Versions;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Owin.Core
{
	public class CleanBreakOwinMiddleware : OwinMiddleware
	{
		private readonly IVersionProvider _versionProvider;
		private readonly IRequestFilter _requestFilter;
		private readonly VersionManager _versionManager;
		private readonly ICache _cache = new DefaultInMemoryCache();

		public CleanBreakOwinMiddleware(
			OwinMiddleware next, 
			IVersionLoader versionLoader, 
			IVersionProvider versionProvider, 
			IVersionFilter versionFilter = null,
			IRequestFilter requestFilter = null) : base(next)
		{
			_versionProvider = versionProvider;
			_requestFilter = requestFilter ?? new NullRequestFilter();
			_versionManager = new VersionManager(versionLoader, versionFilter ?? new NullVersionFilter());
		}

		public override async Task Invoke(IOwinContext context)
		{
			if (_requestFilter.Filter(context.Request))
			{
				IComparable version = _versionProvider.GetVersion(context);
				await migrateRequest(context, version);
				await migrateResponse(context, version);
				return;
			}
			await Next.Invoke(context);
		}

		private string GetCachekey(Request request, IComparable version)
		{
			return $"{request.GetType().Name}_{version}_{request.Method}_{request.Uri}";
		}

		private string GetCachekey(Response response, IComparable version)
		{
			return $"{response.GetType().Name}_{version}_{response.RequestMethod}_{response.RequestUri}";
		}

		private async Task migrateResponse(IOwinContext context, IComparable version)
		{
			Response response = new Response()
			{
				RequestMethod = context.Request.Method,
				RequestUri = context.Request.Uri,
			};

			bool applied;
			string cacheKey = GetCachekey(response, version);
			if (_cache.TryGet(cacheKey, out applied))
			{
				if (!applied)
				{
					await Next.Invoke(context);
					return;
				}
			}

			var owinResponse = context.Response;
			var owinResponseStream = owinResponse.Body;
			var responseBuffer = new MemoryStream();
			context.Response.Body = responseBuffer;
			await Next.Invoke(context);

			string responseJsonBody = "";
			responseBuffer.Seek(0, SeekOrigin.Begin);
			using (StreamReader reader = new StreamReader(responseBuffer))
			{
				responseJsonBody = await reader.ReadToEndAsync();
			}

			response.Body = new BodyContent(string.IsNullOrWhiteSpace(responseJsonBody) ? null : JToken.Parse(responseJsonBody));

			_cache[cacheKey] = _versionManager.DowngradeData(response, version);
			var newResultContent = new StringContent(response.Body.ToString(), Encoding.UTF8, "application/json");
			var customResponseStream = await newResultContent.ReadAsStreamAsync();
			await customResponseStream.CopyToAsync(owinResponseStream);

			owinResponse.ContentLength = customResponseStream.Length;
			owinResponse.Body = owinResponseStream;
		}

		private async Task migrateRequest(IOwinContext context, IComparable version)
		{
			Request request = new Request()
			{
				Method = context.Request.Method,
				Uri = context.Request.Uri,
			};

			string cacheKey = GetCachekey(request, version);
			bool applied;
			if (_cache.TryGet(cacheKey, out applied))
			{
				if (!applied)
				{
					return;
				}
			}

			string jsonBody = "";
			using (StreamReader reader = new StreamReader(context.Request.Body))
			{
				jsonBody = await reader.ReadToEndAsync();
			}
			request.Body = new BodyContent(string.IsNullOrWhiteSpace(jsonBody) ? null : JToken.Parse(jsonBody));

			_cache[cacheKey] = _versionManager.UpgradeData(request, version);
			var content = new StringContent(request.Body.ToString(), Encoding.UTF8, "application/json");
			context.Request.Body = await content.ReadAsStreamAsync();
		}
	}
}
