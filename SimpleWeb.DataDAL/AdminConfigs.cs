using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataDAL
{
    public class AdminConfigs
    {
        /// <summary>
        /// 系统数据库连接字符串
        /// </summary>
        public static string Superstr = System.Configuration.ConfigurationManager.AppSettings["connectstr"];
    }
}
