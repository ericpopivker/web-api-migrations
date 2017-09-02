namespace CleanBreak.Helpers.WebApi.Contract
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