using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    ///系统菜单
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysAdminMenuModel
    {
        #region 原有字段
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DataMember]
        public string MenuName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DataMember]
        public int FatherID { get; set; }
        /// <summary>
        /// 菜单备注
        /// </summary>
        [DataMember]
        public string MenuAlt { get; set; }
        /// <summary>
        /// 父级名称
        /// </summary>
        [DataMember]
        public string FatherName { get; set; }
        /// <summary>
        /// 菜单链接地址
        /// </summary>
        [DataMember]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 菜单状态(1 激活 0 禁用)
        /// </summary>
        [DataMember]
        public int MenuStatus { get; set; }
        /// <summary>
        /// 排序位置
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }
        /// <summary>
        /// 菜单类型（1 菜单 2 按钮）
        /// </summary>
        [DataMember]
        public int MenuType { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        [DataMember]
        public string ControllerName { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        [DataMember]
        public string ActionName { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [DataMember]
        public string AreaName { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        [DataMember]
        public string MenuIcon { get; set; }
        #endregion

        /// <summary>
        /// 权限类型(1 查看 2 编辑  3修改 4 删除)
        /// </summary>
        [DataMember]
        public int PermissionType { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        public int Type { get; set; }
        /// <summary>
        /// 是否具有权限
        /// </summary>
        [DataMember]
        public string IsHave { get; set; }
    }
}
