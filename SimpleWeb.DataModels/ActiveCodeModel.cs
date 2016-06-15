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
    public class ActiveCodeModel
    {
        #region 原始字段
        /// <summary>
        /// ID
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>       
        [DataMember]
        public string ActivationCode { get; set; }
        /// <summary>
        /// 类型（1 激活账户  2 排单使用）
        /// </summary>       
        [DataMember]
        public int AType { get; set; }
        /// <summary>
        /// 状态（20 未使用 15 已分配 10 已使用）
        /// </summary>       
        [DataMember]
        public int AStatus { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; } 
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string MStatusName { get; set; }
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
