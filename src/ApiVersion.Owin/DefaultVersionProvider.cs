using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace ApiVersion.Owin
{
    public class DefaultVersionProvider : IVersionProvider
    {
        public IComparable GetVersion(IOwinContext context)
        {
            return context.Request.Headers["version"];
        }
    }
}
