using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PSI.Process;
using Newtonsoft.Json.Linq;

namespace PSI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginIndex()
        {
            return View();
        }
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pass">密码</param>
        /// <param name="accName">账套</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult checkLogin(string loginName,string pass,string accName)
        {
            if (loginManage.checkLogin(loginName, pass,accName))
            {
                var json = new
                {
                    okMsg = "登录成功！"
                };
                return Json(json, "text/html", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var json = new
                {
                    errorMsg = "密码错误！"
                };
                return Json(json, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JArray accList()
        {
            var list = AccountManage.getAccountList("");
            list = list.Where(x => x.status == 0).ToList();
            JArray json = new JArray(
                from r in list
                select new JObject(
                    new JProperty("ID", r.databaseName),
                    new JProperty("name", r.accountName)));
            return json;
        }


    }
}