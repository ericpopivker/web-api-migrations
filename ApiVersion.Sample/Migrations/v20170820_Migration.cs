using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.Migrations
{
    public class v20170820_Migration : JsonMigration
    {
        public override bool Migrate(object key, JObject data)
        {
            data["v20170820_Migration"] = 1;
            return true;
        }
    }
}
