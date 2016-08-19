using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataBLL;
using SimpleWeb.WebClass;

namespace SimpleWeb.Controllers
{
    public class AppContent
    {
        /// <summary>
        /// 存储用户信息的session名称
        /// </summary>
        public static readonly string SESSION_LOGIN_NAME = "SESSION_LOGIN_NAME";
        public static readonly string VALICODE = "VALICODE";
        public static readonly string SESSION_WEB_LOGIN = "SESSION_WEB_LOGIN";
        /// <summary>
        /// 图片上传路径
        /// </summary>
        public static string UploadPath = System.Configuration.ConfigurationManager.AppSettings["uploadpath"];
        /// <summary>
        /// 存放图片的域名称
        /// </summary>
        public static string Imgdomain = System.Configuration.ConfigurationManager.AppSettings["imgdomain"];
        /// <summary>
        /// 允许上传文件类型
        /// </summary>
        public static string ImgType = System.Configuration.ConfigurationManager.AppSettings["imgtype"];
        /// <summary>
        /// 默认空图
        /// </summary>
        public static string DefaultEmptyImg = System.Configuration.ConfigurationManager.AppSettings["defaultemptyimg"];
        /// <summary>
        /// 网站加密字符串
        /// </summary>
        public static string SecrectStr = SysAdminConfigHelper.GetConfigValue("网站加密字符串");
        /// <summary>
        /// 网站使用模板名称
        /// </summary>
        public static string TempleteName = string.IsNullOrWhiteSpace(SysAdminConfigBLL.GetConfigValue(23)) ? "WebFrontArea" : SysAdminConfigBLL.GetConfigValue(23);
    }
}