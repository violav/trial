using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Due()
        {
            return View();
        }

        public ActionResult altro()
        {
            return View("Due");
        }
    }
}