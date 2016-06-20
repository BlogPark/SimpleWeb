using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.WebFrontArea.Models;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    public class WebnewsController : Controller
    {
        //网站新闻
        // GET: /WebFrontArea/Webnews/
        AdminSiteNewsBll bll = new AdminSiteNewsBll();
        /// <summary>
        /// 网站公告页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
           LogMemberMsg logmember= Session[AppContent.SESSION_WEB_LOGIN] as LogMemberMsg;
           MemberNewsViewModel model = new MemberNewsViewModel();
           model.news = bll.GetModelListByUserID(logmember.MemberID);
            return View(model);
        }


        /// <summary>
        /// 联系网站页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactUs()
        {
            LogMemberMsg logmember = Session[AppContent.SESSION_WEB_LOGIN] as LogMemberMsg;
            ContactUsViewModel model = new ContactUsViewModel();
            model.list=bll.GetContractMessage(logmember.MemberID);
            return View(model);
        }
        [HttpPost]
        public ActionResult ContactUs(WebContactMessageModel message)
        {
            LogMemberMsg logmember = Session[AppContent.SESSION_WEB_LOGIN] as LogMemberMsg;
            if (message != null)
            {
                message.MemberID = logmember.MemberID;
                message.MemberName = logmember.MemberName;
                message.MemberPhone = logmember.MemberPhone;
                int row = bll.AddContactMessage(message);
            }
            return View(message);
        }
    }
}
