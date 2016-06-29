using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class mycapitalViewModel
    {
        public MemberCapitalDetailModel mycapitalinfo { get; set; }
        /// <summary>
        /// 最高接受帮助金额
        /// </summary>
        public string maxacceptamont { get; set; }
        /// <summary>
        /// 最低接受帮助金额
        /// </summary>
        public string minacceptamont { get; set; }
        /// <summary>
        /// 最高提供帮助金额
        /// </summary>
        public string maxhelpamont { get; set; }
        /// <summary>
        /// 最低提供帮助金额
        /// </summary>
        public string minhelpamont { get; set; }

    }
}