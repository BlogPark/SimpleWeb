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
        public static int UpdateMemberStaticFreezeMoney(int memberid, decimal amont, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticFreezeMoney=ISNULL(StaticFreezeMoney,0)+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID,MemberPhone,MemberName,
                  StaticFreezeMoney
                )
        VALUES  ( @MemberID,@MemberPhone,@MemberName,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
                                      new SqlParameter("@Amount",amont)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 更新会员的静态冻结金额
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amont"></param>
        /// <returns></returns>
        public static int UpdateMemberStaticFreezeMoneyAndinster(int memberid, decimal amont, decimal inster, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticFreezeMoney=ISNULL(StaticFreezeMoney,0)+@Amount,
                Interest=@interest
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID,MemberPhone,MemberName,
                  StaticFreezeMoney,Interest
                )
        VALUES  ( @MemberID,@MemberPhone,@MemberName,
                  @Amount,@interest
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
                                      new SqlParameter("@Amount",amont),
                                      new SqlParameter("@interest",inster)
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
        public static int UpdateMemberDynamicFreezeMoney(int memberid, decimal amont, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFreezeMoney=ISNULL(DynamicFreezeMoney,0)+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,MemberPhone,MemberName,
                  DynamicFreezeMoney
                )
        VALUES  ( @MemberID ,@MemberPhone,@MemberName,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                       new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
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
        public static int UpdateMemberStaticCapital(int memberid, decimal amont, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=@Amount,
                   TotalStaticCapital=ISNULL(TotalStaticCapital,0)+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,MemberPhone,MemberName,
                  StaticCapital,
                  TotalStaticCapital
                )
        VALUES  ( @MemberID ,@MemberPhone,@MemberName,
                  @Amount,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                       new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
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
        public static int UpdateMemberStaticCapital(int memberid, decimal amont, decimal interest, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital=@Amount,
                   TotalStaticCapital=ISNULL(TotalStaticCapital,0)+@Amount,
                   Interest=@interest
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,MemberPhone,MemberName,
                  StaticCapital,
                  TotalStaticCapital,
                  Interest
                )
        VALUES  ( @MemberID ,@MemberPhone,@MemberName,
                  @Amount,
                  @Amount,
                  @interest
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                       new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
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
        public static int UpdateMemberDynamicFunds(int memberid, decimal amont, string membername, string memberphone)
        {
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     DynamicFunds=@Amount,
                   TotalDynamicFunds=ISNULL(TotalDynamicFunds,0)+@Amount
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,MemberPhone,MemberName,
                  DynamicFunds,
                  TotalDynamicFunds
                )
        VALUES  ( @MemberID ,@MemberPhone,@MemberName,
                  @Amount,
                  @Amount
                )
    END";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                       new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
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
        SET     StaticCapital=ISNULL(StaticCapital,0)+@Amount
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
        SET     StaticCapital=ISNULL(StaticCapital,)+@Amount,Interest=@interest
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
        SET     DynamicFunds=ISNULL(DynamicFunds,0)+@Amount
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
        SET     DynamicFunds=ISNULL(DynamicFunds,0)+@Amount,Interest=@interest
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
SET     StaticCapital = ISNULL(StaticCapital,0) + StaticFreezeMoney ,
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
SET     DynamicFunds = ISNULL(DynamicFunds,0) + DynamicFreezeMoney ,
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
        public static int UpdateDynamicInterest(int memberid, decimal interestmoney, string membername, string memberphone,string ordercode,int orderid)
        {
            string sqltxt = @"        
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
SET     DynamicInterest = ISNULL(DynamicInterest, 0) + @Amount
OUTPUT  @orderid ,
        @ordercode ,
        DELETED.MemberID ,
        DELETED.MemberName ,
        DELETED.MemberPhone ,
        @Amount
        INTO dbo.LeaderAmount
WHERE   MemberID = @MemberID    
   ";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MemberID",memberid),
                                      new SqlParameter("@Amount",interestmoney),
                                      new SqlParameter("@MemberPhone",memberphone),
                                      new SqlParameter("@MemberName",membername),
                                      new SqlParameter("@orderid",orderid),
                                      new SqlParameter("@ordercode",ordercode)
                                      };
            int rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 为会员派发利息
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int PaymentInterest(int day)
        {
            string sqltxt = @"    UPDATE A
	SET StaticInterest=(ISNULL(StaticCapital,0)*(0.01*ISNULL(Interest,0)))+StaticInterest
	OUTPUT INSERTED.MemberID,DELETED.MemberPhone,DELETED.MemberName,DELETED.StaticCapital*DELETED.Interest*0.01,'系统派发利息',GETDATE(),0,4,''
	INTO dbo.AmountChangeLog
	FROM MemberCapitalDetail A 
	WHERE MemberID IN (SELECT MemberID FROM dbo.MemberExtendInfo WHERE (DATEDIFF(DAY,LastHelperTime,GETDATE())+1)<=@days)";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@days",day)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 为会员的帮助订单分配利息
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int PaymentInterestForOrder(int day)
        {
            string sqltxt = @"UPDATE  A
SET     Interest = CASE WHEN DATEDIFF(DAY, AddTime, GETDATE()) > (@days) THEN 0
                        ELSE ( Amount * ( 0.01 * CurrentInterest ) )
                   END
FROM    SimpleWebDataBase.dbo.HelpeOrder A 
WHERE HStatus <3";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@days",day)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 为会员的帮助订单分配利息并记录日志
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int PaymentInterestForOrderWithLog(int day)
        {
            string sqltxt = @"UPDATE  A
SET     Interest =ISNULL(Interest,0)+( CASE WHEN DATEDIFF(DAY, AddTime, GETDATE()) > ( @days )
                        THEN 0
                        ELSE ( Amount * ( 0.01 * CurrentInterest ) ))
                   END
OUTPUT  INSERTED.MemberID ,
        DELETED.MemberPhone ,
        DELETED.MemberName ,
        INSERTED.Interest - DELETED.Interest ,
        '系统派发利息' ,
        GETDATE() ,
        DELETED.ID ,
        4 ,
        DELETED.OrderCode
        INTO dbo.AmountChangeLog
FROM    SimpleWebDataBase.dbo.HelpeOrder A
WHERE   HStatus < 3";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@days",day)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 汇总会员的本次利息
        /// </summary>
        /// <returns></returns>
        public static int SumInterestMoney()
        {
            string sqltxt = @"UPDATE  A
SET     A.StaticInterest = ISNULL(A.StaticInterest,0)
        + ( SELECT  SUM(Interest)
            FROM    SimpleWebDataBase.dbo.HelpeOrder
            WHERE   MemberID = A.MemberID
          )
OUTPUT  INSERTED.MemberID ,
        DELETED.MemberPhone ,
        DELETED.MemberName ,
        INSERTED.StaticInterest-DELETED.StaticInterest ,
        '系统派发利息' ,
        GETDATE() ,
        0 ,
        4 ,
        ''
        INTO dbo.AmountChangeLog
FROM    SimpleWebDataBase.dbo.MemberCapitalDetail A";
            return helper.ExecuteSql(sqltxt);
        }
        /// <summary>
        /// 汇总会员的冻结利息总额
        /// </summary>
        /// <returns></returns>
        public static int SumInterestMoneyWithoutLog()
        {
            string sqltxt = @"UPDATE  A
SET     A.StaticInterest = ( SELECT  SUM(Interest)
            FROM    SimpleWebDataBase.dbo.HelpeOrder
            WHERE   MemberID = A.MemberID
          )
FROM    SimpleWebDataBase.dbo.MemberCapitalDetail A";
            return helper.ExecuteSql(sqltxt);
        }
        /// <summary>
        /// 清空会员利率
        /// </summary>
        /// <returns></returns>
        public static int ClearHelperInterest()
        {
            string sqltxt = @"UPDATE dbo.HelpeOrder SET Interest=0 ";
            return helper.ExecuteSql(sqltxt);
        }
        /// <summary>
        /// 为会员的推荐人分派奖金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amount"></param>
        /// <param name="intereststr"></param>
        /// <param name="ordercode"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static int PaymentLeaderPrize(int memberid, decimal amount, string intereststr, string ordercode, int oid)
        {
            int result = 0;
            List<int> interests = new List<int>();
            if (!string.IsNullOrWhiteSpace(intereststr))
            {
                foreach (var item in intereststr.Split(','))
                {
                    interests.Add(item.ParseToInt(0));
                }
            }
            if (interests.Count < 1)
                return result;
            //按照配置的利率查找推荐图谱
            List<ReMemberRelationModel> members = new List<ReMemberRelationModel>();
            for (int i = 0; i < interests.Count; i++)
            {
                if (i == 0)
                {
                    ReMemberRelationModel member = ReMemberRelationDAL.GetReMemberRelation(memberid);
                    if (member != null)
                        members.Add(member);
                }
                else
                {
                    if (members.Count > (i - 1))
                    {
                        ReMemberRelationModel member = ReMemberRelationDAL.GetReMemberRelation(members[i - 1].MemberID);
                        if (member != null)
                            members.Add(member);
                    }
                }
            }
            for (int i = 0; i < members.Count; i++)
            {
                decimal money = amount;//若推荐人排单金额小于被推荐人排单金额，则取推荐人排单金额计算
                if (members[i].Amount < amount)
                {
                    money = members[i].Amount;
                }
                //int rowcount = UpdateMemberDynamicFunds(members[i].MemberID, (money / 100 * interests[i]), members[i].MemberTruthName, members[i].MemberPhone);
                int rowcount = UpdateDynamicInterest(members[i].MemberID, (money / 100 * interests[i]), members[i].MemberTruthName, members[i].MemberPhone,ordercode,oid);
                if (rowcount < 1)
                {
                    return result;
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = members[i].MemberID;
                logmodel.MemberName = members[i].MemberTruthName;
                logmodel.MemberPhone = members[i].MemberPhone;
                logmodel.OrderCode = ordercode;
                logmodel.OrderID = oid;
                logmodel.ProduceMoney = (money / 100 * interests[i]);
                logmodel.Remark = "会员:" + members[i].MemberPhone + " 得到来自单据：" + ordercode + "的领导奖";
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return result;
                }
                result++;
            }
            return result;

        }
        /// <summary>
        /// 查询会员的个人全部资产信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static MemberCapitalDetailModel GetMemberStaticCapital(int memberid)
        {
            string sqltxt = @"SELECT  MemberID ,
        MemberPhone ,
        MemberName ,
        StaticCapital ,
        DynamicFunds ,
        StaticInterest ,
        DynamicInterest ,
        StaticPunishMoney ,
        DynamicPunishMoney ,
        StaticFreezeMoney ,
        DynamicFreezeMoney ,
        TotalStaticCapital ,
        TotalDynamicFunds ,
        Interest
FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
WHERE   MemberID = @memberid";
            SqlParameter[] paramter ={
                                    new SqlParameter("@memberid",memberid)
                                    };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            MemberCapitalDetailModel model = new MemberCapitalDetailModel();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(dt.Rows[0]["MemberID"].ToString());
                }
                if (dt.Rows[0]["StaticFreezeMoney"].ToString() != "")
                {
                    model.StaticFreezeMoney = decimal.Parse(dt.Rows[0]["StaticFreezeMoney"].ToString());
                }
                if (dt.Rows[0]["DynamicFreezeMoney"].ToString() != "")
                {
                    model.DynamicFreezeMoney = decimal.Parse(dt.Rows[0]["DynamicFreezeMoney"].ToString());
                }
                if (dt.Rows[0]["TotalStaticCapital"].ToString() != "")
                {
                    model.TotalStaticCapital = decimal.Parse(dt.Rows[0]["TotalStaticCapital"].ToString());
                }
                if (dt.Rows[0]["TotalDynamicFunds"].ToString() != "")
                {
                    model.TotalDynamicFunds = decimal.Parse(dt.Rows[0]["TotalDynamicFunds"].ToString());
                }
                if (dt.Rows[0]["Interest"].ToString() != "")
                {
                    model.Interest = decimal.Parse(dt.Rows[0]["Interest"].ToString());
                }
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                if (dt.Rows[0]["StaticCapital"].ToString() != "")
                {
                    model.StaticCapital = decimal.Parse(dt.Rows[0]["StaticCapital"].ToString());
                }
                if (dt.Rows[0]["DynamicFunds"].ToString() != "")
                {
                    model.DynamicFunds = decimal.Parse(dt.Rows[0]["DynamicFunds"].ToString());
                }
                if (dt.Rows[0]["StaticInterest"].ToString() != "")
                {
                    model.StaticInterest = decimal.Parse(dt.Rows[0]["StaticInterest"].ToString());
                }
                if (dt.Rows[0]["DynamicInterest"].ToString() != "")
                {
                    model.DynamicInterest = decimal.Parse(dt.Rows[0]["DynamicInterest"].ToString());
                }
                if (dt.Rows[0]["StaticPunishMoney"].ToString() != "")
                {
                    model.StaticPunishMoney = decimal.Parse(dt.Rows[0]["StaticPunishMoney"].ToString());
                }
                if (dt.Rows[0]["DynamicPunishMoney"].ToString() != "")
                {
                    model.DynamicPunishMoney = decimal.Parse(dt.Rows[0]["DynamicPunishMoney"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 统计平台总计提供的帮助和接受帮助的金额
        /// </summary>
        /// <param name="dynamicTotal"></param>
        /// <returns></returns>
        public static decimal GetTotalAmontForPlant(out decimal dynamicTotal)
        {
            decimal staticTotal = 0;
            dynamicTotal = 0;
            string sqltxt = @"SELECT  SUM(ISNULL(TotalStaticCapital,0)) as staticnum ,
        SUM(ISNULL(TotalDynamicFunds,0)) as dynamicnum
FROM    SimpleWebDataBase.dbo.MemberCapitalDetail";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            if (dt.Rows.Count > 0)
            {
                staticTotal = dt.Rows[0]["staticnum"].ToString().ParseToDecimal(0);
                dynamicTotal = dt.Rows[0]["dynamicnum"].ToString().ParseToDecimal(0);
            }
            return staticTotal;
        }
        /// <summary>
        /// 解冻会员的静态利息和静态冻结资金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="money"></param>
        /// <param name="instermoney"></param>
        /// <returns></returns>
        public static int UpdateStaticInterestAndStaticFreezeMoney(int memberid, decimal money, decimal instermoney)
        {
            int result = 0;
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
SET     StaticCapital = ISNULL(StaticCapital, 0) + @money + @insertmoney ,
        StaticInterest = ISNULL(StaticInterest, 0) - @insertmoney ,
        StaticFreezeMoney = ISNULL(StaticFreezeMoney, 0) - @money,
        TotalStaticCapital=ISNULL(TotalStaticCapital, 0) + @money + @insertmoney
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@memberid",memberid),
                                          new SqlParameter("@insertmoney",instermoney),
                                          new SqlParameter("@money",money)
                                      };
            result = helper.ExecuteSql(sqltxt, paramter);
            return result;
        }
        /// <summary>
        /// 解冻会员的动态利息金额（领导奖）
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int UpdateDynamicInterest(int memberid, decimal money)
        {
            int result = 0;
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
SET     DynamicFunds = ISNULL(DynamicFunds, 0) + @money,
        DynamicInterest = ISNULL(DynamicInterest, 0) - @money,
        TotalDynamicFunds=ISNULL(TotalDynamicFunds, 0) + @money  
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@money",money),
                                          new SqlParameter("@memberid",memberid)
                                      };
            result = helper.ExecuteSql(sqltxt,paramter);
            return result;
        }
        /// <summary>
        /// 解冻会员的动态冻结金额（推荐奖）
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int UpdateDynamicInterestForComplete(int memberid, decimal money)
        {
            int result = 0;
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
SET     DynamicFunds = ISNULL(DynamicFunds, 0) + @money,
        DynamicFreezeMoney = ISNULL(DynamicFreezeMoney, 0) - @money,
        TotalDynamicFunds=ISNULL(TotalDynamicFunds, 0) + @money  
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@money",money),
                                          new SqlParameter("@memberid",memberid)
                                      };
            result = helper.ExecuteSql(sqltxt, paramter);
            return result;
        }
        /// <summary>
        /// 为会员的推荐人分派奖金
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="amount"></param>
        /// <param name="intereststr"></param>
        /// <param name="ordercode"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static int PaymentLeaderPrizeForComplete(int memberid, string intereststr, string ordercode, int oid)
        {
            int result = 0;
            List<int> interests = new List<int>();
            if (!string.IsNullOrWhiteSpace(intereststr))
            {
                foreach (var item in intereststr.Split(','))
                {
                    interests.Add(item.ParseToInt(0));
                }
            }
            if (interests.Count < 1)
                return result;
            //按照配置的利率查找推荐图谱
            List<ReMemberRelationModel> members = new List<ReMemberRelationModel>();
            for (int i = 0; i < interests.Count; i++)
            {
                if (i == 0)
                {
                    ReMemberRelationModel member = ReMemberRelationDAL.GetReMemberRelation(memberid);
                    if (member != null)
                        members.Add(member);
                }
                else
                {
                    if (members.Count > (i - 1))
                    {
                        ReMemberRelationModel member = ReMemberRelationDAL.GetReMemberRelation(members[i - 1].MemberID);
                        if (member != null)
                            members.Add(member);
                    }
                }
            }
            for (int i = 0; i < members.Count; i++)
            {
                LeaderAmountModel leaderamount = GetLeaderModel(members[i].MemberID,oid);
                if (leaderamount == null)
                    continue;
                //int rowcount = UpdateMemberDynamicFunds(members[i].MemberID, (money / 100 * interests[i]), members[i].MemberTruthName, members[i].MemberPhone);
                int rowcount = UpdateDynamicInterest(members[i].MemberID, leaderamount.Amount);
                if (rowcount < 1)
                {
                    return result;
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = members[i].MemberID;
                logmodel.MemberName = members[i].MemberTruthName;
                logmodel.MemberPhone = members[i].MemberPhone;
                logmodel.OrderCode = ordercode;
                logmodel.OrderID = oid;
                logmodel.ProduceMoney = leaderamount.Amount;
                logmodel.Remark = "会员:" + members[i].MemberPhone + " 解冻来自单据：" + ordercode + "的领导奖";
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return result;
                }
                result++;
            }
            return result;

        }

        public static LeaderAmountModel GetLeaderModel(int memberid,int oid)
        {
            LeaderAmountModel model = new LeaderAmountModel();
            string sqltxt = @"SELECT  ID ,
        OrderID ,
        OrderCode ,
        MemberID ,
        MemberName ,
        MemberPhone ,
        Amount
FROM    SimpleWebDataBase.dbo.LeaderAmount
WHERE   OrderID = @orderid
        AND MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@orderid",oid),
                                      new SqlParameter("@memberid",memberid)};
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                model.Amount = dt.Rows[0]["Amount"].ToString().ParseToDecimal(0);
                model.MemberID = dt.Rows[0]["MemberID"].ToString().ParseToInt(0);
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                model.OrderID = dt.Rows[0]["OrderID"].ToString().ParseToInt(0);
                return model;
            }
            else {
                return null;
            }
           
        }
    }
}
