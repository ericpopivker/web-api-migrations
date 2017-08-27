namespace CleanBreak.Common.Migrations
{
    public abstract class MigrationBase
    {
        public abstract bool Migrate(object key, object data);
    }
}
