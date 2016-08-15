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
    public class ActiveCodeController : Controller
    {
        //
        // GET: /AdminArea/ActiveCode/
        public ActiveCodeBLL bll = new ActiveCodeBLL();
        private readonly int PageSize = 30;
        public ActionResult Index(ActiveCodeModel activecode, int page = 1)
        {
            int totalrowcount = 0;
            activecode.PageIndex = page;
            activecode.PageSize = PageSize;
            List<ActiveCodeModel> activecodelist = bll.GetActiveCodeListForPage(activecode, out totalrowcount);
            PagedList<ActiveCodeModel> pageList = null;
            if (activecodelist != null)
            {
                pageList = new PagedList<ActiveCodeModel>(activecodelist, page, PageSize, totalrowcount);
            }
            this.ViewData["activecode.AType"] = GetTypeListItem(0);
            this.ViewData["activecode.AStatus"] = GetStatusListItem(0);
            ActiveCodeIndexViewModel model = new ActiveCodeIndexViewModel();
            model.activecodelist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "激活码列表";
            return View(model);
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetTypeListItem(int defval = 1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "激活账户", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "排单专用", Value = "2", Selected = defval == 2 });
            return items;
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem(int defval = 20)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "未分配", Value = "20", Selected = defval == 20 });
            items.Add(new SelectListItem { Text = "已分配", Value = "15", Selected = defval == 15 });
            items.Add(new SelectListItem { Text = "已使用", Value = "10", Selected = defval == 10 });
            return items;
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem1(int defval = 1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "未使用", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "已使用", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已过期", Value = "3", Selected = defval == 3 });
            return items;
        }
        /// <summary>
        /// 生成激活码
        /// </summary>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult addcode(int count, int type)
        {
            int row = bll.ProduceActiveCode(count, type);
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
        ///  分配激活码
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ressign(string memberphone, string code)
        {
            List<string> codes = new List<string>();
            codes = code.Split(',').ToList();
            int row = bll.AssignedCode(codes, memberphone);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        [HttpPost]
        public ActionResult ressignmore(string memberphone, int count,int types)
        {
            int row = bll.AssignedMoreCode(count, types, memberphone);
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
        /// 会员激活码
        /// </summary>
        /// <param name="memberactive"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult memberactivecode(MemberActiveCodeModel memberactivecode, int page = 1)
        {
            int totalrowcount = 0;
            memberactivecode.PageIndex = page;
            memberactivecode.PageSize = PageSize;
            List<MemberActiveCodeModel> memberactivecodelist = bll.GetMemberActiveCodeListForPage(memberactivecode, out totalrowcount);
            PagedList<MemberActiveCodeModel> pageList = null;
            if (memberactivecodelist != null)
            {
                pageList = new PagedList<MemberActiveCodeModel>(memberactivecodelist, page, PageSize, totalrowcount);
            }
            this.ViewData["memberactivecode.AMType"] = GetTypeListItem(0);
            this.ViewData["memberactivecode.AMStatus"] = GetStatusListItem1(1);
            MemberActiveIndexViewModel model = new MemberActiveIndexViewModel();
            model.memberactivecodelist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "会员激活码列表";
            return View(model);
        }
        [HttpPost]
        public ActionResult updatestatus(int id)
        {
            int row = bll.UpdateMemberActive(id);
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
        /// 会员激活码使用日志
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult activecodeloglist(int page=1)
        {
            activecodeloglistViewModel model = new activecodeloglistViewModel();
            int totalrowcount;
            List<ActiveCodeLogModel> list = bll.GetActiveCodeLogForPage(0, page, PageSize, out totalrowcount);
            PagedList<ActiveCodeLogModel> pagelist = null;
            if (list != null)
            {
                pagelist = new PagedList<ActiveCodeLogModel>(list, page, PageSize, totalrowcount);
            }
            model.orderlist = pagelist;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "激活码日志列表";
            return View(model);
        }
    }
}
