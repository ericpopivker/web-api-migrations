using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CleanBreak.Integration.Owin
{
	interface ICache
	{
		object this[string key] { get; set; }
		void Set<TValue>(string key, TValue value);
		TValue Get<TValue>(string key);
		bool TryGet<TValue>(string key, out TValue value);
	}
}
