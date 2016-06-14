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
    public class MemberCapitalDetailDAL
    {
        public DbHelperSQL helper = new DbHelperSQL();

        public bool UpdateMemberCapitalDetail(MemberCapitalDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberCapitalDetail set ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" TotalStaticCapital = @TotalStaticCapital , ");
            strSql.Append(" TotalDynamicFunds = @TotalDynamicFunds , ");
            strSql.Append(" StaticCapital = @StaticCapital , ");
            strSql.Append(" DynamicFunds = @DynamicFunds , ");
            strSql.Append(" StaticInterest = @StaticInterest , ");
            strSql.Append(" DynamicInterest = @DynamicInterest , ");
            strSql.Append(" StaticPunishMoney = @StaticPunishMoney , ");
            strSql.Append(" DynamicPunishMoney = @DynamicPunishMoney , ");
            strSql.Append(" StaticFreezeMoney = @StaticFreezeMoney , ");
            strSql.Append(" DynamicFreezeMoney = @DynamicFreezeMoney  ");
            strSql.Append(" where MemberID=@MemberID  ");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@TotalStaticCapital", SqlDbType.Decimal) ,            
                        new SqlParameter("@TotalDynamicFunds", SqlDbType.Decimal) ,            
                        new SqlParameter("@StaticCapital", SqlDbType.Decimal) ,            
                        new SqlParameter("@DynamicFunds", SqlDbType.Decimal) ,            
                        new SqlParameter("@StaticInterest", SqlDbType.Decimal) ,            
                        new SqlParameter("@DynamicInterest", SqlDbType.Decimal) ,            
                        new SqlParameter("@StaticPunishMoney", SqlDbType.Decimal) ,            
                        new SqlParameter("@DynamicPunishMoney", SqlDbType.Decimal) ,            
                        new SqlParameter("@StaticFreezeMoney", SqlDbType.Decimal) ,            
                        new SqlParameter("@DynamicFreezeMoney", SqlDbType.Decimal)   
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.TotalStaticCapital;
            parameters[2].Value = model.TotalDynamicFunds;
            parameters[3].Value = model.StaticCapital;
            parameters[4].Value = model.DynamicFunds;
            parameters[5].Value = model.StaticInterest;
            parameters[6].Value = model.DynamicInterest;
            parameters[7].Value = model.StaticPunishMoney;
            parameters[8].Value = model.DynamicPunishMoney;
            parameters[9].Value = model.StaticFreezeMoney;
            parameters[10].Value = model.DynamicFreezeMoney;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
