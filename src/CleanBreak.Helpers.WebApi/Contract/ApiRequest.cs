using System;
using System.Net.Http;
using CleanBreak.Integration.Owin;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Helpers.WebApi.Contract
{
	public class ApiRequest
	{
		private readonly Request _request;

		public ApiRequest(Request request)
		{
			_request = request;
		}

		public Uri Url
		{
			get { return _request.Uri; }
			internal set { _request.Uri = value; }
		}

		public HttpMethod Method
		{
			get { return new HttpMethod(_request.Method); }
			internal set { _request.Method = value.Method; }
		}

		public BodyContent Body
		{
			get { return _request.Body; }
			internal set { _request.Body = value; }
		}
	}
}