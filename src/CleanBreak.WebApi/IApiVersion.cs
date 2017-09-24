using CleanBreak.WebApi.Changes;

namespace CleanBreak.WebApi
{
	public interface IApiVersion
	{
		IChange[] Changes { get; }
	}
}