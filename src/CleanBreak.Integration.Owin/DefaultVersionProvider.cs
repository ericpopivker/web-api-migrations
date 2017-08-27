using System;
using Microsoft.Owin;

namespace CleanBreak.Integration.Owin
{
    public class DefaultVersionProvider : IVersionProvider
    {
        public IComparable GetVersion(IOwinContext context)
        {
            return context.Request.Headers["version"];
        }
    }
}
