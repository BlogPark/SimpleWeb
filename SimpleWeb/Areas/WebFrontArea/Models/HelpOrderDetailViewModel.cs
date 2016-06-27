using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class HelpOrderDetailViewModel
    {
        public HelpeOrderModel ordermodel { get; set; }

        public MemberInfoModel memberinfo { get; set; }
    }
}