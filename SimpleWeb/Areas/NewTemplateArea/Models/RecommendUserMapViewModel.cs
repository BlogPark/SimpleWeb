using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    public class RecommendUserMapViewModel
    {
        public MemberInfoModel member { get; set; }
        public int childcount { get; set; }
        public bool isParent { get; set; }
    }
}