using ConsoleSample;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.Migrations
{
	public class V20170815_Version : JsonVersionBase
	{
		public override bool Upgrade(JObject data)
		{
			data["v20170815_Migration"] = 1;
			return true;
		}

		public override bool Downgrade(JObject data)
		{
			return false;
		}
	}
}
