using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 会员资金明细
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCapitalDetailModel
    {
        /// <summary>
        /// 会员ID
        /// </summary>       
        [DataMember]
        public int MemberID { get; set; }
        /// <summary>
        /// 静态资金
        /// </summary>       
        [DataMember]
        public decimal StaticCapital { get; set; }
        /// <summary>
        /// 动态资金
        /// </summary>       
        [DataMember]
        public decimal DynamicFunds { get; set; }
        /// <summary>
        /// 静态利息
        /// </summary>       
        [DataMember]
        public decimal StaticInterest { get; set; }
        /// <summary>
        /// 动态利息
        /// </summary>       
        [DataMember]
        public decimal DynamicInterest { get; set; }
        /// <summary>
        /// 静态惩罚金额
        /// </summary>       
        [DataMember]
        public decimal StaticPunishMoney { get; set; }
        /// <summary>
        /// 动态惩罚金额
        /// </summary>       
        [DataMember]
        public decimal DynamicPunishMoney { get; set; }
        /// <summary>
        /// 静态冻结金额
        /// </summary>       
        [DataMember]
        public decimal StaticFreezeMoney { get; set; }
        /// <summary>
        /// 动态冻结金额
        /// </summary>       
        [DataMember]
        public decimal DynamicFreezeMoney { get; set; }
        /// <summary>
        /// 静态总金额
        /// </summary>       
        [DataMember]
        public decimal TotalStaticCapital { get; set; }
        /// <summary>
        /// 动态总金额
        /// </summary>       
        [DataMember]
        public decimal TotalDynamicFunds { get; set; }
    }
}
