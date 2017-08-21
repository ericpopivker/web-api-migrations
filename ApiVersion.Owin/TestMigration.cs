using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiVersion.Owin
{
    class TestMigration : OwinMigrationData
    {
        public bool Useful { get; private set; }

        public void SetAsUsefull()
        {
            Useful = true;
        }
    }
}
