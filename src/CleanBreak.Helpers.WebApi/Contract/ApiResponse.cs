using System;
using System.Net.Http;
using CleanBreak.Integration.Owin;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Helpers.WebApi.Contract
{
	public class ApiResponse
	{
		private readonly Response _response;

		public ApiResponse(Response response)
		{
			_response = response;
		}

		public Uri RequestUrl
		{
			get { return _response.RequestUri; }
			internal set { _response.RequestUri = value; }
		}

		public HttpMethod RequestMethod
		{
			get { return new HttpMethod(_response.RequestMethod); }
			internal set { _response.RequestMethod = value.Method; }
		}		

		public BodyContent Body
		{
			get { return _response.Body; }
			internal set { _response.Body = value; }
		}
	}
}