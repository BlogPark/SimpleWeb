﻿using System;
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
        public static int AddMatchOrder(MatchOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MatchOrder(");
            strSql.Append("HelperOrderID,HelperOrderCode,HelperMemberID,AcceptOrderID,AcceptOrderCode,AcceptMemberID,MatchedMoney,MatchTime,MatchStatus,LastUpdateTime,HelperMemberName,HelperMemberPhone,AcceptMemberName,AcceptMemberPhone");
            strSql.Append(") values (");
            strSql.Append("@HelperOrderID,@HelperOrderCode,@HelperMemberID,@AcceptOrderID,@AcceptOrderCode,@AcceptMemberID,@MatchedMoney,GETDATE(),1,GETDATE(),@HelperMemberName,@HelperMemberPhone,@AcceptMemberName,@AcceptMemberPhone");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@HelperOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@HelperOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HelperMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AcceptMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MatchedMoney", SqlDbType.Decimal) ,  
                        new SqlParameter("@HelperMemberName", SqlDbType.NVarChar) , 
                        new SqlParameter("@HelperMemberPhone", SqlDbType.NVarChar) , 
                        new SqlParameter("@AcceptMemberName", SqlDbType.NVarChar) , 
                        new SqlParameter("@AcceptMemberPhone", SqlDbType.NVarChar)
            };
            parameters[0].Value = model.HelperOrderID;
            parameters[1].Value = model.HelperOrderCode;
            parameters[2].Value = model.HelperMemberID;
            parameters[3].Value = model.AcceptOrderID;
            parameters[4].Value = model.AcceptOrderCode;
            parameters[5].Value = model.AcceptMemberID;
            parameters[6].Value = model.MatchedMoney;
            parameters[7].Value = model.HelperMemberName;
            parameters[8].Value = model.HelperMemberPhone;
            parameters[9].Value = model.AcceptMemberName;
            parameters[10].Value = model.AcceptMemberPhone;
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
        /// <summary>
        /// 得到匹配的单据信息
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static List<MatchOrderModel> GetMatchOrderInfo(int hid, int aid)
        {
            List<MatchOrderModel> list = new List<MatchOrderModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, HelperOrderID, HelperOrderCode, HelperMemberID, AcceptOrderID, AcceptOrderCode, AcceptMemberID, MatchedMoney, MatchTime  ");
            strSql.Append("  from MatchOrder ");
            strSql.Append("  where MatchStatus<>2 ");
            if (hid > 0)
            {
                strSql.Append(" AND HelperOrderID=@hid ");
            }
            if (aid > 0)
            {
                strSql.Append(" AND AcceptOrderID=@aid ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@hid", SqlDbType.Int),
                    new SqlParameter("@aid", SqlDbType.Int)
			};
            parameters[0].Value = hid;
            parameters[1].Value = aid;
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    MatchOrderModel model = new MatchOrderModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["HelperOrderID"].ToString() != "")
                    {
                        model.HelperOrderID = int.Parse(item["HelperOrderID"].ToString());
                    }
                    model.HelperOrderCode = item["HelperOrderCode"].ToString();
                    if (item["HelperMemberID"].ToString() != "")
                    {
                        model.HelperMemberID = int.Parse(item["HelperMemberID"].ToString());
                    }
                    if (item["AcceptOrderID"].ToString() != "")
                    {
                        model.AcceptOrderID = int.Parse(item["AcceptOrderID"].ToString());
                    }
                    model.AcceptOrderCode = item["AcceptOrderCode"].ToString();
                    if (item["AcceptMemberID"].ToString() != "")
                    {
                        model.AcceptMemberID = int.Parse(item["AcceptMemberID"].ToString());
                    }
                    if (item["MatchedMoney"].ToString() != "")
                    {
                        model.MatchedMoney = decimal.Parse(item["MatchedMoney"].ToString());
                    }
                    if (item["MatchTime"].ToString() != "")
                    {
                        model.MatchTime = DateTime.Parse(item["MatchTime"].ToString());
                    }
                    list.Add(model);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更改匹配订单的信息
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static int UpdateStatus(int hid, int aid)
        {
            string sqltxt = @" UPDATE MatchOrder 
  SET MatchStatus=2";
            if (hid > 0)
            {
                sqltxt += " where HelperOrderID=@hid AND MatchStatus=1 ";
            }
            if (aid > 0)
            {
                sqltxt += " where AcceptOrderID=@aid AND MatchStatus=1 ";
            }
            SqlParameter[] parameters = {
					new SqlParameter("@hid", SqlDbType.Int),
                    new SqlParameter("@aid", SqlDbType.Int)
			};
            parameters[0].Value = hid;
            parameters[1].Value = aid;
            return helper.ExecuteSql(sqltxt, parameters);

        }
        /// <summary>
        /// 根据Aid读取匹配的提供帮助列表
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static List<HelpeOrderModel> GetMatchHelpOrderByAid(int aid)
        {
            List<HelpeOrderModel> list = new List<HelpeOrderModel>();
            string sqlttx = @"SELECT  B.ID ,
        B.OrderCode ,
        ( B.Amount - B.MatchedAmount ) AS diffmoney ,
        B.IsFristOrder ,
        B.Amount ,
        B.Interest,
        b.MemberID,
        b.MemberName,
        b.MemberPhone
FROM    dbo.MatchOrder A
        INNER JOIN dbo.HelpeOrder B ON A.HelperOrderID = B.ID
WHERE   A.AcceptOrderID = @aid
        AND MatchStatus = 1";
            SqlParameter[] paramter = { new SqlParameter("@aid", aid) };
            DataTable dt = helper.Query(sqlttx, paramter).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                HelpeOrderModel model = new HelpeOrderModel();
                model.ID = item["ID"].ToString().ParseToInt(0);
                model.OrderCode = item["OrderCode"].ToString();
                model.DiffAmount = item["diffmoney"].ToString().ParseToDecimal(0);
                model.IsFristOrder = item["IsFristOrder"].ToString().ParseToInt(0);
                model.Amount = item["Amount"].ToString().ParseToDecimal(0);
                model.Interest = item["Interest"].ToString().ParseToDecimal(0);
                model.MemberID = item["MemberID"].ToString().ParseToInt(0);
                model.MemberName = item["MemberName"].ToString();
                model.MemberPhone = item["MemberPhone"].ToString();
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 更新匹配单据状态为已打款
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static int UpdateStatusToPayed(int hid, int aid)
        {
            string sqltxt = @"UPDATE MatchOrder  
                               SET MatchStatus=3,LastUpdateTime=GETDATE(),PaymentedTime=GETDATE()
                               WHERE HelperOrderID=@hid AND AcceptOrderID=@aid AND MatchStatus=1";
            SqlParameter[] paramter = { new SqlParameter("@hid",hid),
                                      new SqlParameter("@aid",aid)};
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更新匹配单据状态为已确认
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static int UpdateStatusToComplete(int hid, int aid)
        {
            string sqltxt = @"UPDATE MatchOrder  
                               SET MatchStatus=4,LastUpdateTime=GETDATE(),CompleteTime=GETDATE()
                               WHERE HelperOrderID=@hid AND AcceptOrderID=@aid AND MatchStatus=3";
            SqlParameter[] paramter = { new SqlParameter("@hid",hid),
                                      new SqlParameter("@aid",aid)};
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 按天计算匹配金额
        /// </summary>
        /// <param name="datastart"></param>
        /// <param name="dataend"></param>
        /// <returns></returns>
        public static decimal GetTotalMatchMoneyByDay(string datastart, string dataend)
        {
            string sqltxt = @"SELECT  ISNULL(SUM(MatchedMoney),0)
FROM    dbo.MatchOrder
WHERE   MatchTime >= @datastart
        AND MatchTime <= @dataend
        AND MatchStatus <> 2";
            SqlParameter[] paramter = { new SqlParameter("@datastart", datastart), new SqlParameter("@dataend", dataend) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToDecimal(0);
        }
        /// <summary>
        /// 计算已经匹配的所有金额
        /// </summary>
        /// <param name="datastart"></param>
        /// <param name="dataend"></param>
        /// <returns></returns>
        public static decimal GetTotalMatchMoney()
        {
            string sqltxt = @"SELECT  ISNULL(SUM(MatchedMoney),0)
FROM    dbo.MatchOrder
WHERE   MatchStatus <> 2";
            return helper.GetSingle(sqltxt).ToString().ParseToDecimal(0);
        }
        /// <summary>
        /// 按页查询匹配的单据信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<MatchOrderModel> GetMatchedOrderListByPage(MatchOrderModel model, out int totalrowcount)
        {
            List<MatchOrderModel> list = new List<MatchOrderModel>();
            string columms = @" ID,HelperOrderID,HelperOrderCode,HelperMemberID,AcceptOrderID,AcceptOrderCode,AcceptMemberID,MatchedMoney,MatchTime,MatchStatus,LastUpdateTime,CASE MatchStatus WHEN 1 THEN '已匹配' WHEN 2 THEN '已取消' WHEN 3 THEN '已打款'  WHEN 4 THEN '已完成' END AS MatchStatusName,HelperMemberName,HelperMemberPhone,AcceptMemberName,AcceptMemberPhone ";
            string where = "";
            if (model != null)
            {
                if (model.MatchStatus > 0)
                {
                    where += "MatchStatus='" + model.MatchStatus + "'";
                }
                if (!string.IsNullOrWhiteSpace(model.HelperOrderCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += " HelperOrderCode='" + model.HelperOrderCode.ToString() + "'";
                }
                else if (!string.IsNullOrWhiteSpace(where) && !string.IsNullOrWhiteSpace(model.HelperOrderCode))
                {
                    where += @" AND HelperOrderCode='" + model.HelperOrderCode.ToString() + "'";
                }
                if (!string.IsNullOrWhiteSpace(model.AcceptOrderCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" AcceptOrderCode = '" + model.AcceptOrderCode + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.AcceptOrderCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND AcceptOrderCode = '" + model.AcceptOrderCode + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.MatchOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                MatchOrderModel matchordermodel = new MatchOrderModel();

                if (item["ID"].ToString() != "")
                {
                    matchordermodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["MatchStatus"].ToString() != "")
                {
                    matchordermodel.MatchStatus = int.Parse(item["MatchStatus"].ToString());
                }
                if (item["LastUpdateTime"].ToString() != "")
                {
                    matchordermodel.LastUpdateTime = DateTime.Parse(item["LastUpdateTime"].ToString());
                }
                if (item["HelperOrderID"].ToString() != "")
                {
                    matchordermodel.HelperOrderID = int.Parse(item["HelperOrderID"].ToString());
                }
                matchordermodel.HelperOrderCode = item["HelperOrderCode"].ToString();
                matchordermodel.HelperMemberName = item["HelperMemberName"].ToString();
                matchordermodel.HelperMemberPhone = item["HelperMemberPhone"].ToString();
                matchordermodel.AcceptMemberName = item["AcceptMemberName"].ToString();
                matchordermodel.AcceptMemberPhone = item["AcceptMemberPhone"].ToString();
                if (item["HelperMemberID"].ToString() != "")
                {
                    matchordermodel.HelperMemberID = int.Parse(item["HelperMemberID"].ToString());
                }
                if (item["AcceptOrderID"].ToString() != "")
                {
                    matchordermodel.AcceptOrderID = int.Parse(item["AcceptOrderID"].ToString());
                }
                matchordermodel.AcceptOrderCode = item["AcceptOrderCode"].ToString();
                if (item["AcceptMemberID"].ToString() != "")
                {
                    matchordermodel.AcceptMemberID = int.Parse(item["AcceptMemberID"].ToString());
                }
                if (item["MatchedMoney"].ToString() != "")
                {
                    matchordermodel.MatchedMoney = decimal.Parse(item["MatchedMoney"].ToString());
                }
                if (item["MatchTime"].ToString() != "")
                {
                    matchordermodel.MatchTime = DateTime.Parse(item["MatchTime"].ToString());
                }
                matchordermodel.MatchStatusName = item["MatchStatusName"].ToString();
                list.Add(matchordermodel);
            }
            return list;
        }
        /// <summary>
        /// 按照会员ID查询匹配的提供帮助信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="topnum"></param>
        /// <returns></returns>
        public static List<ExtendMatchOrdersModel> GetHelperMatchedOrderListByMemberID(int memberid, int topnum)
        {
            List<ExtendMatchOrdersModel> list = new List<ExtendMatchOrdersModel>();
            string sqltxt = @"SELECT top (@topnum)  A.HelperOrderCode ,
        A.HelperOrderID,
        A.AcceptOrderCode,
        A.AcceptOrderID,
        A.ID ,
        A.MatchedMoney ,
        CASE A.MatchStatus
          WHEN 1 THEN '已匹配'
          WHEN 2 THEN '已取消'
          WHEN 3 THEN '已打款'
          WHEN 4 THEN '已完成'
        END MatchStatusName ,
        B.PayType ,
        A.LastUpdateTime ,
        A.PaymentedTime ,
        A.CompleteTime ,
        A.MatchStatus ,
        B.AddTime,A.MatchTime
FROM    dbo.MatchOrder A
        INNER JOIN dbo.HelpeOrder B ON A.HelperOrderID = B.ID
WHERE   HelperMemberID = @memberid
ORDER BY A.LastUpdateTime DESC ";
            SqlParameter[] paramter = { new SqlParameter("@topnum", topnum), new SqlParameter("@memberid", memberid) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ExtendMatchOrdersModel model = new ExtendMatchOrdersModel();
                    model.AcceptOrderCode = item["AcceptOrderCode"].ToString();
                    model.AcceptOrderID = item["AcceptOrderID"].ToString().ParseToInt(0);
                    model.AddTime = item["AddTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.CompleteTime = item["CompleteTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.HelperOrderCode = item["HelperOrderCode"].ToString();
                    model.HelperOrderID = item["HelperOrderID"].ToString().ParseToInt(0);
                    model.LastUpdateTime = item["LastUpdateTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.MatchedMoney = item["MatchedMoney"].ToString().ParseToDecimal(0);
                    model.MatchID = item["ID"].ToString().ParseToInt(0);
                    model.MatchStatus = item["MatchStatus"].ToString().ParseToInt(1);
                    model.MatchStatusName = item["MatchStatusName"].ToString();
                    model.PaymentedTime = item["PaymentedTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.PayType = item["PayType"].ToString();
                    model.MatchTime = item["MatchTime"].ToString().ParseToDateTime(DateTime.Now);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 按照会员ID查询匹配的接受帮助信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="topnum"></param>
        /// <returns></returns>
        public static List<ExtendMatchOrdersModel> GetAcceptMatchedOrderListByMemberID(int memberid, int topnum)
        {
            List<ExtendMatchOrdersModel> list = new List<ExtendMatchOrdersModel>();
            string sqltxt = @"SELECT top (@topnum)  A.HelperOrderCode ,
        A.HelperOrderID,
        A.AcceptOrderCode,
        A.AcceptOrderID,
        A.ID ,
        A.MatchedMoney ,
        CASE A.MatchStatus
          WHEN 1 THEN '已匹配'
          WHEN 2 THEN '已取消'
          WHEN 3 THEN '已打款'
          WHEN 4 THEN '已完成'
        END MatchStatusName ,
        B.PayType ,
        A.LastUpdateTime ,
        A.PaymentedTime ,
        A.CompleteTime ,
        A.MatchStatus ,
        B.AddTime,A.MatchTime
FROM    dbo.MatchOrder A
        INNER JOIN dbo.AcceptHelpOrder B ON A.AcceptOrderID = B.ID
WHERE   AcceptMemberID = @memberid
ORDER BY A.LastUpdateTime DESC ";
            SqlParameter[] paramter = { new SqlParameter("@topnum", topnum), new SqlParameter("@memberid", memberid) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ExtendMatchOrdersModel model = new ExtendMatchOrdersModel();
                    model.AcceptOrderCode = item["AcceptOrderCode"].ToString();
                    model.AcceptOrderID = item["AcceptOrderID"].ToString().ParseToInt(0);
                    model.AddTime = item["AddTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.CompleteTime = item["CompleteTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.HelperOrderCode = item["HelperOrderCode"].ToString();
                    model.HelperOrderID = item["HelperOrderID"].ToString().ParseToInt(0);
                    model.LastUpdateTime = item["LastUpdateTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.MatchedMoney = item["MatchedMoney"].ToString().ParseToDecimal(0);
                    model.MatchID = item["ID"].ToString().ParseToInt(0);
                    model.MatchStatus = item["MatchStatus"].ToString().ParseToInt(1);
                    model.MatchStatusName = item["MatchStatusName"].ToString();
                    model.PaymentedTime = item["PaymentedTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.PayType = item["PayType"].ToString();
                    model.MatchTime = item["MatchTime"].ToString().ParseToDateTime(DateTime.Now);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
