using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class ContactUsViewModel
    {
        /// <summary>
        /// 反馈列表
        /// </summary>
        public List<WebContactMessageModel> list { get; set; }
        /// <summary>
        /// 反馈信息明细
        /// </summary>
        public WebContactMessageModel message { get; set; }
    }
}