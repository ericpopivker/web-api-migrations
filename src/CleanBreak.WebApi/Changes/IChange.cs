using CleanBreak.WebApi.ChangeTargets;

namespace CleanBreak.WebApi.Changes
{
	public interface IChange
	{
		string Name { get; set; }
		string Description { get; set; }
		IChangeTarget Target { get; set; }
		void UpgradeRequest(ApiRequest request);
		void DowngradeResponse(ApiResponse response);
	}
}