using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    [Serializable]
    [DataContract]
    public class MemberNewsViewModel
    {
        public List<AdminSiteNewsModel> news { get; set; }
    }
}