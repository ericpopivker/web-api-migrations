using System.Dynamic;
using System.Web.Http;

namespace CleanBreak.Helpers.WebApi.Contract
{
	public class ApiChangeBase : IChange
	{
		internal HttpConfiguration HttpConfiguration { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public IChangeTarget Target { get; set; }

		public virtual void UpgradeRequest(ApiRequest request)
		{
			
		}

		public virtual void DowngradeResponse(ApiResponse response)
		{

		}
	}
}