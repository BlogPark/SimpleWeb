using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    public class paymentinteristViewModel
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        public PagedList<AmountChangeLogModel> logs { get; set; }

        public string smsid { get; set; }
    }
}