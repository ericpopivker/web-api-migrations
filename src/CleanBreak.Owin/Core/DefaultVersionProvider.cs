using System;
using Microsoft.Owin;

namespace CleanBreak.Owin.Core
{
    public class DefaultVersionProvider : IVersionProvider
    {
        public IComparable GetVersion(IOwinContext context)
        {
            return context.Request.Headers["version"];
        }
    }
}
