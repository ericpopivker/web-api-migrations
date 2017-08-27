using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CleanBreak.Helpers.WebApi;
using CleanBreak.Integration.Owin;
using Newtonsoft.Json.Linq;
using WebApiSample.Controllers;


namespace ApiVersion.Web.Sample.ApiMigrations
{
	[WebApiMigrationMap(ControllerType = typeof(OrderController), HttpMethod = "Post")]
    public class v20170817_Migration : OwinMigration
    {
        public override bool Migrate(OwinMigrationKey key, OwinMigrationData body)
        {
            if (key.Direction == DataDirection.Request)
            {
                body.Body = $"\"{JObject.Parse(body.Body)["value"]}\"";
                return true;
            }
            body.Body = $"{{ \"result\": {body.Body}}}";
            return true;
        }
    }
}