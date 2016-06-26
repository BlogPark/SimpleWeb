using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 用户行为日志
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserBehaviorLogModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>       
        [DataMember]
        public int MemberID { get; set; }
        /// <summary>
        /// 会员电话
        /// </summary>       
        [DataMember]
        public string MemberPhone { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>       
        [DataMember]
        public string MemberName { get; set; }
        /// <summary>
        /// 发生IP
        /// </summary>       
        [DataMember]
        public string HappenIP { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>       
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }
    }
    /// <summary>
    /// 资金变动日志
    /// </summary>
    [Serializable]
    [DataContract]
    public class AmountChangeLogModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>		
        private int _id;
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
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
        /// 会员名称
        /// </summary>		
        private string _membername;
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        /// <summary>
        /// 发生金额
        /// </summary>		
        private decimal _producemoney;
        [DataMember]
        public decimal ProduceMoney
        {
            get { return _producemoney; }
            set { _producemoney = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _remark;
        [DataMember]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>		
        private DateTime _addtime;
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>		
        private int _orderid=0;
        [DataMember]
        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }
        	
        private int _type;
        /// <summary>
        /// 类型（1 提供帮助  2 接受帮助 3 奖金派发 4 利息结余 5 系统返还）
        /// </summary>	
        [DataMember]
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>		
        private string _ordercode;
        [DataMember]
        public string OrderCode
        {
            get { return _ordercode; }
            set { _ordercode = value; }
        }        
    }
}
