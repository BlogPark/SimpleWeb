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
    public  class MemberCapitalDetailBLL
    {
        /// <summary>
        /// 为会员派发利息
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int PaymentInterest()
        {
            int result = 0;
            int days = int.Parse(SysAdminConfigDAL.GetConfigsByID(10));
            using (TransactionScope scope=new TransactionScope())
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
        /// 查询会员的个人资产信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public  MemberCapitalDetailModel GetMemberStaticCapital(int memberid)
        {
            return MemberCapitalDetailDAL.GetMemberStaticCapital(memberid);
        }
    }
}
