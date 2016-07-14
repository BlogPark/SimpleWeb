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
    public class MatchOrderBLL
    {
        /// <summary>
        /// 单据匹配方法4
        /// </summary>
        /// <returns></returns>
        public int OperateMatchOrder(int hid,int aid)
        {
            int result=0;
            decimal money = 0;
            HelpeOrderModel help = HelpeOrderDAL.GetHelpOrderInfo(hid);
            AcceptHelpOrderModel accept = AcceptHelpOrderDAL.GetAcceptOrderInfo(aid);
            string helpsmscontent = SysAdminConfigDAL.GetConfigsByID(19);
            string acceptsmscontent = SysAdminConfigDAL.GetConfigsByID(20);
            if (help == null || accept == null)
            {
                return 0;
            }
            if (help.HStatus > 2 || accept.AStatus > 2)
            {
                return 0;
            }
            if (help.DiffAmount == 0 || accept.DiffAmount == 0)
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
                #region 发送短信
                string helpsms = string.Format(helpsmscontent, help.OrderCode);
                string acceptsms = string.Format(acceptsmscontent, accept.OrderCode);
                string resule = SendSMSClass.SendSMS(help.MemberPhone, helpsms);
                resule = SendSMSClass.SendSMS(accept.MemberPhone, acceptsms);
                #endregion
                scope.Complete();
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 根据类型得到分页的日志数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="type"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<AmountChangeLogModel> GetAmountChangeLogByTypeForPage(int pageindex, int pagesize, out int totalrowcount)
        {
            return OperateLogDAL.GetAmountChangeLogByTypeForPage(pageindex,pagesize,4,out totalrowcount);
        }
    }
}
