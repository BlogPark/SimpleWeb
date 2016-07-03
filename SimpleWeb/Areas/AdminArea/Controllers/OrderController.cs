using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.AdminArea.Models;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class OrderController : Controller
    {
        //
        // GET: /AdminArea/Order/
        private HelpeOrderBLL bll = new HelpeOrderBLL();
        private AcceptHelpOrderBLL abll = new AcceptHelpOrderBLL();
        private MatchOrderBLL mbll = new MatchOrderBLL();
        private readonly int PageSize = 2;
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
    }
}
