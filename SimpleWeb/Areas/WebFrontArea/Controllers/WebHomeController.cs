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
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    [WebLoginAttribute]
    public class WebHomeController : Controller
    {
        //
        // GET: /WebFrontArea/WebHome/
        private MemberInfoBLL bll = new MemberInfoBLL();
        private ActiveCodeBLL activecodebll = new ActiveCodeBLL();
        private HelpeOrderBLL hobll = new HelpeOrderBLL();
        private AcceptHelpOrderBLL aobll = new AcceptHelpOrderBLL();
        private AdminSiteNewsBll newsbll = new AdminSiteNewsBll();
        private MemberCapitalDetailBLL cbll = new MemberCapitalDetailBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private readonly int pagesize = 2;
        private WebSettingsModel web;

        public WebHomeController()
        {
            web = webbll.GetWebSiteModel();
        }
        /// <summary>
        /// 登陆后首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            HomeViewModel model = new HomeViewModel();
            model.data = bll.GetIndexNeeddata(logmember.ID);
            model.member = logmember;
            model.recommend = bll.GetReMemberRelation(logmember.ID);
            ViewBag.PageTitle = web.WebName;
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
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
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
            ViewBag.PageTitle = web.WebName;
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
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 会员间转增激活码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberGife(string phone, int count, int type)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return Json(-10);
            }
            int result = activecodebll.GiveActiveCodeFromMember(logmember.ID, type, phone, count);
            return Json(result);
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
            //本人的信息
            model.helperpeople = logmember;
            //匹配列表单据信息和会员信息及推荐人联系方式
            List<AcceptExtendInfoModel> acceptmodel = hobll.GetAcceptextendmodels(orderid);
            model.acceptOrderInfo = acceptmodel;
            model.helporder = order;
            ViewBag.PageTitle = web.WebName;
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
            //本人的信息
            model.acceptpeople = logmember;
            //匹配列表单据信息和会员信息及推荐人联系方式
            List<HelpOrderExtendInfoModel> acceptmodel = aobll.GetHelpextendmodels(orderid);
            model.helpOrderInfo = acceptmodel;
            model.acceptorder = order;
            ViewBag.PageTitle = web.WebName;
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
            
            seachmodel.PageIndex = page;
            seachmodel.PageSize = pagesize;
            List<HelpeOrderModel> orderlist = hobll.GetAllHelpeOrderListForPage(seachmodel, out totalcount);
            PagedList<HelpeOrderModel> pagelist = null;
            if (orderlist != null)
            {
                pagelist = new PagedList<HelpeOrderModel>(orderlist, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
            ViewBag.PageTitle = web.WebName;
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
            seachmodel.PageIndex = page;
            seachmodel.PageSize = pagesize;
            List<AcceptHelpOrderModel> orderlist = aobll.GetAllAcceptOrderListForPage(seachmodel, out totalcount);
            PagedList<AcceptHelpOrderModel> pagelist = null;
            if (orderlist != null)
            {
                pagelist = new PagedList<AcceptHelpOrderModel>(orderlist, page, pagesize, totalcount);
            }
            model.orderlist = pagelist;
            ViewBag.PageTitle = web.WebName;
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
            ViewBag.PageTitle = web.WebName;
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
            ViewBag.PageTitle = web.WebName;
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
            ViewBag.PageTitle = web.WebName;
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
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            mycapitalViewModel model = new mycapitalViewModel();
            model.mycapitalinfo = cbll.GetMemberStaticCapital(logmember.ID);
            model.maxacceptamont = SystemConfigs.GetmaxAcceptAmont();//得到最大的接受帮助限制
            model.minacceptamont = SystemConfigs.GetminAcceptAmont();//得到最小的接受帮助限制
            model.minhelpamont = SystemConfigs.GetminHelpAmont();//得到最小的提供帮助限制
            model.maxhelpamont = SystemConfigs.GetmaxHelpAmont();//得到最大的提供帮助限制
            model.extendinfo = cbll.GetMemberExtendInfo(logmember.ID);//得到会员的扩展信息
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        [HttpPost]
        public ActionResult addhelporder(string helpactivecode, decimal helpamount, string paytype)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            HelpeOrderModel help = new HelpeOrderModel();
            help.ActiveCode = helpactivecode;
            help.Amount = helpamount;
            help.HStatus = 0;
            help.MemberID = logmember.ID;
            help.MemberName = logmember.TruethName;
            help.MemberPhone = logmember.MobileNum;
            help.OrderCode = "H" + DateTime.Now.ToString("yyyyMMddHHmmss");
            help.PayType = paytype;
            int result = hobll.AddHelpeOrder(help);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        [HttpPost]
        public ActionResult addacceptorder(int soucetype, decimal money, string paytype)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            AcceptHelpOrderModel accept = new AcceptHelpOrderModel();
            accept.SourceType = soucetype;
            accept.Amount = money;
            accept.AStatus = 0;
            accept.MemberID = logmember.ID;
            accept.MemberName = logmember.TruethName;
            accept.MemberPhone = logmember.MobileNum;
            accept.OrderCode = "A" + DateTime.Now.ToString("yyyyMMddHHmmss");
            accept.PayType = paytype;
            int result = aobll.AddAcceptHelpOrder(accept);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult exitsystem()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember != null)
            {
                Session.Remove(AppContent.SESSION_WEB_LOGIN);
            }
            return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
        }
    }
}
