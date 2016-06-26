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
    public class RegisterController : Controller
    {
        //
        // GET: /WebFrontArea/Register/
        private MemberInfoBLL bll = new MemberInfoBLL();
        public ActionResult Index(string msd)
        {
            RegisterViewModel model = new RegisterViewModel();
            model.member = new MemberInfoModel();
            model.member.MemberPhone = msd;
            model.regintable = bll.GetReginTableListModel(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(MemberInfoModel member)
        {
            return View(member);
        }

    }
}
