using CleanBreak.Common.Migrations;

namespace CleanBreak.Integration.Owin
{
    public abstract class OwinMigration : MigrationBase
    {
        public override bool Migrate(object key, object data)
        {
            return Migrate((OwinMigrationKey)key, (OwinMigrationData)data);
        }

        public abstract bool Migrate(OwinMigrationKey key, OwinMigrationData body);
    }
}
