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
    public class MatchOrderModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 帮助订单ID
        /// </summary>		
        private int _helperorderid;
        [DataMember]
        public int HelperOrderID
        {
            get { return _helperorderid; }
            set { _helperorderid = value; }
        }
        /// <summary>
        /// 帮助订单编号
        /// </summary>		
        private string _helperordercode;
        [DataMember]
        public string HelperOrderCode
        {
            get { return _helperordercode; }
            set { _helperordercode = value; }
        }
        /// <summary>
        /// 帮助会员ID
        /// </summary>		
        private int _helpermemberid;
        [DataMember]
        public int HelperMemberID
        {
            get { return _helpermemberid; }
            set { _helpermemberid = value; }
        }
        /// <summary>
        /// 接受订单ID
        /// </summary>		
        private int _acceptorderid;
        [DataMember]
        public int AcceptOrderID
        {
            get { return _acceptorderid; }
            set { _acceptorderid = value; }
        }
        /// <summary>
        /// 接受订单编号
        /// </summary>		
        private string _acceptordercode;
        [DataMember]
        public string AcceptOrderCode
        {
            get { return _acceptordercode; }
            set { _acceptordercode = value; }
        }
        /// <summary>
        /// 接受会员ID
        /// </summary>		
        private int _acceptmemberid;
        [DataMember]
        public int AcceptMemberID
        {
            get { return _acceptmemberid; }
            set { _acceptmemberid = value; }
        }
        /// <summary>
        /// 匹配金额
        /// </summary>		
        private decimal _matchedmoney;
        [DataMember]
        public decimal MatchedMoney
        {
            get { return _matchedmoney; }
            set { _matchedmoney = value; }
        }
        /// <summary>
        /// 匹配时间
        /// </summary>		
        private DateTime _matchtime;
        [DataMember]
        public DateTime MatchTime
        {
            get { return _matchtime; }
            set { _matchtime = value; }
        }

        private int _MatchStatus;
        /// <summary>
        /// 匹配状态（ 1 匹配 2 取消  3 已打款 4 已确认）
        /// </summary>
        [DataMember]
        public int MatchStatus
        {
            get { return _MatchStatus; }
            set { _MatchStatus = value; }
        }
    }
}
