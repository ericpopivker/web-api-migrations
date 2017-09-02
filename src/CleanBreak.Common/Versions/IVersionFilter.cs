namespace CleanBreak.Common.Versions
{
	public interface IVersionFilter
	{
		bool FilterDowngrade(object key, VersionWrapper version);
		bool FilterUpgrade(object key, VersionWrapper version);
	}
}
