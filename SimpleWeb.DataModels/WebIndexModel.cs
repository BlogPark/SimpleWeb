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
    public class WebIndexModel
    {
        /// <summary>
        /// 最近的激活币和排单币的使用情况
        /// </summary>
        [DataMember]
        public List<ActiveCodeLogModel> activecodelog { get; set; }
        /// <summary>
        /// 我的激活币个数
        /// </summary>
        [DataMember]
        public int activecodeCount { get; set; }
        /// <summary>
        /// 我的排单币个数
        /// </summary>
        [DataMember]
        public int paidancodeCount { get; set; }
        /// <summary>
        /// 我最近提供的帮助
        /// </summary>
        [DataMember]
        public List<HelpeOrderModel> helperOrders { get; set; }
        /// <summary>
        /// 我最近接受的帮助
        /// </summary>
        [DataMember]
        public List<AcceptHelpOrderModel> acceptOrders { get; set; }

        /// <summary>
        /// 我的资金情况
        /// </summary>
        [DataMember]
        public MemberCapitalDetailModel zijinmodel { get; set; }
        /// <summary>
        /// 我的团队人数
        /// </summary>
        [DataMember]
        public int members { get; set; }
        /// <summary>
        /// 我的资金变动最新情况
        /// </summary>
        [DataMember]
        public List<AmountChangeLogModel> AmontChangLog { get; set; }
    }
}
