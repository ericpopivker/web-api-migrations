using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;

namespace WebApiSample
{
	public class MyFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{

			var message = new HttpRequestMessage(HttpMethod.Post, HttpContext.Current.Request.Url);
			var routeData = GlobalConfiguration.Configuration.Routes.GetRouteData(message);

			message.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
			var controller = new DefaultHttpControllerSelector(GlobalConfiguration.Configuration).SelectController(message);

			var controllerContext = new HttpControllerContext(GlobalConfiguration.Configuration, routeData, message);
			controllerContext.ControllerDescriptor = new HttpControllerDescriptor(GlobalConfiguration.Configuration,
				controller.ControllerName, controller.ControllerType);
			var result = GlobalConfiguration.Configuration.Services.GetActionSelector().SelectAction(controllerContext);

		}

		//public bool AllowMultiple { get; }
		//public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		//{
		//	actionContext.Request.Content = new HttpMessageContent(new HttpRequestMessage());
		//	return continuation();
		//	//throw new NotImplementedException();
		//}
		
	}
}