using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;

namespace SimpleWeb.DataBLL
{
    /// <summary>
    /// 系统配置文件
    /// </summary>
    public static class SystemConfigs
    {
        /// <summary>
        /// 获取最低提供帮助的金额
        /// </summary>
        /// <returns></returns>
        public static string GetminHelpAmont()
        {
            return SysAdminConfigDAL.GetConfigsByID(12);
        }

        /// <summary>
        /// 获取最高提供帮助的金额
        /// </summary>
        /// <returns></returns>
        public static string GetmaxHelpAmont()
        {
            return SysAdminConfigDAL.GetConfigsByID(13);
        }
        /// <summary>
        /// 获取最低接受帮助的金额
        /// </summary>
        /// <returns></returns>
        public static string GetminAcceptAmont()
        {
            return SysAdminConfigDAL.GetConfigsByID(14);
        }
        /// <summary>
        /// 获取最高接受帮助的金额
        /// </summary>
        /// <returns></returns>
        public static string GetmaxAcceptAmont()
        {
            return SysAdminConfigDAL.GetConfigsByID(15);
        }
    }
}
