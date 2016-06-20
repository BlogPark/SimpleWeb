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
    public class MemberExtendInfoModel
    {
        /// <summary>
        /// 会员ID
        /// </summary>		
        private int _memberid;
        [DataMember]
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        /// <summary>
        /// 上次帮助时间
        /// </summary>		
        private DateTime _lasthelpertime;
        [DataMember]
        public DateTime LastHelperTime
        {
            get { return _lasthelpertime; }
            set { _lasthelpertime = value; }
        }
        /// <summary>
        /// 会员累计帮助次数
        /// </summary>		
        private int _memberhelpcount;
        [DataMember]
        public int MemberHelpCount
        {
            get { return _memberhelpcount; }
            set { _memberhelpcount = value; }
        }
        /// <summary>
        /// 上次帮助金额
        /// </summary>		
        private decimal _lasthelpmoney;
        [DataMember]
        public decimal LastHelpMoney
        {
            get { return _lasthelpmoney; }
            set { _lasthelpmoney = value; }
        }        
    }
}
