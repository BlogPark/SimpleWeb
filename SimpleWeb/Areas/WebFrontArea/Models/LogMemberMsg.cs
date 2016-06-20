using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    public class LogMemberMsg
    {
        private string _MemberName;
        /// <summary>
        /// 会员名称
        /// </summary>
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        private string _MemberPhone;
        /// <summary>
        /// 会员电话
        /// </summary>
        public string MemberPhone
        {
            get { return _MemberPhone; }
            set { _MemberPhone = value; }
        }
        private int _MemberID;
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
    }
}