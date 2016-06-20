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
                    rowcount = MemberCapitalDetailDAL.DeductionMemberStaticCapital(model.MemberID, 0 - model.Amount,0);
                }
                else
                {
                    rowcount = MemberCapitalDetailDAL.DeductionMemberDynamicFunds(model.MemberID,0-model.Amount,0);
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
                logmodel.Remark = "会员:"+model.MemberPhone+" 申请接受帮助 "+model.Amount.ToString()+"元";
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
    }
}
