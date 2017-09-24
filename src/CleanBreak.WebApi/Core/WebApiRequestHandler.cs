using System;
using System.Collections.Generic;

namespace CleanBreak.WebApi.Core
{
	public class WebApiRequestHandler
	{
		public Type ControllerType { get; set; }
		public string ActionName { get; set; }
		public Type ActionReturnType { get; set; }
		public IEnumerable<Type> ActionParatemersTypes { get; set; } 
		public string Method { get; set; }
	}
}
