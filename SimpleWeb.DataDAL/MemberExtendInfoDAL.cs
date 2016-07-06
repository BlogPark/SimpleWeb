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
        /// <summary>
        /// 得到会员的扩展信息
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public static MemberExtendInfoModel GetMemberExtendInfo(int MemberID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  ");
            strSql.Append("  from MemberExtendInfo ");
            strSql.Append(" where MemberID=@MemberID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int)			};
            parameters[0].Value = MemberID;
            MemberExtendInfoModel model = new MemberExtendInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastHelperTime"].ToString() != "")
                {
                    model.LastHelperTime = ds.Tables[0].Rows[0]["LastHelperTime"].ToString().ParseToDateTime(DateTime.MinValue);
                }
                if (ds.Tables[0].Rows[0]["MemberHelpCount"].ToString() != "")
                {
                    model.MemberHelpCount = int.Parse(ds.Tables[0].Rows[0]["MemberHelpCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastHelpMoney"].ToString() != "")
                {
                    model.LastHelpMoney = decimal.Parse(ds.Tables[0].Rows[0]["LastHelpMoney"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 取消提供帮助单据后更新统计信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="hid"></param>
        /// <returns></returns>
        public static int CancleHelperOrder(int memberid, int hid)
        {
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberExtendInfo
SET     LastHelperTime = ( SELECT TOP 1
                                    Addtime
                           FROM     HelpeOrder
                           WHERE    MemberID = @memberid
                                    AND id <> @id
                           ORDER BY Addtime DESC
                         ) ,
        MemberHelpCount = MemberHelpCount -1 ,
        LastHelpMoney =( SELECT TOP 1
                                    Amount
                           FROM     HelpeOrder
                           WHERE    MemberID = @memberid
                                    AND id <> @id
                           ORDER BY Addtime DESC
                         )
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@memberid",memberid),
                                      new SqlParameter("@id",hid)                                      
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 得到该分配利息的会员
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static List<MemberInfoModel> GetPaymentInterestMembers(int day)
        {
            List<MemberInfoModel> members = new List<MemberInfoModel>();
            string sqltxt = @"SELECT  MemberID ,
        b.MobileNum ,
        b.TruethName
FROM    SimpleWebDataBase.dbo.MemberExtendInfo A
        INNER JOIN SimpleWebDataBase.dbo.MemberInfo B ON A.MemberID = b.ID
WHERE   ( DATEDIFF(DAY, LastHelperTime, GETDATE()) + 1 ) <=@days";
            SqlParameter[] paramter = {
                                          new SqlParameter("@days",day)
                                    };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                MemberInfoModel model = new MemberInfoModel();
                model.ID = item["MemberID"].ToString().ParseToInt(0);
                model.TruethName = item["TruethName"].ToString();
                model.MobileNum = item["MobileNum"].ToString();
            }
            return members;
        }
        /// <summary>
        /// 添加会员初始化信息
        /// </summary>
        /// <param name="model"></param>
        public static int AddModelExtendinfo(MemberExtendInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberExtendInfo(");
            strSql.Append("MemberID,LastHelperTime,MemberHelpCount,LastHelpMoney");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@LastHelperTime,@MemberHelpCount,@LastHelpMoney");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@LastHelperTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@MemberHelpCount", SqlDbType.Int) ,            
                        new SqlParameter("@LastHelpMoney", SqlDbType.Decimal)             
              
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.LastHelperTime;
            parameters[2].Value = model.MemberHelpCount;
            parameters[3].Value = model.LastHelpMoney;
            return helper.ExecuteSql(strSql.ToString(), parameters);
        }
    }
}
