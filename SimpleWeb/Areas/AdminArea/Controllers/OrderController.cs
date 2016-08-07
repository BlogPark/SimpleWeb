using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.AdminArea.Models;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;
using SimpleWeb.Models;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class OrderController : Controller
    {
        //
        // GET: /AdminArea/Order/
        private HelpeOrderBLL bll = new HelpeOrderBLL();
        private MemberInfoBLL memberbll = new MemberInfoBLL();
        private AcceptHelpOrderBLL abll = new AcceptHelpOrderBLL();
        private MatchOrderBLL mbll = new MatchOrderBLL();
        private MemberCapitalDetailBLL mcbll = new MemberCapitalDetailBLL();
        private SysMenuAndUserBLL userbll = new SysMenuAndUserBLL();
        private OperateLogBLL logbll = new OperateLogBLL();
        private readonly int PageSize = 20;
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 提供帮助订单管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult supplyhelp(HelpeOrderModel order, int page = 1)
        {
            int totalrowcount = 0;
            order.PageIndex = page;
            order.PageSize = PageSize;
            List<HelpeOrderModel> orderlist = bll.GetAllHelpeOrderListForPage(order, out totalrowcount);
            PagedList<HelpeOrderModel> pageList = null;
            if (orderlist != null)
            {
                pageList = new PagedList<HelpeOrderModel>(orderlist, page, PageSize, totalrowcount);
            }
            HelperOrderViewModel model = new HelperOrderViewModel();
            model.orderlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "单据列表";
            this.ViewData["order.HStatus"] = GetStatusListItem(-10);
            return View(model);
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem(int defval = -10)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "全部", Value = "-10", Selected = defval == -10 });
            items.Add(new SelectListItem { Text = "未匹配", Value = "0", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "部分匹配", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "全部匹配", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已撤销", Value = "3", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "已打款", Value = "4", Selected = defval == 4 });
            items.Add(new SelectListItem { Text = "已完成", Value = "5", Selected = defval == 5 });
            return items;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatestatus(int oid, int status)
        {
            if (status > 3)
            {
                return Json("0");
            }
            int row = bll.UpdateToCancle(oid, status > 0 ? 1 : 0);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 更改置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatesortindex(int oid)
        {
            int row = bll.UpdateSortindex(oid);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 接受帮助列表
        /// </summary>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult accepthelp(AcceptHelpOrderModel order, int page = 1)
        {
            int totalrowcount = 0;
            order.PageIndex = page;
            order.PageSize = PageSize;
            List<AcceptHelpOrderModel> orderlist = abll.GetAllAcceptOrderListForPage(order, out totalrowcount);
            PagedList<AcceptHelpOrderModel> pageList = null;
            if (orderlist != null)
            {
                pageList = new PagedList<AcceptHelpOrderModel>(orderlist, page, PageSize, totalrowcount);
            }
            this.ViewData["order.AStatus"] = GetStatusListItem(-10);
            AcceptHelperViewModel model = new AcceptHelperViewModel();
            model.orderlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "订单列表";
            return View(model);
        }
        /// <summary>
        /// 更改接受帮助状态
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updateastatus(int oid, int status)
        {
            if (status > 3)
            {
                return Json("0");
            }
            int row = abll.UpdateToCancle(oid, status > 0 ? 1 : 0);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 更改接受帮助置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updateasortindex(int oid)
        {
            int row = abll.UpdateSortindex(oid);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 匹配页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult matchedmanage(int page = 1, int id = 1)
        {
            int totalrowcount = 0;
            int htotalrowcount = 0;
            AcceptHelpOrderModel aorder = new AcceptHelpOrderModel();
            aorder.PageIndex = page;
            aorder.PageSize = PageSize;
            HelpeOrderModel horder = new HelpeOrderModel();
            horder.PageIndex = id;
            horder.PageSize = PageSize;
            List<AcceptHelpOrderModel> waitorderlist = abll.GetWaitAcceptOrderListForPage(aorder, out totalrowcount);
            List<HelpeOrderModel> helporderlist = bll.GetWaitHelpeOrderListForPage(horder, out htotalrowcount);
            PagedList<AcceptHelpOrderModel> acceptList = null;
            PagedList<HelpeOrderModel> helpList = null;
            if (waitorderlist != null)
            {
                acceptList = new PagedList<AcceptHelpOrderModel>(waitorderlist, page, PageSize, totalrowcount);
            }
            if (helporderlist != null)
            {
                helpList = new PagedList<HelpeOrderModel>(helporderlist, id, PageSize, htotalrowcount);
            }
            MatchedManageViewModel model = new MatchedManageViewModel();
            model.acceptorderlist = acceptList;
            model.helporderlist = helpList;
            return View(model);
        }
        /// <summary>
        /// 匹配单据
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="aids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult matchedorder(string hids, string aids)
        {
            if (string.IsNullOrWhiteSpace(hids))
            {
                return Json("没有传入提供帮助单据");
            }
            if (string.IsNullOrWhiteSpace(aids))
            {
                return Json("没有传入接受帮助单据");
            }
            List<string> hidlist = hids.TrimEnd(',').Split(',').ToList();
            List<string> aidlist = aids.TrimEnd(',').Split(',').ToList();
            if (hidlist.Count < 1)
            {
                return Json("没有单据需要处理");
            }
            if (aidlist.Count < 1)
            {
                return Json("没有单据需要处理");
            }
            int row = 0;
            foreach (var item in hidlist)
            {
                foreach (var aitem in aidlist)
                {
                    int aid = int.Parse(aitem);
                    row += mbll.OperateMatchOrder(int.Parse(item), aid);
                }
            }

            if (row < aidlist.Count)
            {
                return Json("部分单据处理完成");
            }
            return Json("1");
        }

        /// <summary>
        /// 分派利息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentInterist(int page = 1)
        {
            paymentinteristViewModel model = new paymentinteristViewModel();
            int totalrowcount = 0;
            List<AmountChangeLogModel> list = mbll.GetAmountChangeLogByTypeForPage(page, PageSize, out totalrowcount);
            PagedList<AmountChangeLogModel> pagedlist = null;
            if (list != null)
            {
                pagedlist = new PagedList<AmountChangeLogModel>(list, page, PageSize, totalrowcount);
            }
            model.logs = pagedlist;
            return View(model);
        }
        [HttpPost]
        public ActionResult paymentinterist()
        {
            string result = mcbll.PaymentInterestV3();
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        [HttpPost]
        public ActionResult paymentdymicmoney(string phone, decimal money)
        {
            string result = mcbll.FenPaiMoney(phone, money);
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        [HttpPost]
        public ActionResult paymentdymicmoneyv1(int id, decimal money, int type)
        {
            string result = "";
            if (type == 1)
            {
                result = mcbll.FenPaiMoneyToStatic(id, money);
            }
            else if (type == 2)
            {
                result = mcbll.FenPaiMoneyToDynamic(id, money);
            }
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }

        /// <summary>
        /// 会员资产一览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult membercapitaldetail(MemberCapitalDetailModel smodel, int page = 1)
        {
            membercapitaldetailViewModel model = new membercapitaldetailViewModel();
            int totalrowcount = 0;
            MemberCapitalDetailModel seachmodel = new MemberCapitalDetailModel();
            seachmodel.PageSize = PageSize;
            seachmodel.PageIndex = page;
            seachmodel.MemberName = smodel.MemberName;
            seachmodel.MemberPhone = smodel.MemberPhone;
            List<MemberCapitalDetailModel> list = mcbll.GetMembercapitalList(seachmodel, out totalrowcount);
            PagedList<MemberCapitalDetailModel> pageList = null;
            if (list != null)
            {
                pageList = new PagedList<MemberCapitalDetailModel>(list, page, PageSize, totalrowcount);
            }
            model.list = pageList;
            model.pagesize = PageSize;
            model.currentpage = page;
            model.totalcount = totalrowcount;
            ViewBag.PageTitle = "会员资产";
            return View(model);
        }
        [HttpPost]
        public ActionResult punishmentmember(int id, decimal money, string reason, int type)
        {
            string result = "";
            if (type == 1)
            {
                result = mcbll.punishmentStaticMoney(id, money, reason);
            }
            else
            {
                result = mcbll.punishmentDynamicMoney(id, money, reason);
            }
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        /// <summary>
        /// 匹配的历史纪录
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Matchorderlist(MatchOrderModel matchmodel, int page = 1)
        {
            MatchorderlistViewModel model = new MatchorderlistViewModel();
            int totalrowcount = 0;
            MatchOrderModel seachmodel = new MatchOrderModel();
            seachmodel.PageSize = PageSize;
            seachmodel.PageIndex = page;
            seachmodel.MatchStatus = matchmodel.MatchStatus;
            seachmodel.HelperOrderCode = matchmodel.HelperOrderCode;
            seachmodel.AcceptOrderCode = matchmodel.AcceptOrderCode;
            List<MatchOrderModel> list = mbll.GetMatchedOrderListByPage(seachmodel, out totalrowcount);
            PagedList<MatchOrderModel> pageList = null;
            if (list != null)
            {
                pageList = new PagedList<MatchOrderModel>(list, page, PageSize, totalrowcount);
            }
            model.list = pageList;
            model.pagesize = PageSize;
            model.currentpage = page;
            model.totalcount = totalrowcount;
            ViewBag.PageTitle = "匹配列表";
            this.ViewData["matchmodel.MatchStatus"] = GetMatchStatusListItem(0);
            return View(model);
        }
        /// <summary>
        /// 得到匹配状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetMatchStatusListItem(int defval = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "全部", Value = "0", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "已匹配", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "已取消", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已打款", Value = "3", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "已完成", Value = "4", Selected = defval == 4 });
            return items;
        }
        /// <summary>
        /// 检查二次密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult checkconfirmpwd(string pwd)
        {
            SessionLoginModel sysuser = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (sysuser == null)
            {
                return RedirectToAction("Login", "Default", new { area = "AdminArea" });
            }
            string cpwd = userbll.GetAdminConfirmPwd(sysuser.User.ID);
            if (cpwd == pwd)
            {
                return Json("1");
            }
            else
            {
                return Json("验证二次密码失败");
            }

        }
        /// <summary>
        /// 查看系统操作日志
        /// </summary>
        /// <param name="seachmodel"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult behaviorloglist(UserBehaviorLogModel seachmodel, int page = 1)
        {
            behaviorloglistViewModel model = new behaviorloglistViewModel();
            int totalrowcount = 0;
            UserBehaviorLogModel seach = new UserBehaviorLogModel();
            seach.PageIndex = page;
            seach.PageSize = PageSize;
            seach.BehaviorType = seachmodel.BehaviorType;
            seach.MemberName = seachmodel.MemberName;
            seach.MemberPhone = seachmodel.MemberPhone;
            List<UserBehaviorLogModel> list = logbll.GetUserBehaviorLogByPage(seach, out totalrowcount);
            PagedList<UserBehaviorLogModel> pagelist = null;
            if (list.Count > 0)
            {
                pagelist = new PagedList<UserBehaviorLogModel>(list, page, PageSize, totalrowcount);
            }
            model.currentpage = page;
            model.list = pagelist;
            model.pagesize = PageSize;
            model.totalcount = totalrowcount;
            ViewBag.PageTitle = "操作日志";
            this.ViewData["seachmodel.BehaviorType"] = GetBehaviorTypeListItem(1);
            return View(model);
        }
        /// <summary>
        /// 得到Log类型
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetBehaviorTypeListItem(int defval = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "登陆", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "提供帮助", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "接受帮助", Value = "3", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "变更打款", Value = "4", Selected = defval == 4 });
            items.Add(new SelectListItem { Text = "确认单据", Value = "5", Selected = defval == 5 });
            items.Add(new SelectListItem { Text = "撤销单据", Value = "6", Selected = defval == 6 });
            items.Add(new SelectListItem { Text = "发放排单币", Value = "7", Selected = defval == 7 });
            items.Add(new SelectListItem { Text = "发放激活币", Value = "8", Selected = defval == 8 });
            items.Add(new SelectListItem { Text = "奖励会员", Value = "9", Selected = defval == 9 });
            items.Add(new SelectListItem { Text = "惩罚会员", Value = "10", Selected = defval == 10 });
            items.Add(new SelectListItem { Text = "手动派息", Value = "11", Selected = defval == 11 });
            return items;
        }
    }
}
