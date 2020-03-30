using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSI.Models;
using PSI.Process;
using Newtonsoft.Json.Linq;

namespace PSI.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
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
        public ActionResult AddUnit(UnitViewModel model)
        {
            string acc = "AC003";
            UnitCtrl unitCtrl = new UnitCtrl(acc);
            unitCtrl.Unit = new Units();
            unitCtrl.Unit.SID = CommonTools.GetNewID("DW", acc);
            unitCtrl.Unit.UnitName = model.UnitName;
            unitCtrl.Unit.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");
            unitCtrl.Unit.Creator = "admin";
            unitCtrl.Save();
            var json = new
            {
                unitCtrl.Code,
                unitCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditUnit(UnitViewModel model)
        {
            string acc = "AC003";
            UnitCtrl unitCtrl = new UnitCtrl(acc,model.SID);
            unitCtrl.Unit.UnitName = model.UnitName;
            unitCtrl.Edit();
            var json = new
            {
                unitCtrl.Code,
                unitCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeStatus(string SID,string status)
        {
            string acc = "AC003";
            UnitCtrl unitCtrl = new UnitCtrl(acc, SID);
            unitCtrl.Unit.Status = int.Parse(status);
            unitCtrl.ChangeStatus();
            var json = new
            {
                unitCtrl.Code,
                unitCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
       
        public JObject GetList(int page, int limit)
        {
            string acc = "AC003";
            UnitCtrl unitCtrl = new UnitCtrl(acc);
            var list = unitCtrl.GetList();
            var list1 = list.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", unitCtrl.Msg),
                 new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list1
                        select new JObject(
                            new JProperty("SID", r.SID),
                            new JProperty("UnitName", r.UnitName),
                            new JProperty("CreatDate", r.CreatDate),
                            new JProperty("Creator", r.Creator)
                ))));
            return json;
        }
        [HttpPost]
        public JObject GetUnitList(string sid)
        {
            string acc = "AC003";
            UnitCtrl unitCtrl = new UnitCtrl(acc);
            var list=new List<Units>();
            if (!string.IsNullOrWhiteSpace(sid))
            {
                list = unitCtrl.GetList(sid);
            }
            else
            {
                list = unitCtrl.GetList();
            }
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", unitCtrl.Msg),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("ID", r.SID),
                            new JProperty("UnitName", r.UnitName)
                ))));
            return json;
        }
    }
}