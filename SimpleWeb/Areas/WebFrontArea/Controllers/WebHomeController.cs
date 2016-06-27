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
        private HelpeOrderBLL hobll = new HelpeOrderBLL();
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
            MyActiveIndexModel model = new MyActiveIndexModel();
            model.list = activecodes;
            return View(model);
        }
        /// <summary>
        /// 我的排单码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult mylineation(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            int totalcount = 0;
            List<MemberActiveCodeModel> activecodes = activecodebll.GetMemberActiveCodeListForPage(logmember.ID, 2, page, pagesize, out totalcount);
            MyActiveIndexModel model = new MyActiveIndexModel();
            model.list = activecodes;
            return View(model);
        }
        //未完
        public ActionResult helporderDetail(int orderid)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            HelpeOrderModel order = hobll.GetHelpordermodel(orderid);
            if (order.HStatus > 0 && order.HStatus != 3)
            {

                //本人的信息

                //匹配列表单据信息和会员信息及推荐人联系方式
                List<AcceptExtendInfoModel> acceptmodel = hobll.GetAcceptextendmodels(orderid);

            }
            return View();
        }


    }
}
