using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSI.Process;
using PSI.Models;
using System.Data;
using Newtonsoft.Json.Linq;


namespace PSI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JObject GetList(QueryInventory model, int page, int limit)
        {
            string acc = "AC003";
            DataTable dt = InventoryManage.GetInventory(acc);
            List<InventoryViewModel> ilist = CommonTools.TableToList<InventoryViewModel>(dt);
            int total = ilist.Count;
            var list = ilist.AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.AssistCode))
            {
                list = list.Where(x => x.AssistCode.Contains(model.AssistCode));
            }
            if (!string.IsNullOrWhiteSpace(model.HouseID))
            {
                list = list.Where(x => x.HouseID.Equals(model.HouseID));
            }
            if (!string.IsNullOrWhiteSpace(model.Model))
            {
                list = list.Where(x => x.Model.Contains(model.Model));
            }
            if (!string.IsNullOrWhiteSpace(model.ProName))
            {
                list = list.Where(x => x.ProName.Contains(model.ProName));
            }
            if (!string.IsNullOrWhiteSpace(model.Spec))
            {
                list = list.Where(x => x.Specs.Contains(model.Spec));
            }
            ilist = list.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", ""),
                new JProperty("count", total),
                new JProperty("data",
                    new JArray(
                        from r in ilist
                        select new JObject(
                            new JProperty("AssistCode", r.AssistCode),
                            new JProperty("AssistNumber", r.AssistNumber),
                            new JProperty("AssistUnit", r.AssistUnit),
                            new JProperty("BarCode", r.BarCode),
                            new JProperty("Colour", r.Colour),
                            new JProperty("FactryName", r.FactryName),
                            new JProperty("HouseID", r.HouseID),
                            new JProperty("HouseName", r.HouseName),
                            new JProperty("ID", r.ID),
                            new JProperty("InventoryNumber", r.InventoryNumber),
                            new JProperty("InventoryUnit", r.InventoryUnit),
                            new JProperty("IsFloat", r.IsFloat),
                            new JProperty("MainNumber", r.MainNumber),
                            new JProperty("MainUnit", r.MainUnitID),
                            new JProperty("MainUnitName", r.MainUnit),
                            new JProperty("Model", r.Model),
                            new JProperty("OnlyUnit", r.OnlyUnit),
                            new JProperty("Place", r.Place),
                            new JProperty("ProID", r.ProID),
                            new JProperty("ProName", r.ProName),
                            new JProperty("Size", r.Size),
                            new JProperty("Spare1", r.Spare1),
                            new JProperty("Spare2", r.Spare2),
                            new JProperty("Spare3", r.Spare3),
                            new JProperty("Spare5", r.Spare5),
                            new JProperty("Spare4", r.Spare4),
                            new JProperty("Specs", r.Specs),
                            new JProperty("Year", r.Year)))));
            return json;
        }
    }
}