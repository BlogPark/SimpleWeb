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
            strSql.Append("@Area,@Address,@WeixinNum,@AliPayName,@AliPayNum,@SecurityQuestion,@SecurityAnswer,@LogPwd,1,GETDATE(),@TruethName,@Sex,@TelPhone,@MobileNum,@Email,@IdentificationID,@Province,@City");
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
            parameters[8].Value = model.TruethName;
            parameters[9].Value = model.Sex;
            parameters[10].Value = model.TelPhone;
            parameters[11].Value = model.MobileNum;
            parameters[12].Value = model.Email;
            parameters[13].Value = model.IdentificationID;
            parameters[14].Value = model.Province;
            parameters[15].Value = model.City;

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
            parameters[1].Value = string.IsNullOrWhiteSpace(model.Area) ? "" : model.Area.Trim();
            parameters[2].Value = string.IsNullOrWhiteSpace(model.Address) ? "" : model.Address.Trim();
            parameters[3].Value = string.IsNullOrWhiteSpace(model.WeixinNum) ? "" : model.WeixinNum.Trim();
            parameters[4].Value = string.IsNullOrWhiteSpace(model.AliPayName) ? "" : model.AliPayName.Trim();
            parameters[5].Value = string.IsNullOrWhiteSpace(model.AliPayNum) ? "" : model.AliPayNum.Trim();
            parameters[6].Value = string.IsNullOrWhiteSpace(model.SecurityQuestion) ? "" : model.SecurityQuestion.Trim();
            parameters[7].Value = string.IsNullOrWhiteSpace(model.SecurityAnswer) ? "" : model.SecurityAnswer.Trim();
            parameters[8].Value = string.IsNullOrWhiteSpace(model.TruethName) ? "" : model.TruethName.Trim();
            parameters[9].Value = model.Sex;
            parameters[10].Value = string.IsNullOrWhiteSpace(model.TelPhone) ? "" : model.TelPhone.Trim();
            parameters[11].Value = string.IsNullOrWhiteSpace(model.MobileNum) ? "" : model.MobileNum.Trim();
            parameters[12].Value = string.IsNullOrWhiteSpace(model.Email) ? "" : model.Email.Trim();
            parameters[13].Value = string.IsNullOrWhiteSpace(model.IdentificationID) ? "" : model.IdentificationID.Trim();
            parameters[14].Value = string.IsNullOrWhiteSpace(model.Province) ? "" : model.Province.Trim();
            parameters[15].Value = string.IsNullOrWhiteSpace(model.City) ? "" : model.City.Trim();
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
        /// 前端会员自己更新资料
        /// </summary>
        public bool UpdateMemberInfoBySelf(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");
            strSql.Append(" WeixinNum = @WeixinNum , ");
            strSql.Append(" AliPayName = @AliPayName , ");
            strSql.Append(" AliPayNum = @AliPayNum , ");
            strSql.Append(" TruethName = @TruethName , ");
            strSql.Append(" MobileNum = @MobileNum , ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,              
                        new SqlParameter("@WeixinNum", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AliPayNum", SqlDbType.NVarChar) ,
                        new SqlParameter("@TruethName", SqlDbType.NVarChar) ,       
                        new SqlParameter("@MobileNum", SqlDbType.NVarChar) ,  
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = string.IsNullOrWhiteSpace(model.WeixinNum) ? "" : model.WeixinNum.Trim();
            parameters[2].Value = string.IsNullOrWhiteSpace(model.AliPayName) ? "" : model.AliPayName.Trim();
            parameters[3].Value = string.IsNullOrWhiteSpace(model.AliPayNum) ? "" : model.AliPayNum.Trim();
            parameters[4].Value = string.IsNullOrWhiteSpace(model.TruethName) ? "" : model.TruethName.Trim();
            parameters[5].Value = string.IsNullOrWhiteSpace(model.MobileNum) ? "" : model.MobileNum.Trim();
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
            strSql.Append("select ID ,TruethName ,Sex , TelPhone ,MobileNum , Email ,IdentificationID ,Province ,City ,Area ,[Address] , WeixinNum ,  AliPayName , AliPayNum ,SecurityQuestion ,SecurityAnswer , MStatus , AddTime  ");
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
                model.LogPwd = "";
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
        public static int UpdateStatus(int mid, int status)
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
        MobileNum,
        MStatus
FROM    dbo.MemberInfo
WHERE   MobileNum = @MobileNum";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MobileNum",phone)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MemberInfoModel model = new MemberInfoModel();
                model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                model.TruethName = dt.Rows[0]["TruethName"].ToString();
                model.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                model.MStatus = dt.Rows[0]["MStatus"].ToString().ParseToInt(1);
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到正常会员信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMember(int id)
        {
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        MobileNum
FROM    dbo.MemberInfo
WHERE   id = @id and MStatus=2";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",id)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MemberInfoModel model = new MemberInfoModel();
                model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                model.TruethName = dt.Rows[0]["TruethName"].ToString();
                model.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                return model;
            }
            else
                return null;
        }
        public static MemberInfoModel GetNotActiveMember(int id)
        {
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        MobileNum,
        MStatus
FROM    dbo.MemberInfo
WHERE   id = @id and MStatus=1";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",id)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                MemberInfoModel model = new MemberInfoModel();
                model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                model.TruethName = dt.Rows[0]["TruethName"].ToString();
                model.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                model.MStatus = dt.Rows[0]["MStatus"].ToString().ParseToInt(1);
                return model;
            }
            else
                return null;
        }
        /// <summary>
        /// 检查会员填写的信息
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="alipay"></param>
        /// <returns></returns>
        public int GetMemberInfoBycheck(string phone, string name, string alipay)
        {
            string sqltxt = @"SELECT ID  FROM dbo.MemberInfo   WHERE MStatus IN (1,2)";
            if (!string.IsNullOrWhiteSpace(phone))
            {
                sqltxt += @" AND MobileNum=@MobileNum";
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sqltxt += @" AND TruethName=@TruethName";
            }
            if (!string.IsNullOrWhiteSpace(alipay))
            {
                sqltxt += @" AND AliPayNum=@AliPayNum";
            }
            SqlParameter[] paramter = { 
                                      new SqlParameter("@MobileNum",phone),
                                      new SqlParameter("@TruethName",name),
                                      new SqlParameter("@AliPayNum",alipay)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 前端会员登陆
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public MemberInfoModel GetMemberInfo(string phone, string pwd, out string logmsg)
        {
            MemberInfoModel model = null;
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        Sex ,
        TelPhone ,
        MobileNum ,
        Email ,
        IdentificationID ,
        Province ,
        City ,
        Area ,
        [Address] ,
        WeixinNum ,
        AliPayName ,
        AliPayNum ,
        SecurityQuestion ,
        SecurityAnswer ,
        LogPwd ,
        MStatus ,
        AddTime
FROM    dbo.MemberInfo
WHERE MobileNum=@MobileNum";
            SqlParameter[] paramter = { new SqlParameter("@MobileNum", phone) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string logpwd = dt.Rows[0]["LogPwd"].ToString();
                if (logpwd != pwd)
                {
                    logmsg = "登陆密码不正确";
                    return null;
                }
                if (dt.Rows[0]["MStatus"].ToString() == "1")
                {
                    logmsg = "该账户未激活";
                    return null;
                }
                if (dt.Rows[0]["MStatus"].ToString() == "3")
                {
                    logmsg = "该账户已被冻结";
                    return null;
                }
                model = new MemberInfoModel();
                if (dt.Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                }
                if (dt.Rows[0]["Area"].ToString() != "")
                {
                    model.Area = dt.Rows[0]["Area"].ToString();
                }
                model.Address = dt.Rows[0]["Address"].ToString();
                model.WeixinNum = dt.Rows[0]["WeixinNum"].ToString();
                model.AliPayName = dt.Rows[0]["AliPayName"].ToString();
                model.AliPayNum = dt.Rows[0]["AliPayNum"].ToString();
                model.SecurityQuestion = dt.Rows[0]["SecurityQuestion"].ToString();
                model.SecurityAnswer = dt.Rows[0]["SecurityAnswer"].ToString();
                model.LogPwd = "password";
                if (dt.Rows[0]["MStatus"].ToString() != "")
                {
                    model.MStatus = int.Parse(dt.Rows[0]["MStatus"].ToString());
                }
                if (dt.Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(dt.Rows[0]["AddTime"].ToString());
                }
                model.TruethName = dt.Rows[0]["TruethName"].ToString();
                if (dt.Rows[0]["Sex"].ToString() != "")
                {
                    model.Sex = int.Parse(dt.Rows[0]["Sex"].ToString());
                }
                model.TelPhone = dt.Rows[0]["TelPhone"].ToString();
                model.MobileNum = dt.Rows[0]["MobileNum"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.IdentificationID = dt.Rows[0]["IdentificationID"].ToString();
                if (dt.Rows[0]["Province"].ToString() != "")
                {
                    model.Province = dt.Rows[0]["Province"].ToString();
                }
                if (dt.Rows[0]["City"].ToString() != "")
                {
                    model.City = dt.Rows[0]["City"].ToString();
                }
                logmsg = "1";
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = "";
                    log.BehaviorSource = 1;
                    log.BehaviorType = 1;
                    log.HOrderCode = "";
                    log.MemberID = model.ID;
                    log.MemberName = model.TruethName;
                    log.MemberPhone = model.MobileNum;
                    log.ProcAmount = 0;
                    log.Remark = "会员：" + model.MobileNum + "登陆";
                    int rowcount = UserBehaviorLogDAL.AddUserBehaviorLog(log);
                }
                catch { }
                return model;
            }
            else
            {
                logmsg = "手机号不存在";
                return null;
            }
        }
        /// <summary>
        /// 更改会员的密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateUserPwd(int memberid, string pwd)
        {
            string sqltxt = @"
UPDATE dbo.MemberInfo
SET LogPwd=@pwd
WHERE id=@id";
            SqlParameter[] paramter = { new SqlParameter("@pwd", pwd), new SqlParameter("@id", memberid) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 得到会员的总数
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int GetTotalMemberCount(int status)
        {
            string sqltxt = @"SELECT  COUNT(0)
FROM    dbo.MemberInfo ";
            if (status == 2)
            {
                sqltxt += @" WHERE MStatus=@mstatus";
            }
            SqlParameter[] paratmer = { new SqlParameter("@mstatus", status) };
            return helper.GetSingle(sqltxt, paratmer).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 根据名字读取会员的个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetMemberCountInfoByName(string name)
        {
            string sqltxt = @"SELECT  ID 
FROM    dbo.MemberInfo
WHERE   TruethName = @name
        AND MStatus <> 3";
            SqlParameter[] paramter = { new SqlParameter("@name", name) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 根据手机号读取会员的个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetMemberCountInfoByMobile(string mobilenum)
        {
            string sqltxt = @"SELECT  ID 
FROM    dbo.MemberInfo
WHERE   MobileNum = @mobilenum
        AND MStatus <> 3";
            SqlParameter[] paramter = { new SqlParameter("@mobilenum", mobilenum) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 根据支付宝ID读取会员的个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetMemberCountInfoByAlipayNum(string alipaynum)
        {
            string sqltxt = @"SELECT  ID 
FROM    dbo.MemberInfo
WHERE   AliPayNum = @alipaynum
        AND MStatus <> 3";
            SqlParameter[] paramter = { new SqlParameter("@alipaynum", alipaynum) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 根据AliPayName读取会员的个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetMemberCountInfoByAliPayName(string alipayname)
        {
            string sqltxt = @"SELECT  ID 
FROM    dbo.MemberInfo
WHERE   AliPayName = @AliPayName
        AND MStatus <> 3";
            SqlParameter[] paramter = { new SqlParameter("@AliPayName", alipayname) };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            return dt.Rows.Count;
        }
        /// <summary>
        /// 插入验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int AddVerification(string code)
        {
            string sqltxt = @"INSERT  INTO dbo.Verification
        ( VerificationCode )
VALUES  ( @VerificationCode )
SELECT  @@IDENTITY;";
            SqlParameter[] paramter = { new SqlParameter("@VerificationCode", code) };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 修改验证码发送结果
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int UpdateVerification(string sendid, int id)
        {
            string sqltxt = @"Update dbo.Verification
        SET SendID=@SendID where ID=@id";
            SqlParameter[] paramter = { new SqlParameter("@SendID", sendid), new SqlParameter("@id", id) };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 修改验证码发送结果
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string SelectVerification(int id)
        {
            string sqltxt = @"select VerificationCode
       from dbo.Verification
       where ID=@id";
            SqlParameter[] paramter = { new SqlParameter("@id", id) };
            return helper.GetSingle(sqltxt, paramter).ToString();
        }
    }
}
