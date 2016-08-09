using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 单据举报
    /// </summary>
    [Serializable]
    [DataContract]
    public class OrderReportingModel
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
        private int _orderid;
        /// <summary>
        /// 涉及单号ID
        /// </summary>
        [DataMember]
        public int OrderID
        {
            get { return _orderid; }
            set { _orderid = value; }
        }

        private string _ordercode;
        /// <summary>
        /// 涉及单号
        /// </summary>		
        [DataMember]
        public string OrderCode
        {
            get { return _ordercode; }
            set { _ordercode = value; }
        }
        private int _ordertype;
        /// <summary>
        /// 涉及单据类型（1 提供帮助 2 接受帮助）
        /// </summary>
        [DataMember]
        public int OrderType
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }

        private int _memberid;
        /// <summary>
        /// 举报者ID
        /// </summary>		
        [DataMember]
        public int MemberID
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        private string _membername;
        /// <summary>
        /// 举报者名字
        /// </summary>		
        [DataMember]
        public string MemberName
        {
            get { return _membername; }
            set { _membername = value; }
        }
        private string _memberphone;
        /// <summary>
        /// 举报者手机
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private string _title;
        /// <summary>
        /// 标题
        /// </summary>		
        [DataMember]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _reportingtext;
        /// <summary>
        /// 举报内容
        /// </summary>		
        [DataMember]
        public string ReportingText
        {
            get { return _reportingtext; }
            set { _reportingtext = value; }
        }
        private string _reasontype;
        /// <summary>
        /// 举报类型
        /// </summary>		
        [DataMember]
        public string ReasonType
        {
            get { return _reasontype; }
            set { _reasontype = value; }
        }
        private int _rstatus;
        /// <summary>
        /// 举报状态（1 新举报  2 处理中 3 已处理 4 已取消）
        /// </summary>		
        [DataMember]
        public int RStatus
        {
            get { return _rstatus; }
            set { _rstatus = value; }
        }
        private string _handleresult;
        /// <summary>
        /// 处理结果
        /// </summary>		
        [DataMember]
        public string HandleResult
        {
            get { return _handleresult; }
            set { _handleresult = value; }
        }
        private DateTime _addtime;
        /// <summary>
        /// 提交时间
        /// </summary>		
        [DataMember]
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private DateTime _lastupdatetime;
        /// <summary>
        /// 最后更新时间
        /// </summary>		
        [DataMember]
        public DateTime LastUpdateTime
        {
            get { return _lastupdatetime; }
            set { _lastupdatetime = value; }
        }
        #endregion

        #region 扩展字段
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string RStatusName { get; set; }
        #endregion
    }
}
