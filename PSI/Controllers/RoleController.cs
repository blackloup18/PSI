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
    public class RoleController : Controller
    {
       

        public ActionResult RoleIndex()
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

        public ActionResult AuthorizeIndex()
        {
            return View();
        }
        [HttpPost]
        public string getPY(string name)
        {
            return CHPYUtil.GetShortPinyin(name);
        }
        [HttpPost]
        public ActionResult creatRole(string accountName,string databasePath,string remark)
        {
            accCtrl acc = new accCtrl();
            acc.account = new Account();
            acc.account.accountName = accountName;
            acc.account.databasePath = databasePath;
            acc.account.remark = remark;
            acc.creatNew();
            var resulst = new
            {
                acc.code,
               acc.result
            };
            return Json(resulst, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JObject getList(string accName,int page,int limit)
        {
           
            List<Account> list = AccountManage.getAccountList(accName);
            var list1 = list.Skip((page-1)*limit).Take(limit).ToList();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", ""),
                new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list1
                        select new JObject(
                            new JProperty("accountID", r.accountID),
                            new JProperty("accountName", r.accountName),
                            new JProperty("creatDate", r.creatDate),
                            new JProperty("databaseName", r.databaseName),
                            new JProperty("databasePath", r.databasePath),
                            new JProperty("ID", r.ID),
                            new JProperty("remark", r.remark),
                            new JProperty("status", r.status)))));
            return json;
        }
        /// <summary>
        /// 修改账套信息
        /// </summary>
        /// <param name="accountID">账套编号</param>
        /// <param name="accountName">账套名称</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult editRole(string accountID,string accountName,string remark)
        {
            accCtrl acc = new accCtrl(accountID);
            acc.account.accountName = accountName;
            acc.account.remark = remark;
            acc.modify();
            var json = new
            {
                acc.code,
                acc.result
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除账套
        /// </summary>
        /// <param name="accountID">账套编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult delRole(string accountID)
        {
            accCtrl acc = new accCtrl(accountID);
            acc.delAcc();
            var json = new
            {
                acc.code,
                acc.result
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 改变状态状态
        /// </summary>
        /// <param name="accountID">账套编号</param>
        /// <param name="status">0=启用 1-禁用</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult changeStatus(string accountID,string status)
        {
            accCtrl acc = new accCtrl(accountID);
            acc.changeStauts(status);
            var json = new
            {
                acc.code,
                acc.result
            };
            return Json(json, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}