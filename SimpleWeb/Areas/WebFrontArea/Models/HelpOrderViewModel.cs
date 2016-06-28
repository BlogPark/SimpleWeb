using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class HelpOrderViewModel
    {
        /// <summary>
        /// 提供帮助人信息
        /// </summary>
        public MemberInfoModel helperpeople { get; set; }
        /// <summary>
        /// 接受帮助单据信息
        /// </summary>
        public List<AcceptExtendInfoModel> acceptOrderInfo { get; set; }
        /// <summary>
        /// 提供帮助单据信息
        /// </summary>
        public HelpeOrderModel helporder { get; set; }
    }
}