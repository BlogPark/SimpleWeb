using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class SysMenuAndUserBLL
    {
        private SysMenuAndUserDAL dal = new SysMenuAndUserDAL();
        /// <summary>
        /// 登录信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public SysAdminUserModel GetUserForLogin(SysAdminUserModel user)
        {
            return dal.GetUserForLogin(user);
        }
        /// <summary>
        /// 查询用户拥有的菜单权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<SysAdminMenuModel> GetUserAttributeMenu(SysAdminUserModel user)
        {
            return dal.GetUserAttributeMenu(user);
        }
        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SysAdminMenuModel> GetAllSysMenu()
        {
            return dal.GetAllSysMenu();
        }
        /// <summary>
        /// 添加和修改系统菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAndUpdateData(SysAdminMenuModel model)
        {
            return dal.AddAndUpdateData(model);
        }
        /// <summary>
        /// 得到所有的用户组
        /// </summary>
        /// <returns></returns>
        public List<SysAdminUserGroupModel> GetAllAdminGroup()
        {
            return dal.GetAllAdminGroup();
        }
        /// <summary>
        /// 添加和修改用户组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAndUpdateAdminGroup(SysAdminUserGroupModel model)
        {
            return dal.AddAndUpdateAdminGroup(model);
        }
        /// <summary>
        /// 得到所有用户组权限
        /// </summary>
        /// <returns></returns>
        public List<SysAdminGrouprMenuModel> GetAllUserMenu()
        {
            return dal.GetAllUserMenu();
        }
        /// <summary>
        ///查询用户组没权限的菜单 
        /// </summary>
        /// <returns></returns>
        public List<SysAdminMenuModel> GetOtherMenuByGroup(int gid)
        {
            return dal.GetOtherMenuByGroup(gid);
        }
        /// <summary>
        /// 得到用户组拥有的权限菜单
        /// </summary>
        /// <returns></returns>
        public List<SysAdminGrouprMenuModel> GetMenuByGroupID(int gid)
        {
            return dal.GetMenuByGroupID(gid);
        }
        /// <summary>
        /// 得到所有菜单并包含当前组是否有权限
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public List<SysAdminMenuModel> GetAllMenuWithPermission(int gid)
        {
            return dal.GetAllMenuWithPermission(gid);
        }
        /// <summary>
        /// 根据ID查询菜单
        /// </summary>
        /// <returns></returns>
        public List<SysAdminMenuModel> GetSysMenuByIds(string idstr)
        {
            return dal.GetSysMenuByIds(idstr);
        }
        /// <summary>
        /// 插入和修改菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUserGroupPermission(SysAdminGrouprMenuModel model)
        {
            return dal.AddUserGroupPermission(model);
        }
        /// <summary>
        /// 根据用户组ID得到组信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public SysAdminUserGroupModel GetUserGroupInfoByID(int gid)
        {
            return dal.GetUserGroupInfoByID(gid);
        }

        /// <summary>
        /// 更改用户组菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePermissionByID(SysAdminGrouprMenuModel model)
        {
            return dal.UpdatePermissionByID(model);
        }

        /// <summary>
        /// 保留用户的默认皮肤
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="skinname"></param>
        /// <returns></returns>
        public int UpdateUserWebSkin(int userid, string skinname)
        {
            return dal.UpdateUserWebSkin(userid, skinname);
        }
        #region 系统用户操作
        /// <summary>
        /// 得到所有的系统用户
        /// </summary>
        /// <returns></returns>
        public List<SysAdminUserModel> GetAllSysAdminUser()
        {
            return dal.GetAllSysAdminUser();
        }
        /// <summary>
        /// 根据ID查询系统用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public SysAdminUserModel GetSingleAdminUser(int userid)
        {
            return dal.GetSingleAdminUser(userid);
        }
        /// <summary>
        /// 新插入系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNewSysAdminUser(SysAdminUserModel model)
        {
            return dal.AddNewSysAdminUser(model);
        }
        /// <summary>
        /// 修改系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSysAdminUser(SysAdminUserModel model)
        {
            return dal.UpdateSysAdminUser(model);
        }
        /// <summary>
        /// 禁用系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DelSysAdminUser(int userid)
        {
            return dal.DelSysAdminUser(userid);
        }
        /// <summary>
        /// 更改系统管理员登陆密码
        /// </summary>
        /// <param name="newpwd"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UpdateAdminPwd(string newpwd, int userid)
        {
            return dal.UpdateAdminPwd(newpwd,userid);
        }
        #endregion

    }

}
