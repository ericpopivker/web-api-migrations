using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiVersion.Sample.Migrations;
using CleanBreak.Common.Migrations;
using Newtonsoft.Json.Linq;

namespace ConsoleSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var loader = new DefaultMigrationLoader(typeof (v20170820_Migration).Namespace);
			var manager = new MigrationManager(loader);
			var data = JObject.Parse("{'data': 1}");
			manager.Migrate("key1", data, "20170812", MigrationDirection.Forward);
			Console.WriteLine(data);

			Console.WriteLine("Press Enter");
			Console.ReadLine();
		}
	}
}
