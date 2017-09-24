using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace CleanBreak.Owin.Core
{
	public class PathRequestFilter : IRequestFilter
	{
		private readonly string _pathPattern;

		public PathRequestFilter(string pathPattern)
		{
			_pathPattern = pathPattern;
		}

		public bool Filter(IOwinRequest request)
		{
			return Regex.IsMatch(request.Path.ToString(), _pathPattern, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
		}
	}
}
