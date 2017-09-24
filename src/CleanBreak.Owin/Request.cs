using System;

namespace CleanBreak.Owin
{
	public class Request
	{
		public Uri Uri { get; set; }
		public string Method { get; set; }

		public BodyContent Body
		{
			get; set;
		}
	}
}
