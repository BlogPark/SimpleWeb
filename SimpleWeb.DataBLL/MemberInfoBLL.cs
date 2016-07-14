using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SimpleWeb.Common;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class MemberInfoBLL
    {
        private MemberInfoDAL dal = new MemberInfoDAL();
        /// <summary>
        ///注册会员（已追加注册返还金额功能）
        /// </summary>
        public string AddMemberInfo(MemberInfoModel model)
        {
            string result = "1";
            string value = SysAdminConfigDAL.GetConfigsByID(4);//得到注册返还金额  
            #region 验证用户真实姓名 和手机
            int rows = MemberInfoDAL.GetMemberCountInfoByName(model.TruethName.Trim());
            if (rows > 2)
            {
                return "0用户姓名重复";
            }
            rows = MemberInfoDAL.GetMemberCountInfoByMobile(model.MobileNum.Trim());
            if (rows > 0)
            {
                return "0手机号已被注册";
            }
            rows = MemberInfoDAL.GetMemberCountInfoByAlipayNum(model.AliPayNum.Trim());
            if (rows > 0)
            {
                return "0支付宝ID已被注册";
            }
            #endregion
            using (TransactionScope scope = new TransactionScope())
            {
                int memberid = dal.AddMemberInfo(model);
                if (memberid < 1)
                {
                    return "0注册失败";
                }
                decimal amont = 0;
                if (!string.IsNullOrWhiteSpace(value))
                {

                    if (!decimal.TryParse(value, out amont))
                    {
                        return "0注册失败";
                    }
                    int row = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoney(memberid, amont, model.TruethName, model.MobileNum);
                    if (row < 1)
                    {
                        return "0注册失败";
                    }
                }
                //返还激活码金额
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = memberid;
                logmodel.MemberName = model.TruethName;
                logmodel.MemberPhone = model.MobileNum;
                logmodel.ProduceMoney = amont;
                logmodel.Remark = "会员注册赠送" + amont.ToString() + "元";
                logmodel.Type = 5;
                int rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0注册失败";
                }
                //插入推荐人信息表
                MemberInfoModel soucemember = MemberInfoDAL.GetMember(model.MemberPhone);
                ReMemberRelationModel remodel = new ReMemberRelationModel();
                remodel.MemberID = soucemember.ID;
                remodel.MemberTruthName = soucemember.TruethName;
                remodel.MemberPhone = soucemember.MobileNum;
                remodel.RecommMID = memberid;
                remodel.RecommMPhone = model.MobileNum;
                remodel.RecommMTruthName = model.TruethName;
                rowcount = ReMemberRelationDAL.AddReMemberRelation(remodel);
                if (rowcount < 1)
                {
                    return "0注册失败";
                }
                //初始化会员扩展信息表
                MemberExtendInfoModel extendinfo = new MemberExtendInfoModel();
                extendinfo.MemberID = memberid;
                extendinfo.MemberHelpCount = 0;
                extendinfo.LastHelpMoney = 0;
                extendinfo.LastHelperTime = DateTime.Now.AddYears(-1);
                rowcount = MemberExtendInfoDAL.AddModelExtendinfo(extendinfo);
                if (rowcount < 1)
                {
                    return "0注册失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateMemberInfo(MemberInfoModel model)
        {
            return dal.UpdateMemberInfo(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int ID)
        {
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(int mid, int status)
        {
            return MemberInfoDAL.UpdateStatus(mid, status);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetMemberInfoListForPage(MemberInfoModel model, out int totalrowcount)
        {
            return dal.GetMemberInfoListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 得到行政区域列表
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <returns></returns>
        public List<ReginTableModel> GetReginTableListModel(int parentid)
        {
            return dal.GetReginTableListModel(parentid);
        }
        /// <summary>
        /// 得到会员的直荐名单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<ReMemberRelationModel> GetRecommdMemberModel(int rmid)
        {
            return dal.GetRecommdMemberModel(rmid);
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
            return dal.GetMemberInfoBycheck(phone, name, alipay);
        }
        /// <summary>
        /// 前端会员登陆
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public MemberInfoModel GetMemberInfo(string phone, string pwd, out string logmsg)
        {
            return dal.GetMemberInfo(phone, pwd, out logmsg);
        }
        /// <summary>
        /// 更改会员的密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateUserPwd(int memberid, string pwd)
        {
            return dal.UpdateUserPwd(memberid, pwd);
        }
        /// <summary>
        /// 为首页获取数据
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public WebIndexModel GetIndexNeeddata(int memberid)
        {
            int count = 0;
            List<ReMemberRelationModel> rememberlist = ReMemberRelationDAL.GetMemberRecommendMap(memberid, out count);
            WebIndexModel model = new WebIndexModel();
            model.acceptOrders = AcceptHelpOrderDAL.GetTopAcceptOrderListByMemberID(memberid, 4);//接受帮助的订单
            model.activecodeCount = ActiveCodeDAL.GetMemberActiveCodeCount(memberid, 1);//我的激活币的个数
            model.activecodelog = OperateLogDAL.GetActiveCodeLogByMemberID(memberid, 10);//我的激活币和排单币的使用状态
            model.AmontChangLog = OperateLogDAL.GetAmontChangeLogByMemberID(memberid, 10);//我的资金变动日志
            model.helperOrders = HelpeOrderDAL.GetTopHelpeOrderListByMemberID(memberid, 4);//我提供的帮助订单
            model.members = count;//我下级会员的总人数
            model.paidancodeCount = ActiveCodeDAL.GetMemberActiveCodeCount(memberid, 2);//我的排单币个数
            model.zijinmodel = MemberCapitalDetailDAL.GetMemberStaticCapital(memberid);//我的资金状况详情           

            return model;
        }
        /// <summary>
        /// 查找会员的推荐人信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public ReMemberRelationModel GetReMemberRelation(int memberid)
        {
            return ReMemberRelationDAL.GetReMemberRelation(memberid);
        }
        /// <summary>
        /// 激活会员（外用）
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="phone"></param>
        /// <param name="activecode"></param>
        /// <returns></returns>
        public string ActiveMember(int memberid, string phone, string activecode, bool isauto, int souceid = 0)
        {
            string result = "0";
            if (isauto)
            {
                activecode = ActiveCodeDAL.GetRedamActiveCode(1);
            }
            MemberInfoModel member = null;
            if (memberid != 0)
            {
                member = MemberInfoDAL.GetNotActiveMember(memberid);
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                member = MemberInfoDAL.GetMember(phone);
            }
            if (member == null)
            {
                result = "0无此会员";
                return result;
            }
            if (member.MStatus != 1)
            {
                result = "0会员状态不正确";
                return result;
            }
            ActiveCodeModel activecodemodel = ActiveCodeDAL.GetActiveCodeExtendModel(activecode);
            if (activecodemodel == null)
            {
                result = @"0无此激活码";
                return result;
            }
            if (activecodemodel.AType == 2)
            {
                result = @"0激活码类型选择不正确";
                return result;
            }
            if (activecodemodel.AStatus == 10)
            {
                result = @"0激活码已经使用";
                return result;
            }
            if (activecodemodel.AMStatus == 3)
            {
                result = @"0激活码已经过期";
                return result;
            }
            using (TransactionScope scope = new TransactionScope())
            {
                //更改会员的状态
                int rowcount = MemberInfoDAL.UpdateStatus(member.ID, 2);
                if (rowcount < 1)
                {
                    result = "0更新会员状态失败";
                    return result;
                }
                //更改激活码的状态
                rowcount = ActiveCodeDAL.UpdateStatus(activecodemodel.ID, 10);
                if (rowcount < 1)
                {
                    result = "0更新激活码状态失败";
                    return result;
                }
                if (activecodemodel.MID > 0)
                {
                    //更改会员机会码的使用状态
                    if (souceid > 0)
                    {
                        rowcount = ActiveCodeDAL.UpdateMemberActiveCodeToUse(souceid, member.MobileNum, activecodemodel.ActivationCode);
                        if (rowcount < 1)
                        {
                            result = "0更新会员激活码表状态失败";
                            return result;
                        }
                    }
                    else
                    {
                        rowcount = ActiveCodeDAL.UpdateMemberActiveStatus(activecodemodel.MID, 2);
                        if (rowcount < 1)
                        {
                            result = "0更新会员激活码表状态失败";
                            return result;
                        }
                    }
                    //插入使用日志
                    ActiveCodeLogModel logmodel = new ActiveCodeLogModel();
                    logmodel.ActiveCode = activecodemodel.ActivationCode;
                    logmodel.Addtime = DateTime.Now;
                    logmodel.AID = activecodemodel.ID;
                    logmodel.MemberID = activecodemodel.MemberID;
                    logmodel.MemberName = activecodemodel.MemberName;
                    logmodel.MemberPhone = activecodemodel.MemberPhone;
                    logmodel.Remark = "为会员：" + member.MobileNum + " 激活";
                    rowcount = OperateLogDAL.AddActiveCodeLog(logmodel);
                    if (rowcount < 1)
                    {
                        result = "0写入日志失败";
                        return result;
                    }
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        #region 验证码操作
        /// <summary>
        /// 发送注册验证短信
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string SendRegisterSms(string phone)
        {
            string smscontent = SysAdminConfigDAL.GetConfigsByID(18);//得到短信模板
            Random ran = new Random();
            int RandKey = ran.Next(100000, 999999);
            int id = AddVerification(RandKey.ToString());
            string content = string.Format(smscontent, RandKey.ToString(), id.ToString());
            string result = SendSMSClass.SendSMS(phone, content);
            UpdateVerification(result.Substring(1), id);
            if (result.StartsWith("s"))
            {
                return id.ToString();
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 插入验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int AddVerification(string code)
        {
            return MemberInfoDAL.AddVerification(code);
        }
        /// <summary>
        /// 修改验证码发送结果
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateVerification(string sendid, int id)
        {
            return MemberInfoDAL.UpdateVerification(sendid, id);
        }
        /// <summary>
        /// 修改验证码发送结果
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string SelectVerification(int id)
        {
            return MemberInfoDAL.SelectVerification(id);
        }
        #endregion
        /// <summary>
        /// 根据会员ID得到推荐图
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public List<ReMemberRelationModel> GetReMemberRelationList(int memberid)
        {
            return ReMemberRelationDAL.GetReMemberRelationList(memberid);
        }
    }
}
