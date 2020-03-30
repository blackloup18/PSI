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
    public class UserController : Controller
    {
        // GET: User
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
        public ActionResult AddUser(UserViewModel model)
        {
            string account = "AC003";  //账套名，登录时保存到session里
            UserInfoCtrl userInfoCtrl = new UserInfoCtrl(account);
            userInfoCtrl.UserInfo = new UserInfo();
            userInfoCtrl.UserInfo.LoginID = model.LoginID;
            userInfoCtrl.UserInfo.Password = model.Password;
            userInfoCtrl.UserInfo.Phone = model.Phone;
            userInfoCtrl.UserInfo.Status = 0;
            userInfoCtrl.UserInfo.UserName = model.UserName;
            userInfoCtrl.UserInfo.CreatDate = DateTime.Now.ToString("yyyy-MM-dd");
            userInfoCtrl.UserInfo.Creator = "admin"; //登录员工，登录时保存到session里
            userInfoCtrl.Save();
            
          
            var resulst = new
            {
                userInfoCtrl.Code,
                userInfoCtrl.Msg
            };
            return Json(resulst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            string account = "AC003";  //账套名，登录时保存到session里
            UserInfoCtrl userInfoCtrl = new UserInfoCtrl(account, model.SID);
            userInfoCtrl.UserInfo.LoginID = model.LoginID;
            userInfoCtrl.UserInfo.Password = model.Password;
            userInfoCtrl.UserInfo.Phone = model.Phone;
            userInfoCtrl.UserInfo.UserName = model.UserName;
            userInfoCtrl.Modifer();
            var resulst = new
            {
                userInfoCtrl.Code,
                userInfoCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DelUser(UserViewModel model)
        {
            string account = "AC003";  //账套名，登录时保存到session里
            UserInfoCtrl userInfoCtrl = new UserInfoCtrl(account, model.SID);
            userInfoCtrl.Delete();
            var resulst = new
            {
                userInfoCtrl.Code,
                userInfoCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JObject UserList(int page, int limit)
        {
            string account = "AC003";  //账套名，登录时保存到session里
            UserInfoCtrl userInfoCtrl = new UserInfoCtrl(account);
            List<UserInfo> list = userInfoCtrl.UserList();
            var list1 = list.Skip((page - 1) * limit).Take(limit).ToList();
            JObject json = new JObject(
               new JProperty("code", 0),
                new JProperty("msg", ""),
                new JProperty("count", list.Count),
                new JProperty("data",
                    new JArray(
                        from r in list1
                        select new JObject(
                            new JProperty("LoginID", r.LoginID),
                            new JProperty("SID", r.SID),
                            new JProperty("UserName", r.UserName),
                            new JProperty("Phone", r.Phone),
                            new JProperty("Status", r.Status),
                            new JProperty("DepartID", r.DepartID),
                            new JProperty("DepartName", r.DepartName),
                            new JProperty("Creator", r.Creator),
                            new JProperty("CreatDate", r.CreatDate)))));
            return json;                           

        }
        [HttpPost]
        public ActionResult ChangeStatus(string SID,string status)
        {
            string account = "AC003";  //账套名，登录时保存到session里
            UserInfoCtrl userInfoCtrl = new UserInfoCtrl(account, SID);
            userInfoCtrl.UserInfo.Status = int.Parse(status);
            userInfoCtrl.ChangeStatus();
            var resulst = new
            {
                userInfoCtrl.Code,
                userInfoCtrl.Msg
            };
            return Json(resulst, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}