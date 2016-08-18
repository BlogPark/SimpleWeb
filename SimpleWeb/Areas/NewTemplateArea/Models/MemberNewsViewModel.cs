using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    public class MemberNewsViewModel
    {
        public List<AdminSiteNewsModel> news { get; set; }
    }
}