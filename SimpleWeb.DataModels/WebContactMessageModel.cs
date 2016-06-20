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
    public class WebContactMessageModel
    {
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
        /// 留言标题
        /// </summary>       
        [DataMember]
        public string MessageTitle { get; set; }
        /// <summary>
        /// 留言内容
        /// </summary>       
        [DataMember]
        public string MessageContent { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 留言状态（1 提问 2 已回复 3 删除）
        /// </summary>       
        [DataMember]
        public int MStatus { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>       
        [DataMember]
        public string ReplyContent { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>       
        [DataMember]
        public DateTime ReplyTime { get; set; }

    }
}
