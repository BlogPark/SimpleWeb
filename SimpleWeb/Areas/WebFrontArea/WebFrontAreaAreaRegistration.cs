using System.Web.Mvc;

namespace SimpleWeb.Areas.WebFrontArea
{
    public class WebFrontAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WebFrontArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "web_exit",
               "exit",
               new { controller = "WebHome", action = "exitsystem", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "web_mycapital",
               "capital.html",
               new { controller = "WebHome", action = "mycapital", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "web_userprofile",
               "userprofile.html",
               new { controller = "WebHome", action = "userprofile", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "web_news",
               "news.html",
               new { controller = "WebHome", action = "webnews",  id = UrlParameter.Optional }
           );
            context.MapRoute(
             "web_home",
             "index.html",
             new { controller = "WebHome", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
             "web_login",
             "login.html",
             new { controller = "Login", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
            "web_register",
            "register-{msd}.html",
            new { controller = "Register", action = "Index", msd = "", id = UrlParameter.Optional }
        );
            context.MapRoute(
           "web_ordercode",
           "ordercode-{page}.html",
           new { controller = "WebHome", action = "mylineation", page = 1, id = UrlParameter.Optional }
       );
            context.MapRoute(
            "web_activecode",
            "activecode-{page}.html",
            new { controller = "WebHome", action = "myactivation", page = 1, id = UrlParameter.Optional }
        );
            context.MapRoute(
                "WebFrontArea_default",
                "WebFrontArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
