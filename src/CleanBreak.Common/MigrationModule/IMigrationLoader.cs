using System.Collections.Generic;

namespace CleanBreak.Common.MigrationModule
{
    public interface IMigrationLoader
    {
        IEnumerable<MigrationWrapper> Load();
    }
}
