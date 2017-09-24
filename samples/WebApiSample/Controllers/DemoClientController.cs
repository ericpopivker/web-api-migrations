using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiSample.Controllers
{
    public class DemoClientController : Controller
    {
        // GET: DemoClient
        public ActionResult Index()
        {
            return View();
        }
    }
}