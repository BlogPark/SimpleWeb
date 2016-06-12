using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class SysUserMenuViewModel
    {
        /// <summary>
        /// 系统用户组
        /// </summary>
        [DataMember]
        public List<SysAdminUserGroupModel> AdminUser { get; set; }
        /// <summary>
        /// 权限菜单
        /// </summary>
        [DataMember]
        public List<SysAdminGrouprMenuModel> Menus { get; set; }
        /// <summary>
        /// 单独菜单权限
        /// </summary>
        [DataMember]
        public SysAdminGrouprMenuModel SinglePermissions { get; set; }

    }
}