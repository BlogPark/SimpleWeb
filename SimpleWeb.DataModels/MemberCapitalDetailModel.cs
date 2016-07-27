using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 会员资金明细
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberCapitalDetailModel
    {
       
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
        private string _MemberPhone;
        [DataMember]
        public string MemberPhone
        {
            get { return _MemberPhone; }
            set { _MemberPhone = value; }
        }
        private string _MemberName;
        [DataMember]
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
      		
        private decimal _staticcapital;
        /// <summary>
        /// 静态资金
        /// </summary>
        [DataMember]
        public decimal StaticCapital
        {
            get { return _staticcapital; }
            set { _staticcapital = value; }
        }
       	
        private decimal _dynamicfunds;
        /// <summary>
        /// 动态资金
        /// </summary>	
        [DataMember]
        public decimal DynamicFunds
        {
            get { return _dynamicfunds; }
            set { _dynamicfunds = value; }
        }
       	
        private decimal _staticinterest;
        /// <summary>
        /// 静态利息
        /// </summary>	
        [DataMember]
        public decimal StaticInterest
        {
            get { return _staticinterest; }
            set { _staticinterest = value; }
        }
       	
        private decimal _dynamicinterest;
        /// <summary>
        /// 动态利息
        /// </summary>	
        [DataMember]
        public decimal DynamicInterest
        {
            get { return _dynamicinterest; }
            set { _dynamicinterest = value; }
        }
       
        private decimal _staticpunishmoney;
        /// <summary>
        /// 静态惩罚金额
        /// </summary>		
        [DataMember]
        public decimal StaticPunishMoney
        {
            get { return _staticpunishmoney; }
            set { _staticpunishmoney = value; }
        }
      
        private decimal _dynamicpunishmoney;
        /// <summary>
        /// 动态惩罚金额
        /// </summary>		
        [DataMember]
        public decimal DynamicPunishMoney
        {
            get { return _dynamicpunishmoney; }
            set { _dynamicpunishmoney = value; }
        }
        	
        private decimal _staticfreezemoney;
        /// <summary>
        /// 静态冻结金额
        /// </summary>	
        [DataMember]
        public decimal StaticFreezeMoney
        {
            get { return _staticfreezemoney; }
            set { _staticfreezemoney = value; }
        }
        	
        private decimal _dynamicfreezemoney;
        /// <summary>
        /// 动态冻结金额
        /// </summary>	
        [DataMember]
        public decimal DynamicFreezeMoney
        {
            get { return _dynamicfreezemoney; }
            set { _dynamicfreezemoney = value; }
        }
        		
        private decimal _totalstaticcapital;
        /// <summary>
        /// 静态总金额
        /// </summary>
        [DataMember]
        public decimal TotalStaticCapital
        {
            get { return _totalstaticcapital; }
            set { _totalstaticcapital = value; }
        }
       
        private decimal _totaldynamicfunds;
        /// <summary>
        /// 动态总金额
        /// </summary>		
        [DataMember]
        public decimal TotalDynamicFunds
        {
            get { return _totaldynamicfunds; }
            set { _totaldynamicfunds = value; }
        }
       	
        private decimal _interest;
        /// <summary>
        /// 当前利率
        /// </summary>	
        [DataMember]
        public decimal Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }
        #region 扩展字段
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
        #endregion
    }
}
