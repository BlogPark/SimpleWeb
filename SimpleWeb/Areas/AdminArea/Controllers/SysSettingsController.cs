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
        private SysAdminConfigBLL configbll = new SysAdminConfigBLL();
        public ActionResult Index()
        {
            return View();
        }
        #region 系统用户管理页面
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
                User.UserPwd = DESEncrypt.Encrypt(defaultpwd, AppContent.SecrectStr);
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
        #endregion

        #region 系统配置界面
        /// <summary>
        /// 系统配置项
        /// </summary>
        /// <returns></returns>
        public ActionResult SysConfigs()
        {
            SysConfigsViewModel model = new SysConfigsViewModel();
            model.Allconfigs = configbll.GetAllConfigs();
            model.FatherConfigs = configbll.GetFirstConfigs();
            ViewBag.PageTitle = "系统配置";
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSysConfigs(SysAdminConfigsModel AConfig)
        {
            if (AConfig != null)
            {
                int rowcount = configbll.AddConfigInfo(AConfig);
            }
            return RedirectToAction("SysConfigs", "SysSettings", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult UpdateConfigs(SysAdminConfigsModel UConfig)
        {
            if (UConfig != null)
            {
                int rowcount = configbll.UpdateConfigs(UConfig);
            }
            return RedirectToAction("SysConfigs", "SysSettings", new { area = "AdminArea" });
        }
        [HttpPost]
        public ActionResult DelConfigs(int id)
        {
            int rowcount = configbll.DelConfig(id);
            if (rowcount > 0)
                return Json("1");
            else
                return Json("0");
        }
        #endregion

        #region 系统菜单和权限管理
        public ActionResult Menu()
        {
            List<SysAdminMenuModel> models = mbll.GetAllSysMenu();
            SysMenuIndexViewModel vm = new SysMenuIndexViewModel();
            vm.MenuLists = models;
            vm.SingleMenu = new SysAdminMenuModel();
            ViewBag.PageTitle = "系统菜单";
            return View(vm);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(SysAdminMenuModel SingleMenu)
        {
            if (SingleMenu != null)
            {
                SingleMenu.FatherName = SingleMenu.FatherID == 0 ? "" : SingleMenu.FatherName;
                int rowcount = mbll.AddAndUpdateData(SingleMenu);
            }
            return RedirectToAction("Index", "SysMenu");
        }

        /// <summary>
        /// 系统用户组
        /// </summary>
        /// <returns></returns>
        public ActionResult SysAdminGroup()
        {
            List<SysAdminUserGroupModel> list = mbll.GetAllAdminGroup();
            SysAdminGroupViewModel model = new SysAdminGroupViewModel();
            model.AdminGroupLists = list;
            model.AdminGroup = new SysAdminUserGroupModel();
            ViewBag.PageTitle = "系统用户组";
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Auadmingroup(SysAdminUserGroupModel AdminGroup)
        {
            if (AdminGroup != null)
            {
                int rowcount = mbll.AddAndUpdateAdminGroup(AdminGroup);
            }
            return RedirectToAction("SysAdminGroup", "SysMenu", new { area = "AdminMenu" });
        }

        /// <summary>
        /// 系统菜单和权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GroupAndMenu()
        {
            SysUserMenuViewModel model = new SysUserMenuViewModel();
            model.AdminUser = mbll.GetAllAdminGroup();
            model.Menus = mbll.GetAllUserMenu();
            ViewBag.PageTitle = "菜单权限管理";
            return View(model);
        }


        [HttpPost]
        public ActionResult GroupAndMenu(SysAdminGrouprMenuModel SinglePermissions)
        {
            int rowcount = mbll.UpdatePermissionByID(SinglePermissions);
            return RedirectToAction("GroupAndMenu", "SysMenu", new { area = "AdminMenu" });
        }

        [ValidateInput(false)]
        public ActionResult AddPermissions(int gid)
        {
            List<SysAdminMenuModel> AllMenuList = mbll.GetAllMenuWithPermission(gid);
            AddPermissionsViewModel model = new AddPermissionsViewModel();
            model.FirstMenuLists = AllMenuList.Where(p => p.FatherID == 0).ToList();
            model.SecondMenuLists = AllMenuList.Where(p => p.FatherID != 0).ToList();
            model.ButtonMenuLists = AllMenuList.Where(p => p.MenuType == 2).ToList();
            model.UserGroup = mbll.GetUserGroupInfoByID(gid);
            model.gid = model.UserGroup.ID;
            model.gname = model.UserGroup.GroupName;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPermissions(AddPermissionsViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.MenuListstr))
            {
                return View();
            }
            string[] menuids = model.MenuListstr.TrimEnd('|').Split('|');
            foreach (string item in menuids)
            {
                string[] idtype = item.Split(',');
                SysAdminGrouprMenuModel gmodel = new SysAdminGrouprMenuModel();
                gmodel.MID = int.Parse(idtype[0]);
                gmodel.GID = model.gid;
                gmodel.PermissionType = int.Parse(idtype[1]);
                gmodel.GName = model.gname;
                int rowcount = mbll.AddUserGroupPermission(gmodel);
            }
            return RedirectToAction("GroupAndMenu", "SysMenu", new { area = "AdminMenu" });
        }

        /// <summary>
        /// 得到组名称下无权限菜单
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getmenulist(int gid)
        {
            #region 注释块
            //            {
            //    '主菜单1' : {name: '主菜单1', type: 'folder',id:"1",additionalParameters:{'children' : {
            //        '二级菜单1' : {name: '二级菜单1',id:"11", type: 'item'},
            //        '二级菜单2' : {name: '二级菜单2',id:"12", type: 'item'},
            //        '二级菜单3' : {name: '二级菜单3',id:"13", type: 'item'},
            //        '二级菜单4' : {name: '二级菜单4',id:"14", type: 'item'},
            //        '二级菜单5' : {name: '二级菜单5',id:"15", type: 'item'},
            //        '二级菜单6' : {name: '二级菜单6',id:"16", type: 'item'},
            //        '二级菜单7' : {name: '二级菜单7',id:"17", type: 'item'}
            //    }}},
            //    '主菜单2' : {'name': '主菜单2', 'type': 'folder','id':"2",'additionalParameters':{'children' : {
            //        '二级菜单8' : {name: '二级菜单8',id:"21", type: 'item','additionalParameters':{"item-selected":true}},
            //        '二级菜单9' : {name: '二级菜单9',id:"22", type: 'item','additionalParameters':{"item-selected":true}},
            //        '二级菜单10' : {name: '二级菜单10',id:"23", type: 'item','additionalParameters':{"item-selected":"true"}},
            //        '二级菜单11' : {name: '二级菜单11',id:"24", type: 'item','additionalParameters':{"item-selected":"true"}},
            //        '二级菜单12' : {name: '二级菜单12',id:"25", type: 'item','additionalParameters':{"item-selected":"true"}},
            //        '二级菜单13' : {name: '二级菜单13',id:"26", type: 'item','additionalParameters':{"item-selected":"true"}},
            //        '二级菜单14' : {name: '二级菜单14',id:"27", type: 'item','additionalParameters':{"item-selected":"true"}}
            //    }}},
            //    'departments' : {name: '主菜单',id:"3", type: 'item'},
            //    'benefits' : {name: '主菜单',id:"4", type: 'item'}
            //}
            #endregion
            string str = "{";
            List<SysAdminMenuModel> menulist = mbll.GetOtherMenuByGroup(gid);
            var first = menulist.Where(p => p.FatherID == 0 && p.MenuType == 1).ToList();
            string s = "";
            foreach (SysAdminMenuModel item in first)
            {
                s += "'" + item.MenuName + "' : {name: '" + item.MenuName + "', type: 'folder',id:'" + item.ID + "',additionalParameters:{'children' : {";
                string s1 = "";
                var seclist = menulist.Where(p => p.FatherID == item.ID && p.MenuType == 1).ToList();
                foreach (SysAdminMenuModel subitem in seclist)
                {
                    s1 += " '" + subitem.MenuName + "' : {name: '" + subitem.MenuName + "',id:'" + subitem.ID + "',fatherid:'" + subitem.FatherID + "',fathername:'" + subitem.FatherName + "', type: 'item'},";
                }
                s += s1.TrimEnd(',') + " }}},";
            }
            str += s.TrimEnd(',') + "}";
            return Json(str);
        }
        #endregion
    }
}
