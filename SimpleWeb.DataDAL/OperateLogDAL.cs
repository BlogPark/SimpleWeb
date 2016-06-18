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
    public  class OperateLogDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();

        /// <summary>
        /// 增加资金变动日志
        /// </summary>
        public static int AddAmountChangeLog(AmountChangeLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AmountChangeLog(");
            strSql.Append("MemberID,MemberPhone,MemberName,ProduceMoney,Remark,AddTime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberPhone,@MemberName,@ProduceMoney,@Remark,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ProduceMoney", SqlDbType.Decimal) ,         
                        new SqlParameter("@Remark", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AddTime", SqlDbType.DateTime)           
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberPhone;
            parameters[2].Value = model.MemberName;
            parameters[3].Value = model.ProduceMoney;
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
    }
}
