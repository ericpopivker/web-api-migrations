using CleanBreak.Common.Migrations;
using ConsoleSample;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.Migrations
{
    public class v20170815_Migration : JsonMigrationBase
    {
        public override bool Migrate(string key, JObject data)
        {
            data["v20170815_Migration"] = 1;
            return true;
        }
    }
}
