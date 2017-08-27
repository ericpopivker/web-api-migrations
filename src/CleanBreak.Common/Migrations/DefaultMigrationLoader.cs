using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CleanBreak.Common.Migrations
{
    public class DefaultMigrationLoader : IMigrationLoader
    {
        private readonly string _ns;

        public DefaultMigrationLoader(string @namespace)
        {
            _ns = @namespace;
        }

        public IEnumerable<MigrationWrapper> Load()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Namespace == _ns && IsMigration(t))
                .Select(t => CreateMigrationWrapper(t));
        }

        private MigrationWrapper CreateMigrationWrapper(Type migrationType)
        {
            string version = Regex.Match(migrationType.Name, @"v(.*)_").Groups[1].Value;            
            return new MigrationWrapper()
            {
                Version = version,
                Migration = (MigrationBase) Activator.CreateInstance(migrationType)
            };
        }

        private bool IsMigration(Type type)
        {
            if (type == typeof (MigrationBase))
            {
                return true;
            }
            if (type.BaseType == null)
            {
                return false;
            }
            return IsMigration(type.BaseType);
        }
    }
}
