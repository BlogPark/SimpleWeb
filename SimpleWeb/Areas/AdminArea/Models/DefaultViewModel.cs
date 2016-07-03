using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.AdminArea.Models
{
    public class DefaultViewModel
    {
        public AdminIndexModels datamodel { get; set; }
        public string UserName { get; set; }
    }
}