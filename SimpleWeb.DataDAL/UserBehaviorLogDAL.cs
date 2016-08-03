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
    public class UserBehaviorLogDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加用户操作日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddUserBehaviorLog(UserBehaviorLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserBehaviorLog(");
            strSql.Append("Remark,AddTime,MemberID,MemberPhone,MemberName,BehaviorSource,BehaviorType,ProcAmount,HOrderCode,AOrderCode");
            strSql.Append(") values (");
            strSql.Append("@Remark,GETDATE(),@MemberID,@MemberPhone,@MemberName,@BehaviorSource,@BehaviorType,@ProcAmount,@HOrderCode,@AOrderCode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Remark", SqlDbType.NVarChar) ,           
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@BehaviorSource", SqlDbType.Int) ,            
                        new SqlParameter("@BehaviorType", SqlDbType.Int) ,            
                        new SqlParameter("@ProcAmount", SqlDbType.Decimal) ,            
                        new SqlParameter("@HOrderCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AOrderCode", SqlDbType.NVarChar) 
            };

            parameters[0].Value = model.Remark;
            parameters[1].Value = model.MemberID;
            parameters[2].Value = model.MemberPhone;
            parameters[3].Value = model.MemberName;
            parameters[4].Value = model.BehaviorSource;
            parameters[5].Value = model.BehaviorType;
            parameters[6].Value = model.ProcAmount;
            parameters[7].Value = model.HOrderCode;
            parameters[8].Value = model.AOrderCode;
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
        /// 查询用户操作数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<UserBehaviorLogModel> GetUserBehaviorLogByPage(UserBehaviorLogModel model, out int totalrowcount)
        {
            List<UserBehaviorLogModel> list = new List<UserBehaviorLogModel>();
            string columms = @" ID,MemberID,MemberPhone,MemberName,BehaviorSource,BehaviorType,ProcAmount,HOrderCode,AOrderCode,Remark,AddTime,CASE BehaviorType WHEN 1 THEN '登陆' WHEN  2 THEN '提供帮助' WHEN  3 THEN '接受帮助' WHEN 4  THEN '变更打款'  WHEN 5  THEN '确认单据'  WHEN 6  THEN '撤销单据'  WHEN 7  THEN '发放排单币'  WHEN 8  THEN '发放激活币'  WHEN 9  THEN '奖励会员'  WHEN 10  THEN '惩罚会员'  WHEN 11  THEN '系统派息' END AS BehaviorTypeName,CASE BehaviorSource WHEN 1 THEN '前端' WHEN 2 THEN '后台' END AS BehaviorSourceName ";
            string where = "";
            if (model != null)
            {
                if (model.BehaviorType>0)
                {
                    where += "BehaviorType='" + model.BehaviorType + "'";
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
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.UserBehaviorLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    UserBehaviorLogModel usermodel = new UserBehaviorLogModel();
                    if (item["ID"].ToString() != "")
                    {
                        usermodel.ID = int.Parse(item["ID"].ToString());
                    }
                    usermodel.Remark = item["Remark"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        usermodel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        usermodel.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    usermodel.MemberPhone = item["MemberPhone"].ToString();
                    usermodel.MemberName = item["MemberName"].ToString();
                    if (item["BehaviorSource"].ToString() != "")
                    {
                        usermodel.BehaviorSource = int.Parse(item["BehaviorSource"].ToString());
                    }
                    if (item["BehaviorType"].ToString() != "")
                    {
                        usermodel.BehaviorType = int.Parse(item["BehaviorType"].ToString());
                    }
                    if (item["ProcAmount"].ToString() != "")
                    {
                        usermodel.ProcAmount = decimal.Parse(item["ProcAmount"].ToString());
                    }
                    usermodel.HOrderCode = item["HOrderCode"].ToString();
                    usermodel.AOrderCode = item["AOrderCode"].ToString();
                    usermodel.BehaviorSourceName = item["BehaviorSourceName"].ToString();
                    usermodel.BehaviorTypeName = item["BehaviorTypeName"].ToString();
                    list.Add(usermodel);
                }
            }
            return list;
        }
    }
}
