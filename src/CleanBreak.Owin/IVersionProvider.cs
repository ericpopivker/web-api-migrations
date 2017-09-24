using System;
using Microsoft.Owin;

namespace CleanBreak.Owin
{
    public interface IVersionProvider
    {
        IComparable GetVersion(IOwinContext context);
    }
}
