using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class webLoginViewModel
    {
        public MemberInfoModel member { get; set; }

        public string returnurl;
    }
}