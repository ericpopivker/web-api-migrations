using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanBreak.Common.Versions;
using Newtonsoft.Json.Linq;

namespace ConsoleSample
{
	public abstract class JsonVersionBase : VersionBase
	{
		public override bool Upgrade(object data)
		{
			return Upgrade((JObject) data);
		}

		public override bool Downgrade(object data)
		{
			return Downgrade((JObject) data);
		}

		public abstract bool Upgrade(JObject data);
		public abstract bool Downgrade(JObject data);
	}
}
