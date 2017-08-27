using System;

namespace CleanBreak.Common.Migrations
{
    public class MigrationWrapper
    {
        public IComparable Version;
        public MigrationBase Migration;
    }
}
