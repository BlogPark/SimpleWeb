using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.WebFrontArea.Models;
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
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            if (member != null)
            {
                int row = bll.AddMemberInfo(member);
            }
            return View(member);
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
    }
}
