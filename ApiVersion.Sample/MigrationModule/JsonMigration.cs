using System;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.Migrations
{
    public abstract class JsonMigration : Migration
    {
        public override object Migrate(object key, object data)
        {
            return Migrate(key, (JObject)data);
        }

        public abstract JObject Migrate(object key, JObject data);
    }
}
