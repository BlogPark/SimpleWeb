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
            if (ctx.ActionDescriptor.ControllerDescriptor.ControllerName == "public")
                return;
            //部分视图不做验证
            if (ctx.IsChildAction)
                return;

            //判断是否当前系统状态为false，系统自动跳转至登录页面
            if (ctx.HttpContext.Session[AppContent.SESSION_LOGIN_NAME] == null)
            {             
                ctx.Result = new RedirectResult("/ad/login.html");
                return;
            }            
        }
    }
}