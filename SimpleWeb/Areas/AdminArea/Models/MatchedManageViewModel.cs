using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Models
{
    public class MatchedManageViewModel
    {
        public PagedList<AcceptHelpOrderModel> acceptorderlist { get; set; }
        public PagedList<HelpeOrderModel> helporderlist { get; set; }
    }
}