using System;
using Microsoft.Owin;

namespace CleanBreak.Integration.Owin
{
    public interface IVersionProvider
    {
        IComparable GetVersion(IOwinContext context);
    }
}
