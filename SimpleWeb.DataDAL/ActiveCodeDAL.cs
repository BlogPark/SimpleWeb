using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataDAL
{
    public class ActiveCodeDAL
    {
        public DbHelperSQL helper = new DbHelperSQL();
        /// <summary>
        /// 产生新的激活码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ProduceActiveCode(List<ActiveCodeModel> list)
        {
            int result = 0;
            foreach (ActiveCodeModel item in list)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"IF NOT EXISTS ( SELECT  1
                FROM    ActiveCode
                WHERE   ActivationCode = @ActivationCode )
    BEGIN 
        INSERT  INTO ActiveCode
                ( ActivationCode ,
                  AType ,
                  AStatus ,
                  AddTime
                )
        VALUES  ( @ActivationCode ,
                  @AType ,
                  20 ,
                  GETDATE()
                );
        SELECT  @@IDENTITY
    END
ELSE
    BEGIN
        SELECT  0;
    END
");
                SqlParameter[] parameters = {
			            new SqlParameter("@ActivationCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@AType", SqlDbType.Int)   
            };
                parameters[0].Value = item.ActivationCode;
                parameters[1].Value = item.AType;
                int obj = Convert.ToInt32(helper.GetSingle(strSql.ToString(), parameters).ToString());
                result += obj;
            }
            return result;
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeModel> GetActiveCodeListForPage(ActiveCodeModel model, out int totalrowcount)
        {
            List<ActiveCodeModel> list = new List<ActiveCodeModel>();
            string columms = @"ID,ActivationCode,AType,AStatus,AddTime,CASE AStatus WHEN 1 THEN '未使用' WHEN 2 THEN '已使用' END AStatusName";
            string where = "";
            if (model != null)
            {
                where += " AStatus=20 ";
                if (!string.IsNullOrWhiteSpace(model.ActivationCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" ActivationCode Like '%" + model.ActivationCode + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(model.ActivationCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND ActivationCode Like '%" + model.ActivationCode + "%'";
                }
                if (model.AType > 0 && string.IsNullOrWhiteSpace(where))
                {
                    where += @" AType =" + model.AType;
                }
                else if (model.AType > 0 && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND AType =" + model.AType;
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "ID";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.ActiveCode";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActiveCodeModel memberModel = new ActiveCodeModel();
                    if (item["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(item["ID"].ToString());
                    }
                    model.ActivationCode = item["ActivationCode"].ToString();
                    if (item["AType"].ToString() != "")
                    {
                        model.AType = int.Parse(item["AType"].ToString());
                    }
                    if (item["AStatus"].ToString() != "")
                    {
                        model.AStatus = int.Parse(item["AStatus"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    list.Add(memberModel);
                }
            }
            return list;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStatus(List<string> code)
        {
            int result = 0;
            foreach (string item in code)
            {
                string sqltxt = @"UPDATE ActiveCode
  SET AStatus=10
  WHERE ActivationCode=@ActivationCode";
                SqlParameter[] paramter = { 
                                          new SqlParameter("@ActivationCode",item)
                                          };
                result += helper.ExecuteSql(sqltxt, paramter);
            }
            return result;
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStatus(List<ActiveCodeModel> code)
        {
            int result = 0;
            foreach (var item in code)
            {
                string sqltxt = @"UPDATE ActiveCode
  SET AStatus=15
  WHERE ActivationCode=@ActivationCode AND AStatus=20";
                SqlParameter[] paramter = { 
                                          new SqlParameter("@ActivationCode",item.ActivationCode)
                                          };
                result += helper.ExecuteSql(sqltxt, paramter);
            }
            return result;
        }
        /// <summary>
        /// 得到状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetStatus(string code)
        {
            int result = 0;
            string sqltxt = @"SELECT  AStatus
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE   ActivationCode = @ActivationCode";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@ActivationCode",code)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                result = int.Parse(dt.Rows[0]["AStatus"].ToString());
            }
            else
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 得到激活码信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<ActiveCodeModel> GetCodeMassage(List<string> code)
        {
            List<ActiveCodeModel> list = new List<ActiveCodeModel>();
            string codeliststr = string.Join(",", code);
            string sqltxt = @"SELECT  ID ,
        ActivationCode ,
        AType 
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE ActivationCode IN (" + codeliststr + ") AND AStatus=20";
            DataTable dt = helper.Query(sqltxt).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                ActiveCodeModel model = new ActiveCodeModel();
                model.ActivationCode = item["ActivationCode"].ToString();
                model.AType = int.Parse(item["AType"].ToString());
                model.ID = int.Parse(item["ID"].ToString());
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 得到会员信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public MemberInfoModel GetMember(string phone)
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
        /// <summary>
        /// 分配激活码
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="memberphone"></param>
        /// <returns></returns>
        public int AssignedCode(List<string> codes, string memberphone)
        {
            int result = 0;
            //得到激活码信息
            List<ActiveCodeModel> codelist = GetCodeMassage(codes);
            //得到被分配会员信息
            MemberInfoModel member = GetMember(memberphone);
            //开启事务，分配激活码
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //更改原表状态
                    result = UpdateStatus(codelist);
                    if (result < codelist.Count)
                    {
                        throw new Exception("更改激活码原表失败");
                    }
                    List<MemberActiveCodeModel> maclist = new List<MemberActiveCodeModel>();
                    foreach (var item in codelist)
                    {
                        MemberActiveCodeModel mac = new MemberActiveCodeModel();
                        mac.ActiveCode = item.ActivationCode;
                        mac.MemberID = member.ID;
                        mac.MemberName = member.TruethName;
                        mac.MemberPhone = member.MobileNum;
                        mac.AMType = item.AType;
                        maclist.Add(mac);
                    }
                    result = AddMemberActiveCode(maclist);
                    scope.Complete();
                    result = 1;
                }
            }
            catch
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// 添加会员激活码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMemberActiveCode(List<MemberActiveCodeModel> model)
        {
            int result = 0;
            foreach (var item in model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into MemberActiveCode(");
                strSql.Append("ActiveCode,AMType,MemberID,MemberPhone,MemberName,AMStatus,Addtime");
                strSql.Append(") values (");
                strSql.Append("@ActiveCode,@Type,@MemberID,@MemberPhone,@MemberName,1,GETDATE()");
                strSql.Append(") ");
                SqlParameter[] parameters = {
			            new SqlParameter("@ActiveCode", SqlDbType.NVarChar) ,            
                        new SqlParameter("@Type", SqlDbType.Int) ,            
                        new SqlParameter("@MemberID", SqlDbType.Int) ,            
                        new SqlParameter("@MemberPhone", SqlDbType.NVarChar) ,            
                        new SqlParameter("@MemberName", SqlDbType.NVarChar)
            };
                parameters[0].Value = item.ActiveCode;
                parameters[1].Value = item.AMType;
                parameters[2].Value = item.MemberID;
                parameters[3].Value = item.MemberPhone;
                parameters[4].Value = item.MemberName;
                result += Convert.ToInt32(helper.ExecuteSql(strSql.ToString(), parameters));
            }
            return result;
        }
    }
}
