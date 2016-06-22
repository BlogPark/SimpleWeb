using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    /// <summary>
    ///系统公告管理页面Model
    /// </summary>
    [DataContract]
    public class SiteMsgIndexViewModel
    {
        [DataMember]
        public List<AdminSiteNewsModel> modellist { get; set; }
        [DataMember]
        public AdminSiteNewsModel addmodel { get; set; }
    }
}