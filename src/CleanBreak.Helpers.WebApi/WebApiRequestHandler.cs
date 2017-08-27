using System;

namespace CleanBreak.Helpers.WebApi
{
	public class WebApiRequestHandler
	{
		public Type ControllerType { get; set; }
		public string ActionName { get; set; }
		public string Method { get; set; }
	}
}
