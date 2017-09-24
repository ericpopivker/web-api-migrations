using CleanBreak.Common.Versions;
using CleanBreak.Owin.Core;
using Owin;

namespace CleanBreak.Owin
{
	public static class AppBuilderExtensions
	{
		public static void UseCleanBreakForOwin(this IAppBuilder app, string versionsNamespace)
		{
			app.Use<CleanBreakOwinMiddleware>(
				new DefaultVersionLoader(versionsNamespace), 
				new DefaultVersionProvider(),
				new NullVersionFilter());
		}
	}
}
