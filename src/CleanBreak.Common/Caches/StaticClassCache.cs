using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CleanBreak.Common.Caches
{
	public class StaticClassCache : ICache
	{
		private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

		public object this[string key]
		{
			get { return Get<object>(key); }
			set { Set(key, value); }
		}

		public void Set<TValue>(string key, TValue value)
		{
			_cache[key] = value;
		}

		public TValue Get<TValue>(string key)
		{
			object cachedValue;
			if (_cache.TryGetValue(key, out cachedValue))
			{
				return (TValue) cachedValue;
			}
			return default(TValue);
		}

		public bool TryGet<TValue>(string key, out TValue value)
		{					
			object cachedValue;
			if (_cache.TryGetValue(key, out cachedValue))
			{
				value = (TValue) cachedValue;
				return true;
			}
			value = default(TValue);
			return false;
		}
	}
}
