﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataDAL
{
    public class HelpeOrderDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddHelpeOrder(HelpeOrderModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HelpeOrder(");
            strSql.Append("MatchedAmount,AddTime,SortIndex,OrderCode,MemberID,MemberPhone,MemberName,Amount,Interest,PayType,HStatus,ActiveCode");
            strSql.Append(") values (");
            strSql.Append("@MatchedAmount,@AddTime,@SortIndex,@OrderCode,@MemberID,@MemberPhone,@MemberName,@Amount,@Interest,@PayType,@HStatus,@ActiveCode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MatchedAmount", SqlDbType.Decimal) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@SortIndex", SqlDbType.Int) ,            
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal) ,            
                        new SqlParameter("@Interest", SqlDbType.Decimal) ,            
                        new SqlParameter("@PayType", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HStatus", SqlDbType.Int),
                        new SqlParameter("@ActiveCode",SqlDbType.NVarChar)
            };
            parameters[0].Value = model.MatchedAmount;
            parameters[1].Value = model.AddTime;
            parameters[2].Value = model.SortIndex;
            parameters[3].Value = model.OrderCode;
            parameters[4].Value = model.MemberID;
            parameters[5].Value = model.MemberPhone;
            parameters[6].Value = model.MemberName;
            parameters[7].Value = model.Amount;
            parameters[8].Value = model.Interest;
            parameters[9].Value = model.PayType;
            parameters[10].Value = model.HStatus;
            parameters[11].Value = model.ActiveCode;
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
        public List<HelpeOrderModel> GetAllHelpeOrderList()
        {
            List<HelpeOrderModel> list = new List<HelpeOrderModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, MatchedAmount, AddTime, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, Interest, PayType, HStatus,CASE HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销'  WHEN 4 THEN '已打款'  WHEN 5 THEN '已确认' END AS HStatusName  ");
            strSql.Append("  from HelpeOrder ");
            strSql.Append(" ORDER BY SortIndex DESC ,ID DESC ");

            DataSet ds = helper.Query(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    HelpeOrderModel model = new HelpeOrderModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["MatchedAmount"].ToString() != "")
                    {
                        model.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
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
                    if (item["Interest"].ToString() != "")
                    {
                        model.Interest = decimal.Parse(item["Interest"].ToString());
                    }
                    model.PayType = item["PayType"].ToString();
                    if (item["HStatus"].ToString() != "")
                    {
                        model.HStatus = int.Parse(item["HStatus"].ToString());
                    }
                    model.HStatusName = item["HStatusName"].ToString();
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
        public List<HelpeOrderModel> GetAllHelpeOrderListForPage(HelpeOrderModel model, out int totalrowcount)
        {
            List<HelpeOrderModel> list = new List<HelpeOrderModel>();
            string columms = @"ID, MatchedAmount, AddTime, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, Interest, PayType, HStatus,CASE HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销'  WHEN 4 THEN '已打款'  WHEN 5 THEN '已确认' END AS HStatusName";
            string where = "";
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    where += "OrderCode=" + model.OrderCode;
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
                if (model.HStatus > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += @" HStatus =" + model.HStatus;
                }
                else if (model.HStatus > 0 && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND HStatus =" + model.HStatus;
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "SortIndex";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.HelpeOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                HelpeOrderModel helpeordermodel = new HelpeOrderModel();
                if (item["ID"].ToString() != "")
                {
                    helpeordermodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["MatchedAmount"].ToString() != "")
                {
                    helpeordermodel.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    helpeordermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
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
                if (item["Interest"].ToString() != "")
                {
                    helpeordermodel.Interest = decimal.Parse(item["Interest"].ToString());
                }
                helpeordermodel.PayType = item["PayType"].ToString();
                if (item["HStatus"].ToString() != "")
                {
                    helpeordermodel.HStatus = int.Parse(item["HStatus"].ToString());
                }
                helpeordermodel.HStatusName = item["HStatusName"].ToString();
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 添加提供帮助订单
        /// </summary>
        /// <returns></returns>
        public int InsertHelperOrder(HelpeOrderModel model)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //插入帮助订单表
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into HelpeOrder(");
                strSql.Append("MatchedAmount,AddTime,SortIndex,OrderCode,MemberID,MemberPhone,MemberName,Amount,Interest,PayType,HStatus");
                strSql.Append(") values (");
                strSql.Append("@MatchedAmount,@AddTime,@SortIndex,@OrderCode,@MemberID,@MemberPhone,@MemberName,@Amount,@Interest,@PayType,@HStatus");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
			            new SqlParameter("@MatchedAmount", SqlDbType.Decimal) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@SortIndex", SqlDbType.Int) ,            
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal) ,            
                        new SqlParameter("@Interest", SqlDbType.Decimal) ,            
                        new SqlParameter("@PayType", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HStatus", SqlDbType.Int)
            };
                parameters[0].Value = model.MatchedAmount;
                parameters[1].Value = model.AddTime;
                parameters[2].Value = model.SortIndex;
                parameters[3].Value = model.OrderCode;
                parameters[4].Value = model.MemberID;
                parameters[5].Value = model.MemberPhone;
                parameters[6].Value = model.MemberName;
                parameters[7].Value = model.Amount;
                parameters[8].Value = model.Interest;
                parameters[9].Value = model.PayType;
                parameters[10].Value = model.HStatus;
                int obj = int.Parse(helper.GetSingle(strSql.ToString(), parameters).ToString());
                //修改会员的资金表
                string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    SimpleWebDataBase.dbo.MemberCapitalDetail
            WHERE   MemberID = @MemberID )
    BEGIN
        UPDATE  SimpleWebDataBase.dbo.MemberCapitalDetail
        SET     StaticCapital = @Amount ,
                TotalStaticCapital = TotalStaticCapital + @StaticCapital
        WHERE   MemberID = @MemberID
    END
ELSE
    BEGIN
        INSERT  INTO SimpleWebDataBase.dbo.MemberCapitalDetail
                ( MemberID ,
                  StaticCapital ,
                  TotalStaticCapital
                )
        VALUES  ( @MemberID ,
                  @Amount ,
                  @Amount
                )
    END";
                int rowcount = helper.ExecuteSql(sqltxt, parameters);
                //插入日志记录表
                string sql = @"INSERT  INTO SimpleWebDataBase.dbo.AmountChangeLog
        ( MemberID ,
          MemberPhone ,
          MemberName ,
          ProduceMoney ,
          Remark ,
          AddTime ,
          OrderID ,
          [Type] ,
          OrderCode
        )
VALUES  ( @MemberID ,
          @MemberPhone ,
          @MemberName ,
          @ProduceMoney ,
          @Remark ,
          GETDATE() ,
          @OrderID ,
          1 ,
          @OrderCode
        )";
                SqlParameter[] paramter ={
                                             new SqlParameter("@MemberID",SqlDbType.Int),
                                             new SqlParameter("@MemberPhone",SqlDbType.NVarChar),
                                             new SqlParameter("@MemberName",SqlDbType.NVarChar),
                                             new SqlParameter("@ProduceMoney",SqlDbType.Decimal),
                                             new SqlParameter("@Remark",SqlDbType.NVarChar),
                                             new SqlParameter("@OrderID",SqlDbType.Int),
                                             new SqlParameter("@OrderCode",SqlDbType.NVarChar)
                                         };
                paramter[0].Value = model.MemberID;
                paramter[1].Value = model.MemberPhone;
                paramter[2].Value = model.MemberName;
                paramter[3].Value = model.Amount;
                paramter[4].Value = "会员"+model.MemberPhone+"提供帮助";
                paramter[5].Value = obj;
                paramter[6].Value = model.OrderCode;
                rowcount = helper.ExecuteSql(sql,paramter);
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatus(int oid,int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HelpeOrder set ");
            strSql.Append(" HStatus = @HStatus  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@HStatus", SqlDbType.Int) 
            };
            parameters[0].Value = oid;
            parameters[1].Value = status;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 更改置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSortindex(int oid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HelpeOrder set ");
            strSql.Append(" SortIndex = 100  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int)
            };
            parameters[0].Value = oid;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 查询所有的待匹配的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<HelpeOrderModel> GetWaitHelpeOrderListForPage(HelpeOrderModel model, out int totalrowcount)
        {
            List<HelpeOrderModel> list = new List<HelpeOrderModel>();
            string columms = @"ID, MatchedAmount, AddTime, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, Interest, PayType, HStatus,CASE HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部匹配' WHEN 3 THEN '已撤销'  WHEN 4 THEN '已打款'  WHEN 5 THEN '已确认' END AS HStatusName,DATEDIFF(DAY,AddTime,GETDATE()) AS diffday";
            string where = " HStatus=1";           
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "DATEDIFF(DAY,AddTime,GETDATE())";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.HelpeOrder";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                HelpeOrderModel helpeordermodel = new HelpeOrderModel();
                if (item["ID"].ToString() != "")
                {
                    helpeordermodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["MatchedAmount"].ToString() != "")
                {
                    helpeordermodel.MatchedAmount = decimal.Parse(item["MatchedAmount"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    helpeordermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
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
                if (item["Interest"].ToString() != "")
                {
                    helpeordermodel.Interest = decimal.Parse(item["Interest"].ToString());
                }
                helpeordermodel.PayType = item["PayType"].ToString();
                if (item["HStatus"].ToString() != "")
                {
                    helpeordermodel.HStatus = int.Parse(item["HStatus"].ToString());
                }
                helpeordermodel.HStatusName = item["HStatusName"].ToString();
                helpeordermodel.DiffDay = int.Parse(item["diffday"].ToString());
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 更新提供帮助订单的信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static int UpdateHelpOrderMatch(int orderid,decimal money)
        {
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.HelpeOrder
SET   HStatus = CASE ( Amount - (ISNULL(MatchedAmount,0)+ @amount) )
                    WHEN 0 THEN 2
                    ELSE 1
                  END,
         MatchedAmount =ISNULL(MatchedAmount,0)+ @amount 
WHERE   id = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",orderid),
            new SqlParameter("@amount",money)
                                      };
            return helper.ExecuteSql(sqltxt,paramter);
        }
        /// <summary>
        /// 根据ID查询提供帮助订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static HelpeOrderModel GetHelpOrderInfo(int id)
        {
            string sqltxt = @"select OrderCode,MemberID,MemberPhone,MemberName,Amount,(Amount-ISNULL(MatchedAmount)) as DiffAmount from HelpeOrder where ID=@id";
            SqlParameter[] paramter={
                                        new SqlParameter("@id",id)
                                    };
            DataTable dt=helper.Query(sqltxt,paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                HelpeOrderModel model = new HelpeOrderModel();
                model.MemberID=Convert.ToInt32(dt.Rows[0]["MemberID"].ToString());
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.OrderCode = dt.Rows[0]["OrderCode"].ToString();
                model.Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
                model.DiffAmount = Convert.ToDecimal(dt.Rows[0]["DiffAmount"].ToString());
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
        public static int UpdateStatusForComplete(int hid)
        {
            string sqltxt = @"UPDATE  HelpeOrder
SET     HStatus = CASE ( Amount - ISNULL(MatchedAmount, 0) )
                    WHEN 0 THEN 5
                    ELSE 2
                  END
WHERE   id = @id";
            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int) 
            };
            parameters[0].Value =hid;
            int rows = helper.ExecuteSql(sqltxt, parameters);
            return rows;
        }
    }
}