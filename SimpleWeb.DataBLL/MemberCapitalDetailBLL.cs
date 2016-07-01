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
                rowcount = MemberCapitalDetailDAL.ClearHelperInterest();
                if (rowcount < 1)
                {
                    result = "0清掉帮助订单利息失效";
                    return result;
                }
                scope.Complete();
                result ="1";
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
    }
}
