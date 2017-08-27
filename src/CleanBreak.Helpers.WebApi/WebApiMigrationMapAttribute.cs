using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CleanBreak.Helpers.WebApi
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class WebApiMigrationMapAttribute : Attribute
	{
		public Type ControllerType { get; set; }
		public string Action { get; set; }
		public string HttpMethod { get; set; }
	}
}
