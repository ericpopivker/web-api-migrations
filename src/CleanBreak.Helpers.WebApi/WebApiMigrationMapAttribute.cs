using System;

namespace CleanBreak.Helpers.WebApi
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class WebApiMigrationMapAttribute : Attribute
	{
		public Type Controller { get; set; }
		public string Action { get; set; }
		public string HttpMethod { get; set; }
	}
}
