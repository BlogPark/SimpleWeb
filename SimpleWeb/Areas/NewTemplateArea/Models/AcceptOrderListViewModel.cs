using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    public class AcceptOrderListViewModel
    {
        public PagedList<AcceptHelpOrderModel> orderlist { get; set; }

        public AcceptHelpOrderModel seachmodel { get; set; }
    }
}