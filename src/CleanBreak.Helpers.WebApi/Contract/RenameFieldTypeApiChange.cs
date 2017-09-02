using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Helpers.WebApi.Contract
{
	public class RenameFieldTypeApiChange : ApiChangeBase
	{
		public string OldFieldName { get; set; }
		public string NewFieldName { get; set; }

		public override void UpgradeRequest(ApiRequest request)
		{
			//var requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(request.Method.Method, request.Url, HttpConfiguration);				  
			updateFieldName(request.Body.Object, OldFieldName, NewFieldName);
		}

		public override void DowngradeResponse(ApiResponse response)
		{
			//var requestHandler = WebApiRequestHandlerFinder.GetRequestHandler(response.RequestMethod.Method, response.RequestUrl,
			//	HttpConfiguration);

			if (response.Body.IsArray)
			{
				foreach (var item in response.Body.Array)
				{
					updateFieldName(item, NewFieldName, OldFieldName);
				}
			}
			else
			{
				updateFieldName(response.Body.Object, NewFieldName, OldFieldName);
			}
		}

		private void updateFieldName(JToken token, string oldName, string newName)
		{
			var fieldValue = token[oldName];
			token[newName] = fieldValue.DeepClone();
			token[oldName].Parent.Remove();
		}
	}
}