using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PSI.Models;
using PSI.Process;

namespace PSI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult basicSetup()
        {
            return View();
        }

        [HttpPost]
        public JObject getBasicInfo()
        {
            BasicParam basic = AccountManage.getBasicParam();
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("data",
                    new JObject(
                        new JProperty("server", basic.server),
                        new JProperty("defaulBase", basic.defaulBase),
                        new JProperty("Uid", basic.Uid),
                        new JProperty("Pwd", basic.Pwd),
                        new JProperty("AccountDir", basic.AccountDir),
                        new JProperty("BackupDir", basic.BackupDir))));
            return json;
        }
        [HttpPost]
        public JObject updateBasicInfo(BasicParam basic)
        {
            AccountManage.setBasicParam(basic);
            AccountManage.creatAccount("test");
            JObject json = new JObject(
                new JProperty("code", 0),
                new JProperty("msg", "修改成功"));
            return json;
        }
    }
}