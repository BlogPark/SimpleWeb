using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    public class HelpOrderListViewModel
    {
        public List<HelpeOrderModel> helporderlist { get; set; }

        public PagedList<HelpeOrderModel> orderlist { get; set; }

        public HelpeOrderModel seachmodel { get; set; }
    }
}