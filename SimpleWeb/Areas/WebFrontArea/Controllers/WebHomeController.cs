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
using Webdiyer.WebControls.Mvc;

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
        private AcceptHelpOrderBLL aobll = new AcceptHelpOrderBLL();
        private AdminSiteNewsBll newsbll = new AdminSiteNewsBll();
        private MemberCapitalDetailBLL cbll = new MemberCapitalDetailBLL();
        private readonly int pagesize = 2;
        /// <summary>
        /// 登陆后首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }
        /// <summary>
        /// 我的激活码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult myactivation(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("", "", new { area = "" });
            }
            int totalcount = 0;
            List<MemberActiveCodeModel> activecodes = activecodebll.GetMemberActiveCodeListForPage(logmember.ID, 1, page, pagesize, out totalcount);
            MyActiveIndexModel model = new MyActiveIndexModel();
            PagedList<MemberActiveCodeModel> pagelist = null;
            if (activecodes != null)
            {
                pagelist = new PagedList<MemberActiveCodeModel>(activecodes, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
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
            PagedList<MemberActiveCodeModel> pagelist = null;
            if (activecodes != null)
            {
                pagelist = new PagedList<MemberActiveCodeModel>(activecodes, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
            return View(model);
        }
        /// <summary>
        /// 提供帮助单据信息页面
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public ActionResult helporderDetail(int orderid)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            HelpOrderViewModel model = new HelpOrderViewModel();
            HelpeOrderModel order = hobll.GetHelpOrderInfo(orderid, logmember.ID);
            if (order == null)
            {
                return RedirectToAction("Index", "WebHome", new { area = "WebFrontArea" });
            }
            if (order.HStatus > 0 && order.HStatus != 3)
            {
                //本人的信息
                model.helperpeople = logmember;
                //匹配列表单据信息和会员信息及推荐人联系方式
                List<AcceptExtendInfoModel> acceptmodel = hobll.GetAcceptextendmodels(orderid);
                model.acceptOrderInfo = acceptmodel;
            }
            model.helporder = order;
            return View(model);
        }
        /// <summary>
        /// 接受帮助单据信息页面
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public ActionResult AcceptOrderDetail(int orderid)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            AcceptOrderViewModel model = new AcceptOrderViewModel();
            AcceptHelpOrderModel order = aobll.GetAcceptOrderInfo(orderid, logmember.ID);
            if (order == null)
            {
                return RedirectToAction("Index", "WebHome", new { area = "WebFrontArea" });
            }
            if (order.AStatus > 0 && order.AStatus != 3)
            {
                //本人的信息
                model.acceptpeople = logmember;
                //匹配列表单据信息和会员信息及推荐人联系方式
                List<HelpOrderExtendInfoModel> acceptmodel = aobll.GetHelpextendmodels(orderid);
                model.helpOrderInfo = acceptmodel;
            }
            model.acceptorder = order;
            return View(model);
        }
        /// <summary>
        /// 提供帮助列表页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult helpOrderlist(HelpeOrderModel seachmodel, int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            HelpOrderListViewModel model = new HelpOrderListViewModel();
            int totalcount = 0;
            List<HelpeOrderModel> orderlist = hobll.GetAllHelpeOrderListForPage(seachmodel, out totalcount);
            PagedList<HelpeOrderModel> pagelist = null;
            if (orderlist != null)
            {
                pagelist = new PagedList<HelpeOrderModel>(orderlist, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
            return View(model);
        }
        /// <summary>
        /// 接受帮助列表页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult acceptOrderlist(AcceptHelpOrderModel seachmodel, int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            AcceptOrderListViewModel model = new AcceptOrderListViewModel();
            int totalcount = 0;
            List<AcceptHelpOrderModel> orderlist = aobll.GetAllAcceptOrderListForPage(seachmodel, out totalcount);
            PagedList<AcceptHelpOrderModel> pagelist = null;
            if (orderlist != null)
            {
                pagelist = new PagedList<AcceptHelpOrderModel>(orderlist, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
            return View(model);
        }
        /// <summary>
        /// 网站公告页面
        /// </summary>
        /// <returns></returns>
        public ActionResult webnews()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            MemberNewsViewModel model = new MemberNewsViewModel();
            model.news = newsbll.GetModelListByUserID(logmember.ID);
            return View(model);
        }
        /// <summary>
        /// 联系我们页面
        /// </summary>
        /// <returns></returns>
        public ActionResult contentus()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            ContactUsViewModel model = new ContactUsViewModel();
            model.list = newsbll.GetContractMessage(logmember.ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult contentus(WebContactMessageModel message)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            if (message != null)
            {
                message.MemberID = logmember.ID;
                message.MemberName = logmember.TruethName;
                message.MemberPhone = logmember.MobileNum;
                int row = newsbll.AddContactMessage(message);
            }
            return RedirectToAction("contentus", "WebHome", new { area = "WebFrontArea" });
        }
        /// <summary>
        /// 用户中心页面
        /// </summary>
        /// <returns></returns>
        public ActionResult userprofile()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            UserProfileViewModel model = new UserProfileViewModel();
            model.member = logmember;
            return View(model);
        }
        [HttpPost]
        public ActionResult updatepwd(string pwd)
        {
            if (!string.IsNullOrWhiteSpace(pwd))
            {
                return Json("0");
            }
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return Json("0");
            }
            string newpwd = DESEncrypt.Encrypt(pwd, AppContent.SecrectStr);
            int row = bll.UpdateUserPwd(logmember.ID, newpwd);
            return Json(row);
        }
        /// <summary>
        /// 我的资产页面
        /// </summary>
        /// <returns></returns>
        public ActionResult mycapital()
        {
            //MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            //if (logmember == null)
            //{
            //    return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            //}
            mycapitalViewModel model = new mycapitalViewModel();
            model.mycapitalinfo = cbll.GetMemberStaticCapital(1);
            return View(model);
        }
    }
}
