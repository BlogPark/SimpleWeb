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
    public class ReginTableModel
    {
        /// <summary>
        /// REGION_ID
        /// </summary>       
        [DataMember]
        public int REGION_ID { get; set; }
        /// <summary>
        /// REGION_CODE
        /// </summary>       
        [DataMember]
        public string REGION_CODE { get; set; }
        /// <summary>
        /// REGION_NAME
        /// </summary>       
        [DataMember]
        public string REGION_NAME { get; set; }
        /// <summary>
        /// PARENT_ID
        /// </summary>       
        [DataMember]
        public int PARENT_ID { get; set; }
        /// <summary>
        /// REGION_LEVEL
        /// </summary>       
        [DataMember]
        public int REGION_LEVEL { get; set; }
        /// <summary>
        /// REGION_ORDER
        /// </summary>       
        [DataMember]
        public int REGION_ORDER { get; set; }
        /// <summary>
        /// REGION_NAME_EN
        /// </summary>       
        [DataMember]
        public string REGION_NAME_EN { get; set; }
        /// <summary>
        /// REGION_SHORTNAME_EN
        /// </summary>       
        [DataMember]
        public string REGION_SHORTNAME_EN { get; set; }
    }
}
