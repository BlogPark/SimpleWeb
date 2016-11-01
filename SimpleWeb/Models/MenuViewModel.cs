using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Models
{
    [Serializable]
    [DataContract]
    public class MenuViewModel
    {
        [DataMember]
        public List<SysAdminMenuModel> firstlist { get; set; }

        [DataMember]
        public List<SysAdminMenuModel> sublist { get; set; }

        [DataMember]
        public string Currentpage { get; set; }

    }
}