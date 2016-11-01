using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class AcceptOrderViewModel
    {
        /// <summary>
        /// 提供帮助人信息
        /// </summary>
        public MemberInfoModel acceptpeople { get; set; }
        /// <summary>
        /// 接受帮助单据信息
        /// </summary>
        public List<HelpOrderExtendInfoModel> helpOrderInfo { get; set; }
        /// <summary>
        /// 提供帮助单据信息
        /// </summary>
        public AcceptHelpOrderModel acceptorder { get; set; }
    }
}