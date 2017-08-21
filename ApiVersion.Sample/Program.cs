using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ApiVersion.Sample.MigrationModule;
using ApiVersion.Sample.Migrations;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new MigrationLoader(typeof (v20170820_Migration).Namespace);
            var manager = new MigrationManager(loader);
            var result = (JObject)manager.Migrate(1, JObject.Parse("{'data': 1}"), "20170812", MigrationDirection.Forward);
            Console.WriteLine(result);
        }
    }
}
