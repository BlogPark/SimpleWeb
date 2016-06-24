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
    public class ReMemberRelationDAL
    {
        private static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 添加会员推荐关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddReMemberRelation(ReMemberRelationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ReMemberRelation(");
            strSql.Append("MemberID,MemberPhone,MemberTruthName,RecommMID,RecommMPhone,RecommMTruthName,RStatus,AddTime");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberPhone,@MemberTruthName,@RecommMID,@RecommMPhone,@RecommMTruthName,1,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberTruthName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@RecommMID", SqlDbType.Int) ,            
                        new SqlParameter("@RecommMPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@RecommMTruthName", SqlDbType.NVarChar) 
            };

            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberPhone;
            parameters[2].Value = model.MemberTruthName;
            parameters[3].Value = model.RecommMID;
            parameters[4].Value = model.RecommMPhone;
            parameters[5].Value = model.RecommMTruthName;
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
        /// 废除推荐关系
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int UpdateReMemberRelationStatus(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReMemberRelation set ");
            strSql.Append(" RStatus = 0  ");
            strSql.Append(" where  ID=@ID  ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int)
            };
            parameters[0].Value = ID;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 废除推荐关系
        /// </summary>
        /// <returns></returns>
        public static int UpdateReMemberRelationStatus(int memberid,int recommemberid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ReMemberRelation set ");
            strSql.Append(" RStatus = 0  ");
            strSql.Append(" where MemberID=@memberid AND RecommMID=@recommmid");
            SqlParameter[] parameters = {
			            new SqlParameter("@memberid", SqlDbType.Int),
                        new  SqlParameter("@recommmid",SqlDbType.Int)
            };
            parameters[0].Value = memberid;
            parameters[1].Value = recommemberid;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
    }
}
