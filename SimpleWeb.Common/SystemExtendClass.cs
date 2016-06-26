using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class SystemExtendClass
    {
        public static int ParseToInt(this string soucenum, int defaultnum)
        {
            if (string.IsNullOrWhiteSpace(soucenum))
            {
                return 0;
            }
            int parse=0;
            if (!int.TryParse(soucenum, out parse))
            {
                return defaultnum;
            }
            return parse;
        }

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
    }
}
