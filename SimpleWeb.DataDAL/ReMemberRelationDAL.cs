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
        public static int UpdateReMemberRelationStatus(int memberid, int recommemberid)
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
        /// <summary>
        /// 按照被推荐的会员ID查找推荐人
        /// </summary>
        /// <param name="recommmid"></param>
        /// <returns></returns>
        public static ReMemberRelationModel GetReMemberRelation(int recommmid)
        {
            string sqltxt = @"SELECT  ID ,
        A.MemberID ,
        MemberPhone ,
        MemberTruthName ,
        RecommMID ,
        RecommMPhone ,
        RecommMTruthName ,
        RStatus ,
        AddTime,
        B.LastHelpMoney
FROM    ReMemberRelation A
INNER JOIN dbo.MemberExtendInfo B ON A.MemberID=B.MemberID 
WHERE   RecommMID = @RecommMID";
            SqlParameter[] parameters = {
					new SqlParameter("@RecommMID", SqlDbType.Int)
			};
            parameters[0].Value = recommmid;
            ReMemberRelationModel model = new ReMemberRelationModel();
            DataSet ds = helper.Query(sqltxt, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                model.MemberPhone = ds.Tables[0].Rows[0]["MemberPhone"].ToString();
                model.MemberTruthName = ds.Tables[0].Rows[0]["MemberTruthName"].ToString();
                if (ds.Tables[0].Rows[0]["RecommMID"].ToString() != "")
                {
                    model.RecommMID = int.Parse(ds.Tables[0].Rows[0]["RecommMID"].ToString());
                }
                model.RecommMPhone = ds.Tables[0].Rows[0]["RecommMPhone"].ToString();
                model.RecommMTruthName = ds.Tables[0].Rows[0]["RecommMTruthName"].ToString();
                if (ds.Tables[0].Rows[0]["RStatus"].ToString() != "")
                {
                    model.RStatus = int.Parse(ds.Tables[0].Rows[0]["RStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastHelpMoney"].ToString() != "")
                {
                    model.Amount = ds.Tables[0].Rows[0]["LastHelpMoney"].ToString().ParseToDecimal(0);
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 按照会员ID查找被推荐人列表
        /// </summary>
        /// <param name="recommmid"></param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationList(int memberid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, MemberID, MemberPhone, MemberTruthName, RecommMID, RecommMPhone, RecommMTruthName, RStatus, AddTime  ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where MemberID=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int)
			};
            parameters[0].Value = memberid;
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ReMemberRelationModel model = new ReMemberRelationModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberTruthName = item["MemberTruthName"].ToString();
                    if (item["RecommMID"].ToString() != "")
                    {
                        model.RecommMID = int.Parse(item["RecommMID"].ToString());
                    }
                    model.RecommMPhone = item["RecommMPhone"].ToString();
                    model.RecommMTruthName = item["RecommMTruthName"].ToString();
                    if (item["RStatus"].ToString() != "")
                    {
                        model.RStatus = int.Parse(item["RStatus"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
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
        /// 按照会员ID查找被推荐人列表
        /// </summary>
        /// <param name="recommmid"></param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetReMemberRelationList(string memberids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, MemberID, MemberPhone, MemberTruthName, RecommMID, RecommMPhone, RecommMTruthName, RStatus, AddTime  ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where MemberID in (" + memberids.TrimEnd(',') + ")");
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            DataSet ds = helper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ReMemberRelationModel model = new ReMemberRelationModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MemberTruthName = item["MemberTruthName"].ToString();
                    if (item["RecommMID"].ToString() != "")
                    {
                        model.RecommMID = int.Parse(item["RecommMID"].ToString());
                    }
                    model.RecommMPhone = item["RecommMPhone"].ToString();
                    model.RecommMTruthName = item["RecommMTruthName"].ToString();
                    if (item["RStatus"].ToString() != "")
                    {
                        model.RStatus = int.Parse(item["RStatus"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
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
        /// 查询会员的推荐地图
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<ReMemberRelationModel> GetMemberRecommendMap(int memberid, out int count)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            string memberids = memberid.ToString();
            count = 0;
            do
            {
                if (!string.IsNullOrWhiteSpace(memberids))
                {
                    List<ReMemberRelationModel> relist = GetReMemberRelationList(memberids.TrimEnd(','));
                    if (relist != null)
                    {
                        memberids = "";
                        foreach (var item in relist)
                        {
                            memberids += item.RecommMID + ",";
                        }
                        list.AddRange(relist);
                        count += relist.Count;
                    }
                    else
                    {
                        memberids = "";
                    }
                }
            } while (!string.IsNullOrWhiteSpace(memberids));
            return list;
        }
        /// <summary>
        /// 查找会员的推荐人数
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static int GetReMemberCount(int memberid)
        {
            string sqltxt = @"SELECT COUNT(0)
  FROM SimpleWebDataBase.dbo.ReMemberRelation
  WHERE MemberID=@memberid AND RStatus=1";
            SqlParameter[] paramter = { new SqlParameter("@memberid",memberid) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(1);
        }

        /// <summary>
        /// 得到会员推荐图谱
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static List<RecommendMap> GetRecommendMap(int memberid)
        {
            List<RecommendMap> list = new List<RecommendMap>();
            string sqltxt = @"SELECT  MemberID AS pid ,
        RecommMID AS id ,
        ( RecommMPhone + '(' + RecommMTruthName + ')' ) AS name ,
        ( SELECT    COUNT(0)
          FROM      SimpleWebDataBase.dbo.ReMemberRelation
          WHERE     MemberID = A.RecommMID
                    AND RStatus = 1
        ) AS childcount
FROM    SimpleWebDataBase.dbo.ReMemberRelation A
WHERE   MemberID = @memberid
        AND RStatus = 1";
            SqlParameter[] paramter = { new SqlParameter("@memberid",memberid) };
            DataTable dt=helper.Query(sqltxt,paramter).Tables[0];
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    RecommendMap model = new RecommendMap();
                    model.id = item["id"].ToString().ParseToInt(0);
                    model.pid = item["pid"].ToString().ParseToInt(0);
                    model.name = item["name"].ToString();
                    model.childcount = item["childcount"].ToString().ParseToInt(0);
                    model.isParent = model.childcount > 0;
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
