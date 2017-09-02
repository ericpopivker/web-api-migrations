using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CleanBreak.Common.Caches;

namespace CleanBreak.Helpers.WebApi.Impl
{
	public class TargetContext
	{
		public HttpConfiguration HttpConfiguration { get; internal set; }
		public ICache Cache { get; internal set; }
	}
}
