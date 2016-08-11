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
    public  class LeaderAmountModel
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
        /// 单据ID
        /// </summary>		
        private int _orderid;
        [DataMember]
        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>		
        private string _ordercode;
        [DataMember]
        public string OrderCode
        {
            get { return _ordercode; }
            set { _ordercode = value; }
        }
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
        /// 会员名字
        /// </summary>		
        private string _membername;
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        /// <summary>
        /// 会员电话
        /// </summary>		
        private string _memberphone;
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>		
        private decimal _amount;
        [DataMember]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _ltype;
        /// <summary>
        /// 类型（0 领导奖 1 推荐奖）
        /// </summary>
        [DataMember]
        public int Ltype
        {
            get { return _ltype; }
            set { _ltype = value; }
        }
    }
}
