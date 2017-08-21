using System;

namespace ApiVersion.Sample.Migrations
{
    public abstract class Migration
    {
        public abstract object Migrate(object key, object data);
    }
}
