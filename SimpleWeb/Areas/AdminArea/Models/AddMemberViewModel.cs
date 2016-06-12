using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class AddMemberViewModel
    {
        [DataMember]
        public MemberInfoModel member { get; set; }

        [DataMember]
        public List<ReginTableModel> regintable { get; set; }
    }
}