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
    public class HelpeOrderDAL
    {
        private DbHelperSQL helper = new DbHelperSQL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddHelpeOrder(HelpeOrderModel model)
        {
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
            strSql.Append("select ID, MatchedAmount, AddTime, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, Interest, PayType, HStatus,CASE HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' END AS HStatusName  ");
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
            string columms = @"ID, MatchedAmount, AddTime, SortIndex, OrderCode, MemberID, MemberPhone, MemberName, Amount, Interest, PayType, HStatus,CASE HStatus WHEN 0 THEN '未匹配' WHEN 1 THEN '部分匹配' WHEN 2 THEN '全部成交' WHEN 3 THEN '已撤销' END AS HStatusName";
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
                //if (!string.IsNullOrWhiteSpace(model.TruethName) && string.IsNullOrWhiteSpace(where))
                //{
                //    where += @" TruethName ='" + model.TruethName + "'";
                //}
                //else if (!string.IsNullOrWhiteSpace(model.TruethName) && !string.IsNullOrWhiteSpace(where))
                //{
                //    where += @" AND TruethName ='" + model.TruethName + "'";
                //}
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
            page.orderby = "ID";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.MemberInfo";
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
    }
}
