namespace CleanBreak.Common.Versions
{
	public class NullVersionFilter : IVersionFilter
	{
		public bool FilterDowngrade(object key, VersionWrapper version)
		{
			return true;
		}

		public bool FilterUpgrade(object key, VersionWrapper version)
		{
			return true;
		}
	}
}
