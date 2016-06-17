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
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
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
        /// <summary>
        /// 产生验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImg()
        {
            int width = 100;
            int height =  40;
            int fontsize =  20;
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
            Session[AppContent.VALICODE] = code;
            return File(bytes, @"image/jpeg");
        }
        /// <summary>
        /// 验证码对比
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult valitecode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Json("请填写验证码");
            }
            string soucecode = Session[AppContent.VALICODE].ToString();
            if (code.Trim() != soucecode)
            {
                return Json("验证码不正确");
            }
            return Json("1");
        }
    }
}
