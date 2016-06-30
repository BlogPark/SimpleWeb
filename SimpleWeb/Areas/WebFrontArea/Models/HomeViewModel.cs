using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class HomeViewModel
    {
        public WebIndexModel data { get; set; }

        public MemberInfoModel member { get; set; }

        public ReMemberRelationModel recommend { get; set; }
    }
}