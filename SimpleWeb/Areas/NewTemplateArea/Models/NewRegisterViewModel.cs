using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    [Serializable]
    [DataContract]
    public class NewRegisterViewModel
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