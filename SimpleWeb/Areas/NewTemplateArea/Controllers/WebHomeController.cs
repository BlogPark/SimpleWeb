using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.NewTemplateArea.Models;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.NewTemplateArea.Controllers
{
    [WebLoginAttribute]
    public class WebHomeController : Controller
    {
        //
        // GET: /NewTemplateArea/WebHome/
        private MemberInfoBLL bll = new MemberInfoBLL();
        private OperateLogBLL logbll = new OperateLogBLL();
        private ActiveCodeBLL activecodebll = new ActiveCodeBLL();
        private HelpeOrderBLL hobll = new HelpeOrderBLL();
        private AcceptHelpOrderBLL aobll = new AcceptHelpOrderBLL();
        private AdminSiteNewsBll newsbll = new AdminSiteNewsBll();
        private MemberCapitalDetailBLL cbll = new MemberCapitalDetailBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private OrderReportingBLL reportbll = new OrderReportingBLL();
        private readonly int pagesize = 10;
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            WebHomeViewModel model = new WebHomeViewModel();
            model.data = bll.GetIndexNeeddata(logmember.ID);
            model.member = logmember;
            model.recommend = bll.GetReMemberRelation(logmember.ID);
            model.mycapitalinfo = cbll.GetMemberStaticCapital(logmember.ID);
            model.maxacceptamont = SystemConfigs.GetmaxAcceptAmont();//得到最大的接受帮助限制
            model.minacceptamont = SystemConfigs.GetminAcceptAmont();//得到最小的接受帮助限制
            model.minhelpamont = SystemConfigs.GetminHelpAmont();//得到最小的提供帮助限制
            model.maxhelpamont = SystemConfigs.GetmaxHelpAmont();//得到最大的提供帮助限制
            model.extendinfo = cbll.GetMemberExtendInfo(logmember.ID);//得到会员的扩展信息
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
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
            model.EnableCount = activecodebll.GetMemberActiveCodeCount(logmember.ID);
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
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
            model.EnableCount = activecodebll.GetMemberPandanCodeCount(logmember.ID);
            ViewBag.PageTitle = web.WebName;
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            HelpOrderViewModel model = new HelpOrderViewModel();
            HelpeOrderModel order = hobll.GetHelpOrderInfo(orderid, logmember.ID);
            if (order == null)
            {
                return RedirectToAction("Index", "WebHome", new { area = "NewTemplateArea" });
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            HelpOrderListViewModel model = new HelpOrderListViewModel();
            int totalcount = 0;
            seachmodel.MemberID = logmember.ID;
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            AcceptOrderListViewModel model = new AcceptOrderListViewModel();
            int totalcount = 0;
            seachmodel.MemberID = logmember.ID;
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            UserProfileViewModel model = new UserProfileViewModel();
            model.member = logmember;
            ViewBag.PageTitle = web.WebName;
            return View(model);
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
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            mycapitalViewModel model = new mycapitalViewModel();
            model.mycapitalinfo = cbll.GetMemberStaticCapital(logmember.ID);
            model.maxacceptamont = SystemConfigs.GetmaxAcceptAmont();//得到最大的接受帮助限制
            model.minacceptamont = SystemConfigs.GetminAcceptAmont();//得到最小的接受帮助限制
            model.minhelpamont = SystemConfigs.GetminHelpAmont();//得到最小的提供帮助限制
            model.maxhelpamont = SystemConfigs.GetmaxHelpAmont();//得到最大的提供帮助限制
            model.extendinfo = cbll.GetMemberExtendInfo(logmember.ID);//得到会员的扩展信息
            model.changlog = logbll.GetAmountChangeLogByTop(logmember.ID,20);//得到最新20条资金变动日志
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 激活码/排单币使用情况
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ActiveCodeLog(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            ActiveCodeLogViewModel model = new ActiveCodeLogViewModel();
            int totalrowcount = 0;
            List<ActiveCodeLogModel> list = activecodebll.GetActiveCodeLogForPage(logmember.ID, page, pagesize, out totalrowcount);
            PagedList<ActiveCodeLogModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<ActiveCodeLogModel>(list, page, pagesize, totalrowcount);
            }
            model.List = pagelist;
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 会员的资金变动列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult mycapitallist(int page = 1)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            mycapitallistViewModel model = new mycapitallistViewModel();
            int totalrowcount = 0;
            List<AmountChangeLogModel> list = logbll.GetAmountChangeLogByPage(page, pagesize, 0, logmember.ID, out totalrowcount);
            PagedList<AmountChangeLogModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<AmountChangeLogModel>(list, page, pagesize, totalrowcount);
            }
            model.list = pagelist;
            ViewBag.PageTitle = web.WebName;
            return View(model);
        }
        /// <summary>
        /// 我的团队页面
        /// </summary>
        /// <returns></returns>
        public ActionResult recommendusermap()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            RecommendUserMapViewModel model = new RecommendUserMapViewModel();
            int count = bll.recommendint(logmember.ID);
            model.member = logmember;
            model.childcount = count;
            model.isParent = count > 0;
            ViewBag.PageTitle = web.WebName;
            return View(model);
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
            return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
        }
    }
}
