using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.AdminArea.Models;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /AdminArea/Order/
        private HelpeOrderBLL bll = new HelpeOrderBLL();
        private AcceptHelpOrderBLL abll = new AcceptHelpOrderBLL();
        private readonly int PageSize = 30;
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
            this.ViewData["order.HStatus"] = GetStatusListItem(0);
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
            items.Add(new SelectListItem { Text = "全部", Value = "-10", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "未匹配", Value = "0", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "部分匹配", Value = "1", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "全部成交", Value = "2", Selected = defval == 3 });
            items.Add(new SelectListItem { Text = "已撤销", Value = "3", Selected = defval == 3 });
            return items;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatestatus(int oid)
        {
            int row = bll.UpdateStatus(oid,3);
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

        [HttpGet]
        public ActionResult accepthelp(AcceptHelpOrderModel order,int page=1)
        {
            int totalrowcount = 0;
            order.PageIndex = page;
            order.PageSize = PageSize;
            List<AcceptHelpOrderModel> orderlist = abll.GetAllHelpeOrderListForPage(order, out totalrowcount);
            PagedList<AcceptHelpOrderModel> pageList = null;
            if (orderlist != null)
            {
                pageList = new PagedList<AcceptHelpOrderModel>(orderlist, page, PageSize, totalrowcount);
            }
            this.ViewData["order.AStatus"] = GetStatusListItem(0);
            AcceptHelperViewModel model = new AcceptHelperViewModel();
            model.orderlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "订单列表";
            return View(model);
        }
    }
}
