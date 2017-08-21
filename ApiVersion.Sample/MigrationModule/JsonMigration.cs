using System;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.Migrations
{
    public abstract class JsonMigration : Migration
    {
        public override bool Migrate(object key, object data)
        {
            return Migrate(key, (JObject)data);
        }

        public abstract bool Migrate(object key, JObject data);
    }
}
