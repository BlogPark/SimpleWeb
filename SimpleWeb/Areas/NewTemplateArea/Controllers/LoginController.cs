using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Common;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.NewTemplateArea.Controllers
{
    public class LoginController : Controller
    {
        //会员登陆页面
        // GET: /NewTemplateArea/Login/

        MemberInfoBLL bll = new MemberInfoBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private WebSettingsModel web;
        public LoginController()
        {
            web = webbll.GetWebSiteModel();
        }
        public ActionResult Index()
        {
            MemberInfoModel model = new MemberInfoModel();
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 会员登陆
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            if (member == null)
            {
                return View(member);
            }
            string newpwd = DESEncrypt.Encrypt(member.LogPwd, AppContent.SecrectStr);
            string logmsg = "";
            MemberInfoModel logmember = bll.GetMemberInfo(member.MobileNum, newpwd, out logmsg);
            if (logmsg == "1")
            {
                Session[AppContent.SESSION_WEB_LOGIN] = logmember;
                return RedirectToAction("Index", "WebHome", new { area = "WebFrontArea" });
            }
            else
            {
                ViewBag.TempMsg = logmsg;
                return View(member);
            }
        }

        public ActionResult NoPwdLogin(int id)
        {
            if (Session[AppContent.SESSION_LOGIN_NAME] == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            MemberInfoModel member = bll.GetModel(id);
            if (member != null)
            {
                Session.Remove(AppContent.SESSION_WEB_LOGIN);
                Session[AppContent.SESSION_WEB_LOGIN] = member;
                return RedirectToAction("Index", "WebHome", new { area = "WebFrontArea" });
            }
            return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
        }

    }
}
