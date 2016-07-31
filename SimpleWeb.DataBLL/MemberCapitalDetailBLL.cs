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
    public class MemberCapitalDetailBLL
    {
        /// <summary>
        /// 为会员派发利息(旧版)
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int PaymentInterest()
        {
            int result = 0;
            int days = int.Parse(SysAdminConfigDAL.GetConfigsByID(10));
            using (TransactionScope scope = new TransactionScope())
            {
                //分派利息
                int rowcunt = MemberCapitalDetailDAL.PaymentInterest(days);
                if (rowcunt < 1)
                {
                    return 0;
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 为会员派发利息(V2版)
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string PaymentInterestV2()
        {
            string result = "";
            int days = int.Parse(SysAdminConfigDAL.GetConfigsByID(10));
            using (TransactionScope scope = new TransactionScope())
            {
                //为每个单据派发利息
                int rowcount = MemberCapitalDetailDAL.PaymentInterestForOrder(days);
                if (rowcount < 1)
                {
                    result = "0为单据派发利息失败";
                    return result;
                }
                //汇总并加入到账户信息记录日志
                rowcount = MemberCapitalDetailDAL.SumInterestMoney();
                if (rowcount < 1)
                {
                    result = "0为会员更新利息总额失败";
                    return result;
                }
                //清空单据上面的利息金额
                rowcount = MemberCapitalDetailDAL.ClearHelperInterest();
                if (rowcount < 1)
                {
                    result = "0清掉帮助订单利息失效";
                    return result;
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 为会员派发利息(V3版)
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string PaymentInterestV3()
        {
            string result = "";
            int days = int.Parse(SysAdminConfigDAL.GetConfigsByID(10));
            using (TransactionScope scope = new TransactionScope())
            {
                //为每个单据派发利息
                int rowcount = MemberCapitalDetailDAL.PaymentInterestForOrderWithLog(days);
                if (rowcount < 1)
                {
                    result = "0为单据派发利息失败";
                    return result;
                }
                //汇总并加入到账户信息记录日志
                rowcount = MemberCapitalDetailDAL.SumInterestMoneyWithoutLog();
                if (rowcount < 1)
                {
                    result = "0为会员更新利息总额失败";
                    return result;
                }
                //解冻单据利率为2，状态为5的单据的利息
                rowcount = MemberCapitalDetailDAL.UpdateStaticCaptail();
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 查询会员的个人资产信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public MemberCapitalDetailModel GetMemberStaticCapital(int memberid)
        {
            return MemberCapitalDetailDAL.GetMemberStaticCapital(memberid);
        }
        /// <summary>
        /// 得到会员的扩展信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public MemberExtendInfoModel GetMemberExtendInfo(int memberid)
        {
            return MemberExtendInfoDAL.GetMemberExtendInfo(memberid);
        }
        /// <summary>
        /// 为会员分派奖金
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string FenPaiMoney(string memberphone, decimal money)
        {
            string result = "0";
            MemberInfoModel member = MemberInfoDAL.GetMember(memberphone);
            if (member == null)
            {
                return "0无此会员";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateMemberDynamicFunds(member.ID, money, member.TruethName, member.MobileNum);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = member.ID;
                logmodel.MemberName = member.TruethName;
                logmodel.MemberPhone = member.MobileNum;
                logmodel.OrderCode = "";
                logmodel.OrderID = 0;
                logmodel.ProduceMoney = money;
                logmodel.Remark = "会员:" + member.MobileNum + " 得到系统派发的奖金";
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 为会员分派奖金
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string FenPaiMoneyToDynamic(int memberid, decimal money)
        {
            string result = "0";
            MemberInfoModel member = MemberInfoDAL.GetMember(memberid);
            if (member == null)
            {
                return "0无此会员";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateMemberDynamicFunds(member.ID, money, member.TruethName, member.MobileNum);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = member.ID;
                logmodel.MemberName = member.TruethName;
                logmodel.MemberPhone = member.MobileNum;
                logmodel.OrderCode = "";
                logmodel.OrderID = 0;
                logmodel.ProduceMoney = money;
                logmodel.Remark = "会员:" + member.MobileNum + " 得到系统派发的奖金";
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 为会员分派奖金
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string FenPaiMoneyToStatic(int memberid, decimal money)
        {
            string result = "0";
            MemberInfoModel member = MemberInfoDAL.GetMember(memberid);
            if (member == null)
            {
                return "0无此会员";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(member.ID, money, member.TruethName, member.MobileNum);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = member.ID;
                logmodel.MemberName = member.TruethName;
                logmodel.MemberPhone = member.MobileNum;
                logmodel.OrderCode = "";
                logmodel.OrderID = 0;
                logmodel.ProduceMoney = money;
                logmodel.Remark = "会员:" + member.MobileNum + " 得到系统派发的奖金";
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 惩罚会员动态金额
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string punishmentDynamicMoney(int memberid, decimal money, string reason)
        {
            string result = "0";
            MemberInfoModel member = MemberInfoDAL.GetMember(memberid);
            if (member == null)
            {
                return "0无此会员";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateDynamicPunishMoney(member.ID, money, member.TruethName, member.MobileNum);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = member.ID;
                logmodel.MemberName = member.TruethName;
                logmodel.MemberPhone = member.MobileNum;
                logmodel.OrderCode = "";
                logmodel.OrderID = 0;
                logmodel.ProduceMoney = (0 - money);
                logmodel.Remark = "会员:" + member.MobileNum + " 被平台惩罚：" + money.ToString() + " 元，原因：" + reason;
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }

        /// <summary>
        /// 惩罚会员静态金额
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public string punishmentStaticMoney(int memberid, decimal money, string reason)
        {
            string result = "0";
            MemberInfoModel member = MemberInfoDAL.GetMember(memberid);
            if (member == null)
            {
                return "0无此会员";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                int rowcount = MemberCapitalDetailDAL.UpdateStaticPunishMoney(member.ID, money, member.TruethName, member.MobileNum);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = member.ID;
                logmodel.MemberName = member.TruethName;
                logmodel.MemberPhone = member.MobileNum;
                logmodel.OrderCode = "";
                logmodel.OrderID = 0;
                logmodel.ProduceMoney = (0 - money);
                logmodel.Remark = "会员:" + member.MobileNum + " 被平台惩罚：" + money.ToString() + " 元，原因：" + reason;
                logmodel.Type = 3;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 得到会员的资产信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberCapitalDetailModel> GetMembercapitalList(MemberCapitalDetailModel model, out int totalrowcount)
        {
            return MemberCapitalDetailDAL.GetMemberCapitalByPage(model, out totalrowcount);
        }
    }
}
