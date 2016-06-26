using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;
using System.Transactions;

namespace SimpleWeb.DataBLL
{
    public class AcceptHelpOrderBLL
    {
        private AcceptHelpOrderDAL dal = new AcceptHelpOrderDAL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAcceptHelpOrder(AcceptHelpOrderModel model)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //插入表
                int orderid = dal.AddAcceptHelpOrder(model);
                if (orderid < 1)
                {
                    return 0;
                }
                //扣减会员的相应金额记录
                //点击接受帮助后不再为会员计算利息
                int rowcount = 0;
                if (model.SourceType == 1)
                {
                    rowcount = MemberCapitalDetailDAL.DeductionMemberStaticCapital(model.MemberID, 0 - model.Amount, 0);
                }
                else
                {
                    rowcount = MemberCapitalDetailDAL.DeductionMemberDynamicFunds(model.MemberID, 0 - model.Amount, 0);
                }
                if (rowcount < 1)
                {
                    return 0;
                }
                //增加会员的资金变动记录
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = model.MemberID;
                logmodel.MemberName = model.MemberName;
                logmodel.MemberPhone = model.MemberPhone;
                logmodel.OrderCode = model.OrderCode;
                logmodel.OrderID = orderid;
                logmodel.ProduceMoney = 0 - model.Amount;
                logmodel.Remark = "会员:" + model.MemberPhone + " 申请接受帮助 " + model.Amount.ToString() + "元";
                logmodel.Type = 1;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                scope.Complete();
            }
            return result;
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetAllHelpeOrderList()
        {
            return dal.GetAllHelpeOrderList();
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetAllAcceptOrderListForPage(AcceptHelpOrderModel model, out int totalrowcount)
        {
            return dal.GetAllAcceptOrderListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 查询所有的待匹配的接受帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetWaitAcceptOrderListForPage(AcceptHelpOrderModel model, out int totalrowcount)
        {
            return dal.GetWaitAcceptOrderListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 更新状态为已完成
        /// </summary>
        /// <returns></returns>
        public int UpdateStatusToComplete(int aid)
        {
            int result = 0;
            List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(0, aid);
            using (TransactionScope scope = new TransactionScope())
            {
                //更改当前的状态
                int rowcount = AcceptHelpOrderDAL.UpdateStatus(aid, 5);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更改匹配的提供帮助订单的状态
                foreach (var item in matchorders)
                {
                    rowcount = HelpeOrderDAL.UpdateStatusForComplete(item.HelperOrderID);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    HelpeOrderModel order = HelpeOrderDAL.GetHelpOrderInfo(item.HelperOrderID);
                    //解冻会员的静态冻结资金
                    rowcount = MemberCapitalDetailDAL.UpdateStaticThawDetail(order.MemberID);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    //插入会员的资金变动记录
                    AmountChangeLogModel logmodel = new AmountChangeLogModel();
                    logmodel.MemberID = order.MemberID;
                    logmodel.MemberName = order.MemberName;
                    logmodel.MemberPhone = order.MemberPhone;
                    logmodel.OrderCode = order.OrderCode;
                    logmodel.OrderID = item.HelperOrderID;
                    logmodel.ProduceMoney = 0;
                    logmodel.Remark = "会员:" + order.MemberPhone + " 所有冻结资金解冻";
                    logmodel.Type = 5;
                    rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 更新状态为已取消（前端使用）
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public int UpdateToCancle(int aid,int ispipei)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //更改单据状态
                int rowcount = AcceptHelpOrderDAL.UpdateStatus(aid, 3);
                if (rowcount < 1)
                {
                    return 0;
                }
                //返还会员对应类型的资金
                AcceptHelpOrderModel order = AcceptHelpOrderDAL.GetAcceptOrderInfo(aid);
                if (order.SourceType == 1)//静态资金
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(order.MemberID,order.Amount,order.MemberName,order.MemberPhone);
                }
                else if (order.SourceType == 2)//动态资金
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberDynamicFunds(order.MemberID, order.Amount,order.MemberName,order.MemberPhone);
                }
                if (rowcount < 1)
                {
                    return 0;
                }
                //插入会员资金变动日志
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = order.MemberID;
                logmodel.MemberName = order.MemberName;
                logmodel.MemberPhone = order.MemberPhone;
                logmodel.OrderCode = order.OrderCode;
                logmodel.OrderID = aid;
                logmodel.ProduceMoney = order.Amount;
                logmodel.Remark = "会员:" + order.MemberPhone + " 取消提供帮助，返还扣减的资金 " + order.Amount.ToString() + "元";
                logmodel.Type = 5;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);                
                if (rowcount < 1)
                {
                    return 0;
                }
                if (ispipei>0)//若已经匹配则取消对应单据的信息
                {
                    List<MatchOrderModel> matchs = MatchOrderDAL.GetMatchOrderInfo(0, aid);
                    rowcount = MatchOrderDAL.UpdateStatus(0, aid);//更改状态为取消
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    foreach (var item in matchs)//更改接受帮助订单的状态
                    {
                        rowcount = HelpeOrderDAL.CancleOrderForHelp(item.HelperOrderID, item.MatchedMoney);
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                    }
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 更改接受帮助置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSortindex(int aid)
        {
            return dal.UpdateSortindex(aid);
        }
    }
}
