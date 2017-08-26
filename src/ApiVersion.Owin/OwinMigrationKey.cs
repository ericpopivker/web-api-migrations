using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApiVersion.Sample.OwinMigrations
{
    public class OwinMigrationKey : IEquatable<OwinMigrationKey>
    {
        public Uri Uri { get; set; }
        public DataDirection Direction { get; set; }
        public string Method { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Uri != null ? Uri.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) Direction;
                hashCode = (hashCode*397) ^ (Method != null ? Method.GetHashCode() : 0);
                return hashCode;
            }
        }

        bool IEquatable<OwinMigrationKey>.Equals(OwinMigrationKey other)
        {
            return Equals(Uri, other.Uri) && Direction == other.Direction && string.Equals(Method, other.Method);
        }
    }
}
