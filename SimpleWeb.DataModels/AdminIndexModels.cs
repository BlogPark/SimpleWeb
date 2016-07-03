using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    [Serializable]
    [DataContract]
    public class AdminIndexModels
    {
        /// <summary>
        /// 系统总会员数
        /// </summary>
        public int TotalMemberCount { get; set; }
        /// <summary>
        /// 系统活动会员数
        /// </summary>
        public int ActiveMemberCount { get; set; }
        /// <summary>
        /// 系统总共提供的帮助金额
        /// </summary>
        public decimal TotalHelpAmont { get; set; }
        /// <summary>
        /// 系统总共接受帮助的金额
        /// </summary>
        public decimal TotalAcceptAmont { get; set; }
        /// <summary>
        /// 系统的总共激活码数量
        /// </summary>
        public int ActiveCodeCount { get; set; }
        /// <summary>
        /// 系统总共的拍单码数量
        /// </summary>
        public int PaidanCodeCount { get; set; }
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string AdminuserName { get; set; }

       
    }
}
