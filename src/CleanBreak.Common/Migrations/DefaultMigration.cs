namespace CleanBreak.Common.Migrations
{
    public abstract class DefaultMigration : MigrationBase
    {
        public override bool Migrate(object key, object data)
        {
            return Migrate(key, data);
        }
    }
}
