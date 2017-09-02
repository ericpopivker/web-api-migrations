using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CleanBreak.Common.Versions
{
    public class DefaultVersionLoader : IVersionLoader
    {
        private readonly string _ns;

        public DefaultVersionLoader(string @namespace)
        {
            _ns = @namespace;
        }

        public IEnumerable<VersionWrapper> Load()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Namespace == _ns && IsMigration(t))
                .Select(t => CreateMigrationWrapper(t));
        }

        private VersionWrapper CreateMigrationWrapper(Type versionType)
        {
            string versionNumber = Regex.Match(versionType.Name, @"v(.*)", RegexOptions.IgnoreCase).Groups[1].Value;            
            return new VersionWrapper()
            {
                Number = versionNumber,
                Version = (VersionBase) Activator.CreateInstance(versionType)
            };
        }

        private bool IsMigration(Type type)
        {
            if (type == typeof (VersionBase))
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
