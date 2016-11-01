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
    public class AddPermissionsViewModel
    {
        /// <summary>
        /// 所有一级菜单
        /// </summary>
        [DataMember]
        public List<SysAdminMenuModel> FirstMenuLists { get; set; }
        /// <summary>
        /// 所有二级菜单
        /// </summary>
        [DataMember]
        public List<SysAdminMenuModel> SecondMenuLists { get; set; }
        /// <summary>
        /// 所有菜单按钮
        /// </summary>
        [DataMember]
        public List<SysAdminMenuModel> ButtonMenuLists { get; set; }
        /// <summary>
        /// 当前赋值用户组信息
        /// </summary>
        [DataMember]
        public SysAdminUserGroupModel UserGroup { get; set; }
        /// <summary>
        ///  新增菜单信息和权限值字符串
        /// </summary>
        [DataMember]
        public string MenuListstr { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        [DataMember]
        public int gid { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [DataMember]
        public string gname { get; set; }
    }
}