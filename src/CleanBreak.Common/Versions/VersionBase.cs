namespace CleanBreak.Common.Versions
{
	public abstract class VersionBase
	{
		public abstract bool Upgrade(object data);
		public abstract bool Downgrade(object data);
	}
}
