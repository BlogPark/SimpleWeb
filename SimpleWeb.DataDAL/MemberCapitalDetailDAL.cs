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
        public static DbHelperSQL helper = new DbHelperSQL();

        public static bool UpdateMemberCapitalDetail(MemberCapitalDetailModel model)
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
        /// <summary>
        /// 更新会员的静态冻结金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberStaticFreezeMoney(int memberid, decimal amont)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticFreezeMoney=@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  StaticFreezeMoney
                )
        VALUES  ( @MemberID ,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的动态冻结金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberDynamicFreezeMoney(int memberid, decimal amont)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFreezeMoney=@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  DynamicFreezeMoney
                )
        VALUES  ( @MemberID ,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的静态资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberStaticCapital(int memberid, decimal amont)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=@Amount,
                   TotalStaticCapital=TotalStaticCapital+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  StaticCapital,
                  TotalStaticCapital
                )
        VALUES  ( @MemberID ,
                  @Amount,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的静态资金和利率
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberStaticCapital(int memberid, decimal amont, decimal interest)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=@Amount,
                   TotalStaticCapital=TotalStaticCapital+@Amount,
                   Interest=@interest
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  StaticCapital,
                  TotalStaticCapital,
                  Interest
                )
        VALUES  ( @MemberID ,
                  @Amount,
                  @Amount,
                  @interest
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont),
                                      new SqlParameter("@interest",interest)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的动态资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberDynamicFunds(int memberid, decimal amont)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFunds=@Amount,
                   TotalDynamicFunds=TotalDynamicFunds+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  DynamicFunds,
                  TotalDynamicFunds
                )
        VALUES  ( @MemberID ,
                  @Amount,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 扣减会员的静态资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int DeductionMemberStaticCapital(int memberid, decimal amont)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=StaticCapital+@Amount
        WHERE   MemberID = @MemberID
    ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 扣减会员的静态资金和利率
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int DeductionMemberStaticCapital(int memberid, decimal amont, decimal interest)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=StaticCapital+@Amount,Interest=@interest
        WHERE   MemberID = @MemberID
    ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont),
                                      new SqlParameter("@interest",interest)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的动态资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int DeductionMemberDynamicFunds(int memberid, decimal amont)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFunds=DynamicFunds+@Amount
        WHERE   MemberID = @MemberID
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的动态资金和利率
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int DeductionMemberDynamicFunds(int memberid, decimal amont, decimal interest)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFunds=DynamicFunds+@Amount,,Interest=@interest
        WHERE   MemberID = @MemberID
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",amont),
                                      new SqlParameter("@interest",interest)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的当前利率
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="interest"></param>
        /// <returns></returns>
        public static int UpdateMemberInterest(int memberid, decimal interest)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     interest=@interest
        WHERE   MemberID = @MemberID
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@interest",interest)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 解冻会员的静态冻结资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int UpdateStaticThawDetail(int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     StaticCapital = StaticCapital + StaticFreezeMoney ,
        StaticFreezeMoney = 0
WHERE   MemberID = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",memberid)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 解冻会员的动态冻结资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int UpdateDynamicThawDetail(int memberid)
        {
            string sqltxt = @"UPDATE  MemberCapitalDetail
SET     DynamicFunds = DynamicFunds + DynamicFreezeMoney ,
        DynamicFreezeMoney = 0
WHERE   MemberID = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",memberid)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }

        /// <summary>
        /// 更新会员的静态利息金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="interest"></param>
        /// <returns></returns>
        public static int UpdateStaticInterest(int memberid, decimal interestmoney)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticInterest=ISNULL(StaticInterest,0)+@StaticInterest
        WHERE   MemberID = @MemberID
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@StaticInterest",interestmoney)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的动态利息金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="interest"></param>
        /// <returns></returns>
        public static int UpdateDynamicInterest(int memberid, decimal interestmoney)
        {
            string sqltxt = @"
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicInterest=ISNULL(DynamicInterest,0)+@DynamicInterest
        WHERE   MemberID = @MemberID
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@DynamicInterest",interestmoney)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }

    }
}
