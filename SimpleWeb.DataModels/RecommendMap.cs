using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
   
    public class RecommendMap
    {
        /// <summary>
        /// 被推荐会员ID
        /// </summary>
      
        public int id { get; set; }
        /// <summary>
        /// 推荐人ID
        /// </summary>
      
        public int pid { get; set; }
        /// <summary>
        /// 下属数量
        /// </summary>
       
        public int childcount { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        
        public string name { get; set; }
        /// <summary>
        /// 是否主项
        /// </summary>
       
        public bool isParent { get; set; }
    }
}
