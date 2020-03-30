using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSI.Controllers.Admin
{
    public class BaseInfoController : Controller
    {
        // GET: BaseInfo
        public ActionResult Index()
        {
            return View();
        }
    }
}