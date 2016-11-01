using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 等待释放领导奖金
    /// </summary>
    [Serializable]
    [DataContract]
    public class WaitFreeLeaderAmountModel
    {

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
        private string _membername;
        /// <summary>
        /// MemberName
        /// </summary>		
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        private string _memberphone;
        /// <summary>
        /// 会员手机
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private decimal _amount;
        /// <summary>
        /// Amount
        /// </summary>		
        [DataMember]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private int _type;
        /// <summary>
        /// 类型（1 领导奖 2 推荐奖）
        /// </summary>		
        [DataMember]
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private int _astatus;
        /// <summary>
        /// 状态（1 待释放 2 已释放 3 单据取消不能释放）
        /// </summary>		
        [DataMember]
        public int AStatus
        {
            get { return _astatus; }
            set { _astatus = value; }
        }
        private DateTime _theoryfreetime;
        /// <summary>
        /// 理论释放时间
        /// </summary>		
        [DataMember]
        public DateTime TheoryFreeTime
        {
            get { return _theoryfreetime; }
            set { _theoryfreetime = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 添加时间
        /// </summary>		
        [DataMember]
        public DateTime Addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private string _ordercode;
        /// <summary>
        /// 理论释放时间
        /// </summary>		
        [DataMember]
        public string OrderCode
        {
            get { return _ordercode; }
            set { _ordercode = value; }
        }
        private int _orderid;
        /// <summary>
        /// 添加时间
        /// </summary>		
        [DataMember]
        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }    
    }
}
