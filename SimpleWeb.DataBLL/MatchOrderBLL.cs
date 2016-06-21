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
    public class MatchOrderBLL
    {
        /// <summary>
        /// 单据匹配方法
        /// </summary>
        /// <returns></returns>
        public int OperateMatchOrder(int hid,int aid)
        {
            int result=0;
            decimal money = 0;
            HelpeOrderModel help = HelpeOrderDAL.GetHelpOrderInfo(hid);
            AcceptHelpOrderModel accept = AcceptHelpOrderDAL.GetAcceptOrderInfo(aid);
            if (help == null || accept == null)
            {
                return 0;
            }
            //系统计算匹配金额
            if (help.DiffAmount < accept.DiffAmount)
            {
                money = help.DiffAmount;
            }
            else if (help.DiffAmount > accept.DiffAmount)
            {
                money = accept.DiffAmount;
            }
            else
            {
                money = help.DiffAmount;
            }
            using (TransactionScope scope=new TransactionScope())
            {
                //插入匹配表
                MatchOrderModel matchmodel = new MatchOrderModel();
                matchmodel.AcceptMemberID = accept.MemberID;
                matchmodel.AcceptOrderCode = accept.OrderCode;
                matchmodel.AcceptOrderID = aid;
                matchmodel.HelperMemberID = help.MemberID;
                matchmodel.HelperOrderCode = help.OrderCode;
                matchmodel.HelperOrderID = hid;
                matchmodel.MatchedMoney = money;
                matchmodel.MatchTime = DateTime.Now;
                int rowcount = MatchOrderDAL.AddMatchOrder(matchmodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更新提供订单表状态
                rowcount = HelpeOrderDAL.UpdateHelpOrderMatch(hid,money);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更新接受订单表状态
                rowcount = AcceptHelpOrderDAL.UpdateAcceptOrderMatch(aid, money);
                if (rowcount < 1)
                {
                    return 0;
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }
    }
}
