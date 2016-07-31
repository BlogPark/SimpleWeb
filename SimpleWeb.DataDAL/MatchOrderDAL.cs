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
            strSql.Append("HelperOrderID,HelperOrderCode,HelperMemberID,AcceptOrderID,AcceptOrderCode,AcceptMemberID,MatchedMoney,MatchTime,MatchStatus");
            strSql.Append(") values (");
            strSql.Append("@HelperOrderID,@HelperOrderCode,@HelperMemberID,@AcceptOrderID,@AcceptOrderCode,@AcceptMemberID,@MatchedMoney,GETDATE(),1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@HelperOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@HelperOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HelperMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderID", SqlDbType.Int) ,            
                        new SqlParameter("@AcceptOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AcceptMemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MatchedMoney", SqlDbType.Decimal) 
            };
            parameters[0].Value = model.HelperOrderID;
            parameters[1].Value = model.HelperOrderCode;
            parameters[2].Value = model.HelperMemberID;
            parameters[3].Value = model.AcceptOrderID;
            parameters[4].Value = model.AcceptOrderCode;
            parameters[5].Value = model.AcceptMemberID;
            parameters[6].Value = model.MatchedMoney;
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
            strSql.Append("  where MatchStatus=1 ");
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
FROM    SimpleWebDataBase.dbo.MatchOrder A
        INNER JOIN SimpleWebDataBase.dbo.HelpeOrder B ON A.HelperOrderID = B.ID
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
        public static int UpdateStatusToPayed(int hid,int aid)
        {
            string sqltxt = @"UPDATE MatchOrder  
                               SET MatchStatus=3  
                               WHERE HelperOrderID=@hid AND AcceptOrderID=@aid AND MatchStatus=1";
            SqlParameter[] paramter = { new SqlParameter("@hid",hid),
                                      new SqlParameter("@aid",aid)};
            return helper.ExecuteSql(sqltxt,paramter);
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
                               SET MatchStatus=4  
                               WHERE HelperOrderID=@hid AND AcceptOrderID=@aid AND MatchStatus=1";
            SqlParameter[] paramter = { new SqlParameter("@hid",hid),
                                      new SqlParameter("@aid",aid)};
            return helper.ExecuteSql(sqltxt, paramter);
        }
    }
}
