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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProduct()
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
        public ActionResult AddUnitIndex()
        {
            return View();
        }
        public ActionResult EditUnitIndex()
        {
            return View();
        }
        public ActionResult AddUnitIndex1()
        {
            return View();
        }
        public ActionResult EditUnitIndex1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            string acc = "AC003";
            ProductCtrl productCtrl = new ProductCtrl(acc);
            Products pro = new Products();
            pro.AssistCode = model.AssistCode;
            pro.BarCode = model.BarCode;
            pro.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");
            pro.Creator = "admin";
            pro.FactryName = model.FactryName;
            pro.PinyinCode = model.PinyinCode;
            pro.Model = model.Model;
            pro.ClassID = model.ClassID;
            pro.ProName = model.ProName;
            pro.Remark = model.Remark;
            pro.SID = CommonTools.GetNewID("SP",acc);
            pro.Specs = model.Specs;
            pro.Status = 0;
            pro.Colour = model.Colour;
            pro.Place = model.Place;
            pro.Size = model.Size;
            if (!string.IsNullOrWhiteSpace(model.OnlyUnit))
            {
                pro.OnlyUnit = int.Parse(model.OnlyUnit);
            }
            else
            {
                pro.OnlyUnit = 1;
            }
            pro.Spare1 = model.Spare1;
            pro.Spare2 = model.Spare2;
            pro.Spare3 = model.Spare3;
            pro.Spare4 = model.Spare4;
            pro.Spare5 = model.Spare5;
            pro.DIY1 = model.DIY1;
            pro.DIY2 = model.DIY2;
            pro.DIY3 = model.DIY3;
            pro.DIY4 = model.DIY4;
            pro.DIY5 = model.DIY5;
            pro.Year = model.Year;
            pro.MainUnit = model.MainUnit;
            pro.PurchaseUnit = model.PurchaseUnit;
            pro.PurchaseRate =CommonTools.ToDecimal(model.PurchaseRate);
            pro.SaleUnit = model.SaleUnit;
            pro.SaleRate = CommonTools.ToDecimal(model.SaleRate);
            pro.InventoryUnit = model.InventoryUnit;
            pro.InventoryRate = CommonTools.ToDecimal(model.InventoryRate);
            if (pro.OnlyUnit == 0)
            {
                pro.IsFloat = 0;
                pro.AssistRate = 0;
                pro.AssistUnit = "";
            }
            else
            {
                pro.IsFloat = CommonTools.ToInt(model.IsFloat);
                pro.AssistUnit = model.AssistUnit;
                pro.AssistRate = CommonTools.ToDecimal(model.AssistRate);
            }
            productCtrl.Product = pro;
            productCtrl.Save();
            var resulst = new
            {
                productCtrl.Code,
                productCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            string acc = "AC003";
            ProductCtrl productCtrl = new ProductCtrl(acc,model.SID);
            productCtrl.Product.AssistCode = model.AssistCode;
            productCtrl.Product.BarCode = model.BarCode;
            productCtrl.Product.FactryName = model.FactryName;
            productCtrl.Product.Model = model.Model;
            productCtrl.Product.ProName = model.ProName;
            productCtrl.Product.Remark = model.Remark;
            productCtrl.Product.Specs = model.Specs;
            productCtrl.Product.ClassID = model.ClassID;
            productCtrl.Product.PinyinCode = model.PinyinCode;
            productCtrl.Product.OnlyUnit = int.Parse(model.OnlyUnit);
            productCtrl.Product.Spare1 = model.Spare1;
            productCtrl.Product.Spare2 = model.Spare2;
            productCtrl.Product.Spare3 = model.Spare3;
            productCtrl.Product.Spare4 = model.Spare4;
            productCtrl.Product.Spare5 = model.Spare5;
            productCtrl.Product.DIY1 = model.DIY1;
            productCtrl.Product.DIY2 = model.DIY2;
            productCtrl.Product.DIY3 = model.DIY3;
            productCtrl.Product.DIY4 = model.DIY4;
            productCtrl.Product.DIY5 = model.DIY5;
            productCtrl.Product.Year = model.Year;
            productCtrl.Product.PurchaseUnit = model.PurchaseUnit;
            productCtrl.Product.PurchaseRate = CommonTools.ToDecimal(model.PurchaseRate);
            productCtrl.Product.SaleUnit = model.SaleUnit;
            productCtrl.Product.SaleRate = CommonTools.ToDecimal(model.SaleRate);
            productCtrl.Product.InventoryUnit = model.InventoryUnit;
            productCtrl.Product.InventoryRate = CommonTools.ToDecimal(model.InventoryRate);
            productCtrl.Edit();
            var resulst = new
            {
                productCtrl.Code,
                productCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DelProduct(string SID)
        {
            string acc = "AC003";
            ProductCtrl productCtrl = new ProductCtrl(acc, SID);
            //删除前先判断商品是否有使用
            productCtrl.Delete();
            var resulst = new
            {
                productCtrl.Code,
                productCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JObject GetProductList(QueryProduct query, int page, int limit)
        {
            string acc = "AC003";
            ProductCtrl productCtrl = new ProductCtrl(acc);
            var que = productCtrl.GetList().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.ProID))
            {
                que = que.Where(x => x.SID.Contains(query.ProID));
            }
            if (!string.IsNullOrWhiteSpace(query.ProName))
            {
                que = que.Where(x => x.ProName.Contains(query.ProName));
            }
            if (!string.IsNullOrWhiteSpace(query.Models))
            {
                que = que.Where(x => x.Model.Contains(query.Models));
            }
            if (!string.IsNullOrWhiteSpace(query.Specs))
            {
                que = que.Where(x => x.Specs.Contains(query.Specs));
            }
            var list = que.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
               new JProperty("code", 0),
                new JProperty("msg", ""),
                new JProperty("count", que.ToList().Count),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("AssistCode", r.AssistCode),
                            new JProperty("BarCode", r.BarCode),
                            new JProperty("CreatDate", r.CreatDate),
                            new JProperty("Creator", r.Creator),
                            new JProperty("FactryName", r.FactryName),
                            new JProperty("ID", r.ID),
                            new JProperty("Model", r.Model),
                             new JProperty("PinyinCode", r.PinyinCode),
                            new JProperty("OnlyUnit", r.OnlyUnit),
                            new JProperty("ClassName", r.ClassName),
                            new JProperty("ClassID", r.ClassID),
                            new JProperty("MainUnitName", r.MainUnitName),
                            new JProperty("AssistUnitName", r.AssistUnitName),
                            new JProperty("AssistRate", r.AssistRate),
                            new JProperty("IsFloat", r.IsFloat),
                            new JProperty("ProName", r.ProName),
                            new JProperty("Remark", r.Remark),
                            new JProperty("SID", r.SID),
                            new JProperty("Specs", r.Specs),
                            new JProperty("Status", r.Status)))));
            return json;
        }
        //[HttpGet]
        //public JObject GetUnitList(string ProductID, int page, int limit)
        //{
        //    string acc = "AC003";
        //    ProductCtrl pro = new ProductCtrl(acc, ProductID);
        //    var list1 = pro.Units.Skip((page - 1) * limit).Take(limit).ToList();
        //    JObject json = new JObject(
        //        new JProperty("code", 0),
        //        new JProperty("msg", pro.Msg),
        //         new JProperty("count", pro.Units.Count),
        //        new JProperty("data",
        //            new JArray(
        //                from r in list1
        //                select new JObject(
        //                    new JProperty("ID", r.ID),
        //                    new JProperty("UnitID", r.UnitID),
        //                    new JProperty("Rate", r.Rate),
        //                     new JProperty("IsMaster", r.IsMaster),
        //                      new JProperty("Remark", r.Remark),
        //                    new JProperty("CreatDate", r.CreatDate),
        //                    new JProperty("Creator", r.Creator)
        //        ))));
        //    return json;
        //    //var resulst = new
        //    //{
        //    //    pro.Units
        //    //};
        //    //return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);

        //}
    }
}