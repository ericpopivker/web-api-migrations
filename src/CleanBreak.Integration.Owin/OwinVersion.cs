using CleanBreak.Common.Versions;

namespace CleanBreak.Integration.Owin
{
	public abstract class OwinVersion : VersionBase
	{
		public override bool Upgrade(object data)
		{
			return Upgrade((Request)data);
		}

		public override bool Downgrade(object data)
		{
			return Downgrade((Response)data);
		}

		public abstract bool Upgrade(Request request);
		public abstract bool Downgrade(Response response);
	}
}
