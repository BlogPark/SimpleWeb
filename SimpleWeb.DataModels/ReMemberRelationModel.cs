using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 会员推荐关系表
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReMemberRelationModel
    {
        private int _id;
        /// <summary>
        /// 主键自增
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
        /// 会员手机
        /// </summary>		
        [DataMember]
        public string MemberPhone
        {
            get { return _memberphone; }
            set { _memberphone = value; }
        }
        private string _membertruthname;
        /// <summary>
        /// 会员真实姓名
        /// </summary>		
        [DataMember]
        public string MemberTruthName
        {
            get { return _membertruthname; }
            set { _membertruthname = value; }
        }
        private int _recommmid;
        /// <summary>
        /// 推荐会员ID
        /// </summary>		
        [DataMember]
        public int RecommMID
        {
            get { return _recommmid; }
            set { _recommmid = value; }
        }
        private string _recommmphone;
        /// <summary>
        /// 推荐会员手机
        /// </summary>		
        [DataMember]
        public string RecommMPhone
        {
            get { return _recommmphone; }
            set { _recommmphone = value; }
        }
        private string _recommmtruthname;
        /// <summary>
        /// 推荐会员真实名字
        /// </summary>		
        [DataMember]
        public string RecommMTruthName
        {
            get { return _recommmtruthname; }
            set { _recommmtruthname = value; }
        }
        private int _rstatus;
        /// <summary>
        /// 状态（1 活动 0 禁用）
        /// </summary>		
        [DataMember]
        public int RStatus
        {
            get { return _rstatus; }
            set { _rstatus = value; }
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

    }

    [Serializable]
    [DataContract]
    public class Treemodel
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public SubTreemodel additionalParameters{get;set;}
    }
    [Serializable]
    [DataContract]
    public class SubTreemodel
    {
        [DataMember]
        public List<Treemodel> children { get; set; }
    }
}
