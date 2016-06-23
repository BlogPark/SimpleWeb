using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.AdminArea.Models
{
    [Serializable]
    [DataContract]
    public class MatchedManageViewModel
    {
        [DataMember]
        public PagedList<AcceptHelpOrderModel> acceptorderlist { get; set; }

        //[DataMember]
        //public PagedList<HelpeOrderModel> helporderlist { get; set; }
    }
}