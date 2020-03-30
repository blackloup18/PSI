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
    public class WarehouseController : Controller
    {
        // GET: Warehouse
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
        public ActionResult AddHouse(WareViewModel model)
        {
            string acc = "AC003";
            WareHouseCtrl wareHouseCtrl = new WareHouseCtrl(acc);
            wareHouseCtrl.Warehouses = new Warehouses();
            wareHouseCtrl.Warehouses.Address = model.Address;
            wareHouseCtrl.Warehouses.HouseName = model.HouseName;
            wareHouseCtrl.Warehouses.HouseType = model.HouseType;
            wareHouseCtrl.Warehouses.Remark = model.Remark;
            wareHouseCtrl.Warehouses.Status = int.Parse(model.Status);
            wareHouseCtrl.Warehouses.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");
            wareHouseCtrl.Warehouses.Creator = "admin";
            wareHouseCtrl.Save();
            var json = new
            {
                wareHouseCtrl.Code,
                wareHouseCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditHouse(WareViewModel model)
        {
            string acc = "AC003";
            WareHouseCtrl wareHouseCtrl = new WareHouseCtrl(acc, model.ID);
            wareHouseCtrl.Warehouses.HouseName = model.HouseName;
            wareHouseCtrl.Warehouses.Address = model.Address;
            wareHouseCtrl.Warehouses.HouseType = model.HouseType;
            wareHouseCtrl.Warehouses.Remark = model.Remark;
            wareHouseCtrl.Edit();
            var json = new
            {
                wareHouseCtrl.Code,
                wareHouseCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeStatus(string ID,string status)
        {
            string acc = "AC003";
            WareHouseCtrl wareHouseCtrl = new WareHouseCtrl(acc, ID);
            wareHouseCtrl.Warehouses.Status = int.Parse(status);
            wareHouseCtrl.ChangeStatus();
            var json = new
            {
                wareHouseCtrl.Code,
                wareHouseCtrl.Msg
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        
        public JObject GetWarehouseList(int page, int limit)
        {
            string acc = "AC003";
            WareHouseCtrl wareHouseCtrl = new WareHouseCtrl(acc);
            var list = wareHouseCtrl.GetList();
            var list1 = list.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", wareHouseCtrl.Msg),
                 new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list1
                        select new JObject(
                            new JProperty("ID", r.ID),
                            new JProperty("Address", r.Address),
                            new JProperty("CreatDate", r.CreatDate),
                            new JProperty("Creator", r.Creator),
                            new JProperty("HouseName", r.HouseName),
                            new JProperty("HouseType", r.HouseType),
                            new JProperty("Remark", r.Remark),
                            new JProperty("Status", r.Status)))));
            return json;
        }  
        [HttpPost]
        public JObject WarehouseList()
        {
            string acc = "AC003";
            WareHouseCtrl wareHouseCtrl = new WareHouseCtrl(acc);
            var list = wareHouseCtrl.GetList();
            JObject json = new JObject(
                new JProperty("code", wareHouseCtrl.Code),
                new JProperty("msg", wareHouseCtrl.Msg),
                 new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("ID", r.ID),
                            new JProperty("HouseName", r.HouseName)))));
            return json;
        }
    }
}