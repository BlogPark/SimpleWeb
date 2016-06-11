using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    /// <summary>
    ///系统用户
    /// </summary>
    [Serializable]
    [DataContract]
    public class SysAdminUserModel
    {
        #region 原有字段
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [DataMember]
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户状态(1 启用 0 禁用)
        /// </summary>
        [DataMember]
        public int UserStatus { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [DataMember]
        public string UserEmail { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DataMember]
        public string TruethName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [DataMember]
        public string UserPhone { get; set; }
        /// <summary>
        /// 验证问题
        /// </summary>
        [DataMember]
        public string Question { get; set; }
        /// <summary>
        /// 问题答案
        /// </summary>
        [DataMember]
        public string Answer { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        [DataMember]
        public int GID { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        [DataMember]
        public string GName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [DataMember]
        public string LoginName { get; set; }

        /// <summary>
        /// 头像信息
        /// </summary>
        [DataMember]
        public string HeaderImg { get; set; }
        /// <summary>
        /// 用户名拼音
        /// </summary>
        [DataMember]
        public string PinYin { get; set; }
        /// <summary>
        /// 用户名拼音首字母
        /// </summary>
        [DataMember]
        public string FirstPinYin { get; set; }
        /// <summary>
        /// 网站主题样式
        /// </summary>
        [DataMember]
        public string WebSkin { get; set; }
        /// <summary>
        /// 上次登陆IP
        /// </summary>
        [DataMember]
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        [DataMember]
        public DateTime LastLoginTime { get; set; }
        #endregion

        /// <summary>
        /// 登录结果字符串
        /// </summary>
        [DataMember]
        public string LoginResult { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string UserStatusName { get; set; }
    }
}
