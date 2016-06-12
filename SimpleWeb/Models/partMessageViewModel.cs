using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Models
{
    [Serializable]
    [DataContract]
    public class partMessageViewModel
    {
        /// <summary>
        /// 未读消息列表
        /// </summary>
        [DataMember]
        public List<AdminSiteNewsModel> newmsglist { get; set; }
        /// <summary>
        /// 已读消息列表
        /// </summary>
        [DataMember]
        public List<AdminSiteNewsModel> oldmsglist { get; set; }
        /// <summary>
        /// 已读消息列表
        /// </summary>
        [DataMember]
        public int newcount { get; set; }
    }
}