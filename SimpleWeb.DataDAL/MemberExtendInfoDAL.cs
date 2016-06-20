using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataDAL
{
    public class MemberExtendInfoDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 修改会员的扩展信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int Update(int memberid, decimal money)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberExtendInfo
            WHERE   MemberID = @memberid )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberExtendInfo
        SET     LastHelperTime = GETDATE() ,
                MemberHelpCount = MemberHelpCount + 1 ,
                LastHelpMoney = @money
        WHERE   MemberID = @memberid
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberExtendInfo
                ( MemberID ,
                  LastHelperTime ,
                  MemberHelpCount ,
                  LastHelpMoney
                )
        VALUES  ( @memberid ,
                  GETDATE() ,
                  1 ,
                  @money
                )
    END";
            SqlParameter[] parameters = {
			            new SqlParameter("@memberid", SqlDbType.Int) ,                    
                        new SqlParameter("@money", SqlDbType.Decimal)             
              
            };
            parameters[0].Value = memberid;
            parameters[1].Value = money;
            int rows = helper.ExecuteSql(sqltxt, parameters);
            return rows;
        }
    }
}
