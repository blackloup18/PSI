using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSI.Models;
using PSI.Process;
using Newtonsoft.Json.Linq;
using PSI.Tools;

namespace PSI.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult PurchaseIndex()
        {
            return View();
        }
        public ActionResult PurchaseBill()  //采购单
        {
            return View();
        }
        public ActionResult InStockIndex()
        {
            return View();
        }
        public ActionResult InStockBill()  //入库单
        {
            return View();
        }
        public JObject GetClientList(string param)
        {
            string acc = "AC003";
            ClientCtrl clientCtrl = new ClientCtrl(acc);
            var query = clientCtrl.GetList().Where(x => x.ClientType == "C").AsQueryable();
            if (!string.IsNullOrWhiteSpace(param))
            {
                query = query.Where(x => x.ClientName.Contains(param) || x.ClientCode.Contains(param)).AsQueryable();
            }
            var list = query.ToList();
            JObject json = new JObject(
                new JProperty("code", clientCtrl.Code),
                new JProperty("msg", clientCtrl.Msg),
                new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("ClientCode", r.ClientCode),
                            new JProperty("ClientName", r.ClientName),
                            new JProperty("Phone", r.Phone),
                            new JProperty("Address", r.Address),
                            new JProperty("SID", r.SID)))));
            return json;                
        }   
        [HttpPost]
        public JArray WareHouseList()
        {
            string acc = "AC003";
            WareHouseCtrl ware = new WareHouseCtrl(acc);
            var list = ware.GetList();
            JArray json = new JArray(
                from r in list
                select new JObject(
                    new JProperty("value", r.ID),
                    new JProperty("text", r.HouseName)));
            return json;
        }
        public ActionResult ProductSelect()
        {
            return View();
        }
        public JObject AddBill_RK(string str)
        {
            try
            {
                InStockBillViewModel model = JsonTools.ParseFormJson<InStockBillViewModel>(str);
                MainBill main = new MainBill();
                main.Address = model.MainBill.Address;
                main.BillDate = model.MainBill.BillDate;
                main.ClientName = model.MainBill.ClientName;
                main.Phone = model.MainBill.Phone;
            }
        }
    }
}