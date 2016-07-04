using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.AdminArea.Models;
using SimpleWeb.Common;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Filters;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class MemberOperaController : Controller
    {
        //
        // GET: /AdminArea/MemberOpera/
        private MemberInfoBLL bll = new MemberInfoBLL();
        private readonly int PageSize = 30;
        public ActionResult Index(MemberInfoModel member, int page = 1)
        {
            int totalrowcount = 0;
            member.PageIndex = page;
            member.PageSize = PageSize;
            List<MemberInfoModel> memberlist = bll.GetMemberInfoListForPage(member, out totalrowcount);
            PagedList<MemberInfoModel> pageList = null;
            if (memberlist != null)
            {
                pageList = new PagedList<MemberInfoModel>(memberlist, page, PageSize, totalrowcount);
            }
            this.ViewData["member.MStatus"] = GetStatusListItem(0);
            MemberIndexViewModel model = new MemberIndexViewModel();
            model.memberlist = pageList;
            model.totalcount = totalrowcount;
            model.pagesize = PageSize;
            model.currentpage = page;
            ViewBag.PageTitle = "会员列表";
            return View(model);
        }
        /// <summary>
        /// 得到状态列表
        /// </summary>
        /// <param name="defval"></param>
        /// <returns></returns>
        private List<SelectListItem> GetStatusListItem(int defval = 1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "全部", Value = "0", Selected = defval == 0 });
            items.Add(new SelectListItem { Text = "待激活", Value = "1", Selected = defval == 1 });
            items.Add(new SelectListItem { Text = "已激活", Value = "2", Selected = defval == 2 });
            items.Add(new SelectListItem { Text = "已冻结", Value = "3", Selected = defval == 3 });
            return items;
        }
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMember()
        {
            AddMemberViewModel model = new AddMemberViewModel();
            model.regintable = bll.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMember(MemberInfoModel member)
        {
            if (member != null)
            {
                member.MStatus = 1;
                member.LogPwd = DESEncrypt.Encrypt("666666", AppContent.SecrectStr);//加密密码
                int row = bll.AddMemberInfo(member);
            }
            return RedirectToActionPermanent("Index", "MemberOpera", new { area = "AdminArea" });
        }
        [HttpGet]
        public ActionResult EditMember(int mid)
        {
            EditMemberViewModel model = new EditMemberViewModel();
            model.member = bll.GetModel(mid);
            model.regintable = bll.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditMember(MemberInfoModel member)
        {
            if (member != null)
            {
                bool row = bll.UpdateMemberInfo(member);
            }
            return RedirectToActionPermanent("Index", "MemberOpera", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult obtainreagin(int id)
        {
            List<ReginTableModel> list = bll.GetReginTableListModel(id);
            return Json(list);
        }
        [HttpPost]
        public ActionResult updatesta(int id, int status)
        {
            string result = "";
            if (status == 2)
            {
                result=bll.ActiveMember(id,"","",true);//系统分配一个随机激活码
            }
            else
            { 
                result =bll.UpdateStatus(id, status).ToString();
            }
            if (!result.StartsWith("0"))
            {
                return Json("1");
            }
            else
            {
                return Json(result);
            }
        }

        [HttpPost]
        public ActionResult GetRelation(int mid)
        {
            //List<ReMemberRelationModel> list=
            return Json("ss");
        }
    }
}
