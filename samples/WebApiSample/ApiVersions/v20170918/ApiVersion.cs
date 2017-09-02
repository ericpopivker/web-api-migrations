using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CleanBreak.Helpers.WebApi.Contract;
using WebApiSample.Dtos;

namespace WebApiSample.ApiVersions.v20170918
{
    public class ApiVersion : IApiVersion
    {
	   public IChange[] Changes => new IChange[]
	   {
		   new RenameFieldTypeApiChange()
		   {
			   Name = "Change field name of the Order from OrderId to Id",
			   Description = "",
			   Target = new ApiChangeTarget
			   {
				   ClassType = typeof (OrderDto),
			   },
			   OldFieldName = "OrderId",
				 NewFieldName = "Id"
		   }
	   };
    }
}