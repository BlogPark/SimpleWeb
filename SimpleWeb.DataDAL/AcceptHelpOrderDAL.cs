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
    public class AcceptHelpOrderDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAcceptHelpOrder(AcceptHelpOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AcceptHelpOrder(");
            strSql.Append("AddTime,LastUpdateTime,AStatus,SortIndex,OrderCode,MemberID,MemberPhone,MemberName,Amount,PayType,MatchedAmount,TurnOutOrder,SourceType");
            strSql.Append(") values (");
            strSql.Append("GETDATE(),GETDATE(),@AStatus,@SortIndex,@OrderCode,@MemberID,@MemberPhone,@MemberName,@Amount,@PayType,@MatchedAmount,@TurnOutOrder,@SourceType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {           
                        new SqlParameter("@AStatus", SqlDbType.Int) ,            
                        new SqlParameter("@SortIndex", SqlDbType.Int) ,            
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal) ,            
                        new SqlParameter("@PayType", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MatchedAmount", SqlDbType.Decimal) ,            
                        new SqlParameter("@TurnOutOrder", SqlDbType.NVarChar),
                        new SqlParameter("@SourceType",SqlDbType.NVarChar)
            };
            parameters[0].Value = model.AStatus;
            parameters[1].Value = model.SortIndex;
            parameters[2].Value = model.OrderCode;
            parameters[3].Value = model.MemberID;
            parameters[4].Value = model.MemberPhone;
            parameters[5].Value = model.MemberName;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.PayType;
            parameters[8].Value = model.MatchedAmount;
            parameters[9].Value = model.TurnOutOrder;
            parameters[10].Value = model.SourceType;
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
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetAllAcceptOrderList()
        {
            List<AcceptHelpOrderModel> list = new List<AcceptHelpOrderModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已完成' END AS AStatusName  ");
            strSql.Append("  from AcceptHelpOrder ");
            strSql.Append(" ORDER BY SortIndex DESC ,ID DESC ");
            DataSet ds = helper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    AcceptHelpOrderModel model = new AcceptHelpOrderModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["AStatus"].ToString() != "")
                    {
                        model.AStatus = int.Parse(item["AStatus"].ToString());
                    }
                    if (item["SortIndex"].ToString() != "")
                    {
                        model.SortIndex = int.Parse(item["SortIndex"].ToString());
                    }
                    model.OrderCode = item["OrderCode"].ToString();
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberName = item["MemberName"].ToString();
                    if (item["Amount"].ToString() != "")
                    {
                        model.Amount = decimal.Parse(item["Amount"].ToString());
                    }
                    model.PayType = item["PayType"].ToString();
                    if (item["MatchedAmount"].ToString() != "")
                    {
                        model.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                    }
                    model.TurnOutOrder = item["TurnOutOrder"].ToString();
                    model.AStatusName = item["AStatusName"].ToString();
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
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetAllAcceptOrderListForPage(AcceptHelpOrderModel model, out int totalrowcount)
        {
            List<AcceptHelpOrderModel> list = new List<AcceptHelpOrderModel>();
            string columms = @"ID, AddTime, AStatus, SourceType,SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已完成' END AS AStatusName,CASE SourceType WHEN 1 THEN '静态资金' WHEN 2 THEN '动态资金' END AS SourceTypeName ";
            string where = "";
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    where += "OrderCode='" + model.OrderCode + "'";
                }
                if (model.MemberID > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberID =" + model.MemberID;
                }
                else if (model.MemberID > 0 && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberID =" + model.MemberID;
                }
                if (!string.IsNullOrWhiteSpace(model.MemberPhone) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberPhone Like '%" + model.MemberPhone + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberPhone) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberPhone Like '%" + model.MemberPhone + "%'";
                }
                if (!string.IsNullOrWhiteSpace(model.MemberName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberName ='" + model.MemberName + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberName ='" + model.MemberName + "'";
                }
                if (model.AStatus > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += @" AStatus =" + model.AStatus;
                }
                else if (model.AStatus > 0 && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND AStatus =" + model.AStatus;
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "SortIndex";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.AcceptHelpOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                AcceptHelpOrderModel helpeordermodel = new AcceptHelpOrderModel();
                if (item["ID"].ToString() != "")
                {
                    helpeordermodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    helpeordermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                if (item["AStatus"].ToString() != "")
                {
                    helpeordermodel.AStatus = int.Parse(item["AStatus"].ToString());
                }
                if (item["SortIndex"].ToString() != "")
                {
                    helpeordermodel.SortIndex = int.Parse(item["SortIndex"].ToString());
                }
                helpeordermodel.OrderCode = item["OrderCode"].ToString();
                if (item["MemberID"].ToString() != "")
                {
                    helpeordermodel.MemberID = int.Parse(item["MemberID"].ToString());
                }
                helpeordermodel.MemberPhone = item["MemberPhone"].ToString();
                helpeordermodel.MemberName = item["MemberName"].ToString();
                if (item["Amount"].ToString() != "")
                {
                    helpeordermodel.Amount = decimal.Parse(item["Amount"].ToString());
                }
                model.PayType = item["PayType"].ToString();
                if (item["MatchedAmount"].ToString() != "")
                {
                    helpeordermodel.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                }
                if (item["SourceType"].ToString() != "")
                {
                    helpeordermodel.SourceType = item["SourceType"].ToString().ParseToInt(0);
                }
                helpeordermodel.SourceTypeName = item["SourceTypeName"].ToString();
                helpeordermodel.TurnOutOrder = item["TurnOutOrder"].ToString();
                helpeordermodel.AStatusName = item["AStatusName"].ToString();
                list.Add(helpeordermodel);
            }
            return list;
        }
        /// <summary>
        /// 查询所有的待匹配的接受帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetWaitAcceptOrderListForPage(AcceptHelpOrderModel model, out int totalrowcount)
        {
            List<AcceptHelpOrderModel> list = new List<AcceptHelpOrderModel>();
            string columms = @" ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已完成' END AS AStatusName,DATEDIFF(DAY,AddTime,GETDATE()) AS diffday ";
            string where = " AStatus not in (3,5) and (Amount-MatchedAmount)>0 ";
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = " (DATEDIFF(DAY,AddTime,GETDATE())) ";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.AcceptHelpOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                AcceptHelpOrderModel acceptordermodel = new AcceptHelpOrderModel();
                if (item["ID"].ToString() != "")
                {
                    acceptordermodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    acceptordermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                if (item["AStatus"].ToString() != "")
                {
                    acceptordermodel.AStatus = int.Parse(item["AStatus"].ToString());
                }
                if (item["SortIndex"].ToString() != "")
                {
                    acceptordermodel.SortIndex = int.Parse(item["SortIndex"].ToString());
                }
                acceptordermodel.OrderCode = item["OrderCode"].ToString();
                if (item["MemberID"].ToString() != "")
                {
                    acceptordermodel.MemberID = int.Parse(item["MemberID"].ToString());
                }
                acceptordermodel.MemberPhone = item["MemberPhone"].ToString();
                acceptordermodel.MemberName = item["MemberName"].ToString();
                if (item["Amount"].ToString() != "")
                {
                    acceptordermodel.Amount = decimal.Parse(item["Amount"].ToString());
                }
                acceptordermodel.PayType = item["PayType"].ToString();
                if (item["MatchedAmount"].ToString() != "")
                {
                    acceptordermodel.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                }
                acceptordermodel.TurnOutOrder = item["TurnOutOrder"].ToString();
                acceptordermodel.AStatusName = item["AStatusName"].ToString();
                acceptordermodel.DiffDay = int.Parse(item["diffday"].ToString());
                list.Add(acceptordermodel);
            }
            return list;
        }
        /// <summary>
        /// 更新提供帮助订单的信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int UpdateAcceptOrderMatch(int orderid, decimal money)
        {
            string sqltxt = @"UPDATE  dbo.AcceptHelpOrder
SET  AStatus = CASE ( Amount - (ISNULL(MatchedAmount,0)+ @amount) )
                    WHEN 0 THEN 2
                    ELSE 1
                  END,LastUpdateTime=GETDATE(),
          MatchedAmount =ISNULL(MatchedAmount,0)+ @amount
WHERE   id = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",orderid),
            new SqlParameter("@amount",money)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 根据ID查询提供帮助订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AcceptHelpOrderModel GetAcceptOrderInfo(int id)
        {
            string sqltxt = @"select AStatus,OrderCode,MemberID,MemberPhone,MemberName,Amount,(Amount-ISNULL(MatchedAmount,0)) as DiffAmount,SourceType,MatchedAmount,DATEDIFF(DAY,AddTime,GETDATE()) AS diffday  from AcceptHelpOrder where ID=@id";
            SqlParameter[] paramter ={
                                        new SqlParameter("@id",id)
                                    };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                AcceptHelpOrderModel model = new AcceptHelpOrderModel();
                model.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                model.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                model.DiffAmount = Convert.ToDecimal(dt.Rows[0]["DiffAmount"].ToString());
                model.SourceType = Convert.ToInt32(dt.Rows[0]["SourceType"].ToString());
                model.MatchedAmount = Convert.ToDecimal(dt.Rows[0]["MatchedAmount"].ToString());
                model.DiffDay = dt.Rows[0]["diffday"].ToString().ParseToInt(0);
                model.AStatus = dt.Rows[0]["AStatus"].ToString().ParseToInt(0);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据ID查询提供帮助订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AcceptHelpOrderModel GetAcceptOrderInfo(int id, int memberid)
        {
            string sqltxt = @"SELECT  ID,OrderCode ,
        MemberID ,
        MemberPhone ,
        MemberName ,
        Amount ,
        ( Amount - ISNULL(MatchedAmount, 0) ) AS DiffAmount ,
        SourceType ,
        MatchedAmount,
        DATEDIFF(DAY,AddTime,GETDATE()) AS diffday,
        CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已完成' END AS AStatusName
       ,CASE SourceType WHEN 1 THEN '静态资金' WHEN 2 THEN '动态资金' END AS SourceTypeName,
      AStatus,
     PayType
FROM    AcceptHelpOrder
WHERE   ID = @id AND MemberID=@memberid";
            SqlParameter[] paramter ={
                                        new SqlParameter("@id",id),
                                        new SqlParameter("@memberid",memberid)
                                    };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                AcceptHelpOrderModel model = new AcceptHelpOrderModel();
                model.ID = dt.Rows[0]["ID"].ToString().ParseToInt(0);
                model.MemberID = Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                model.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                model.DiffAmount = Convert.ToDecimal(dt.Rows[0]["DiffAmount"].ToString());
                model.SourceType = Convert.ToInt32(dt.Rows[0]["SourceType"].ToString());
                model.MatchedAmount = Convert.ToDecimal(dt.Rows[0]["MatchedAmount"].ToString());
                model.DiffDay = dt.Rows[0]["diffday"].ToString().ParseToInt(0);
                model.SourceTypeName = dt.Rows[0]["SourceTypeName"].ToString();
                model.PayType = dt.Rows[0]["PayType"].ToString();
                model.AStatus = dt.Rows[0]["AStatus"].ToString().ParseToInt(0);
                model.AStatusName = dt.Rows[0]["AStatusName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatus(int aid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AcceptHelpOrder set ");
            strSql.Append(" AStatus = @AStatus , ");
            strSql.Append(" LastUpdateTime=GETDATE()  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@AStatus", SqlDbType.Int) 
            };
            parameters[0].Value = aid;
            parameters[1].Value = status;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 更改状态为完成
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatusToComplete(int aid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AcceptHelpOrder set ");
            strSql.Append(" AStatus = ( CASE ( Amount - ISNULL(MatchedAmount, 0) ) WHEN 0 THEN 5 ELSE 1 END ),");
            strSql.Append(" LastUpdateTime=GETDATE()  ");
            strSql.Append(" where ID=@ID AND AStatus=4");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) 
            };
            parameters[0].Value = aid;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 更改状态为完成
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatusAndMoneyToComplete(int aid, decimal money)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AcceptHelpOrder set ");
            strSql.Append(" AStatus = ( CASE ( Amount - ISNULL(FinishAmount, 0)-@amount ) WHEN 0 THEN 5 ELSE 1 END ),");
            strSql.Append(" FinishAmount=ISNULL(FinishAmount, 0)+@amount,  ");
            strSql.Append(" LastUpdateTime=GETDATE()  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,
                        new SqlParameter("@amount", SqlDbType.Decimal)
            };
            parameters[0].Value = aid;
            parameters[1].Value = money;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }

        /// <summary>
        /// 更新接受帮助的订单的状态
        /// </summary>
        /// <returns></returns>
        public static int CancleOrderForHelp(int aid, decimal money)
        {
            string sqltxt = @"UPDATE  AcceptHelpOrder
SET     AStatus = CASE ( MatchedAmount - @money )
                    WHEN 0 THEN 0
                    ELSE 1
                  END ,LastUpdateTime=GETDATE(),
        MatchedAmount = MatchedAmount - @money
WHERE   id = @id";
            SqlParameter[] paramter = { new SqlParameter("@id", aid), new SqlParameter("@money", money) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 更改接受帮助置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSortindex(int aid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AcceptHelpOrder set ");
            strSql.Append(" SortIndex = 100  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int)
            };
            parameters[0].Value = aid;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 根据接受帮助订单号得到匹配的信息
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public static List<HelpOrderExtendInfoModel> GetHelpextendmodels(int aid)
        {
            List<HelpOrderExtendInfoModel> list = new List<HelpOrderExtendInfoModel>();
            string sqltxt = @"SELECT  A.ID ,
        A.OrderCode ,
        A.MemberID ,
        A.MemberPhone ,
        A.MemberName ,
        B.WeixinNum ,
        B.AliPayNum ,
        B.AliPayName,
        C.MemberID AS rememberid ,
        C.MemberPhone AS rememberphone ,
        C.MemberTruthName AS remembername ,
        D.MatchedMoney,
        A.HStatus,
        D.MatchStatus,
        D.LastUpdateTime,
        D.PaymentedTime,
        D.CompleteTime,
        D.MatchTime,
        CASE D.MatchStatus WHEN 1 THEN '已匹配' WHEN 2 THEN '已取消' WHEN 3 THEN '已打款' WHEN 4 THEN '已完成' END AS MatchStatusName,
        CASE A.HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销'  WHEN 4 THEN '对方已打款'  WHEN 5 THEN '双方已确认' END AS HStatusName
FROM   dbo.MatchOrder D
        INNER JOIN dbo.HelpeOrder A ON D.HelperOrderID = A.ID
        INNER JOIN dbo.MemberInfo B ON A.MemberID = B.ID
        INNER JOIN dbo.ReMemberRelation C ON B.ID = C.RecommMID
WHERE   D.AcceptOrderID = @orderid";
            SqlParameter[] paramter = { new SqlParameter("@orderid", aid) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            { 
                foreach (DataRow item in dt.Rows)
                {
                    HelpOrderExtendInfoModel model = new HelpOrderExtendInfoModel();
                    model.helpmemberAlipayId = item["AliPayNum"].ToString();//支付宝名称
                    model.helpmemberAlipayName = item["AliPayName"].ToString();//支付宝ID
                    model.helpmemberid = item["MemberID"].ToString().ParseToInt(0);//会员id
                    model.helpmemberName = item["MemberName"].ToString();//会员名称
                    model.helpmemberPhone = item["MemberPhone"].ToString();//会员电话
                    model.helpmemberweixin = item["WeixinNum"].ToString();//会员微信
                    model.helpordercode = item["OrderCode"].ToString();//单据编号
                    model.helporderid = item["ID"].ToString().ParseToInt(0);//单据ID
                    model.MatchedMoney = item["MatchedMoney"].ToString().ParseToDecimal(0);//匹配金额
                    model.rememberid = item["rememberid"].ToString().ParseToInt(0);
                    model.remembername = item["remembername"].ToString();
                    model.rememberphone = item["rememberphone"].ToString();
                    model.HStatusName = item["HStatusName"].ToString();
                    model.HStatus = item["HStatus"].ToString().ParseToInt(0);
                    model.MatchStatus = item["MatchStatus"].ToString().ParseToInt(1);
                    model.MatchStatusName = item["MatchStatusName"].ToString();
                    model.LastUpdateTime = item["LastUpdateTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.PaymentedTime = item["PaymentedTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.CompleteTime = item["CompleteTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.MatchTime = item["MatchTime"].ToString().ParseToDateTime(DateTime.Now);
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public static List<AcceptHelpOrderModel> GetTopAcceptOrderListByMemberID(int memberid, int top = 10)
        {
            List<AcceptHelpOrderModel> list = new List<AcceptHelpOrderModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP (@topnum) ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已完成' END AS AStatusName,CASE SourceType WHEN 1 THEN '静态资金' WHEN 2 THEN '动态资金' END AS SourceTypeName  ");
            strSql.Append("  from AcceptHelpOrder ");
            strSql.Append(" WHERE MemberID=@memberid ");
            strSql.Append(" ORDER BY ID DESC ");
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid), new SqlParameter("@topnum", top) };
            DataSet ds = helper.Query(strSql.ToString(), paramter);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    AcceptHelpOrderModel model = new AcceptHelpOrderModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["AStatus"].ToString() != "")
                    {
                        model.AStatus = int.Parse(item["AStatus"].ToString());
                    }
                    if (item["SortIndex"].ToString() != "")
                    {
                        model.SortIndex = int.Parse(item["SortIndex"].ToString());
                    }
                    model.OrderCode = item["OrderCode"].ToString();
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberName = item["MemberName"].ToString();
                    if (item["Amount"].ToString() != "")
                    {
                        model.Amount = decimal.Parse(item["Amount"].ToString());
                    }
                    model.PayType = item["PayType"].ToString();
                    if (item["MatchedAmount"].ToString() != "")
                    {
                        model.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                    }
                    model.TurnOutOrder = item["TurnOutOrder"].ToString();
                    model.AStatusName = item["AStatusName"].ToString();
                    model.SourceTypeName = item["SourceTypeName"].ToString();
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
        /// 查询会员是否还有未完成的提供订单
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int GetNotFinishOrderByMember(int memberid)
        {
            string sqltxt = @"SELECT  ID
FROM    dbo.AcceptHelpOrder
WHERE   MemberID = @memberid
        AND AStatus < 3";
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }

        /// <summary>
        /// 返回系统排单总金额
        /// </summary>
        /// <returns></returns>
        public static decimal GetTotalAcceptMoney()
        {
            string sqltxt = @"SELECT  ISNULL(SUM(Amount),0)
  FROM dbo.AcceptHelpOrder
  WHERE AStatus<>3 ";
            return helper.GetSingle(sqltxt).ToString().ParseToDecimal(0);
        }
        /// <summary>
        /// 按天查询接受帮助的金额
        /// </summary>
        /// <param name="datastart"></param>
        /// <param name="dataend"></param>
        /// <returns></returns>
        public static decimal GetTodayAcceptMoney(string datastart, string dataend)
        {
            string sqltxt = @"SELECT  ISNULL(SUM(Amount), 0)
FROM    dbo.AcceptHelpOrder
WHERE   AddTime >= @starttime
        AND AddTime <= @endtime AND AStatus=0 ";
            SqlParameter[] paramter = { new SqlParameter("@starttime", datastart), new SqlParameter("@endtime", dataend) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToDecimal(0);

        }
    }
}
