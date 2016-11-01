using System.Web.Mvc;

namespace SimpleWeb.Areas.NewTemplateArea
{
    public class NewTemplateAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "NewTemplateArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "new_web_capitaldetaillog_page",
              "user/capitaldetaillog-{page}.html",
              new { controller = "WebHome", action = "mycapitallist", page = 1, id = UrlParameter.Optional }
          );
            context.MapRoute(
              "new_web_capitaldetaillog",
              "user/capitaldetaillog.html",
              new { controller = "WebHome", action = "mycapitallist", id = UrlParameter.Optional }
          );
            context.MapRoute(
              "new_web_activecodelog_page",
              "user/activecodelog-{page}.html",
              new { controller = "WebHome", action = "ActiveCodeLog", page = 1, id = UrlParameter.Optional }
          );
            context.MapRoute(
              "new_web_activecodelog",
              "user/activecodelog.html",
              new { controller = "WebHome", action = "ActiveCodeLog", id = UrlParameter.Optional }
          );

            context.MapRoute(
               "new_web_team",
               "user/team.html",
               new { controller = "WebHome", action = "recommendusermap", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "new_web_nopwdlogin",
               "user/member{id}.html",
               new { controller = "Login", action = "NoPwdLogin", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "new_web_exit",
               "user/logout.html",
               new { controller = "WebHome", action = "exitsystem", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "new_web_mycapital",
               "user/capital.html",
               new { controller = "WebHome", action = "mycapital", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "new_web_userprofile",
               "user/userprofile.html",
               new { controller = "WebHome", action = "userprofile", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "new_web_news",
               "user/news.html",
               new { controller = "WebHome", action = "webnews", id = UrlParameter.Optional }
           );
            context.MapRoute(
             "new_web_home",
             "user/index.html",
             new { controller = "WebHome", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
             "new_web_login",
             "user/login.html",
             new { controller = "Login", action = "Index", id = UrlParameter.Optional }
         );
            context.MapRoute(
            "new_web_register",
            "user/register-{msd}.html",
            new { controller = "Register", action = "Index", msd = "", id = UrlParameter.Optional }
        );
            context.MapRoute(
           "new_web_ordercode",
           "user/ordercode-{page}.html",
           new { controller = "WebHome", action = "mylineation", page = 1, id = UrlParameter.Optional }
       );
            context.MapRoute(
            "new_web_activecode",
            "user/activecode-{page}.html",
            new { controller = "WebHome", action = "myactivation", page = 1, id = UrlParameter.Optional }
        );
            context.MapRoute(
                "NewTemplateArea_default",
                "NewTemplateArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
