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
    public class OperateLogDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();

        /// <summary>
        /// 增加资金变动日志
        /// </summary>
        public static int AddAmountChangeLog(AmountChangeLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AmountChangeLog(");
            strSql.Append("Type,OrderCode,MemberID,MemberPhone,MemberName,ProduceMoney,Remark,AddTime,OrderID");
            strSql.Append(") values (");
            strSql.Append("@Type,@OrderCode,@MemberID,@MemberPhone,@MemberName,@ProduceMoney,@Remark,GETDATE(),@OrderID");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Type", SqlDbType.Int) ,            
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ProduceMoney", SqlDbType.Decimal) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@OrderID", SqlDbType.Int)      
            };
            parameters[0].Value = model.Type;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.MemberID;
            parameters[3].Value = model.MemberPhone;
            parameters[4].Value = model.MemberName;
            parameters[5].Value = model.ProduceMoney;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.OrderID;
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
        /// 增加用户行为日志
        /// </summary>
        public static int AddUserBehaviorLog(UserBehaviorLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserBehaviorLog(");
            strSql.Append("MemberID,MemberPhone,MemberName,HappenIP,Remark,AddTime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberPhone,@MemberName,@HappenIP,@Remark,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@HappenIP", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)      
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberPhone;
            parameters[2].Value = model.MemberName;
            parameters[3].Value = model.HappenIP;
            parameters[4].Value = model.Remark;
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
        /// 添加激活码操作记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddActiveCodeLog(ActiveCodeLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ActiveCodeLog(");
            strSql.Append("MemberID,MemberName,MemberPhone,ActiveCode,AID,Remark,Addtime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberPhone,@ActiveCode,@AID,@Remark,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ActiveCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AID", SqlDbType.Int) ,            
                        new SqlParameter("@Remark", SqlDbType.NVarChar)             
              
            };

            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberPhone;
            parameters[3].Value = model.ActiveCode;
            parameters[4].Value = model.AID;
            parameters[5].Value = model.Remark;

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
        /// 查询会员的机会码使用记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static List<ActiveCodeLogModel> GetActiveCodeLogByMemberID(int memberid, int top = 10)
        {
            List<ActiveCodeLogModel> list = new List<ActiveCodeLogModel>();
            string sqltxt = @"SELECT TOP ( @topnum )
        ID ,
        MemberID ,
        MemberName ,
        MemberPhone ,
        ActiveCode ,
        AID ,
        Remark ,
        Addtime
FROM    SimpleWebDataBase.dbo.ActiveCodeLog
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { new SqlParameter("@memberid", memberid), new SqlParameter("@topnum", top) };
            DataTable dt = helper.Query(sqltxt,paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActiveCodeLogModel model = new ActiveCodeLogModel();
                    model.ActiveCode = item["ActiveCode"].ToString();
                    model.Addtime = item["Addtime"].ToString().ParseToDateTime(DateTime.Now);
                    model.AID = item["AID"].ToString().ParseToInt(0);
                    model.ID = item["ID"].ToString().ParseToInt(0);
                    model.MemberID = item["MemberID"].ToString().ParseToInt(0);
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.Remark = item["Remark"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询会员的资金变动记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static List<AmountChangeLogModel> GetAmontChangeLogByMemberID(int memberid, int top = 10)
        {
            List<AmountChangeLogModel> list = new List<AmountChangeLogModel>();
            string sqltxt = @"SELECT TOP ( @topnum )
        ID ,
        MemberID ,
        MemberPhone ,
        MemberName ,
        ProduceMoney ,
        Remark ,
        AddTime ,
        OrderID ,
        [Type] ,
        OrderCode,
        CASE [Type]
          WHEN 1 THEN '提供帮助'
          WHEN 2 THEN '接受帮助'
          WHEN 3 THEN '奖金派发'
          WHEN 4 THEN '利息结余'
          WHEN 5 THEN '系统返还'
        END AS TypeName
FROM    SimpleWebDataBase.dbo.AmountChangeLog
WHERE   MemberID = @memberid";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@memberid",memberid),
                                      new SqlParameter("@topnum",top)
                                      };
            DataTable dt = helper.Query(sqltxt,paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    AmountChangeLogModel model = new AmountChangeLogModel();
                    model.AddTime = item["AddTime"].ToString().ParseToDateTime(DateTime.Now);
                    model.ID = item["ID"].ToString().ParseToInt(0);
                    model.MemberID = item["MemberID"].ToString().ParseToInt(0);
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.OrderCode = item["OrderCode"].ToString();
                    model.OrderID = item["OrderID"].ToString().ParseToInt(0);
                    model.ProduceMoney = item["ProduceMoney"].ToString().ParseToDecimal(0);
                    model.Remark = item["Remark"].ToString();
                    model.Type = item["Type"].ToString().ParseToInt(0);
                    model.TypeName = item["TypeName"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据类型得到分页的日志数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="type"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public static List<AmountChangeLogModel> GetAmountChangeLogByTypeForPage(int pageindex, int pagesize, int type, out int totalrowcount)
        {
            List<AmountChangeLogModel> list = new List<AmountChangeLogModel>();
            string columms = @"ID ,MemberID ,MemberPhone ,MemberName ,ProduceMoney ,Remark ,AddTime ,OrderID ,[Type] ,OrderCode";
            string where = " [Type]="+type.ToString();                
            
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "AddTime";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.AmountChangeLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                AmountChangeLogModel model = new AmountChangeLogModel();
                if (item["ID"].ToString() != "")
                {
                    model.ID = int.Parse(item["ID"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                if (item["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(item["MemberID"].ToString());
                }
                model.MemberPhone = item["MemberPhone"].ToString();
                if (item["ProduceMoney"].ToString() != "")
                {
                    model.ProduceMoney = decimal.Parse(item["ProduceMoney"].ToString());
                }
                model.MemberName = item["MemberName"].ToString();
                if (item["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                }
                model.Remark = item["Remark"].ToString();
                if (item["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(item["OrderID"].ToString());
                }
                model.OrderCode = item["OrderCode"].ToString();
                model.Type = int.Parse(item["Type"].ToString());
                list.Add(model);
            }
            return list;
        }
    }
}
