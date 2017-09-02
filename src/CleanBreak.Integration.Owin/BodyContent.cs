using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CleanBreak.Integration.Owin
{
	public class BodyContent
	{
		private JToken _body;

		public BodyContent(JToken body)
		{
			_body = body;
		}


		public bool IsArray => _body is JArray;

		public JToken Token => _body;

		public JObject Object => (JObject) _body;

		public JArray Array => (JArray) _body;

		public override string ToString()
		{
			return _body?.ToString() ?? string.Empty;
		}
	}
}
