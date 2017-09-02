using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiVersion.Sample.Migrations;
using CleanBreak.Common.Versions;
using Newtonsoft.Json.Linq;

namespace ConsoleSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var loader = new DefaultVersionLoader(typeof (V20170820_Version).Namespace);
			var manager = new VersionManager(loader, new NullVersionFilter());
			var data = JObject.Parse("{'data': 1}");
			manager.UpgradeData(data, "20170812");
			Console.WriteLine(data);

			Console.WriteLine("Press Enter");
			Console.ReadLine();
		}
	}
}
