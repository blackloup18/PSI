using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSI.Controllers
{
    public class AdminMainController : Controller
    {
        // GET: AdminMain
        public ActionResult AdminMainIndex()
        {
            return View();
        }

        public ActionResult Default()
        {
            return View();
        }
    }
}