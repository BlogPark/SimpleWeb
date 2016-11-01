using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 用户操作日志表
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserBehaviorLogModel
    {
        #region 原始字段
        private int _id;
        /// <summary>
        /// 自增组件
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
        private int _behaviorsource;
        /// <summary>
        /// 行为来源（1 前端 2 后台）
        /// </summary>		
        [DataMember]
        public int BehaviorSource
        {
            get { return _behaviorsource; }
            set { _behaviorsource = value; }
        }
        private int _behaviortype = 1;
        /// <summary>
        /// 行为类型（1 登陆 2 提供帮助 3 接受帮助 4 变更打款  5 确认单据 6 撤销单据 7 发放排单币 8 发放激活币 9 奖励会员 10 惩罚会员 11 系统派息）
        /// </summary>		
        [DataMember]
        public int BehaviorType
        {
            get { return _behaviortype; }
            set { _behaviortype = value; }
        }
        private decimal _procamount;
        /// <summary>
        /// 发生金额
        /// </summary>		
        [DataMember]
        public decimal ProcAmount
        {
            get { return _procamount; }
            set { _procamount = value; }
        }
        private string _hordercode;
        /// <summary>
        /// 提供帮助单据编号
        /// </summary>		
        [DataMember]
        public string HOrderCode
        {
            get { return _hordercode; }
            set { _hordercode = value; }
        }
        private string _aordercode;
        /// <summary>
        /// 接受帮助单据编号
        /// </summary>		
        [DataMember]
        public string AOrderCode
        {
            get { return _aordercode; }
            set { _aordercode = value; }
        }
        private string _remark;
        /// <summary>
        /// 描述信息
        /// </summary>		
        [DataMember]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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
        /// 类型名称
        /// </summary>
        [DataMember]
        public string BehaviorTypeName { get; set; }
        /// <summary>
        /// 来源名称
        /// </summary>
        [DataMember]
        public string BehaviorSourceName { get; set; }
        #endregion
    }
}
