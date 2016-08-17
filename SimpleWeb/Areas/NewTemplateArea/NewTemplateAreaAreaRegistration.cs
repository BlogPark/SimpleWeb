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
                "NewTemplateArea_default",
                "NewTemplateArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
