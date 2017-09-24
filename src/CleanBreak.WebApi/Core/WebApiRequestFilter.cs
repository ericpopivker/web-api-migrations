using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CleanBreak.Owin.Core;
using Microsoft.Owin;

namespace CleanBreak.WebApi.Core
{
	public class WebApiRequestFilter : IRequestFilter
	{
		private readonly HttpConfiguration _httpConfiguration;

		public WebApiRequestFilter(HttpConfiguration httpConfiguration)
		{
			_httpConfiguration = httpConfiguration;
		}

		public bool Filter(IOwinRequest request)
		{
			WebApiRequestHandler requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(request.Method, request.Uri,
				_httpConfiguration);
			return requestHandler != null;
		}
	}
}
