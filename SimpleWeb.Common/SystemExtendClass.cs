﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class SystemExtendClass
    {
        /// <summary>
        /// 字符串转换为Int
        /// </summary>
        /// <param name="soucenum"></param>
        /// <param name="defaultnum"></param>
        /// <returns></returns>
        public static int ParseToInt(this string soucenum, int defaultnum)
        {
            if (string.IsNullOrWhiteSpace(soucenum))
            {
                return 0;
            }
            int parse = 0;
            if (!int.TryParse(soucenum, out parse))
            {
                return defaultnum;
            }
            return parse;
        }
        /// <summary>
        /// 字符串转换成Decimal
        /// </summary>
        /// <param name="soucedecimal"></param>
        /// <param name="defaultnum"></param>
        /// <returns></returns>
        public static decimal ParseToDecimal(this string soucedecimal, decimal defaultnum)
        {
            if (string.IsNullOrWhiteSpace(soucedecimal))
            {
                return 0;
            }
            decimal parse = 0;
            if (!decimal.TryParse(soucedecimal, out parse))
            {
                return defaultnum;
            }
            return parse;
        }
        /// <summary>
        /// 字符串转换成时间
        /// </summary>
        /// <param name="soucedate"></param>
        /// <param name="defaultdate"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string soucedate, DateTime defaultdate)
        {
            if (string.IsNullOrWhiteSpace(soucedate))
            {
                return defaultdate;
            }
            DateTime parse = DateTime.Now;
            if (!DateTime.TryParse(soucedate, out parse))
            {
                return defaultdate;
            }
            return parse;
        }
        /// <summary>
        /// 自定义正则表达式替换
        /// </summary>
        /// <param name="soucestr"></param>
        /// <param name="RegexFormula"></param>
        /// <param name="valuestr"></param>
        /// <returns></returns>
        public static string MyReplace(this string soucestr, string RegexFormula, string valuestr)
        {
            Regex re = new Regex(RegexFormula, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return re.Replace(soucestr, valuestr);
        }
    }
}
