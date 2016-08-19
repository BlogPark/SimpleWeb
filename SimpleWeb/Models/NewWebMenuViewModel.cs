using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWeb.Models
{
    public class NewWebMenuViewModel
    {
        /// <summary>
        /// 登陆人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆人手机
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 静态资金
        /// </summary>
        public decimal StaticMoney { get; set; }
        /// <summary>
        /// 动态资金
        /// </summary>
        public decimal DynamicMoney { get; set; }
        /// <summary>
        /// 激活码个数
        /// </summary>
        public int ActionCodeCount { get; set; }
        /// <summary>
        /// 排单币个数
        /// </summary>
        public int CurrencyCodeCount { get; set; }
        /// <summary>
        /// 团队人数
        /// </summary>
        public int TeamPersonCount { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string linkurl { get; set; }
    }
}