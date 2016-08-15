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
        public static DbHelperSQL helper = new DbHelperSQL();
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
            string columms = @"ID,ActivationCode,AType,CASE AType WHEN 1 THEN '激活码' WHEN 2 THEN '排单币' END ATypeName,AStatus,AddTime,CASE AStatus WHEN 20 THEN '未使用' WHEN 15 THEN '已分配'  WHEN 10 THEN '已使用' END AStatusName ";
            string where = "";
            if (model != null)
            {
                if (model.AStatus > 0)
                {
                    where += " AStatus= " + model.AStatus;
                }
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
                    ActiveCodeModel active = new ActiveCodeModel();
                    if (item["ID"].ToString() != "")
                    {
                        active.ID = int.Parse(item["ID"].ToString());
                    }
                    active.ActivationCode = item["ActivationCode"].ToString();
                    if (item["AType"].ToString() != "")
                    {
                        active.AType = int.Parse(item["AType"].ToString());
                    }
                    if (item["AStatus"].ToString() != "")
                    {
                        active.AStatus = int.Parse(item["AStatus"].ToString());
                    }
                    if (item["AddTime"].ToString() != "")
                    {
                        active.AddTime = DateTime.Parse(item["AddTime"].ToString());
                    }
                    active.AStatusName = item["AStatusName"].ToString();
                    active.ATypeName = item["ATypeName"].ToString();
                    list.Add(active);
                }
            }
            return list;
        }
        /// <summary>
        /// 更改状态为已使用
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
        /// 更改状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static int UpdateStatus(int id, int status)
        {
            int result = 0;
            string sqltxt = @"UPDATE ActiveCode
  SET AStatus=@AStatus
  WHERE ID=@id ";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@id",id),
                                          new SqlParameter("@AStatus",status)
                                          };
            result = helper.ExecuteSql(sqltxt, paramter);
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
            string codeliststr = "";
            foreach (var item in code)
            {
                codeliststr += "'" + item + "',";
            }
            string sqltxt = @"SELECT  ID ,
        ActivationCode ,
        AType 
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE ActivationCode IN (" + codeliststr.TrimEnd(',') + ") AND AStatus=20";
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
        /// 得到激活码信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ActiveCodeModel GetSingleCodeMassage(string code)
        {

            string sqltxt = @"SELECT  ID ,
        ActivationCode ,
        AType 
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE ActivationCode =@code  AND AStatus=20";
            SqlParameter[] paramter = { new SqlParameter("@code", code) };
            DataTable dt = helper.Query(sqltxt).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ActiveCodeModel model = new ActiveCodeModel();
                model.ActivationCode = dt.Rows[0]["ActivationCode"].ToString();
                model.AType = int.Parse(dt.Rows[0]["AType"].ToString());
                model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                return model;
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
        public MemberInfoModel GetMember(string phone)
        {
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        MobileNum,MStatus
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
                return null;
        }
        /// <summary>
        /// 得到会员信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public MemberInfoModel GetMember(int mid)
        {
            string sqltxt = @"SELECT  ID ,
        TruethName ,
        MobileNum
FROM    dbo.MemberInfo
WHERE   ID = @Mid";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@Mid",mid)
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
            if (member.MStatus != 2)
            {
                return 0;
            }
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
                    if (result < 1)
                    {
                        return 0;
                    }
                    try
                    {
                        UserBehaviorLogModel log = new UserBehaviorLogModel();
                        log.AOrderCode = "";
                        log.BehaviorSource = 2;
                        if (codelist[0].AType == 1)
                        {
                            log.BehaviorType = 8;
                        }
                        else
                        {
                            log.BehaviorType = 7;
                        }
                        log.HOrderCode = "";
                        log.MemberID = member.ID;
                        log.MemberName = member.TruethName;
                        log.MemberPhone = member.MobileNum;
                        log.ProcAmount = 0;
                        log.Remark = "系统派发激活码/排单币";
                        UserBehaviorLogDAL.AddUserBehaviorLog(log);
                    }
                    catch { }
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
        /// <summary>
        /// 得到会员的激活码信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberActiveCodeModel> GetMemberActiveCodeListForPage(MemberActiveCodeModel model, out int totalrowcount)
        {
            List<MemberActiveCodeModel> list = new List<MemberActiveCodeModel>();
            string columms = @"ID ,ActiveCode ,AMType,CASE AMType WHEN 1 THEN '激活账户' WHEN 2 THEN '排单专用' END AMTypeName ,MemberID ,MemberPhone ,MemberName ,AMStatus ,Addtime,CASE AMStatus WHEN 1 THEN '未使用' WHEN 2 THEN '已使用'  WHEN 3 THEN '已过期' END AMStatusName";
            string where = "";
            if (model != null)
            {
                if (model.AMStatus > 0)
                {
                    where += " AMStatus= " + model.AMStatus;
                }
                if (!string.IsNullOrWhiteSpace(model.MemberPhone) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" MemberPhone Like '%" + model.MemberPhone + "%'";
                }
                else if (!string.IsNullOrWhiteSpace(model.MemberPhone) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND MemberPhone Like '%" + model.MemberPhone + "%'";
                }
                if (!string.IsNullOrWhiteSpace(model.ActiveCode) && string.IsNullOrWhiteSpace(where))
                {
                    where += @" ActiveCode ='" + model.ActiveCode + "'";
                }
                else if (!string.IsNullOrWhiteSpace(model.ActiveCode) && !string.IsNullOrWhiteSpace(where))
                {
                    where += @" AND ActiveCode ='" + model.ActiveCode + "'";
                }
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "Addtime";
            page.pageindex = model.PageIndex;
            page.pagesize = model.PageSize;
            page.tablename = @"dbo.MemberActiveCode";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberActiveCodeModel memberactive = new MemberActiveCodeModel();
                    if (item["ID"].ToString() != "")
                    {
                        memberactive.ID = int.Parse(item["ID"].ToString());
                    }
                    memberactive.ActiveCode = item["ActiveCode"].ToString();
                    if (item["MemberID"].ToString() != "")
                    {
                        memberactive.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    memberactive.MemberPhone = item["MemberPhone"].ToString();
                    memberactive.MemberName = item["MemberName"].ToString();
                    if (item["AMStatus"].ToString() != "")
                    {
                        memberactive.AMStatus = int.Parse(item["AMStatus"].ToString());
                    }
                    if (item["Addtime"].ToString() != "")
                    {
                        memberactive.Addtime = DateTime.Parse(item["Addtime"].ToString());
                    }
                    memberactive.AMStatusName = item["AMStatusName"].ToString();
                    memberactive.AMTypeName = item["AMTypeName"].ToString();
                    list.Add(memberactive);
                }
            }
            return list;
        }
        /// <summary>
        /// 修改会员激活码状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateMemberActive(int id)
        {
            string sqltxt = @"UPDATE  MemberActiveCode
SET     AMStatus = 3
WHERE   ID = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",id)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 修改会员激活码状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status">状态值（1 未使用 2 已使用 3 过期）</param>
        /// <returns></returns>
        public static int UpdateMemberActiveStatus(int id, int status)
        {
            string sqltxt = @"UPDATE  MemberActiveCode
SET     AMStatus = @status
WHERE   ID = @id";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@id",id),
                                      new SqlParameter("@status",status)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 修改会员激活码为使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMemberActiveToUse(int memberid, string ordercode, string activecode)
        {
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberActiveCode
SET     AMStatus = 2 ,
        UseCode = @usecode ,
        UserTime = GETDATE()
WHERE   MemberID = @memberid
        AND ActiveCode = @activecode
        AND AMType=2";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@memberid",memberid),
                                      new SqlParameter("@usecode",ordercode),
                                      new SqlParameter("@activecode",activecode)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 修改会员激活码为使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMemberActiveCodeToUse(int memberid, string ordercode, string activecode)
        {
            string sqltxt = @"UPDATE  SimpleWebDataBase.dbo.MemberActiveCode
SET     AMStatus = 2 ,
        UseCode = @usecode ,
        UserTime = GETDATE()
WHERE   MemberID = @memberid
        AND ActiveCode = @activecode
        AND AMType=1";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@memberid",memberid),
                                      new SqlParameter("@usecode",ordercode),
                                      new SqlParameter("@activecode",activecode)
                                      };
            return helper.ExecuteSql(sqltxt, paramter);
        }
        /// <summary>
        /// 会员间赠送激活码
        /// </summary>
        /// <param name="soucememberID">原始会员ID</param>
        /// <param name="type">赠送激活码类型</param>
        /// <param name="acceptMemberPhone">接受会员电话</param>
        /// <param name="count">赠送数量</param>
        /// <returns>返回值（-1 接受会员不存在 -2 没有足够的激活币赠送 0 数据库操作失败 1 成功）</returns>
        public int GiveActiveCodeFromMember(int soucememberID, int type, string acceptMemberPhone, int count)
        {
            int result = 0;
            //读取接受会员信息
            MemberInfoModel member = GetMember(acceptMemberPhone);
            if (member == null)
            {
                return -1;
            }
            if (member.MStatus != 2)
            {
                return -1;
            }
            //开启事务
            using (TransactionScope scope = new TransactionScope())
            {
                //查询该会员名下可用类型的激活码数量
                List<MemberActiveCodeModel> aclist = GetMemberActiveCodeList(soucememberID, type);
                if (aclist.Count < count)
                {
                    return -2;
                }
                //修改激活码的所有者信息
                string sqltxt = @"UPDATE TOP ( @count)
        MemberActiveCode
SET     MemberID = @memberid ,
        MemberPhone = @MemberPhone ,
        MemberName = @MemberName
WHERE   MemberID = @soucemid
        AND AMStatus = 1
        AND AMType = @AMType";
                SqlParameter[] paramter = {
                                              new SqlParameter("@memberid",member.ID),
                                              new SqlParameter("@MemberPhone",member.MobileNum),
                                              new SqlParameter("@MemberName",member.TruethName),
                                              new SqlParameter("@soucemid",soucememberID),
                                              new SqlParameter("@AMType",type),
                                              new SqlParameter("@count",count)
                                        };
                int row = helper.ExecuteSql(sqltxt, paramter);
                if (row < 0)
                {
                    return 0;
                }
                int rowcount = row;
                //记录转出者名下日志
                MemberInfoModel sourcemodel = GetMember(soucememberID);
                ActiveCodeLogModel souce = new ActiveCodeLogModel();
                souce.MemberID = soucememberID;
                souce.MemberName = sourcemodel.TruethName;
                souce.MemberPhone = sourcemodel.MobileNum;
                souce.ActiveCode = "";
                souce.AID = 0;
                souce.Remark = "为会员：" + member.TruethName + " 手机号：" + member.MobileNum + " 转账" + rowcount.ToString() + "个" + (type == 1 ? "激活币" : "排单币");
                row = OperateLogDAL.AddActiveCodeLog(souce);
                if (row < 0)
                {
                    return 0;
                }
                //记录转入者名下日志
                ActiveCodeLogModel accept = new ActiveCodeLogModel();
                accept.MemberID = member.ID;
                accept.MemberName = member.TruethName;
                accept.MemberPhone = member.MobileNum;
                accept.ActiveCode = "";
                accept.AID = 0;
                accept.Remark = "接受来自会员：" + sourcemodel.TruethName + " 手机号：" + sourcemodel.MobileNum + " 转给的" + rowcount.ToString() + "个" + (type == 1 ? "激活币" : "排单币");
                row = OperateLogDAL.AddActiveCodeLog(accept);
                if (row < 0)
                {
                    return 0;
                }
                //记录系统操作日志
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = "";
                    log.BehaviorSource = 1;
                    if (type == 1)
                        log.BehaviorType = 8;
                    else
                        log.BehaviorType = 7;
                    log.HOrderCode = "";
                    log.MemberID = member.ID;
                    log.MemberName = member.TruethName;
                    log.MemberPhone = member.MobileNum;
                    log.ProcAmount = 0;
                    log.Remark = "会员：" + member.MobileNum + " 得到来自" + sourcemodel.MobileNum + "转来的" + rowcount.ToString() + "个" + (type == 1 ? "激活币" : "排单币");
                    UserBehaviorLogDAL.AddUserBehaviorLog(log);
                }
                catch { }
                scope.Complete();
                result = 1;
            }
            return result;
        }
        public List<MemberActiveCodeModel> GetMemberActiveCodeList(int memberid, int type)
        {
            List<MemberActiveCodeModel> list = new List<MemberActiveCodeModel>();
            string sqltxt = @"SELECT  ActiveCode
FROM    SimpleWebDataBase.dbo.MemberActiveCode
WHERE   AMType = @type
        AND MemberID = @memberid
        AND AMStatus = 1";
            SqlParameter[] paramter = { 
                                          new SqlParameter("@type",type),
                                          new SqlParameter("@memberid",memberid)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                MemberActiveCodeModel model = new MemberActiveCodeModel();
                model.ActiveCode = item["ActiveCode"].ToString();
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 得到会员的激活码使用信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeLogModel> GetActiveCodeLogListForPage(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<ActiveCodeLogModel> list = new List<ActiveCodeLogModel>();
            string columms = @"ID ,MemberID ,MemberName ,MemberPhone ,ActiveCode ,AID ,Remark ,Addtime";
            string where = "";
            if (memberid > 0)
            {
                where = " MemberID=" + memberid.ToString();
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "Addtime";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.ActiveCodeLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActiveCodeLogModel activelog = new ActiveCodeLogModel();
                    if (item["ID"].ToString() != "")
                    {
                        activelog.ID = int.Parse(item["ID"].ToString());
                    }
                    activelog.ActiveCode = item["ActiveCode"].ToString();
                    if (item["MemberID"].ToString() != "")
                    {
                        activelog.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    activelog.MemberPhone = item["MemberPhone"].ToString();
                    activelog.MemberName = item["MemberName"].ToString();
                    if (item["AID"].ToString() != "")
                    {
                        activelog.AID = int.Parse(item["AID"].ToString());
                    }
                    if (item["Addtime"].ToString() != "")
                    {
                        activelog.Addtime = DateTime.Parse(item["Addtime"].ToString());
                    }
                    activelog.Remark = item["Remark"].ToString();
                    list.Add(activelog);
                }
            }
            return list;
        }
        /// <summary>
        /// 得到会员的分类激活码信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberActiveCodeModel> GetMemberActiveCodeListForPage(int memberid, int typeid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<MemberActiveCodeModel> list = new List<MemberActiveCodeModel>();
            string columms = @"ID,ActiveCode,AMType,MemberID,MemberPhone,MemberName,AMStatus,Addtime,UseCode,UserTime,CASE AMStatus WHEN 1 THEN '未使用' WHEN 2 THEN '已使用' WHEN 3 THEN '已过期' END AS AMStatusName";
            string where = "";
            if (memberid > 0)
            {
                where = " MemberID=" + memberid.ToString() + " AND AMType=" + typeid.ToString();
            }
            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "Addtime";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.MemberActiveCode";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    MemberActiveCodeModel activecode = new MemberActiveCodeModel();
                    if (item["ID"].ToString() != "")
                    {
                        activecode.ID = int.Parse(item["ID"].ToString());
                    }
                    if (item["UserTime"].ToString() != "")
                    {
                        activecode.UserTime = DateTime.Parse(item["UserTime"].ToString());
                    }
                    activecode.ActiveCode = item["ActiveCode"].ToString();
                    if (item["AMType"].ToString() != "")
                    {
                        activecode.AMType = int.Parse(item["AMType"].ToString());
                    }
                    if (item["MemberID"].ToString() != "")
                    {
                        activecode.MemberID = int.Parse(item["MemberID"].ToString());
                    }
                    activecode.MemberPhone = item["MemberPhone"].ToString();
                    activecode.MemberName = item["MemberName"].ToString();
                    if (item["AMStatus"].ToString() != "")
                    {
                        activecode.AMStatus = int.Parse(item["AMStatus"].ToString());
                    }
                    if (item["Addtime"].ToString() != "")
                    {
                        activecode.Addtime = DateTime.Parse(item["Addtime"].ToString());
                    }
                    activecode.UseCode = item["UseCode"].ToString();
                    activecode.AMStatusName = item["AMStatusName"].ToString();
                    list.Add(activecode);
                }
            }
            return list;
        }

        public static int GetMemberActiveCodeCount(int memberid, int type)
        {
            string sqltxt = @"SELECT  COUNT(0)
FROM    SimpleWebDataBase.dbo.MemberActiveCode
WHERE   MemberID = @memberid
        AND AMStatus = 1
        AND AMType = @type";
            SqlParameter[] paramter = { new SqlParameter("@memberid",memberid),
                                      new SqlParameter("@type",type)};
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 根据Code读取信息及分配信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ActiveCodeModel GetActiveCodeExtendModel(string code)
        {
            ActiveCodeModel model = new ActiveCodeModel();
            string sqltxt = @"SELECT  A.ID ,
        A.ActivationCode ,
        A.AType ,
        A.AStatus,
        B.MemberID,
        B.MemberName,
        B.MemberPhone,
        B.ID as mid,
        B.AMStatus
FROM    SimpleWebDataBase.dbo.ActiveCode A 
LEFT JOIN SimpleWebDataBase.dbo.MemberActiveCode B ON A.ActivationCode=B.ActiveCode
WHERE A.ActivationCode=@code";
            SqlParameter[] paramter = { 
                                      new SqlParameter("@code",code)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                model.ActivationCode = dt.Rows[0]["ActivationCode"].ToString();
                model.AStatus = dt.Rows[0]["AStatus"].ToString().ParseToInt(0);
                model.AType = dt.Rows[0]["AType"].ToString().ParseToInt(0);
                model.ID = dt.Rows[0]["ID"].ToString().ParseToInt(0);
                model.MemberID = dt.Rows[0]["MemberID"].ToString().ParseToInt(0);
                model.MemberName = dt.Rows[0]["MemberName"].ToString();
                model.MemberPhone = dt.Rows[0]["MemberPhone"].ToString();
                model.MID = dt.Rows[0]["mid"].ToString().ParseToInt(0);
                model.AMStatus = dt.Rows[0]["AMStatus"].ToString().ParseToInt(0);
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 随机读取一个为分配  未使用的激活码
        /// </summary>
        /// <param name="type">激活码 1  排单码 2</param>
        /// <returns></returns>
        public static string GetRedamActiveCode(int type)
        {
            string sqltxt = @"SELECT TOP 1
        A.ActivationCode
FROM    SimpleWebDataBase.dbo.ActiveCode A
WHERE   A.AType = @type
        AND A.AStatus = 20";
            SqlParameter[] paramter = { new SqlParameter("@type", type) };
            return helper.GetSingle(sqltxt, paramter).ToString();
        }
        /// <summary>
        /// 按照类型统计全部的激活码数量
        /// </summary>
        /// <param name="typenum"></param>
        /// <returns></returns>
        public static int GetTotalCount(int typenum)
        {
            string sqltxt = @"SELECT COUNT(0)
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE   AType = @atype ";
            SqlParameter[] paramter = {
                                          new SqlParameter("@atype",typenum)
                                      };
            return helper.GetSingle(sqltxt, paramter).ToString().ParseToInt(0);
        }
        /// <summary>
        /// 按照类型和数量读取激活码
        /// </summary>
        /// <param name="typenum"></param>
        /// <returns></returns>
        public static List<string> GetTypeCountActiveCode(int type, int count)
        {
            List<string> list = new List<string>();
            string sqltxt = @"SELECT TOP (@topnum) ActivationCode
FROM    SimpleWebDataBase.dbo.ActiveCode
WHERE   AType = @atype AND AStatus = 20";
            SqlParameter[] paramter = {
                                          new SqlParameter("@atype",type),
                                          new SqlParameter("@topnum",count)
                                      };
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["ActivationCode"].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据会员的ID读取特定数量的特定类型激活码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<string> GetMemberCodeByCount(int type, int memberid, int count)
        {
            List<string> list = new List<string>();
            string sqltxt = @"SELECT TOP ( @topnum )
        ActiveCode
FROM    SimpleWebDataBase.dbo.MemberActiveCode
WHERE   AMType = @type
        AND MemberID = @memberid
        AND AMStatus = 1";
            SqlParameter[] paramter = { new SqlParameter("@type",type),
                                      new SqlParameter("@topnum",count),
                                      new SqlParameter("@memberid",memberid)};
            DataTable dt = helper.Query(sqltxt, paramter).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item["ActiveCode"].ToString());
                }
            }
            return list;
        }
        /// <summary>
        /// 查询分页的排单币使用信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public static List<ActiveCodeLogModel> GetActiveCodeLogForPage(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            List<ActiveCodeLogModel> list = new List<ActiveCodeLogModel>();
            string columms = @"ID,MemberID,MemberName,MemberPhone,ActiveCode,AID,Remark,Addtime";
            string where = "";
            if (memberid > 0)
            {
                where += "MemberID='" + memberid + "'";
            }

            PageProModel page = new PageProModel();
            page.colums = columms;
            page.orderby = "Addtime";
            page.pageindex = pageindex;
            page.pagesize = pagesize;
            page.tablename = @"dbo.ActiveCodeLog";
            page.where = where;
            DataTable dt = PublicHelperDAL.GetTable(page, out totalrowcount);
            foreach (DataRow item in dt.Rows)
            {
                ActiveCodeLogModel activecodelogmodel = new ActiveCodeLogModel();
                if (item["ID"].ToString() != "")
                {
                    activecodelogmodel.ID = int.Parse(item["ID"].ToString());
                }
                if (item["AddTime"].ToString() != "")
                {
                    activecodelogmodel.Addtime = DateTime.Parse(item["Addtime"].ToString());
                }
                activecodelogmodel.ActiveCode = item["ActiveCode"].ToString();
                if (item["MemberID"].ToString() != "")
                {
                    activecodelogmodel.MemberID = int.Parse(item["MemberID"].ToString());
                }
                activecodelogmodel.MemberPhone = item["MemberPhone"].ToString();
                activecodelogmodel.MemberName = item["MemberName"].ToString();
                if (item["AID"].ToString() != "")
                {
                    activecodelogmodel.AID = int.Parse(item["AID"].ToString());
                }
                activecodelogmodel.Remark = item["Remark"].ToString();
                list.Add(activecodelogmodel);
            }
            return list;
        }
    }
}
