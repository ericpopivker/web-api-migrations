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
        public override OwinMigrationData Migrate(OwinMigrationKey key, OwinMigrationData body)
        {
            if (key.Method != "POST" || key.Uri.LocalPath.StartsWith("api/values", StringComparison.OrdinalIgnoreCase))
            {
                return body;
            }

            if (key.Direction == DataDirection.Incoming)
            {
                if (SetUpAsUseful(body)) return null;
                body.Body = $"\"{JObject.Parse(body.Body)["value"]}\"";
                return body;
            }
            if (SetUpAsUseful(body)) return null;
            body.Body = $"{{ \"result\": {body.Body}}}";
            return body;
        }
    }
}