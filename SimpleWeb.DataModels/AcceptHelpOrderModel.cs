using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 接受帮助单据
    /// </summary>
    [Serializable]
    [DataContract]
    public class AcceptHelpOrderModel
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
        /// 申请金额
        /// </summary>		
        [DataMember]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private int _sourcetype;
        /// <summary>
        /// 来源（1 静态资金 2 动态资金）
        /// </summary>		
        [DataMember]
        public int SourceType
        {
            get { return _sourcetype; }
            set { _sourcetype = value; }
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
        private string _turnoutorder;
        /// <summary>
        /// 对应订单
        /// </summary>		
        [DataMember]
        public string TurnOutOrder
        {
            get { return _turnoutorder; }
            set { _turnoutorder = value; }
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
        private int _astatus;
        /// <summary>
        /// 状态（0 未匹配 1 部分匹配 2 全部完成 3 已撤销）
        /// </summary>		
        [DataMember]
        public int AStatus
        {
            get { return _astatus; }
            set { _astatus = value; }
        }
        private int _sortindex;
        /// <summary>
        /// 排序
        /// </summary>		
        [DataMember]
        public int SortIndex
        {
            get { return _sortindex; }
            set { _sortindex = value; }
        }  
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string AStatusName { get; set; }
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
        public int DissDay { get; set; }
        #endregion
    }
}
