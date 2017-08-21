using System;

namespace ApiVersion.Sample.Migrations
{
    public abstract class Migration
    {
        public abstract bool Migrate(object key, object data);
    }
}
