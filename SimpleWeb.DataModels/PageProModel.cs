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
    public class PageProModel
    {
        /// <summary>
        /// 需要查询的列
        /// </summary>
        [DataMember]
        public string colums { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        [DataMember]
        public string where { get; set; }
        /// <summary>
        /// 查询的页索引
        /// </summary>
        [DataMember]
        public int pageindex { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int pagesize { get; set; }
        /// <summary>
        /// 查询的表名
        /// </summary>
        [DataMember]
        public string tablename { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [DataMember]
        public string orderby { get; set; }
    }
}
