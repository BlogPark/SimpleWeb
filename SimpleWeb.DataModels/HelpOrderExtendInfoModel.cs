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
    public  class HelpOrderExtendInfoModel
    {
        [DataMember]
        public int helporderid { get; set; }
        [DataMember]
        public string helpordercode { get; set; }
        [DataMember]
        public decimal MatchedMoney { get; set; }
        [DataMember]
        public int helpmemberid { get; set; }
        [DataMember]
        public string helpmemberName { get; set; }
        [DataMember]
        public string helpmemberPhone { get; set; }
        [DataMember]
        public int rememberid { get; set; }
        [DataMember]
        public string remembername { get; set; }
        [DataMember]
        public string rememberphone { get; set; }
        [DataMember]
        public string helpmemberAlipayId { get; set; }
        [DataMember]
        public string helpmemberweixin { get; set; }
    }
}
