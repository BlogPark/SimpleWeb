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
            strSql.Append("AddTime,AStatus,SortIndex,OrderCode,MemberID,MemberPhone,MemberName,Amount,PayType,MatchedAmount,TurnOutOrder,SourceType");
            strSql.Append(") values (");
            strSql.Append("@AddTime,@AStatus,@SortIndex,@OrderCode,@MemberID,@MemberPhone,@MemberName,@Amount,@PayType,@MatchedAmount,@TurnOutOrder,@SourceType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
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
            parameters[0].Value = model.AddTime;
            parameters[1].Value = model.AStatus;
            parameters[2].Value = model.SortIndex;
            parameters[3].Value = model.OrderCode;
            parameters[4].Value = model.MemberID;
            parameters[5].Value = model.MemberPhone;
            parameters[6].Value = model.MemberName;
            parameters[7].Value = model.Amount;
            parameters[8].Value = model.PayType;
            parameters[9].Value = model.MatchedAmount;
            parameters[10].Value = model.TurnOutOrder;
            parameters[11].Value = model.SourceType;
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
            strSql.Append("select ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' END AS AStatusName  ");
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
            string columms = @"ID, AddTime, AStatus, SourceType,SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' END AS AStatusName,CASE SourceType WHEN 1 THEN '静态资金' WHEN 2 THEN '动态资金' END AS SourceTypeName ";
            string where = "";
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    where += "OrderCode='" + model.OrderCode+"'";
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
                if (item["SourceType"].ToString() != "")
                {
                    model.SourceType = item["SourceType"].ToString().ParseToInt(0);
                }
                model.SourceTypeName = item["SourceTypeName"].ToString();
                model.TurnOutOrder = item["TurnOutOrder"].ToString();
                model.AStatusName = item["AStatusName"].ToString();
                list.Add(model);
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
            string columms = @" ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' END AS AStatusName,DATEDIFF(DAY,AddTime,GETDATE()) AS diffday ";
            string where = " AStatus <2 ";
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
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.AcceptHelpOrder
SET  AStatus = CASE ( Amount - (ISNULL(MatchedAmount,0)+ @amount) )
                    WHEN 0 THEN 2
                    ELSE 1
                  END,
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
            string sqltxt = @"select OrderCode,MemberID,MemberPhone,MemberName,Amount,(Amount-ISNULL(MatchedAmount,0)) as DiffAmount,SourceType,MatchedAmount,DATEDIFF(DAY,AddTime,GETDATE()) AS diffday  from AcceptHelpOrder where ID=@id";
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
        public static AcceptHelpOrderModel GetAcceptOrderInfo(int id,int memberid)
        {
            string sqltxt = @"SELECT  OrderCode ,
        MemberID ,
        MemberPhone ,
        MemberName ,
        Amount ,
        ( Amount - ISNULL(MatchedAmount, 0) ) AS DiffAmount ,
        SourceType ,
        MatchedAmount,
        DATEDIFF(DAY,AddTime,GETDATE()) AS diffday,
        CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已收款' END AS AStatusName
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
            strSql.Append(" AStatus = @AStatus  ");
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
        /// 更新接受帮助的订单的状态
        /// </summary>
        /// <returns></returns>
        public static int CancleOrderForHelp(int aid, decimal money)
        {
            string sqltxt = @"UPDATE  AcceptHelpOrder
SET     AStatus = CASE ( MatchedAmount - @money )
                    WHEN 0 THEN 0
                    ELSE 1
                  END ,
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
        C.MemberID AS rememberid ,
        C.MemberPhone AS rememberphone ,
        C.MemberTruthName AS remembername ,
        D.MatchedMoney
FROM    SimpleWebDataBase.dbo.MatchOrder D
        INNER JOIN SimpleWebDataBase.dbo.HelpeOrder A ON D.HelperOrderID = A.ID
        INNER JOIN SimpleWebDataBase.dbo.MemberInfo B ON A.MemberID = B.ID
        INNER JOIN SimpleWebDataBase.dbo.ReMemberRelation C ON B.ID = C.RecommMID
WHERE   D.AcceptOrderID = @orderid";
            SqlParameter[] paramter = { new SqlParameter("@orderid", aid) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    HelpOrderExtendInfoModel model = new HelpOrderExtendInfoModel();
                    model.helpmemberAlipayId = item["AliPayNum"].ToString();//支付宝账户
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
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public static List<AcceptHelpOrderModel> GetTopAcceptOrderListByMemberID(int memberid,int top=10)
        {
            List<AcceptHelpOrderModel> list = new List<AcceptHelpOrderModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP (@topnum) ID, AddTime, AStatus, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, PayType, MatchedAmount, TurnOutOrder,CASE AStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' WHEN 4 THEN '对方已打款' WHEN 5 THEN '已收款' END AS AStatusName,CASE SourceType WHEN 1 THEN '静态资金' WHEN 2 THEN '动态资金' END AS SourceTypeName  ");
            strSql.Append("  from AcceptHelpOrder ");
            strSql.Append(" WHERE MemberID=@memberid ");
            strSql.Append(" ORDER BY ID DESC ");
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid), new SqlParameter("@topnum", top) };
            DataSet ds = helper.Query(strSql.ToString(),paramter);
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
    }
}
