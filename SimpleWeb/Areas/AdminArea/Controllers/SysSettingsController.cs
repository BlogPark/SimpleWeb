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

namespace SimpleWeb.Areas.AdminArea.Controllers
{
    public class SysSettingsController : Controller
    {
        //
        // GET: /AdminArea/SysSettings/

        private SysMenuAndUserBLL mbll = new SysMenuAndUserBLL();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 系统用户管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminUser()
        {
            AdminUserViewModel model = new AdminUserViewModel();
            model.UserLists = mbll.GetAllSysAdminUser();
            model.Groups = mbll.GetAllAdminGroup();
            ViewBag.PageTitle = "系统用户";
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAdminUser(SysAdminUserModel User)
        {
            if (User != null)
            {
                User.HeaderImg = "/img/avatars/avatar3.jpg";
                string defaultpwd = "123456";//创建默认密码
                User.UserPwd = DESEncrypt.Encrypt(defaultpwd,AppContext.SecrectStr);
                User.GName = User.GName.Trim();
                string pinyin = PinYinConverter.Get(User.UserName.Trim());
                User.PinYin = pinyin;
                User.FirstPinYin = string.IsNullOrWhiteSpace(pinyin) ? "A" : pinyin.Substring(0, 1);
                int rowcount = mbll.AddNewSysAdminUser(User);
            }
            return RedirectToAction("AdminUser", "SysSettings", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult UpdAdminUser(SysAdminUserModel UpdateUser)
        {
            if (UpdateUser != null)
            {
                UpdateUser.GName = UpdateUser.GName.Trim();
                string pinyin = PinYinConverter.Get(UpdateUser.UserName.Trim());
                UpdateUser.PinYin = pinyin;
                UpdateUser.FirstPinYin = string.IsNullOrWhiteSpace(pinyin) ? "A" : pinyin.Substring(0, 1);
                int rowcount = mbll.UpdateSysAdminUser(UpdateUser);
            }
            return RedirectToAction("AdminUser", "SysSettings", new { area = "AdminArea" });
        }
        [HttpPost]
        public JsonResult DelAdminUser(int id)
        {
            int rowcount = mbll.DelSysAdminUser(id);
            if (rowcount > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }


    }
}
