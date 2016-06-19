using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Models
{
    [Serializable]
    [DataContract]
    public class RegisterViewModel
    {
        /// <summary>
        /// 会员
        /// </summary>
        [DataMember]
        public MemberInfoModel member { get; set; }
        [DataMember]
        public List<ReginTableModel> regintable { get; set; }
    }
}