using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Filters;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [WebLoginAttribute]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login", new { area = AppContent.TempleteName });
        }
        /// <summary>
        /// 网站维护页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CommingSoon()
        {
            return View();
        }
    }
}
