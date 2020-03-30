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
    public class ClientController : Controller
    {
        // GET: Client
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
        public ActionResult AddClient(ClientViewModel model)
        {
            string acc = "AC003";
            ClientCtrl clientCtrl = new ClientCtrl(acc);
            clientCtrl.Client = new Client();
            clientCtrl.Client.Account = model.Account;
            clientCtrl.Client.Address = model.Address;
            clientCtrl.Client.Bank = model.Bank;
            clientCtrl.Client.ClientCode = model.ClientCode;
            clientCtrl.Client.ClientName = model.ClientName;
            clientCtrl.Client.ClientType = model.ClientType;
            clientCtrl.Client.Contact = model.Contact;
            clientCtrl.Client.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");    
            clientCtrl.Client.Creator = "admin";
            clientCtrl.Client.Email = model.Email;
            clientCtrl.Client.Fax = model.Fax;
            clientCtrl.Client.Phone = model.Phone;
            clientCtrl.Client.QQ = model.QQ;
            clientCtrl.Client.Status = 0;
            clientCtrl.Client.WeChat = model.WeChat;
            clientCtrl.Client.Remark = model.Remark;
            clientCtrl.Save();
            var resulst = new
            {
                clientCtrl.Code,
                clientCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditClient(ClientViewModel model)
        {
            string acc = "AC003";
            ClientCtrl clientCtrl = new ClientCtrl(acc,model.SID);
            clientCtrl.Client.Account = model.Account;
            clientCtrl.Client.Address = model.Address;
            clientCtrl.Client.Bank = model.Bank;
            clientCtrl.Client.ClientCode = model.ClientCode;
            clientCtrl.Client.ClientName = model.ClientName;
            clientCtrl.Client.ClientType = model.ClientType;
            clientCtrl.Client.Contact = model.Contact;
            clientCtrl.Client.Email = model.Email;
            clientCtrl.Client.Fax = model.Fax;
            clientCtrl.Client.Phone = model.Phone;
            clientCtrl.Client.QQ = model.QQ;
            clientCtrl.Client.WeChat = model.WeChat;
            clientCtrl.Client.Remark = model.Remark;
            clientCtrl.Edit();
            var resulst = new
            {
                clientCtrl.Code,
                clientCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangeStatus(string sID,string status)
        {
            string acc = "AC003";
            ClientCtrl clientCtrl = new ClientCtrl(acc,sID);
            clientCtrl.ChangeStatus(status);
            var resulst = new
            {
                clientCtrl.Code,
                clientCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JObject GetList(ClientViewModel model, int page, int limit)
        {
            string acc = "AC003";
            ClientCtrl clientCtrl = new ClientCtrl(acc);
            var query = clientCtrl.GetList().AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.ClientType))
            {
                query = query.Where(x => x.ClientCode.Equals(model.ClientType));
            }
            if (!string.IsNullOrWhiteSpace(model.ClientCode))
            {
                query = query.Where(x => x.ClientCode.Contains(model.ClientCode));
            }
            if (!string.IsNullOrWhiteSpace(model.ClientName))
            {
                query = query.Where(x => x.ClientCode.Contains(model.ClientName));
            }
            var list = query.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
               new JProperty("code", 0),
                new JProperty("msg", ""),
                new JProperty("count", query.ToList().Count),
                new JProperty("data",
                    new JArray(
                        from r in list
                        select new JObject(
                            new JProperty("Account", r.Account),
                            new JProperty("Address", r.Address),
                            new JProperty("Bank", r.Bank),
                            new JProperty("ClientCode", r.ClientCode),
                            new JProperty("ClientName", r.ClientName),
                            new JProperty("ClientType", r.ClientType),
                            new JProperty("Contact", r.Contact),
                            new JProperty("CreatDate", r.CreatDate),
                            new JProperty("Creator", r.Creator),
                            new JProperty("Email", r.Email),
                            new JProperty("Fax", r.Fax),
                            new JProperty("ID", r.ID),
                            new JProperty("Phone", r.Phone),
                            new JProperty("QQ", r.QQ),
                            new JProperty("Remark", r.Remark),
                            new JProperty("SID", r.SID),
                            new JProperty("Status", r.Status),
                            new JProperty("WeChat", r.WeChat)))));
            return json;
        }
    }
}