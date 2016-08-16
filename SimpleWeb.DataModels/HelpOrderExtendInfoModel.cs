using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    [Serializable]
    [DataContract]
    public  class HelpOrderExtendInfoModel
    {
        [DataMember]
        public int helporderid { get; set; }
        [DataMember]
        public string helpordercode { get; set; }
        [DataMember]
        public decimal MatchedMoney { get; set; }
        [DataMember]
        public int helpmemberid { get; set; }
        [DataMember]
        public string helpmemberName { get; set; }
        [DataMember]
        public string helpmemberPhone { get; set; }
        [DataMember]
        public int rememberid { get; set; }
        [DataMember]
        public string remembername { get; set; }
        [DataMember]
        public string rememberphone { get; set; }
        [DataMember]
        public string helpmemberAlipayId { get; set; }
        [DataMember]
        public string helpmemberAlipayName { get; set; }
        [DataMember]
        public string helpmemberweixin { get; set; }
        [DataMember]
        public string HStatusName { get; set; }
        [DataMember]
        public int HStatus { get; set; }
        /// <summary>
        /// 匹配单据状态
        /// </summary>
        [DataMember]
        public int MatchStatus { get; set; }
        /// <summary>
        /// 匹配单据状态名称
        /// </summary>
        [DataMember]
        public string MatchStatusName { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 会员打款时间
        /// </summary>
        [DataMember]
        public DateTime PaymentedTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime CompleteTime { get; set; }
    }
}
