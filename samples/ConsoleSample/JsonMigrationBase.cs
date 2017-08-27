using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanBreak.Common.Migrations;
using Newtonsoft.Json.Linq;

namespace ConsoleSample
{
	public abstract class JsonMigrationBase : MigrationBase
	{
		public override bool Migrate(object key, object data)
		{
			return Migrate((string) key, (JObject) data);
		}

		public abstract bool Migrate(string key, JObject data);
	}
}
