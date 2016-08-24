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
    public class AdminSiteNewsDal
    {
        static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAdminSiteNew(AdminSiteNewsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AdminSiteNews(");
            strSql.Append("STitle,SContent,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,SStatus,SAddTime");
            strSql.Append(") values (");
            strSql.Append("@STitle,@SContent,@SendUserID,@SendUserName,@ReceiveUserID,@ReceiveUserName,1,GETDATE()");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {      
                        new SqlParameter("@STitle", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SContent", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SendUserID", SqlDbType.Int) ,            
                        new SqlParameter("@SendUserName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@ReceiveUserID", SqlDbType.Int) ,            
                        new SqlParameter("@ReceiveUserName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SStatus", SqlDbType.Int)  
            };
            parameters[0].Value = model.STitle;
            parameters[1].Value = model.SContent;
            parameters[2].Value = model.SendUserID;
            parameters[3].Value = model.SendUserName;
            parameters[4].Value = model.ReceiveUserID;
            parameters[5].Value = model.ReceiveUserName;
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
        /// 设置紧急条目
        /// </summary>
        public bool SetUrgent(int id, int isurgent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdminSiteNews set ");
            strSql.Append(" IsUrgent = @IsUrgent ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@IsUrgent", SqlDbType.Int)
            };
            parameters[0].Value = id;
            parameters[1].Value = isurgent;
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
        /// 设置置顶条目
        /// </summary>
        public bool SetIsTop(int id, int istop)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdminSiteNews set ");
            strSql.Append(" IsTop = @IsTop  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,                
                        new SqlParameter("@IsTop", SqlDbType.Int)      
            };
            parameters[0].Value = id;
            parameters[1].Value = istop;
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
        /// 更新条目状态
        /// </summary>
        public bool UpdateStatus(int id, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AdminSiteNews set ");
            strSql.Append(" SStatus = @SStatus ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,          
                        new SqlParameter("@SStatus", SqlDbType.Int)  
            };

            parameters[0].Value = id;
            parameters[1].Value = status;
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
        /// 根据ID得到一个对象实体
        /// </summary>
        public AdminSiteNewsModel GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, IsUrgent, IsTop, STitle, SContent, SendUserID, SendUserName, ReceiveUserID, ReceiveUserName, SStatus, SAddTime  ");
            strSql.Append("  from AdminSiteNews ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            AdminSiteNewsModel model = new AdminSiteNewsModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUrgent"].ToString() != "")
                {
                    model.IsUrgent = int.Parse(ds.Tables[0].Rows[0]["IsUrgent"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
                }
                model.STitle = ds.Tables[0].Rows[0]["STitle"].ToString();
                model.SContent = ds.Tables[0].Rows[0]["SContent"].ToString();
                if (ds.Tables[0].Rows[0]["SendUserID"].ToString() != "")
                {
                    model.SendUserID = int.Parse(ds.Tables[0].Rows[0]["SendUserID"].ToString());
                }
                model.SendUserName = ds.Tables[0].Rows[0]["SendUserName"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveUserID"].ToString() != "")
                {
                    model.ReceiveUserID = int.Parse(ds.Tables[0].Rows[0]["ReceiveUserID"].ToString());
                }
                model.ReceiveUserName = ds.Tables[0].Rows[0]["ReceiveUserName"].ToString();
                if (ds.Tables[0].Rows[0]["SStatus"].ToString() != "")
                {
                    model.SStatus = int.Parse(ds.Tables[0].Rows[0]["SStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SAddTime"].ToString() != "")
                {
                    model.SAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["SAddTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户ID得到对应的系统公告
        /// </summary>
        public static List<AdminSiteNewsModel> GetModelListByUserID(int userid, int topnum = 100)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP " + topnum);
            strSql.Append(" ID, IsUrgent, IsTop, STitle, SContent, SendUserID, SendUserName, ReceiveUserID, ReceiveUserName, SStatus,case SStatus  when 1 then '发布' when 2 then '已阅' when 3 then '已删除' end as SStatusName , SAddTime  ");
            strSql.Append("  from AdminSiteNews ");
            strSql.Append(" where ReceiveUserID IN (" + userid.ToString() + ",0) ");
            strSql.Append(" ORDER BY  IsUrgent DESC,IsTop DESC,ID DESC ");
            List<AdminSiteNewsModel> list = new List<AdminSiteNewsModel>();
            DataSet ds = helper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    AdminSiteNewsModel model = new AdminSiteNewsModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["IsUrgent"].ToString() != "")
                    {
                        model.IsUrgent = int.Parse(item["IsUrgent"].ToString());
                    }
                    if (item["IsTop"].ToString() != "")
                    {
                        model.IsTop = int.Parse(item["IsTop"].ToString());
                    }
                    model.STitle = item["STitle"].ToString();
                    model.SContent = item["SContent"].ToString();
                    if (item["SendUserID"].ToString() != "")
                    {
                        model.SendUserID = int.Parse(item["SendUserID"].ToString());
                    }
                    model.SendUserName = item["SendUserName"].ToString();
                    if (item["ReceiveUserID"].ToString() != "")
                    {
                        model.ReceiveUserID = int.Parse(item["ReceiveUserID"].ToString());
                    }
                    model.ReceiveUserName = item["ReceiveUserName"].ToString();
                    if (item["SStatus"].ToString() != "")
                    {
                        model.SStatus = int.Parse(item["SStatus"].ToString());
                    }
                    if (item["SAddTime"].ToString() != "")
                    {
                        model.SAddTime = DateTime.Parse(item["SAddTime"].ToString());
                    }
                    model.SStatusName = item["SStatusName"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据ID得到一个对象实体
        /// </summary>
        public List<AdminSiteNewsModel> GetTop3ModelListByUserID(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 3 ID, IsUrgent, IsTop, STitle, SContent, SendUserID, SendUserName, ReceiveUserID, ReceiveUserName, SStatus, SAddTime  ");
            strSql.Append("  from AdminSiteNews ");
            strSql.Append(" where ReceiveUserID=@userID AND SStatus IN (1,2) ");
            strSql.Append(" ORDER BY  IsTop DESC,ID DESC ");
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.Int)
			};
            parameters[0].Value = userid;
            List<AdminSiteNewsModel> list = new List<AdminSiteNewsModel>();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    AdminSiteNewsModel model = new AdminSiteNewsModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["IsUrgent"].ToString() != "")
                    {
                        model.IsUrgent = int.Parse(item["IsUrgent"].ToString());
                    }
                    if (item["IsTop"].ToString() != "")
                    {
                        model.IsTop = int.Parse(item["IsTop"].ToString());
                    }
                    model.STitle = item["STitle"].ToString();
                    model.SContent = item["SContent"].ToString();
                    if (item["SendUserID"].ToString() != "")
                    {
                        model.SendUserID = int.Parse(item["SendUserID"].ToString());
                    }
                    model.SendUserName = item["SendUserName"].ToString();
                    if (item["ReceiveUserID"].ToString() != "")
                    {
                        model.ReceiveUserID = int.Parse(item["ReceiveUserID"].ToString());
                    }
                    model.ReceiveUserName = item["ReceiveUserName"].ToString();
                    if (item["SStatus"].ToString() != "")
                    {
                        model.SStatus = int.Parse(item["SStatus"].ToString());
                    }
                    if (item["SAddTime"].ToString() != "")
                    {
                        model.SAddTime = DateTime.Parse(item["SAddTime"].ToString());
                    }
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询会员的留言信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<WebContactMessageModel> GetContractMessage(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ReplyTime, MemberID, MemberName, MemberPhone, MessageTitle, MessageContent, AddTime, MStatus, ReplyContent,CASE MStatus WHEN 1 THEN '新留言' WHEN 2 THEN '已回复' WHEN 3 THEN '删除' END AS MStatusName  ");
            strSql.Append("  from WebContactMessage ");
            strSql.Append(" where MemberID=@MemberID");
            strSql.Append(" Order By ID Desc ");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int)
			};
            parameters[0].Value = userid;
            List<WebContactMessageModel> list = new List<WebContactMessageModel>();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WebContactMessageModel model = new WebContactMessageModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["ReplyTime"].ToString() != "")
                    {
                        model.ReplyTime = DateTime.Parse(item["ReplyTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MessageTitle = item["MessageTitle"].ToString();
                    model.MessageContent = item["MessageContent"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MStatus"].ToString() != "")
                    {
                        model.MStatus = int.Parse(item["MStatus"].ToString());
                    }
                    model.ReplyContent = item["ReplyContent"].ToString();
                    model.MStatusName = item["MStatusName"].ToString();
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
        /// 查询所有的会员留言信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<WebContactMessageModel> GetAllContractMessage()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ReplyTime, MemberID, MemberName, MemberPhone, MessageTitle, MessageContent, AddTime, MStatus, case MStatus when 1 then '刚提问' when 2 then '已回复' when 3 then '删除' end as MStatusName,ReplyContent  ");
            strSql.Append("  from WebContactMessage ");
            strSql.Append(" Order By ID Desc ");
            List<WebContactMessageModel> list = new List<WebContactMessageModel>();
            DataSet ds = helper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    WebContactMessageModel model = new WebContactMessageModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["ReplyTime"].ToString() != "")
                    {
                        model.ReplyTime = DateTime.Parse(item["ReplyTime"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        model.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    model.MemberName = item["MemberName"].ToString();
                    model.MemberPhone = item["MemberPhone"].ToString();
                    model.MessageTitle = item["MessageTitle"].ToString();
                    model.MessageContent = item["MessageContent"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    if (item["MStatus"].ToString() != "")
                    {
                        model.MStatus = int.Parse(item["MStatus"].ToString());
                    }
                    model.ReplyContent = item["ReplyContent"].ToString();
                    model.MStatusName = item["MStatusName"].ToString();
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
        /// 添加留言信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddContactMessage(WebContactMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebContactMessage(");
            strSql.Append("MemberID,MemberName,MemberPhone,MessageTitle,MessageContent,AddTime,MStatus");
            strSql.Append(") values (");
            strSql.Append("@MemberID,@MemberName,@MemberPhone,@MessageTitle,@MessageContent,GETDATE(),1");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {           
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MessageTitle", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MessageContent", SqlDbType.NVarChar)     
            };
            parameters[0].Value = model.MemberID;
            parameters[1].Value = model.MemberName;
            parameters[2].Value = model.MemberPhone;
            parameters[3].Value = model.MessageTitle;
            parameters[4].Value = model.MessageContent;
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
        /// 回复会员留言
        /// </summary>
        public bool UpdateMsg(WebContactMessageModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebContactMessage set ");
            strSql.Append(" ReplyTime = GETDATE() , ");
            strSql.Append(" MStatus = 2 , ");
            strSql.Append(" ReplyContent = @ReplyContent  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,
                        new SqlParameter("@ReplyContent", SqlDbType.NVarChar)      
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ReplyContent;
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
        /// 删除会员留言
        /// </summary>
        public bool deleteMsg(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebContactMessage set ");
            strSql.Append(" MStatus = 3 ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) 
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
        /// 得到系统公告的条目数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetSysNewsCount(int userid)
        {
            string sqltxt = @"SELECT  COUNT(0) AS con
FROM    dbo.AdminSiteNews
WHERE  SStatus <> 3 AND ReceiveUserID IN (" + userid + @",0)";
            return helper.GetSingle(sqltxt).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 得到会员留言的最新回复数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetNewWebContentCount(int userid)
        {
            string sqltxt = @"SELECT  COUNT(0) AS con
FROM    dbo.WebContactMessage
WHERE   MStatus = 2
        AND ISNULL(IsReaded, 0) = 0
        AND MemberID=@id";
            SqlParameter[] paramter = { new SqlParameter("@id", userid) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
    }
}
