using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Areas.WebFrontArea.Models;

namespace SimpleWeb.Areas.WebFrontArea.Controllers
{
    public class LoginController : Controller
    {
        //会员登陆页面
        // GET: /WebFrontArea/Login/

        public ActionResult Index(string bl="")
        {
            webLoginViewModel model = new webLoginViewModel();
            model.returnurl = bl;
            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.TempMsg = "无此用户信息";
            return View();
        }
    }
}
