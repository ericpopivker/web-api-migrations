using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CleanBreak.WebApi;

namespace WebApiSample.ApiVersions
{
    public class CleanBreakApiConfig : ICleanBreakApiConfig
    {
	    public IApiVersion[] Versions => new[]
	    {
		    new v20170918.ApiVersion()
	    };
    }
}