using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleWeb.DataBLL;

namespace SimpleWeb.WebClass
{
    /// <summary>
    /// 系统配置项缓存
    /// </summary>
    public class SysAdminConfigHelper
    {
        static SysAdminConfigHelper()
        {
            init();
        }
        private static DateTime sTime = DateTime.Now.AddHours(-12);
        private static object _obj = new object();
        private static SysAdminConfigBLL sysadminconfigbll = new SysAdminConfigBLL();

        #region 变量声明
        private static Dictionary<string, string> Configs = new Dictionary<string, string>();
        #endregion
        public static void RemoveAll()
        {
            sTime = DateTime.Now.AddHours(-12);
            init();
        }
        private static void init()
        {
            //缓存10个小时 
            if (Math.Abs((DateTime.Now - sTime).Hours) > 10)
            {
                sTime = DateTime.Now;
                lock (_obj)
                {
                    
                    Configs = sysadminconfigbll.GetAllConfigsDic();                  
                }
            }
        }
        /// <summary>
        /// 根据配置节点名称查找值
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public static string GetConfigValue(string ConfigKey)
        {
            string configvalue = "";
            init();
            if (Configs == null || !Configs.Any())
            {
                Configs = sysadminconfigbll.GetAllConfigsDic();
            }
            if (Configs != null || Configs.Any())
            {
                if (Configs.ContainsKey(ConfigKey))
                {
                    configvalue = Configs[ConfigKey];
                }
            }
            return configvalue;
        }
    }
}