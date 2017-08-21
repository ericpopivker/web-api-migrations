using System;
using ApiVersion.Owin;
using ApiVersion.Sample.Migrations;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.OwinMigrations
{
    public abstract class OwinMigration : Migration
    {
        public override object Migrate(object key, object data)
        {
            return Migrate((OwinMigrationKey)key, (OwinMigrationData)data);
        }

        public abstract OwinMigrationData Migrate(OwinMigrationKey key, OwinMigrationData body);

        protected bool SetUpAsUseful(OwinMigrationData body)
        {
            var testMigration = body as TestMigration;
            if (testMigration == null)
            {
                return false;
            }
            testMigration.SetAsUsefull();
            return true;
        }

        protected string NothingChanged()
        {
            return null;
        }
    }
}
