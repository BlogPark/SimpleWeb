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
    public class MemberActiveCodeModel
    {
        #region 原始字段
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>       
        [DataMember]
        public string ActiveCode { get; set; }
        /// <summary>
        /// 激活码类型（1 激活码 2 排单码）
        /// </summary>       
        [DataMember]
        public int AMType { get; set; }
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
        /// 激活码使用状态(1 未使用 2 已使用 3 已过期)
        /// </summary>       
        [DataMember]
        public int AMStatus { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime Addtime { get; set; }

        private string _UseCode = "";
        /// <summary>
        /// 使用单号/用户名
        /// </summary>
        [DataMember]
        public string UseCode
        {
            get { return _UseCode; }
            set { _UseCode = value; }
        }

        private DateTime _UserTime;
        /// <summary>
        /// 使用时间
        /// </summary>
        [DataMember]
        public DateTime UserTime
        {
            get { return _UserTime; }
            set { _UserTime = value; }
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string AMStatusName { get; set; }
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
        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember]
        public string AMTypeName { get; set; }
        #endregion
    }
}
