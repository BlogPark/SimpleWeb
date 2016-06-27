using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using SimpleWeb.Controllers;
using SimpleWeb.DataModels;
using SimpleWeb.Models;

namespace SimpleWeb.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminLoginAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            //登录模式不做验证
            if (ctx.ActionDescriptor.ControllerDescriptor.ControllerName == "Login")
                return;
            //对外接口不做验证
            if (ctx.ActionDescriptor.ControllerDescriptor.ControllerName == "publicaction")
                return;
            //部分视图不做验证
            if (ctx.IsChildAction)
                return;

            //判断是否当前系统状态为false，系统自动跳转至登录页面
            if (ctx.HttpContext.Session[AppContent.SESSION_LOGIN_NAME] == null)
            {
                var url = ctx.RequestContext.HttpContext.Request == null
                        ? ""
                        : ctx.RequestContext.HttpContext.Request.Url.ToString();
                ctx.Result = new RedirectResult("/Login/Index?returnurl=" + System.Web.HttpUtility.UrlEncode(url));
                return;
            }
            if (ctx.HttpContext.Session[AppContent.SESSION_LOGIN_NAME] != null)
            {
                string controllername = ctx.ActionDescriptor.ControllerDescriptor.ControllerName;
                SessionLoginModel model = ctx.HttpContext.Session[AppContent.SESSION_LOGIN_NAME] as SessionLoginModel;
                List<SysAdminMenuModel> list = model.UserMenus.Where(p => p.ControllerName == controllername).ToList();
                if (list == null || list.Count < 1)
                {
                    if (controllername != "Home")
                    {
                        var url = ctx.RequestContext.HttpContext.Request == null
                          ? "/Home/Index"
                          : ctx.RequestContext.HttpContext.Request.Url.ToString();
                        ctx.Result = new RedirectResult(url);
                        return;
                    }
                }
            }
        }
    }
}