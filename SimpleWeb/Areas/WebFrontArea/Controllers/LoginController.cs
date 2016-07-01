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
        public ActionResult Index(string bl = "")
        {
            MemberInfoModel model = new MemberInfoModel();
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
    }
}
