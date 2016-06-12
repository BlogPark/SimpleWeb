using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Common;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private SysMenuAndUserBLL bll = new SysMenuAndUserBLL();
        [HttpGet]
        public ActionResult Index(string returnurl)
        {
            LoginViewModel model = new LoginViewModel();
            model.returnurl = returnurl;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            SysAdminUserModel user = new SysAdminUserModel();
            user.LoginName = model.LoginId;
            user.UserPwd = model.Pass;
            user.LastLoginTime = DateTime.Now;
            user.LastLoginIP = ComClass.GetIP();
            SysAdminUserModel result = bll.GetUserForLogin(user);
            if (result.LoginResult.StartsWith("0"))
            {
                model.loginresult = result.LoginResult.Substring(1);
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("skin_color");
                aCookie.Value = result.WebSkin;
                aCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(aCookie);
                List<SysAdminMenuModel> usermenu = bll.GetUserAttributeMenu(result);
                result.UserPwd = "";
                SessionLoginModel sessionmodel = new SessionLoginModel();
                sessionmodel.User = result;
                sessionmodel.UserMenus = usermenu;
                Session[AppContent.SESSION_LOGIN_NAME] = sessionmodel;
                string url = Url.Action("LoginOut", "IndexPub");
                if (!string.IsNullOrWhiteSpace(model.returnurl) && !model.returnurl.Contains(url))
                {
                    return Redirect(model.returnurl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return View(model);
        }
        /// <summary>
        /// 更改用户皮肤配置
        /// </summary>
        /// <param name="skinname"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updateskin(string skinname)
        {
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            int rowcount = bll.UpdateUserWebSkin(user.User.ID, skinname);
            if (rowcount > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }

        }
    }
}
