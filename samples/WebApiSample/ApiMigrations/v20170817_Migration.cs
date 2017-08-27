using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CleanBreak.Helpers.WebApi;
using CleanBreak.Integration.Owin;
using Newtonsoft.Json.Linq;
using WebApiSample.Controllers;


namespace ApiVersion.Web.Sample.ApiMigrations
{
    public class v20170817_Migration : OwinMigration
    {
        public override bool Migrate(OwinMigrationKey key, OwinMigrationData body)
        {
	        var requestHandler = this.GetWebApiRequestHandler();
	        if (requestHandler == null)
	        {
		        return false;
	        }
	        if (!(requestHandler.ControllerType == typeof (OrderController) && requestHandler.ActionName == "Post"))
	        {
		        return false;
	        }
            //if ( key.Method != "POST" || key.Uri.LocalPath.StartsWith("api/order", StringComparison.OrdinalIgnoreCase))
            //{
            //    return false;
            //}

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