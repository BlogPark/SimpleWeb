using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 用户行为日志
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserBehaviorLogModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
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
        /// 发生IP
        /// </summary>       
        [DataMember]
        public string HappenIP { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>       
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }
    }
    /// <summary>
    /// 资金变动日志
    /// </summary>
    [Serializable]
    [DataContract]
    public class AmountChangeLogModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
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
        /// 发生金额
        /// </summary>       
        [DataMember]
        public decimal ProduceMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>       
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }

    }


}
