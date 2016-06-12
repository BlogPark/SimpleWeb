using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataDAL
{
    public class PublicHelperDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static DataTable GetTable(PageProModel page, out int totalrowcount)
        {
            totalrowcount = 0;
            var totalrowcountpram = new SqlParameter("@totalrecord", System.Data.SqlDbType.Int);
            totalrowcountpram.Direction = System.Data.ParameterDirection.Output;
            SqlParameter[] paramter = new[] { 
            new SqlParameter("@orderby",page.orderby),
            new SqlParameter("@tablename",page.tablename),
            new SqlParameter("@colums",page.colums),
            new SqlParameter("@where",page.where),
            new SqlParameter("@pageindex",page.pageindex),
             new SqlParameter("@pagesize",page.pagesize),
            totalrowcountpram
            };
            DataSet ds = helper.RunProcedureDataSet("PagePro", paramter);
            totalrowcount = Convert.ToInt32(totalrowcountpram.Value);
            return ds.Tables[0];
        }
    }
}
