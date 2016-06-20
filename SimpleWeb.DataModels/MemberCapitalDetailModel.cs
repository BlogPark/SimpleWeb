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
        /// 静态资金
        /// </summary>		
        private decimal _staticcapital;
        [DataMember]
        public decimal StaticCapital
        {
            get { return _staticcapital; }
            set { _staticcapital = value; }
        }
        /// <summary>
        /// 动态资金
        /// </summary>		
        private decimal _dynamicfunds;
        [DataMember]
        public decimal DynamicFunds
        {
            get { return _dynamicfunds; }
            set { _dynamicfunds = value; }
        }
        /// <summary>
        /// 静态利息
        /// </summary>		
        private decimal _staticinterest;
        [DataMember]
        public decimal StaticInterest
        {
            get { return _staticinterest; }
            set { _staticinterest = value; }
        }
        /// <summary>
        /// 动态利息
        /// </summary>		
        private decimal _dynamicinterest;
        [DataMember]
        public decimal DynamicInterest
        {
            get { return _dynamicinterest; }
            set { _dynamicinterest = value; }
        }
        /// <summary>
        /// 静态惩罚金额
        /// </summary>		
        private decimal _staticpunishmoney;
        [DataMember]
        public decimal StaticPunishMoney
        {
            get { return _staticpunishmoney; }
            set { _staticpunishmoney = value; }
        }
        /// <summary>
        /// 动态惩罚金额
        /// </summary>		
        private decimal _dynamicpunishmoney;
        [DataMember]
        public decimal DynamicPunishMoney
        {
            get { return _dynamicpunishmoney; }
            set { _dynamicpunishmoney = value; }
        }
        /// <summary>
        /// 静态冻结金额
        /// </summary>		
        private decimal _staticfreezemoney;
        [DataMember]
        public decimal StaticFreezeMoney
        {
            get { return _staticfreezemoney; }
            set { _staticfreezemoney = value; }
        }
        /// <summary>
        /// 动态冻结金额
        /// </summary>		
        private decimal _dynamicfreezemoney;
        [DataMember]
        public decimal DynamicFreezeMoney
        {
            get { return _dynamicfreezemoney; }
            set { _dynamicfreezemoney = value; }
        }
        /// <summary>
        /// 静态总金额
        /// </summary>		
        private decimal _totalstaticcapital;
        [DataMember]
        public decimal TotalStaticCapital
        {
            get { return _totalstaticcapital; }
            set { _totalstaticcapital = value; }
        }
        /// <summary>
        /// 动态总金额
        /// </summary>		
        private decimal _totaldynamicfunds;
        [DataMember]
        public decimal TotalDynamicFunds
        {
            get { return _totaldynamicfunds; }
            set { _totaldynamicfunds = value; }
        }
        /// <summary>
        /// 当前利率
        /// </summary>		
        private decimal _interest;
        [DataMember]
        public decimal Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }        
    }
}
