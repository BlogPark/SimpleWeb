using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    /// 会员表
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberInfoModel
    {
        #region 原表字段
        /// <summary>
        /// 自增主键
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>       
        [DataMember]
        public string TruethName { get; set; }
        /// <summary>
        /// 性别(1 男 2 女)
        /// </summary>       
        [DataMember]
        public int Sex { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>       
        [DataMember]
        public string TelPhone { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>       
        [DataMember]
        public string MobileNum { get; set; }
        /// <summary>
        /// 注册邮箱
        /// </summary>       
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 身份证编号
        /// </summary>       
        [DataMember]
        public string IdentificationID { get; set; }
        /// <summary>
        /// 省编号
        /// </summary>       
        [DataMember]
        public string Province { get; set; }
        /// <summary>
        /// 市编号
        /// </summary>       
        [DataMember]
        public string City { get; set; }
        /// <summary>
        /// 区/县编号
        /// </summary>       
        [DataMember]
        public string Area { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>       
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>       
        [DataMember]
        public string WeixinNum { get; set; }
        /// <summary>
        /// 支付宝名称
        /// </summary>       
        [DataMember]
        public string AliPayName { get; set; }
        /// <summary>
        /// 支付宝ID
        /// </summary>       
        [DataMember]
        public string AliPayNum { get; set; }
        /// <summary>
        /// 密保问题
        /// </summary>       
        [DataMember]
        public string SecurityQuestion { get; set; }
        /// <summary>
        /// SecurityAnswer
        /// </summary>       
        [DataMember]
        public string SecurityAnswer { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>       
        [DataMember]
        public string LogPwd { get; set; }
        /// <summary>
        /// 会员状态(1 待激活 2 已激活 3 已冻结)
        /// </summary>       
        [DataMember]
        public int MStatus { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; } 
        #endregion

        #region 扩展字段
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string MStatusName { get; set; }
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
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime? begintime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime? endtime { get; set; }
        /// <summary>
        /// 推荐会员电话
        /// </summary>
        [DataMember]
        public string MemberPhone { get; set; }
        #endregion
    }
}
