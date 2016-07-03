using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Controllers;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class WebLoginAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {           
            //部分视图不做验证
            if (ctx.IsChildAction)
                return;
            //判断是否当前系统状态为false，系统自动跳转至登录页面
            WebSettingsBLL bll = new WebSettingsBLL();
            WebSettingsModel sitemodel = bll.GetWebSiteModel();
            if (sitemodel.IsOpen == 0)
            {
                ctx.Result = new RedirectResult("/Home/CommingSoon");
                return;
            }
            if (ctx.ActionDescriptor.ControllerDescriptor.ControllerName == "Login")
                return;
            if (ctx.HttpContext.Session[AppContent.SESSION_WEB_LOGIN] == null)
            {
                var url = ctx.RequestContext.HttpContext.Request == null
                        ? ""
                        : ctx.RequestContext.HttpContext.Request.Url.ToString();
                ctx.Result = new RedirectResult("/login.html");
                return;
            }          
        }
    }
}