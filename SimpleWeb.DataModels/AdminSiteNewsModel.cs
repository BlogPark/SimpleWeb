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
    public class AdminSiteNewsModel
    {
        #region 原表字段
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>       
        [DataMember]
        public string STitle { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>       
        [DataMember]
        public string SContent { get; set; }
        /// <summary>
        /// 发送人ID
        /// </summary>       
        [DataMember]
        public int SendUserID { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>       
        [DataMember]
        public string SendUserName { get; set; }
        /// <summary>
        /// 接收人ID
        /// </summary>       
        [DataMember]
        public int ReceiveUserID { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>       
        [DataMember]
        public string ReceiveUserName { get; set; }
        /// <summary>
        /// 状态值（1 发布 2 已阅 3 删除）
        /// </summary>       
        [DataMember]
        public int SStatus { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime SAddTime { get; set; }
        /// <summary>
        /// 是否紧急
        /// </summary>       
        [DataMember]
        public int IsUrgent { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>       
        [DataMember]
        public int IsTop { get; set; }        
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态值（1 发布 2 已阅 3 删除）
        /// </summary>       
        [DataMember]
        public string SStatusName { get; set; }
        #endregion
    }
}
