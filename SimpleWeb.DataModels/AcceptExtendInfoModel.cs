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
    public class AcceptExtendInfoModel
    {
        /// <summary>
        /// 接受单据ID
        /// </summary>
        [DataMember]
        public int acceptorderid { get; set; }
        /// <summary>
        /// 接受单据编号
        /// </summary>
        [DataMember]
        public string acceptordercode { get; set; }
        /// <summary>
        /// 匹配金额
        /// </summary>
        [DataMember]
        public decimal MatchedMoney { get; set; }
        /// <summary>
        /// 接受会员id
        /// </summary>
        [DataMember]
        public int acceptmemberid { get; set; }
        /// <summary>
        /// 接受会员名字
        /// </summary>
        [DataMember]
        public string acceptmemberName { get; set; }
        /// <summary>
        /// 接受会员电话
        /// </summary>
        [DataMember]
        public string acceptmemberPhone { get; set; }
        /// <summary>
        /// 推荐会员ID
        /// </summary>
        [DataMember]
        public int rememberid { get; set; }
        /// <summary>
        /// 推荐会员名字
        /// </summary>
        [DataMember]
        public string remembername { get; set; }
        /// <summary>
        /// 推荐会员电话
        /// </summary>
        [DataMember]
        public string rememberphone { get; set; }
        /// <summary>
        /// 接受会员支付宝
        /// </summary>
        [DataMember]
        public string acceptmemberAlipayId { get; set; }
        /// <summary>
        /// 接受会员支付宝
        /// </summary>
        [DataMember]
        public string acceptmemberAlipayName { get; set; }
        /// <summary>
        /// 接受会员微信
        /// </summary>
        [DataMember]
        public string acceptmemberweixin { get; set; }
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
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public DateTime LastUpdateTime { get; set; }
    }
}
