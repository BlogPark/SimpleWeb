using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    ///SysAdminGrouprMenu
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysAdminGrouprMenuModel
    {
        #region 原有字段
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        [DataMember]
        public int GID { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [DataMember]
        public string GName { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [DataMember]
        public int MID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DataMember]
        public string MName { get; set; }
        /// <summary>
        /// 菜单类型（1 菜单 2 按钮）
        /// </summary>
        [DataMember]
        public int MType { get; set; }
        /// <summary>
        /// 权限类型(1 查看 2 编辑  3修改 4 删除)
        /// </summary>
        [DataMember]
        public int PermissionType { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 是否允许修改
        /// </summary>
        [DataMember]
        public int IsEdit { get; set; }

        #endregion
        /// <summary>
        /// 上级ID
        /// </summary>
        [DataMember]
        public int FatherID { get; set; }
        /// <summary>
        /// 菜单类型名称
        /// </summary>
        [DataMember]
        public string MenuTypeName { get; set; }
        /// <summary>
        /// 菜单权限名称
        /// </summary>
        [DataMember]
        public string PermissionTypeName { get; set; }
        /// <summary>
        /// 允许修改编辑
        /// </summary>
        [DataMember]
        public string IsEditName { get; set; }
    }
}
