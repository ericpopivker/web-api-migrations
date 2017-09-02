using CleanBreak.Helpers.WebApi.Impl;

namespace CleanBreak.Helpers.WebApi.Contract
{
	public interface IChangeTarget
	{
		bool IsMap(ApiRequest request, TargetContext context);
		bool IsMap(ApiResponse response, TargetContext context);
	}
}