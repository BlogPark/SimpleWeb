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
    public class WebSettingsDAL
    {
        public static DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int UpdateWebSetting(WebSettingsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebSettings set ");
            strSql.Append(" WebFax = @WebFax , ");
            strSql.Append(" WebMobile = @WebMobile , ");
            strSql.Append(" WebPhone = @WebPhone , ");
            strSql.Append(" WebEmail = @WebEmail , ");
            strSql.Append(" WebAboutUs = @WebAboutUs , ");
            strSql.Append(" IsOpen = @IsOpen , ");
            strSql.Append(" DomainName = @DomainName , ");
            strSql.Append(" WebName = @WebName , ");
            strSql.Append(" WebDescription = @WebDescription , ");
            strSql.Append(" WebType = @WebType , ");
            strSql.Append(" WebPutonrecord = @WebPutonrecord , ");
            strSql.Append(" WebDefaultKey = @WebDefaultKey , ");
            strSql.Append(" WebAddress = @WebAddress  ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) ,            
                        new SqlParameter("@WebFax", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebMobile", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebEmail", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebAboutUs", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IsOpen", SqlDbType.Int) ,            
                        new SqlParameter("@DomainName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebDescription", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebType", SqlDbType.NVarChar) ,          
                        new SqlParameter("@WebPutonrecord", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebDefaultKey", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebAddress", SqlDbType.NVarChar)             
            };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.WebFax;
            parameters[2].Value = model.WebMobile;
            parameters[3].Value = model.WebPhone;
            parameters[4].Value = model.WebEmail;
            parameters[5].Value = model.WebAboutUs;
            parameters[6].Value = model.IsOpen;
            parameters[7].Value = model.DomainName;
            parameters[8].Value = model.WebName;
            parameters[9].Value = model.WebDescription;
            parameters[10].Value = model.WebType;
            parameters[11].Value = model.WebPutonrecord;
            parameters[12].Value = model.WebDefaultKey;
            parameters[13].Value = model.WebAddress;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 得到网站的基础配置信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static WebSettingsModel GetWebSiteModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, WebFax, WebMobile, WebPhone, WebEmail, WebAboutUs, IsOpen, DomainName, WebName, WebDescription, WebType, WebLogoAlt, WebLogo, WebPutonrecord, WebDefaultKey, WebAddress  ");
            strSql.Append("  from WebSettings ");
            strSql.Append(" where IsUsed=1");
            strSql.Append(" Order By ID DESC ");
            WebSettingsModel model = new WebSettingsModel();
            DataSet ds = helper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.WebFax = ds.Tables[0].Rows[0]["WebFax"].ToString();
                model.WebMobile = ds.Tables[0].Rows[0]["WebMobile"].ToString();
                model.WebPhone = ds.Tables[0].Rows[0]["WebPhone"].ToString();
                model.WebEmail = ds.Tables[0].Rows[0]["WebEmail"].ToString();
                model.WebAboutUs = ds.Tables[0].Rows[0]["WebAboutUs"].ToString();
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    model.IsOpen = int.Parse(ds.Tables[0].Rows[0]["IsOpen"].ToString());
                }
                model.DomainName = ds.Tables[0].Rows[0]["DomainName"].ToString();
                model.WebName = ds.Tables[0].Rows[0]["WebName"].ToString();
                model.WebDescription = ds.Tables[0].Rows[0]["WebDescription"].ToString();
                model.WebType = ds.Tables[0].Rows[0]["WebType"].ToString();
                model.WebLogoAlt = ds.Tables[0].Rows[0]["WebLogoAlt"].ToString();
                model.WebLogo = ds.Tables[0].Rows[0]["WebLogo"].ToString();
                model.WebPutonrecord = ds.Tables[0].Rows[0]["WebPutonrecord"].ToString();
                model.WebDefaultKey = ds.Tables[0].Rows[0]["WebDefaultKey"].ToString();
                model.WebAddress = ds.Tables[0].Rows[0]["WebAddress"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 新增网站信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddWebSite(WebSettingsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WebSettings(");
            strSql.Append("WebFax,WebMobile,WebPhone,WebEmail,WebAboutUs,IsOpen,DomainName,WebName,WebDescription,WebType,WebLogoAlt,WebLogo,WebPutonrecord,WebDefaultKey,WebAddress");
            strSql.Append(") values (");
            strSql.Append("@WebFax,@WebMobile,@WebPhone,@WebEmail,@WebAboutUs,@IsOpen,@DomainName,@WebName,@WebDescription,@WebType,@WebLogoAlt,@WebLogo,@WebPutonrecord,@WebDefaultKey,@WebAddress");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@WebFax", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebMobile", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebEmail", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebAboutUs", SqlDbType.NVarChar) ,            
                        new SqlParameter("@IsOpen", SqlDbType.Int) ,            
                        new SqlParameter("@DomainName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebName", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebDescription", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebType", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebLogoAlt", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebLogo", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebPutonrecord", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebDefaultKey", SqlDbType.NVarChar) ,            
                        new SqlParameter("@WebAddress", SqlDbType.NVarChar)         
            };
            parameters[0].Value = model.WebFax;
            parameters[1].Value = model.WebMobile;
            parameters[2].Value = model.WebPhone;
            parameters[3].Value = model.WebEmail;
            parameters[4].Value = model.WebAboutUs;
            parameters[5].Value = model.IsOpen;
            parameters[6].Value = model.DomainName;
            parameters[7].Value = model.WebName;
            parameters[8].Value = model.WebDescription;
            parameters[9].Value = model.WebType;
            parameters[10].Value = model.WebLogoAlt;
            parameters[11].Value = model.WebLogo;
            parameters[12].Value = model.WebPutonrecord;
            parameters[13].Value = model.WebDefaultKey;
            parameters[14].Value = model.WebAddress;
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
        /// 关闭网站
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CloseWebSite(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WebSettings set ");
            strSql.Append(" IsOpen = 0 ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int) 
            };
            parameters[0].Value =id;
            int rows = helper.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
    }
}
