using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.NewTemplateArea.Models;
using SimpleWeb.Common;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;

namespace SimpleWeb.Areas.NewTemplateArea.Controllers
{
    [WebLoginAttribute]
    public class RegisterController : Controller
    {
        //注册页面
        // GET: /NewTemplateArea/Register/

        private MemberInfoBLL bll = new MemberInfoBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private WebSettingsModel web;
        public RegisterController()
        {
            web = webbll.GetWebSiteModel();
        }
        public ActionResult Index(string msd)
        {
            string name = SysAdminConfigBLL.GetConfigValue(23);
            if (name == "WebFrontArea")
            {
                return RedirectToAction("Index", "Register", new { area = "WebFrontArea" });
            }
            NewRegisterViewModel model = new NewRegisterViewModel();
            model.member = new MemberInfoModel();
            model.member.MemberPhone = msd;
            model.regintable = bll.GetReginTableListModel(1);
            ViewBag.PageTitle = web.WebName;
            ViewData["Error"] = "";
            return View(model);   
        }
        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            if (member != null)
            {
                member.LogPwd = DESEncrypt.Encrypt(member.LogPwd, AppContent.SecrectStr);
                string row = bll.AddMemberInfo(member);
                if (row == "1")
                {
                    return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
                }
                else
                {
                    ViewData["Error"] = row.Substring(1);
                }
            }
            NewRegisterViewModel model = new NewRegisterViewModel();
            model.member = member;
            model.member.MemberPhone = member.MemberPhone;
            model.regintable = bll.GetReginTableListModel(1);
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }

    }
}
