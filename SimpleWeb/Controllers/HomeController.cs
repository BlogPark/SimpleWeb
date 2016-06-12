using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Models;

namespace SimpleWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            SessionLoginModel user = Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
            if (user == null)
            {
                return RedirectToAction("Login", "Default", new { area = "AdminArea" });
            }
            return View();
        }

    }
}
