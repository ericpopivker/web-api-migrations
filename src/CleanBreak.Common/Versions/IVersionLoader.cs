using System.Collections.Generic;

namespace CleanBreak.Common.Versions
{
    public interface IVersionLoader
    {
        IEnumerable<VersionWrapper> Load();
    }
}
