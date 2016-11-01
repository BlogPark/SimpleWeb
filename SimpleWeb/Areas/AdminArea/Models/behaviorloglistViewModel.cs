using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class behaviorloglistViewModel
    {
        [DataMember]
        public UserBehaviorLogModel seachmodel { get; set; }
        [DataMember]
        public PagedList<UserBehaviorLogModel> list { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        [DataMember]
        public int totalcount { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        [DataMember]
        public int currentpage { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int pagesize { get; set; }
    }
}