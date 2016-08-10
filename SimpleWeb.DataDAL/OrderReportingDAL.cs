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
    public class OrderReportingDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加单据的举报信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddOrderReporting(OrderReportingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OrderReporting(");
            strSql.Append("AddTime,LastUpdateTime,OrderCode,MemberID,MemberName,MemberPhone,Title,ReportingText,ReasonType,RStatus,OrderID,OrderType");
            strSql.Append(") values (");
            strSql.Append("GETDATE(),GETDATE(),@OrderCode,@MemberID,@MemberName,@MemberPhone,@Title,@ReportingText,@ReasonType,1,@OrderID,@OrderType");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {         
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReportingText", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReasonType", SqlDbType.NVarChar),
                        new SqlParameter("@OrderID",SqlDbType.Int),
                        new SqlParameter("@OrderType",SqlDbType.Int)
            };
            parameters[0].Value = model.OrderCode;
            parameters[1].Value = model.MemberID;
            parameters[2].Value = model.MemberName;
            parameters[3].Value = model.MemberPhone;
            parameters[4].Value = model.Title;
            parameters[5].Value = model.ReportingText;
            parameters[6].Value = model.ReasonType;
            parameters[7].Value = model.OrderID;
            parameters[8].Value = model.OrderType;
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
        /// 更新举报信息为已处理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool UpdateHandleResult(int id,string result)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderReporting set ");
            strSql.Append(" HandleResult = @HandleResult , ");
            strSql.Append(" LastUpdateTime = GETDATE() , ");
            strSql.Append(" RStatus = 3  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@HandleResult", SqlDbType.NVarChar,300)               
            };
            parameters[0].Value = id;
            parameters[1].Value = result;
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
        /// 更新举报信息为取消
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool UpdateToCancle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OrderReporting set ");
            strSql.Append(" LastUpdateTime = GETDATE() , ");
            strSql.Append(" RStatus = 4  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4)           
            };
            parameters[0].Value = id;
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
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<OrderReportingModel> GetAllOrderReportingForPage(OrderReportingModel model, out int totalrowcount)
        {
            List<OrderReportingModel> list = new List<OrderReportingModel>();
            string columms = @"ID ,OrderID,OrderCode,OrderType,MemberID,MemberName,MemberPhone,Title,ReportingText,ReasonType,RStatus,CASE RStatus WHEN 1 THEN '新举报' WHEN  2 THEN '处理中' WHEN  3 THEN '已处理' WHEN  4 THEN '已取消' AS RStatusName,HandleResult,AddTime,LastUpdateTime";
            string where = "";
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    where += "OrderCode='" + model.OrderCode + "'";
                }
                if (model.MemberID > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += " MemberID=" + model.MemberID.ToString();
                }
                else if (!string.IsNullOrWhiteSpace(where) && model.MemberID > 0)
                {
                    where += @" AND MemberID=" + model.MemberID.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model.MemberPhone) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberPhone = '" + model.MemberPhone + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberPhone) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberPhone = '" + model.MemberPhone + "'";
                }
                if (!string.IsNullOrWhiteSpace(model.MemberName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberName ='" + model.MemberName + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberName ='" + model.MemberName + "'";
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
                OrderReportingModel OrderReportingmodel = new OrderReportingModel();
                if (item["ID"].ToString() != "")
                {
                    OrderReportingmodel.ID = int.Parse(item["ID"].ToString());
                }
                OrderReportingmodel.HandleResult = item["HandleResult"].ToString();
                if (item["AddTime"].ToString() != "")
                {
                    OrderReportingmodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                if (item["LastUpdateTime"].ToString() != "")
                {
                    OrderReportingmodel.LastUpdateTime = DateTime.Parse(item["LastUpdateTime"].ToString());
                }
                OrderReportingmodel.OrderCode = item["OrderCode"].ToString();
                if (item["MemberID"].ToString() != "")
                {
                    OrderReportingmodel.MemberID = int.Parse(item["MemberID"].ToString());
                }
                OrderReportingmodel.MemberName = item["MemberName"].ToString();
                OrderReportingmodel.MemberPhone = item["MemberPhone"].ToString();
                OrderReportingmodel.Title = item["Title"].ToString();
                OrderReportingmodel.ReportingText = item["ReportingText"].ToString();
                OrderReportingmodel.ReasonType = item["ReasonType"].ToString();
                if (item["RStatus"].ToString() != "")
                {
                    OrderReportingmodel.RStatus = int.Parse(item["RStatus"].ToString());
                }
                list.Add(OrderReportingmodel);
            }
            return list;
        }

    }
}
