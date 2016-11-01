using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 匹配单据扩展信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ExtendMatchOrdersModel
    {
        /// <summary>
        /// 提供单据ID
        /// </summary>
        [DataMember]
        public int HelperOrderID { get; set; }
        /// <summary>
        /// 提供单据编号
        /// </summary>
        [DataMember]
        public string HelperOrderCode { get; set; }
        /// <summary>
        /// 接受单据ID
        /// </summary>
        [DataMember]
        public int AcceptOrderID { get; set; }
        /// <summary>
        /// 接受单据编号
        /// </summary>
        [DataMember]
        public string AcceptOrderCode { get; set; }
        /// <summary>
        /// 匹配ID
        /// </summary>
        [DataMember]
        public int MatchID { get; set; }
        /// <summary>
        /// 匹配金额
        /// </summary>
        [DataMember]
        public decimal MatchedMoney { get; set; }
        /// <summary>
        /// 匹配状态
        /// </summary>
        [DataMember]
        public string MatchStatusName { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [DataMember]
        public string PayType { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        [DataMember]
        public DateTime PaymentedTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        [DataMember]
        public DateTime CompleteTime { get; set; }
        /// <summary>
        /// 匹配状态
        /// </summary>
        [DataMember]
        public int MatchStatus { get; set; }
        /// <summary>
        /// 单据添加时间
        /// </summary>
        [DataMember]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 单据匹配时间
        /// </summary>
        [DataMember]
        public DateTime MatchTime { get; set; }
    }
}
