using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    /// <summary>
    /// 用户管理页面
    /// </summary>
    [Serializable]
    [DataContract]
    public class AdminUserViewModel
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        [DataMember]
        public List<SysAdminUserModel> UserLists { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [DataMember]
        public SysAdminUserModel User { get; set; }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        [DataMember]
        public SysAdminUserModel UpdateUser { get; set; }
        /// <summary>
        /// 用户组信息
        /// </summary>
        [DataMember]
        public List<SysAdminUserGroupModel> Groups { get; set; }
    }
}