namespace CleanBreak.Common.Caches
{
	public interface ICache
	{
		object this[string key] { get; set; }
		void Set<TValue>(string key, TValue value);
		TValue Get<TValue>(string key);
		bool TryGet<TValue>(string key, out TValue value);
	}
}
