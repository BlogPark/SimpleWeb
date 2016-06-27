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

        //Session[AppContent.SESSION_WEB_LOGIN] 
        private MemberInfoBLL bll = new MemberInfoBLL();
        private ActiveCodeBLL activecodebll = new ActiveCodeBLL();
        private readonly int pagesize = 30;
        public ActionResult Index()
        {
            return View();
        }
       
        /// <summary>
        /// 我的激活码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult myactivation(int page=1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("", "", new { area=""});
            }
            int totalcount=0;
            List<MemberActiveCodeModel> activecodes = activecodebll.GetMemberActiveCodeListForPage(logmember.ID, 1, page, pagesize, out totalcount);
            return View();
        }
    }
}
