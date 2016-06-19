using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.WebFrontArea.Models;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    public class WebHomeController : Controller
    {
        //
        // GET: /WebFrontArea/WebHome/
        private MemberInfoBLL bll = new MemberInfoBLL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(string fph)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.regintable = bll.GetReginTableListModel(1);
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(MemberInfoModel fph)
        {
            return View();
        }
    }
}
