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
    public class ProductClassController : Controller
    {
        // GET: ProductClass
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JArray ProductClassList()
        {
            string acc = "AC003";
            ClassCtrl classCtrl = new ClassCtrl(acc);
            JArray json = new JArray(
                from r in classCtrl.ParentClassList
                select new JObject(
                    new JProperty("id", r.ID),
                    new JProperty("field", r.SID),
                    new JProperty("title", r.ClassName),
                    new JProperty("spread", true),
                    new JProperty("children", ChildClassList(r.SID))));
            return json;                        
        }
        public JArray ChildClassList(string ParentID)
        {
            string acc = "AC003";
            ClassCtrl classCtrl = new ClassCtrl(acc);
            if (!classCtrl.HasChild(ParentID))
            {
                JArray json = new JArray();
                return json;
            }
            else
            {
                var rlist = classCtrl.ChildClassList(ParentID);
                JArray jArray = new JArray(
                    from r in rlist
                    select new JObject(
                        new JProperty("id", r.ID),
                        new JProperty("field", r.SID),
                        new JProperty("title", r.ClassName),
                        new JProperty("spread", true),
                        new JProperty("children", ChildClassList(r.SID))));
                return jArray;                        
            }
        }
        public ActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrUpdateClass(ProductClassViewModel model)
        {
            string acc = "AC003";
            ClassCtrl classCtrl = null;
            if (!string.IsNullOrWhiteSpace(model.SID))
            {
                classCtrl = new ClassCtrl(acc, model.SID);
                classCtrl.ProductClass.ClassName = model.ClassName;
                classCtrl.ProductClass.ParentID = model.ParentID;
                classCtrl.Edit();
                var json = new
                {
                    classCtrl.Code,
                    classCtrl.Msg
                };
                return Json(json, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                classCtrl = new ClassCtrl(acc);
                classCtrl.ProductClass = new ProductClass();
                classCtrl.ProductClass.SID = CommonTools.GetNewID("LB", acc);
                classCtrl.ProductClass.ClassName = model.ClassName;
                classCtrl.ProductClass.ParentID = model.ParentID;
                classCtrl.Save();
                var json = new
                {
                    classCtrl.Code,
                    classCtrl.Msg
                };
                return Json(json, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JObject GetClassList()
        {
            string acc = "AC003";
            ClassCtrl classCtrl = new ClassCtrl(acc);
            var list = classCtrl.AllClassList;
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("value", r.SID),
                            new JProperty("text", r.ClassName)))));
            return json;
        }
        public ActionResult UpdateClass(string SID)
        {
            string acc = "AC003";
            ClassCtrl classCtrl = new ClassCtrl(acc, SID);
            ViewBag.SID = SID;
            ViewBag.ClassName = classCtrl.ProductClass.ClassName;
            ViewBag.ParentID = classCtrl.ProductClass.ParentID;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteClass(string SID)
        {
            string acc = "AC003";
            ClassCtrl classCtrl = new ClassCtrl(acc);
            classCtrl.Delete(SID);
            var json = new
            {
                classCtrl.Code,
                classCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }

    }
}