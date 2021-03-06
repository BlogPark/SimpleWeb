﻿using System;
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

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    [AdminLoginAttribute]
    public class SiteMsgController : Controller
    {
        //网站公告管理
        // GET: /AdminArea/SiteMsg/
        AdminSiteNewsBll bll = new AdminSiteNewsBll();
        WebSettingsBLL websetbll = new WebSettingsBLL();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            SiteMsgIndexViewModel model = new SiteMsgIndexViewModel();
            model.modellist = bll.GetModelListByUserID(0);
            return View(model);
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="addmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addnotice(AdminSiteNewsModel addmodel)
        {
            if (addmodel == null)
            {
                return RedirectToAction("Index", "SysNotice", new { area = "AdminArea" });
            }
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            addmodel.SendUserID = user.User.ID;
            addmodel.SendUserName = user.User.UserName;
            addmodel.SStatus = 1;
            addmodel.ReceiveUserID = 0;
            addmodel.ReceiveUserName = "全体会员";
            int id = bll.AddAdminSiteNew(addmodel);
            return RedirectToAction("Index", "SiteMsg", new { area = "AdminArea" });
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult delenotice(int id)
        {
            if (id == 0)
            {
                return Json("0");
            }
            bool success = bll.UpdateStatus(id, 3);
            if (success)
            {
                return Json("1");
            }
            else { return Json("0"); }
        }

        /// <summary>
        /// 会员留言管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberMsg()
        {
            MemberMsgViewModel model = new MemberMsgViewModel();
            model.list = bll.GetAllContractMessage();
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateMsg(WebContactMessageModel updatemodel)
        {
            if (updatemodel != null)
            {
                bool row = bll.UpdateMsg(updatemodel);
            }
            return RedirectToAction("MemberMsg", "SiteMsg", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult deletemembermsg(int id)
        {
            bool row = bll.deleteMsg(id);
            if (row)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }

        /// <summary>
        /// 网站基本信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult websitemsg()
        {
            WebSettingsModel model = new WebSettingsModel();
            model = websetbll.GetWebSiteModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult websitemsg(WebSettingsModel model)
        {
            if (model != null)
            {
                int row = websetbll.UpdateWebSetting(model);
            }
            return View(model);
        }
    }
}
