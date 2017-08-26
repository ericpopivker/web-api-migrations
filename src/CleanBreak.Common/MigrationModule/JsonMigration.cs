namespace CleanBreak.Common.MigrationModule
{
    public abstract class JsonMigration : Migration
    {
        public override bool Migrate(object key, object data)
        {
            return Migrate(key, (JObject)data);
        }

        public abstract bool Migrate(object key, JObject data);
    }
}
