using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.WebFrontArea.Models;
using SimpleWeb.Common;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    [WebLoginAttribute]
    public class LoginController : Controller
    {
        //会员登陆页面
        // GET: /WebFrontArea/Login/
        MemberInfoBLL bll = new MemberInfoBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private WebSettingsModel web;
        public LoginController()
        {
            web = webbll.GetWebSiteModel();
        }
        public ActionResult Index(string bl = "")
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
        ///// <summary>
        ///// 产生验证码图片
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetImg()
        //{
        //    int width = 100;
        //    int height = 40;
        //    int fontsize = 20;
        //    string code = string.Empty;
        //    byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
        //    Session[AppContent.VALICODE] = code;
        //    return File(bytes, @"image/jpeg");
        //}
        //[HttpGet]
        //public ActionResult checkcode(string code)
        //{
        //    var scode = Session[AppContent.VALICODE];
        //    if (scode == null)
        //    {
        //        return Json("0");
        //    }
        //    if (code.ToUpper() == scode.ToString().ToUpper())
        //    {
        //        return Json("1");
        //    }
        //    else
        //    {
        //        return Json("0");
        //    }
        //}
    }
}
