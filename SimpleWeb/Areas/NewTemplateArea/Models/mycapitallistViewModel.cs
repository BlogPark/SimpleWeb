﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataModels;
using Webdiyer.WebControls.Mvc;

namespace SimpleWeb.Areas.NewTemplateArea.Models
{
    public class mycapitallistViewModel
    {
        public PagedList<AmountChangeLogModel> list { get; set; }
    }
}