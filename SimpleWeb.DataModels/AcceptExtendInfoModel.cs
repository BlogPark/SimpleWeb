using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    [Serializable]
    [DataContract]
    public class AcceptExtendInfoModel
    {
        [DataMember]
        public int acceptorderid { get; set; }
        [DataMember]
        public string acceptordercode { get; set; }
        [DataMember]
        public decimal MatchedMoney { get; set; }
        [DataMember]
        public int acceptmemberid { get; set; }
        [DataMember]
        public string acceptmemberName { get; set; }
        [DataMember]
        public string acceptmemberPhone { get; set; }
        [DataMember]
        public int rememberid { get; set; }
        [DataMember]
        public string remembername { get; set; }
        [DataMember]
        public string rememberphone { get; set; }
        [DataMember]
        public string acceptmemberAlipayId { get; set; }
        [DataMember]
        public string acceptmemberweixin { get; set; }
    }
}
