using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace CleanBreak.Owin.Core
{
	public interface IRequestFilter
	{
		bool Filter(IOwinRequest request);
	}
}
