using System.Web.Http;
using CleanBreak.Common.Caches;

namespace CleanBreak.WebApi.Core
{
	public class TargetContext
	{
		public HttpConfiguration HttpConfiguration { get; internal set; }
		public ICache Cache { get; internal set; }
	}
}
