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
    public class SysAdminConfigDAL
    {
        DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 得到所有的系统配置
        /// </summary>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetAllConfigs()
        {
            List<SysAdminConfigsModel> list = new List<SysAdminConfigsModel>();
            string sqltxt = @"SELECT  ID ,
        ConfigName ,
        ConfigFID ,
        ConfigValue ,
        ConfigRemark ,
        AddTime ,
        ConfigStatus ,
        CASE ConfigStatus
          WHEN 1 THEN '启用'
          ELSE '禁用'
        END AS ConfigStatusName
FROM    dbo.SysAdminConfigs WITH(NOLOCK)";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                SysAdminConfigsModel model = new SysAdminConfigsModel();
                model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                model.ConfigFID = int.Parse(item["ConfigFID"].ToString());
                model.ConfigName = item["ConfigName"].ToString();
                model.ConfigRemark = item["ConfigRemark"].ToString();
                model.ConfigStatus = int.Parse(item["ConfigStatus"].ToString());
                model.ConfigStatusName = item["ConfigStatusName"].ToString();
                model.ConfigValue = item["ConfigValue"].ToString();
                model.ID = int.Parse(item["ID"].ToString());
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 得到所有生效配置的字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllConfigsDic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sqltxt = @"SELECT  
        ConfigName ,        
        ConfigValue 
FROM    dbo.SysAdminConfigs WITH(NOLOCK)
WHERE ConfigStatus=1 ";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                dic.Add(item["ConfigName"].ToString(), item["ConfigValue"].ToString());
            }
            return dic;
        }
        /// <summary>
        /// 插入配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddConfigInfo(SysAdminConfigsModel model)
        {
            int rowcount = 0;
            string sqltxt = @"INSERT  INTO dbo.SysAdminConfigs
        ( ConfigName ,
          ConfigFID ,
          ConfigValue ,
          ConfigRemark ,
          AddTime ,
          ConfigStatus
        )
VALUES  ( @ConfigName ,
          @ConfigFID ,
          @ConfigValue ,
          @ConfigRemark ,
          GETDATE() ,
          @ConfigStatus
        )";
            SqlParameter[] paramter ={
                                    new SqlParameter("@ConfigName",model.ConfigName),
                                    new SqlParameter("@ConfigFID",model.ConfigFID),
                                    new SqlParameter("@ConfigValue",model.ConfigValue),
                                    new SqlParameter("@ConfigRemark",model.ConfigRemark),
                                    new SqlParameter("@ConfigStatus",model.ConfigStatus)
                                    };
            rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 根据ID得到配置信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetConfigsByIDs(string ids)
        {
            List<SysAdminConfigsModel> list = new List<SysAdminConfigsModel>();
            string sqltxt = @"SELECT  ID ,
        ConfigName ,
        ConfigFID ,
        ConfigValue ,
        ConfigRemark ,
        AddTime ,
        ConfigStatus ,
        CASE ConfigStatus
          WHEN 1 THEN '启用'
          ELSE '禁用'
        END AS ConfigStatusName
FROM    dbo.SysAdminConfigs WITH(NOLOCK)
WHERE ID IN (" + ids + ")";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                SysAdminConfigsModel model = new SysAdminConfigsModel();
                model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                model.ConfigFID = int.Parse(item["ConfigFID"].ToString());
                model.ConfigName = item["ConfigName"].ToString();
                model.ConfigRemark = item["ConfigRemark"].ToString();
                model.ConfigStatus = int.Parse(item["ConfigStatus"].ToString());
                model.ConfigStatusName = item["ConfigStatusName"].ToString();
                model.ConfigValue = item["ConfigValue"].ToString();
                model.ID = int.Parse(item["ID"].ToString());
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConfigs(SysAdminConfigsModel model)
        {
            int rowcount = 0;
            string sqltxt = @"UPDATE  dbo.SysAdminConfigs
SET     ConfigName = @ConfigName ,
        ConfigFID = @ConfigFID ,
        ConfigValue = @ConfigValue ,
        ConfigRemark = @ConfigRemark ,
        ConfigStatus = @ConfigStatus
WHERE   ID = @id";
            SqlParameter[] paramter ={
                                    new SqlParameter("@ConfigName",model.ConfigName),
                                    new SqlParameter("@ConfigFID",model.ConfigFID),
                                    new SqlParameter("@ConfigValue",model.ConfigValue),
                                    new SqlParameter("@ConfigRemark",model.ConfigRemark),
                                    new SqlParameter("@ConfigStatus",model.ConfigStatus),
                                    new SqlParameter("@id",model.ID)
                                    };
            rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }
        /// <summary>
        /// 得到顶级配置项目
        /// </summary>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetFirstConfigs()
        {
            List<SysAdminConfigsModel> list = new List<SysAdminConfigsModel>();
            string sqltxt = @"SELECT  ID ,
        ConfigName ,
        ConfigFID ,
        ConfigValue ,
        ConfigRemark ,
        AddTime ,
        ConfigStatus ,
        CASE ConfigStatus
          WHEN 1 THEN '启用'
          ELSE '禁用'
        END AS ConfigStatusName
FROM    dbo.SysAdminConfigs WITH(NOLOCK)
WHERE ConfigFID=0 ";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                SysAdminConfigsModel model = new SysAdminConfigsModel();
                model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                model.ConfigFID = int.Parse(item["ConfigFID"].ToString());
                model.ConfigName = item["ConfigName"].ToString();
                model.ConfigRemark = item["ConfigRemark"].ToString();
                model.ConfigStatus = int.Parse(item["ConfigStatus"].ToString());
                model.ConfigStatusName = item["ConfigStatusName"].ToString();
                model.ConfigValue = item["ConfigValue"].ToString();
                model.ID = int.Parse(item["ID"].ToString());
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 禁用配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelConfig(int id)
        {
            int rowcount = 0;
            string sqltxt = @"IF EXISTS ( SELECT  1
            FROM    dbo.SysAdminConfigs
            WHERE   id = @id
                    AND ConfigFID = 0 )
    BEGIN
        UPDATE  dbo.SysAdminConfigs
        SET     ConfigStatus = 0
        WHERE   id = @id
    END
ELSE
    BEGIN
        UPDATE  dbo.SysAdminConfigs
        SET     ConfigFID = 0
        WHERE   ConfigFID = @id
        UPDATE  dbo.SysAdminConfigs
        SET     ConfigStatus = 0
        WHERE   id = @id
    END";
            SqlParameter[] paramter = { new SqlParameter("@id", id) };
            rowcount = helper.ExecuteSql(sqltxt, paramter);
            return rowcount;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SysAdminConfigsModel GetSingleSysAdminConfigsModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, ConfigName, ConfigFID, ConfigValue, ConfigRemark, AddTime, ConfigStatus  ");
            strSql.Append("  from SysAdminConfigs ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int)
			};
            parameters[0].Value = ID;
            SysAdminConfigsModel model = new SysAdminConfigsModel();
            DataSet ds = helper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.ConfigName = ds.Tables[0].Rows[0]["ConfigName"].ToString();
                if (ds.Tables[0].Rows[0]["ConfigFID"].ToString() != "")
                {
                    model.ConfigFID = int.Parse(ds.Tables[0].Rows[0]["ConfigFID"].ToString());
                }
                model.ConfigValue = ds.Tables[0].Rows[0]["ConfigValue"].ToString();
                model.ConfigRemark = ds.Tables[0].Rows[0]["ConfigRemark"].ToString();
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ConfigStatus"].ToString() != "")
                {
                    model.ConfigStatus = int.Parse(ds.Tables[0].Rows[0]["ConfigStatus"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
