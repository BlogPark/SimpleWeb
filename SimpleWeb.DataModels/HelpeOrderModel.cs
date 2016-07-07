using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 提供帮助列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class HelpeOrderModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// ID
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _ordercode;
        /// <summary>
        /// 单据编号
        /// </summary>		
        [DataMember]
        public string OrderCode
        {
            get { return _ordercode; }
            set { _ordercode = value; }
        }
        private int _memberid;
        /// <summary>
        /// 会员ID
        /// </summary>		
        [DataMember]
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        private string _memberphone;
        /// <summary>
        /// 会员电话
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private string _membername;
        /// <summary>
        /// 会员名称
        /// </summary>		
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        private decimal _amount;
        /// <summary>
        /// 申请数量
        /// </summary>		
        [DataMember]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private decimal _interest;
        /// <summary>
        /// 利息
        /// </summary>		
        [DataMember]
        public decimal Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }
        private string _paytype;
        /// <summary>
        /// 支付方式
        /// </summary>		
        [DataMember]
        public string PayType
        {
            get { return _paytype; }
            set { _paytype = value; }
        }
        private int _hstatus=-10;
        /// <summary>
        /// 状态（0 未匹配 1 部分匹配 2 全部匹配  3 已撤销  4 已打款 5 已确认）
        /// </summary>		
        [DataMember]
        public int HStatus
        {
            get { return _hstatus; }
            set { _hstatus = value; }
        }
        private decimal _matchedamount;
        /// <summary>
        /// 已匹配金额
        /// </summary>		
        [DataMember]
        public decimal MatchedAmount
        {
            get { return _matchedamount; }
            set { _matchedamount = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 添加时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _sortindex;
        /// <summary>
        /// 排序字段
        /// </summary>		
        [DataMember]
        public int SortIndex
        {
            get { return _sortindex; }
            set { _sortindex = value; }
        }
        private int _ostatus;
        /// <summary>
        /// 单据状态（1 新提交  2 已打款  3 已完成）
        /// </summary>		
        [DataMember]
        public int OStatus
        {
            get { return _ostatus; }
            set { _ostatus = value; }
        }
        private string _activecode;
        /// <summary>
        /// 使用激活码
        /// </summary>		
        [DataMember]
        public string ActiveCode
        {
            get { return _activecode; }
            set { _activecode = value; }
        }
        private decimal _currentinterest;
        /// <summary>
        /// 当前利率
        /// </summary>
        [DataMember]
        public decimal Currentinterest
        {
            get { return _currentinterest; }
            set { _currentinterest = value; }
        }

        private DateTime _LastUpdateTime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]

        public DateTime LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set { _LastUpdateTime = value; }
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string HStatusName { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }
        /// <summary>
        /// 差异天数
        /// </summary>
        [DataMember]
        public int DiffDay { get; set; }
        /// <summary>
        /// 激活码ID
        /// </summary>
        [DataMember]
        public int ActiveCodeID { get; set; }
        /// <summary>
        /// 剩余匹配金额
        /// </summary>
        [DataMember]
        public decimal DiffAmount { get; set; }
        #endregion
    }
}
