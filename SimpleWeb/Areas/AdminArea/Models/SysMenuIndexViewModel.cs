using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    /// <summary>
    /// 系统菜单管理首页
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysMenuIndexViewModel
    {
        /// <summary>
        /// 系统所有菜单
        /// </summary>
        [DataMember]
        public List<SysAdminMenuModel> MenuLists { get; set; }

        /// <summary>
        /// 系统菜单
        /// </summary>
        [DataMember]
        public SysAdminMenuModel SingleMenu { get; set; }
    }
}