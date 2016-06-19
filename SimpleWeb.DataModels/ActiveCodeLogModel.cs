using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 激活码操作记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class ActiveCodeLogModel
    {
        #region 原有字段
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        [DataMember]
        public int MemberID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        [DataMember]
        public string MemberName { get; set; }
        /// <summary>
        /// 会员电话
        /// </summary>
        [DataMember]
        public string MemberPhone { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>
        [DataMember]
        public string ActiveCode { get; set; }
        /// <summary>
        /// 激活码ID
        /// </summary>
        [DataMember]
        public int AID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DataMember]
        public DateTime Addtime { get; set; }

        #endregion
    }
}
