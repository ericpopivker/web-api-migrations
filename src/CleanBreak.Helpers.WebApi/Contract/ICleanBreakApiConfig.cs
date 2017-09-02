using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CleanBreak.Helpers.WebApi.Contract
{
    public interface ICleanBreakApiConfig
    {
	   IApiVersion[] Versions { get; }
    }
}
