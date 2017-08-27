using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CleanBreak.Integration.Owin
{
	class DefaultInMemoryCache : ICache, IDisposable
	{
		private readonly TimeSpan EXPIRATION_VALUE = TimeSpan.FromSeconds(60);
		private MemoryCache _cache = new MemoryCache("CleanBreak.Integration.Owin.Cache");

		public object this[string key]
		{
			get { return Get<object>(key); }
			set { Set(key, value); }
		}

		public void Set<TValue>(string key, TValue value)
		{
			_cache.Set(key, value, DateTimeOffset.UtcNow + EXPIRATION_VALUE);
		}

		public TValue Get<TValue>(string key)
		{
			CacheItem cacheItem = _cache.GetCacheItem(key);
			return cacheItem == null ? default(TValue) : (TValue)cacheItem.Value;
		}

		public bool TryGet<TValue>(string key, out TValue value)
		{					
			value = default(TValue);
			CacheItem cacheItem = _cache.GetCacheItem(key);
			if (cacheItem == null)
			{
				return false;
			}
			value = (TValue)cacheItem.Value;
			return true;
		}

		public void Dispose()
		{
			_cache.Dispose();
			_cache = null;
		}
	}
}
