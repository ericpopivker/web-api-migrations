using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiVersion.Owin;
using ApiVersion.Sample.OwinMigrations;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Web.Sample.ApiMigrations
{
    public class v20170817_Migration : OwinMigration
    {
        public override bool Migrate(OwinMigrationKey key, OwinMigrationData body)
        {
            if (key.Method != "POST" || key.Uri.LocalPath.StartsWith("api/values", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (key.Direction == DataDirection.Incoming)
            {
                body.Body = $"\"{JObject.Parse(body.Body)["value"]}\"";
                return true;
            }
            body.Body = $"{{ \"result\": {body.Body}}}";
            return true;
        }
    }
}