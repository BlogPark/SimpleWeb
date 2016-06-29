using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        public int AddMemberInfo(MemberInfoModel model)
        {
            int result = 0;
            string value = SysAdminConfigDAL.GetConfigsByID(4);//得到注册返还金额            
            using (TransactionScope scope = new TransactionScope())
            {
                int memberid = dal.AddMemberInfo(model);
                if (memberid < 1)
                {
                    return 0;
                }
                decimal amont = 0;
                if (!string.IsNullOrWhiteSpace(value))
                {

                    if (!decimal.TryParse(value, out amont))
                    {
                        return 0;
                    }
                    int row = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoney(memberid, amont,model.TruethName,model.MobileNum);
                    if (row < 1)
                    {
                        return 0;
                    }
                }
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
                    return 0;
                }
                //更改激活码的使用状态(未完成)

                //插入推荐人信息表
                MemberInfoModel soucemember = MemberInfoDAL.GetMember(model.MemberPhone);
                ReMemberRelationModel remodel = new ReMemberRelationModel();
                remodel.MemberID = soucemember.ID;
                remodel.MemberTruthName = soucemember.TruethName;
                remodel.MemberPhone = soucemember.MobileNum;
                remodel.RecommMID = memberid;
                remodel.RecommMPhone = model.TelPhone;
                remodel.RecommMTruthName = model.TruethName;
                rowcount = ReMemberRelationDAL.AddReMemberRelation(remodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                scope.Complete();
                result = 1;
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
            return dal.UpdateStatus(mid, status);
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
            return dal.GetMemberInfoBycheck(phone,name,alipay);
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
            return dal.UpdateUserPwd(memberid,pwd);
        }
        /// <summary>
        /// 为首页获取数据
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public WebIndexModel GetIndexNeeddata(int memberid)
        {
            WebIndexModel model = new WebIndexModel();
            model.acceptOrders=AcceptHelpOrderDAL.GetTopAcceptOrderListByMemberID(memberid,10);//接受帮助的订单
                model.activecodeCount=ActiveCodeDAL.GetMemberActiveCodeCount(memberid,1);//我的激活币的个数
                //model.activecodelog=//我的激活币和排单币的使用状态
                //model.AmontChangLog=//我的资金变动日志
                model.helperOrders=HelpeOrderDAL.GetTopHelpeOrderListByMemberID(memberid,10);//我提供的帮助订单
                //model.members=//我下级会员的总人数
                model.paidancodeCount=ActiveCodeDAL.GetMemberActiveCodeCount(memberid,2);//我的排单币个数
                //model.zijinmodel=//我的资金状况详情                
            return model;
        }
    }
}
