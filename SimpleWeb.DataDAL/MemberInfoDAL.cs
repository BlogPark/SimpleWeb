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
    public class MemberInfoDAL
    {
        static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddMemberInfo(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MemberInfo(");
            strSql.Append("Area,Address,WeixinNum,AliPayName,AliPayNum,SecurityQuestion,SecurityAnswer,LogPwd,MStatus,AddTime,TruethName,Sex,TelPhone,MobileNum,Email,IdentificationID,Province,City");
            strSql.Append(") values (");
            strSql.Append("@Area,@Address,@WeixinNum,@AliPayName,@AliPayNum,@SecurityQuestion,@SecurityAnswer,@LogPwd,@MStatus,GETDATE(),@TruethName,@Sex,@TelPhone,@MobileNum,@Email,@IdentificationID,@Province,@City");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Area", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Address", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WeixinNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SecurityQuestion", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SecurityAnswer", SqlDbType.NVarChar) ,            
                        new SqlParameter("@LogPwd", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MStatus", SqlDbType.Int) ,                   
                        new SqlParameter("@TruethName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Sex", SqlDbType.Int) ,            
                        new SqlParameter("@TelPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MobileNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Email", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IdentificationID", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Province", SqlDbType.NVarChar) ,            
                        new SqlParameter("@City", SqlDbType.NVarChar)    
            };
            parameters[0].Value = model.Area;
            parameters[1].Value = model.Address;
            parameters[2].Value = model.WeixinNum;
            parameters[3].Value = model.AliPayName;
            parameters[4].Value = model.AliPayNum;
            parameters[5].Value = model.SecurityQuestion;
            parameters[6].Value = model.SecurityAnswer;
            parameters[7].Value = model.LogPwd;
            parameters[8].Value = model.MStatus;
            parameters[9].Value = model.TruethName;
            parameters[10].Value = model.Sex;
            parameters[11].Value = model.TelPhone;
            parameters[12].Value = model.MobileNum;
            parameters[13].Value = model.Email;
            parameters[14].Value = model.IdentificationID;
            parameters[15].Value = model.Province;
            parameters[16].Value = model.City;

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateMemberInfo(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" Area = @Area , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" WeixinNum = @WeixinNum , ");
            strSql.Append(" AliPayName = @AliPayName , ");
            strSql.Append(" AliPayNum = @AliPayNum , ");
            strSql.Append(" SecurityQuestion = @SecurityQuestion , ");
            strSql.Append(" SecurityAnswer = @SecurityAnswer , ");
            strSql.Append(" TruethName = @TruethName , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" TelPhone = @TelPhone , ");
            strSql.Append(" MobileNum = @MobileNum , ");
            strSql.Append(" Email = @Email , ");
            strSql.Append(" IdentificationID = @IdentificationID , ");
            strSql.Append(" Province = @Province , ");
            strSql.Append(" City = @City  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@Area", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Address", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WeixinNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SecurityQuestion", SqlDbType.NVarChar) ,            
                        new SqlParameter("@SecurityAnswer", SqlDbType.NVarChar) ,
                        new SqlParameter("@TruethName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Sex", SqlDbType.Int) ,            
                        new SqlParameter("@TelPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MobileNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Email", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IdentificationID", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Province", SqlDbType.NVarChar) ,            
                        new SqlParameter("@City", SqlDbType.NVarChar)         
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Area;
            parameters[2].Value = model.Address;
            parameters[3].Value = model.WeixinNum;
            parameters[4].Value = model.AliPayName;
            parameters[5].Value = model.AliPayNum;
            parameters[6].Value = model.SecurityQuestion;
            parameters[7].Value = model.SecurityAnswer;
            parameters[8].Value = model.TruethName;
            parameters[9].Value = model.Sex;
            parameters[10].Value = model.TelPhone;
            parameters[11].Value = model.MobileNum;
            parameters[12].Value = model.Email;
            parameters[13].Value = model.IdentificationID;
            parameters[14].Value = model.Province;
            parameters[15].Value = model.City;
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
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, Area, Address, WeixinNum, AliPayName, AliPayNum, SecurityQuestion, SecurityAnswer, LogPwd, MStatus, AddTime, TruethName, Sex, TelPhone, MobileNum, Email, IdentificationID, Province, City  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Area"].ToString() != "")
                {
                    model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                }
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.WeixinNum = ds.Tables[0].Rows[0]["WeixinNum"].ToString();
                model.AliPayName = ds.Tables[0].Rows[0]["AliPayName"].ToString();
                model.AliPayNum = ds.Tables[0].Rows[0]["AliPayNum"].ToString();
                model.SecurityQuestion = ds.Tables[0].Rows[0]["SecurityQuestion"].ToString();
                model.SecurityAnswer = ds.Tables[0].Rows[0]["SecurityAnswer"].ToString();
                model.LogPwd = ds.Tables[0].Rows[0]["LogPwd"].ToString();
                if (ds.Tables[0].Rows[0]["MStatus"].ToString() != "")
                {
                    model.MStatus = int.Parse(ds.Tables[0].Rows[0]["MStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                model.TruethName = ds.Tables[0].Rows[0]["TruethName"].ToString();
                if (ds.Tables[0].Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(ds.Tables[0].Rows[0]["Sex"].ToString());
                }
                model.TelPhone = ds.Tables[0].Rows[0]["TelPhone"].ToString();
                model.MobileNum = ds.Tables[0].Rows[0]["MobileNum"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.IdentificationID = ds.Tables[0].Rows[0]["IdentificationID"].ToString();
                if (ds.Tables[0].Rows[0]["Province"].ToString() != "")
                {
                    model.Province = ds.Tables[0].Rows[0]["Province"].ToString();
                }
                if (ds.Tables[0].Rows[0]["City"].ToString() != "")
                {
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(int mid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" MStatus = @MStatus  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@MStatus", SqlDbType.Int) 
            };
            parameters[0].Value = mid;
            parameters[1].Value = status;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetMemberInfoListForPage(MemberInfoModel model, out int totalrowcount)
        {
            List<MemberInfoModel> list = new List<MemberInfoModel>();
            string columms = @"ID,TruethName,Sex,TelPhone,MobileNum,Email,IdentificationID,Province,City,Area,ADDRESS,WeixinNum,AliPayName,AliPayNum,SecurityQuestion,SecurityAnswer,MStatus,AddTime,CASE MStatus WHEN 1 THEN '待激活' WHEN 2 THEN '已激活' WHEN 3 THEN '已冻结' END MStatusName";
            string where = "";
            if (model != null)
            {
                if (model.ID > 0)
                {
                    where += "ID=" + model.ID;
                }
                if (!string.IsNullOrWhiteSpace(model.MobileNum) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MobileNum Like '%" + model.MobileNum + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MobileNum) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MobileNum Like '%" + model.MobileNum + "%'";
                }
                if (!string.IsNullOrWhiteSpace(model.TruethName) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" TruethName ='" + model.TruethName + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.TruethName) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND TruethName ='" + model.TruethName + "'";
                }
                if (model.MStatus > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MStatus =" + model.MStatus;
                }
                else if (model.MStatus > 0 && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MStatus =" + model.MStatus;
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
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberInfoModel memberModel = new MemberInfoModel();
                    if (item["ID"].ToString() != "")
                    {
                        memberModel.ID = int.Parse(item["ID"].ToString());
                    }
                    memberModel.TruethName = item["TruethName"].ToString();
                    if (item["Sex"].ToString() != "")
                    {
                        memberModel.Sex = int.Parse(item["Sex"].ToString());
                    }
                    if (item["MStatus"].ToString() != "")
                    {
                        memberModel.MStatus = int.Parse(item["MStatus"].ToString());
                    }
                    memberModel.TelPhone = item["TelPhone"].ToString();
                    if (item["AddTime"].ToString() != "")
                    {
                        memberModel.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    memberModel.MobileNum = item["MobileNum"].ToString();
                    memberModel.Email = item["Email"].ToString();
                    memberModel.IdentificationID = item["IdentificationID"].ToString();
                    memberModel.Province = item["Province"].ToString();
                    memberModel.City = item["City"].ToString();
                    memberModel.Area = item["Area"].ToString();
                    memberModel.Address = item["ADDRESS"].ToString();
                    memberModel.AliPayName = item["AliPayName"].ToString();
                    memberModel.AliPayNum = item["AliPayNum"].ToString();
                    memberModel.SecurityQuestion = item["SecurityQuestion"].ToString();
                    memberModel.WeixinNum = item["WeixinNum"].ToString();
                    memberModel.SecurityAnswer = item["SecurityAnswer"].ToString();
                    memberModel.MStatusName = item["MStatusName"].ToString();
                    list.Add(memberModel);
                }
            }
            return list;
        }
        /// <summary>
        /// 得到行政区域列表
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <returns></returns>
        public List<ReginTableModel> GetReginTableListModel(int parentid)
        {
            List<ReginTableModel> list = new List<ReginTableModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select REGION_ID, REGION_CODE, REGION_NAME, PARENT_ID, REGION_LEVEL, REGION_ORDER, REGION_NAME_EN, REGION_SHORTNAME_EN  ");
            strSql.Append("  from ReginTable ");
            strSql.Append(" where PARENT_ID=@PARENT_ID  ORDER BY LEN(REGION_NAME) ASC ,REGION_NAME_EN ASC");
            SqlParameter[] parameters = {
					new SqlParameter("@PARENT_ID", SqlDbType.Int)	};
            parameters[0].Value = parentid;
            DataSet ds = helper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    ReginTableModel model = new ReginTableModel();
                    if (item["REGION_ID"].ToString() != "")
                    {
                        model.REGION_ID = int.Parse(item["REGION_ID"].ToString());
                    }
                    model.REGION_CODE = item["REGION_CODE"].ToString();
                    model.REGION_NAME = item["REGION_NAME"].ToString();
                    if (item["PARENT_ID"].ToString() != "")
                    {
                        model.PARENT_ID = int.Parse(item["PARENT_ID"].ToString());
                    }
                    if (item["REGION_LEVEL"].ToString() != "")
                    {
                        model.REGION_LEVEL = int.Parse(item["REGION_LEVEL"].ToString());
                    }
                    if (item["REGION_ORDER"].ToString() != "")
                    {
                        model.REGION_ORDER = int.Parse(item["REGION_ORDER"].ToString());
                    }
                    model.REGION_NAME_EN = item["REGION_NAME_EN"].ToString();
                    model.REGION_SHORTNAME_EN = item["REGION_SHORTNAME_EN"].ToString();
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
        /// 得到会员的直荐名单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<ReMemberRelationModel> GetRecommdMemberModel(int rmid)
        {
            List<ReMemberRelationModel> list = new List<ReMemberRelationModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, MemberID, MemberPhone, MemberTruthName, RecommMID, RecommMPhone, RecommMTruthName, RStatus, AddTime  ");
            strSql.Append("  from ReMemberRelation ");
            strSql.Append(" where RecommMID=@rmid");
            SqlParameter[] parameters = {
					new SqlParameter("@rmid", SqlDbType.Int)
			};
            parameters[0].Value = rmid;

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
        /// 得到会员信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMember(string phone)
        {
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        MobileNum
FROM    dbo.MemberInfo
WHERE   MobileNum = @MobileNum";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MobileNum",phone)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            MemberInfoModel model = new MemberInfoModel();
            model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
            model.TruethName = dt.Rows[0]["TruethName"].ToString();
            model.MobileNum = dt.Rows[0]["MobileNum"].ToString();
            return model;
        }
    }
}
