using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Common;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class IndexPubController : Controller
    {
        //
        // GET: /IndexPub/
        private SysMenuAndUserBLL bll = new SysMenuAndUserBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private MemberInfoBLL memberbll = new MemberInfoBLL();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 首页左侧菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu(string currentpage)
        {
            SessionLoginModel model = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            MenuViewModel models = new MenuViewModel();
            if (model != null)
            {
                string idstr = "";
                idstr = string.Join(",", model.UserMenus.Where(p => p.MenuType == 1).Select(p => p.FatherID).Distinct());
                models.firstlist = bll.GetSysMenuByIds(idstr.TrimEnd(','));
                models.sublist = model.UserMenus.Where(p => p.FatherID != 0).ToList();
                models.Currentpage = currentpage;
            }
            return View(models);
        }
        /// <summary>
        /// 管理员通知
        /// </summary>
        /// <returns></returns>
        public ActionResult Notification()
        {
            return View();
        }
        /// <summary>
        /// 管理员日程
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminTask()
        {
            return View();
        }
        /// <summary>
        /// 管理员消息
        /// </summary>
        /// <returns></returns>
        public ActionResult Message()
        {
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            partMessageViewModel model = new partMessageViewModel();
            if (user != null)
            {
                AdminSiteNewsBll bll = new AdminSiteNewsBll();
                List<AdminSiteNewsModel> list = bll.GetTop3ModelListByUserID(user.User.ID);
                if (list != null && list.Count > 0)
                {
                    model.newmsglist = list.Where(m => m.SStatus == 1).ToList();
                    model.newcount = model.newmsglist.Count;
                    model.oldmsglist = list.Where(m => m.SStatus == 2).ToList();
                }
            }
            return View(model);
        }
        /// <summary>
        /// 管理员信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo()
        {
            SessionLoginModel model = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (model != null)
            {
                return View(model.User);
            }
            else
            {
                return View(new SysAdminUserModel());
            }
        }

        public ActionResult LoginOut()
        {
            Session.Clear();// Session[AppContext.SESSION_LOGIN_NAME] = null;
            return RedirectToAction("Login", "Default", new { area = "AdminArea", returnurl = "" });
        }

        public ActionResult mytest()
        {
            return View();
        }


        public ActionResult webfooter()
        {
            webfooterViewModel model = new webfooterViewModel();
            WebSettingsModel setting = webbll.GetWebSiteModel();
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                model.Linkurl = setting.DomainName;
            }
            else
            {
                model.Linkurl = setting.DomainName + Url.Action("Index", "Register", new { area = "WebFrontArea", msd = logmember.MobileNum });
            }
            return View(model);
        }
        public ActionResult GetqrCode(string msd)
        {
            byte[] bytes = QRCodeHelper.CreateBitMap(msd);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult webmenu()
        {
            webmenuViewModel model = new webmenuViewModel();
            model.webname = "诚信创客";
            return View(model);
        }
        public ActionResult newwebmenu()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "NewTemplateArea" });
            }
            WebIndexModel webmodel=memberbll.GetIndexNeeddata(logmember.ID);
            NewWebMenuViewModel model = new NewWebMenuViewModel();
            model.ActionCodeCount = webmodel.activecodeCount;
            model.CurrencyCodeCount = webmodel.paidancodeCount;
            model.DynamicMoney = webmodel.zijinmodel.DynamicFunds;
            model.StaticMoney = webmodel.zijinmodel.StaticCapital;
            model.UserName = logmember.TruethName;
            model.UserPhone = logmember.MobileNum;
            model.TeamPersonCount = webmodel.members;
            return View(model);
        }
    }
}
