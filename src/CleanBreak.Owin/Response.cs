using System;

namespace CleanBreak.Owin
{
	public class Response
	{
		public Uri RequestUri { get; set; }
		public string RequestMethod { get; set; }	

		public BodyContent Body
		{
			get; set;
		}
	}
}
