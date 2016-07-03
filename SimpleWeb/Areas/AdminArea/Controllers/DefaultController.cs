using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.AdminArea.Models;
using SimpleWeb.Common;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;
using SimpleWeb.Models;
using SimpleWeb.WebClass;

namespace SimpleWeb.Areas.AdminArea.Controllers
{

    public class DefaultController : Controller
    {
        //
        // GET: /AdminArea/Default/

        private SysMenuAndUserBLL bll = new SysMenuAndUserBLL();
        private AdminIndexBLL adminbll = new AdminIndexBLL();

        public ActionResult Index()
        {
            SessionLoginModel sysuser = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (sysuser == null)
            {
                return RedirectToAction("Login", "Default", new { area = "AdminArea" });
            }
            DefaultViewModel model = new DefaultViewModel();
            model.datamodel = adminbll.GetDefaultData();
            model.UserName = sysuser.User.TruethName;
            return View(model);
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnurl = "")
        {
            LoginViewModel model = new LoginViewModel();
            model.returnurl = returnurl;
            this.ViewBag.Title = SysAdminConfigHelper.GetConfigValue("管理后台登陆页标题");
            this.ViewBag.Description = SysAdminConfigHelper.GetConfigValue("管理后台登陆页描述");
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            SysAdminUserModel user = new SysAdminUserModel();
            user.LoginName = model.LoginId;
            user.UserPwd = DESEncrypt.Encrypt(model.Pass, AppContent.SecrectStr);//加密密码
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
                    return RedirectToAction("Index", "Default", new { area = "AdminArea" });
                }
            }
            this.ViewBag.Title = SysAdminConfigHelper.GetConfigValue("管理后台登陆页标题");
            this.ViewBag.Description = SysAdminConfigHelper.GetConfigValue("管理后台登陆页描述");
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
