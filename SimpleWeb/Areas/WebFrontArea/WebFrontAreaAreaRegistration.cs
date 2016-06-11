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
                "WebFrontArea_default",
                "WebFrontArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
