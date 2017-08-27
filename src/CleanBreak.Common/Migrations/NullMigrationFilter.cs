using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanBreak.Common.Migrations
{
	public class NullMigrationFilter : IMigrationFilter
	{
		public bool Filter(object key, MigrationWrapper migration)
		{
			return true;
		}
	}
}
