namespace CleanBreak.Common.Migrations
{
    public abstract class MigrationBase
    {
		public MigrationDirection Direction { get; internal set; }

        public abstract bool Migrate(object key, object data);
    }
}
