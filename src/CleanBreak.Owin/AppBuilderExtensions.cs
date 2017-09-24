using CleanBreak.Common.Versions;
using CleanBreak.Owin.Core;
using Owin;

namespace CleanBreak.Owin
{
	public static class AppBuilderExtensions
	{
		public static void UseCleanBreakForOwin(this IAppBuilder app, string versionsClassNamespace, string appliedUriPathRegexPattern = null)
		{
			app.Use<CleanBreakOwinMiddleware>(
				new DefaultVersionLoader(versionsClassNamespace), 
				new DefaultVersionProvider(),
				new NullVersionFilter(),
				appliedUriPathRegexPattern == null ? null : new PathRequestFilter(appliedUriPathRegexPattern));
		}
	}
}
