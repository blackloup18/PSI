using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult MainIndex()
        {
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }
    }
}