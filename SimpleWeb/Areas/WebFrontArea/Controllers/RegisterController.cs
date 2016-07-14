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
    public class RegisterController : Controller
    {
        //
        // GET: /WebFrontArea/Register/
        private MemberInfoBLL bll = new MemberInfoBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private WebSettingsModel web;
        public RegisterController()
        {
            web = webbll.GetWebSiteModel();
        }
        public ActionResult Index(string msd)
        {
            RegisterViewModel model = new RegisterViewModel();
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
            RegisterViewModel model = new RegisterViewModel();
            model.member = member;
            model.member.MemberPhone = member.MemberPhone;
            model.regintable = bll.GetReginTableListModel(1);
            ViewBag.PageTitle = web.WebName;
            return View(model);

        }
        [HttpPost]
        public ActionResult checkphone(string phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                int count = bll.GetMemberInfoBycheck(phone, "", "");
                if (count > 0)
                {
                    return Json("0");
                }
            }
            return Json("1");
        }
        [HttpPost]
        public ActionResult checkname(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                int count = bll.GetMemberInfoBycheck("", name, "");
                if (count > 0)
                {
                    return Json("0");
                }
            }
            return Json("1");
        }
        [HttpPost]
        public ActionResult checkalipay(string alipay)
        {
            if (!string.IsNullOrWhiteSpace(alipay))
            {
                int count = bll.GetMemberInfoBycheck("", "", alipay);
                if (count > 0)
                {
                    return Json("0");
                }
            }
            return Json("1");
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult sendsms(string phone)
        {
            string result = bll.SendRegisterSms(phone);
            return Json(result);
        }
        /// <summary>
        /// 检查短信
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult checkverification(int id,string code)
        {
            string datacode = bll.SelectVerification(id);
            if (datacode != code)
            {
                return Json("0");
            }
            else
            {
                return Json("1");
            }
        }

    }
}
