using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanBreak.Common.Migrations
{
    public class MigrationManager
    {
        private readonly IMigrationLoader _migrationLoader;
        private MigrationWrapper[] _migrations; 

        public MigrationManager(IMigrationLoader migrationLoader)
        {
            _migrationLoader = migrationLoader;
            _migrations = _migrationLoader.Load().OrderBy(s => s.Version).ToArray();
        }

        public bool Migrate(object key, object data, IComparable currentVersion, MigrationDirection direction)
        {
            IEnumerable<MigrationWrapper> migrationPipeline = _migrations;
            if (direction == MigrationDirection.Forward)
            {
                migrationPipeline = migrationPipeline.SkipWhile(s => s.Version.CompareTo(currentVersion) <= 0);
            }            
            if (direction == MigrationDirection.Backward)
            {
                migrationPipeline = migrationPipeline.SkipWhile(s => s.Version.CompareTo(currentVersion) <= 0).Reverse();
            }
            bool migrated = false;
            foreach (var migration in migrationPipeline)
            {
	            migration.Migration.Direction = direction;
                migrated |= migration.Migration.Migrate(key, data);
            }
            return migrated;
        }

        
    }
}
