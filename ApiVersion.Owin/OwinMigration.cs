using System;
using ApiVersion.Owin;
using ApiVersion.Sample.Migrations;
using Newtonsoft.Json.Linq;

namespace ApiVersion.Sample.OwinMigrations
{
    public abstract class OwinMigration : Migration
    {
        public override bool Migrate(object key, object data)
        {
            return Migrate((OwinMigrationKey)key, (OwinMigrationData)data);
        }

        public abstract bool Migrate(OwinMigrationKey key, OwinMigrationData body);
    }
}
