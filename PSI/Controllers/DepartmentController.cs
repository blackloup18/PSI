using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSI.Process;
using PSI.Models;
using Newtonsoft.Json.Linq;

namespace PSI.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddIndex()
        {
            return View();
        }
        public ActionResult EditIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(DepartViewModel model)
        {
            string acc = "AC003";
            DepartmentCtrl departmentCtrl = new DepartmentCtrl(acc);
            departmentCtrl.Department = new Department();
            departmentCtrl.Department.DepartName = model.departName;
            departmentCtrl.Department.Creator = "admin";
            departmentCtrl.Department.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");
            departmentCtrl.Save();
            var resulst = new
            {
                departmentCtrl.Code,
                departmentCtrl.Msg
            };
            return Json(resulst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditDepartment(DepartViewModel model)
        {
            string acc = "AC003";
            DepartmentCtrl departmentCtrl = new DepartmentCtrl(acc, model.SID);
            departmentCtrl.Department.DepartName = model.departName;
            departmentCtrl.Modifer();
            var resulst = new
            {
                departmentCtrl.Code,
                departmentCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DelDepartment(string SID)
        {
            string acc = "AC003";
            DepartmentCtrl departmentCtrl = new DepartmentCtrl(acc, SID);
            departmentCtrl.Delete();
            var resulst = new
            {
                departmentCtrl.Code,
                departmentCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JObject DepartmentList(int page, int limit)
        {
            string acc = "AC003";
            DepartmentCtrl departmentCtrl = new DepartmentCtrl(acc);
            var list = departmentCtrl.GetList();
            var list1 = list.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", departmentCtrl.Msg),
                new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                       from r in list1
                       select new JObject(
                           new JProperty("CreatDate", r.CreatDate),
                           new JProperty("Creator", r.Creator),
                           new JProperty("DepartName", r.DepartName),
                           new JProperty("SID", r.SID)))));
            return json;
        }
        [HttpGet]
        public ActionResult  OptList()
        {
            string acc = "AC003";
            DepartmentCtrl departmentCtrl = new DepartmentCtrl(acc);
            var list = departmentCtrl.GetList().Select(x => new { x.SID, x.DepartName }).ToList();
            var resulst = new
            {
                data = list
            };
            return Json(list, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}