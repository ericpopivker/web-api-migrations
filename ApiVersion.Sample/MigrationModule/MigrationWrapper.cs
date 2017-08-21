using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiVersion.Sample.Migrations;

namespace ApiVersion.Sample.MigrationModule
{
    public class MigrationWrapper
    {
        public IComparable Version;
        public Migration Migration;
    }
}
