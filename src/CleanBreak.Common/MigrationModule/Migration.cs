namespace CleanBreak.Common.MigrationModule
{
    public abstract class Migration
    {
        public abstract bool Migrate(object key, object data);
    }
}
