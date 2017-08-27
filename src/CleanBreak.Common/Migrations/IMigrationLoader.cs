using System.Collections.Generic;

namespace CleanBreak.Common.Migrations
{
    public interface IMigrationLoader
    {
        IEnumerable<MigrationWrapper> Load();
    }
}
