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
    public class RecommendMap
    {
        /// <summary>
        /// 被推荐会员ID
        /// </summary>
        [DataMember]
        public int id { get; set; }
        /// <summary>
        /// 推荐人ID
        /// </summary>
        [DataMember]
        public int pid { get; set; }
        /// <summary>
        /// 下属数量
        /// </summary>
        [DataMember]
        public int childcount { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// 是否主项
        /// </summary>
        [DataMember]
        public bool isParent { get; set; }
    }
}
