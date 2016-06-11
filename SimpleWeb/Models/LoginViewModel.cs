using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SimpleWeb.Models
{
    [Serializable]
    [DataContract]
    public class LoginViewModel
    {
        [Display(Name = "登录名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "登录名不能为空")]
        [RegularExpression("[a-zA-Z0-9_]{3,20}", ErrorMessage = "登录名必须由4-20位字母、数字或者下划线组成")]
        [DataMember]
        public string LoginId
        {
            get;
            set;
        }

        [Display(Name = "登录密码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "登录密码不能为空")]
        [RegularExpression("[a-zA-Z0-9_]{6,20}", ErrorMessage = "登录密码必须由6-20位字母、数字或者下划线组成")]
        [DataMember]
        public string Pass
        {
            get;
            set;
        }
        [Display(Name = "记住密码")]
        [DataMember]
        public bool Remember
        {
            get;
            set;
        }

        /// <summary>
        /// 返回连接
        /// </summary>
        [DataMember]
        public string returnurl { get; set; }
        [DataMember]
        public string loginresult { get; set; }
    }
}