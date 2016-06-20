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
    public class MatchOrderDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加匹配信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMatchOrder(MatchOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MatchOrder(");
            strSql.Append("HelperOrderID,HelperOrderCode,HelperMemberID,AcceptOrderID,AcceptOrderCode,AcceptMemberID,MatchedMoney,MatchTime");
            strSql.Append(") values (");
            strSql.Append("@HelperOrderID,@HelperOrderCode,@HelperMemberID,@AcceptOrderID,@AcceptOrderCode,@AcceptMemberID,@MatchedMoney,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@HelperOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@HelperOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HelperMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AcceptMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MatchedMoney", SqlDbType.Decimal) 
            };
            parameters[0].Value = model.HelperOrderID;
            parameters[1].Value = model.HelperOrderCode;
            parameters[2].Value = model.HelperMemberID;
            parameters[3].Value = model.AcceptOrderID;
            parameters[4].Value = model.AcceptOrderCode;
            parameters[5].Value = model.AcceptMemberID;
            parameters[6].Value = model.MatchedMoney;
            object obj = helper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}
