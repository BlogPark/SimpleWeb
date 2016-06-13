using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 会员推荐关系表
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReMemberRelationModel
    {
        /// <summary>
        /// 主键自增
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>       
        [DataMember]
        public int MemberID { get; set; }
        /// <summary>
        /// 会员手机
        /// </summary>       
        [DataMember]
        public string MemberPhone { get; set; }
        /// <summary>
        /// 会员真实姓名
        /// </summary>       
        [DataMember]
        public string MemberTruthName { get; set; }
        /// <summary>
        /// 推荐会员ID
        /// </summary>       
        [DataMember]
        public int RecommMID { get; set; }
        /// <summary>
        /// 推荐会员手机
        /// </summary>       
        [DataMember]
        public string RecommMPhone { get; set; }
        /// <summary>
        /// 推荐会员真实名字
        /// </summary>       
        [DataMember]
        public string RecommMTruthName { get; set; }
        /// <summary>
        /// 状态（1 活动 0 禁用）
        /// </summary>       
        [DataMember]
        public int RStatus { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }

    }

    [Serializable]
    [DataContract]
    public class Treemodel
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public SubTreemodel additionalParameters{get;set;}
    }
    [Serializable]
    [DataContract]
    public class SubTreemodel
    {
        [DataMember]
        public List<Treemodel> children { get; set; }
    }
}
