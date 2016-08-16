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
        #region 原始字段
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
        private string _helpermembername;
        /// <summary>
        /// 提供帮助会员名字
        /// </summary>		
        [DataMember]
        public string HelperMemberName
        {
            get { return _helpermembername; }
            set { _helpermembername = value; }
        }
        private string _helpermemberphone;
        /// <summary>
        /// 提供帮助会员电话
        /// </summary>		
        [DataMember]
        public string HelperMemberPhone
        {
            get { return _helpermemberphone; }
            set { _helpermemberphone = value; }
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
        private string _acceptmembername;
        /// <summary>
        /// 接受会员名字
        /// </summary>		
        [DataMember]
        public string AcceptMemberName
        {
            get { return _acceptmembername; }
            set { _acceptmembername = value; }
        }
        private string _acceptmemberphone;
        /// <summary>
        /// 接受会员电话
        /// </summary>		
        [DataMember]
        public string AcceptMemberPhone
        {
            get { return _acceptmemberphone; }
            set { _acceptmemberphone = value; }
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

        private int _MatchStatus = 0;
        /// <summary>
        /// 匹配状态（ 1 匹配 2 取消  3 已打款 4 已确认）
        /// </summary>
        [DataMember]
        public int MatchStatus
        {
            get { return _MatchStatus; }
            set { _MatchStatus = value; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>		
        private DateTime _lastupdatetime;
        [DataMember]
        public DateTime LastUpdateTime
        {
            get { return _lastupdatetime; }
            set { _lastupdatetime = value; }
        }
        private int _isdebitovertime;
        /// <summary>
        /// 是否已经超时扣款
        /// </summary>		
        [DataMember]
        public int IsDebitOverTime
        {
            get { return _isdebitovertime; }
            set { _isdebitovertime = value; }
        }
        private DateTime _paymentedtime;
        /// <summary>
        /// 会员打款时间
        /// </summary>		
        [DataMember]
        public DateTime PaymentedTime
        {
            get { return _paymentedtime; }
            set { _paymentedtime = value; }
        }
        private DateTime _completetime;
        /// <summary>
        /// 单据确认时间
        /// </summary>		
        [DataMember]
        public DateTime CompleteTime
        {
            get { return _completetime; }
            set { _completetime = value; }
        }        
        #endregion

        #region 扩展字段
        /// <summary>
        /// 页索引
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// 匹配状态名称
        /// </summary>
        [DataMember]
        public string MatchStatusName { get; set; }
        #endregion
    }
}
