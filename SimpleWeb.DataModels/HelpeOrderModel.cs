using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 提供帮助列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class HelpeOrderModel
    {
        #region 原始字段
        /// <summary>
        /// ID
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>       
        [DataMember]
        public string OrderCode { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>       
        [DataMember]
        public int MemberID { get; set; }
        /// <summary>
        /// 会员电话
        /// </summary>       
        [DataMember]
        public string MemberPhone { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>       
        [DataMember]
        public string MemberName { get; set; }
        /// <summary>
        /// 申请数量
        /// </summary>       
        [DataMember]
        public decimal Amount { get; set; }
        /// <summary>
        /// 利息
        /// </summary>       
        [DataMember]
        public decimal Interest { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>       
        [DataMember]
        public string PayType { get; set; }
        /// <summary>
        /// 状态（0 未匹配 1 部分匹配 2 全部成交 3 已撤销 ）
        /// </summary>       
        [DataMember]
        public int HStatus { get; set; }
        /// <summary>
        /// 已匹配金额
        /// </summary>       
        [DataMember]
        public decimal MatchedAmount { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>       
        [DataMember]
        public int SortIndex { get; set; } 
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string HStatusName { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        #endregion
    }
}
