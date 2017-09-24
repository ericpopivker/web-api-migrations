using CleanBreak.WebApi.Changes;
using CleanBreak.WebApi.Core;

namespace CleanBreak.WebApi.ChangeTargets
{
	public interface IChangeTarget
	{
		bool IsMap(ApiRequest request, TargetContext context);
		bool IsMap(ApiResponse response, TargetContext context);
	}
}