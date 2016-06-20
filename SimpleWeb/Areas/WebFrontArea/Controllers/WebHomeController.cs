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

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    public class WebHomeController : Controller
    {
        //
        // GET: /WebFrontArea/WebHome/
        private MemberInfoBLL bll = new MemberInfoBLL();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult Register(string fph)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.regintable = bll.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(MemberInfoModel member)
        {
            if (member != null)
            {
                member.LogPwd = DESEncrypt.Encrypt(member.LogPwd, AppContent.SecrectStr);//加密密码
                int row = bll.AddMemberInfo(member);
            }
            return RedirectToActionPermanent("Index", "WebHome", new { area = "WebFrontArea" });
        }

        /// <summary>
        /// 我的激活码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivationCode()
        {
            LogMemberMsg logmember = Session[AppContent.SESSION_WEB_LOGIN] as LogMemberMsg;

            return View();
        }
    }
}
