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
    public class HelpeOrderBLL
    {
        private HelpeOrderDAL dal = new HelpeOrderDAL();
        /// <summary>
        /// 添加一条提供帮助单据
        /// </summary>
        public int AddHelpeOrder(HelpeOrderModel model)
        {
            int result = 0;
            string interest = SysAdminConfigDAL.GetConfigsByID(5);//得到排单后的利率
            MemberExtendInfoModel meinfo = MemberExtendInfoDAL.GetMemberExtendInfo(model.MemberID);
            using (TransactionScope scope = new TransactionScope())
            {
                //更改激活码的使用状态
                int rowcount = ActiveCodeDAL.UpdateStatus(model.ActiveCodeID, 10);
                if (rowcount < 1)
                {
                    return 0;
                }
                rowcount = ActiveCodeDAL.UpdateMemberActiveToUse(model.MemberID, model.OrderCode, model.ActiveCode);
                if (rowcount < 1)
                {
                    return 0;
                }
                //插入激活码的使用日志
                ActiveCodeLogModel aclogmodel = new ActiveCodeLogModel();
                aclogmodel.ActiveCode = model.ActiveCode;
                aclogmodel.AID = model.ActiveCodeID;
                aclogmodel.MemberID = model.MemberID;
                aclogmodel.MemberName = model.MemberName;
                aclogmodel.MemberPhone = model.MemberPhone;
                aclogmodel.Remark = "会员:" + model.MemberName + " 使用排单币:" + model.ActiveCode + " 进行排单";
                rowcount = OperateLogDAL.AddActiveCodeLog(aclogmodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更改会员的资金信息和利率
                decimal dinterest = 1;
                if (!string.IsNullOrWhiteSpace(interest))
                {
                    if (!decimal.TryParse(interest, out dinterest))
                    {
                        return 0;
                    }
                }
                rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(model.MemberID, model.Amount, dinterest);
                if (rowcount < 1)
                {
                    return 0;
                }
                //插入表记录
                int orderid = dal.AddHelpeOrder(model);
                if (orderid < 1)
                {
                    return 0;
                }
                //更新会员的统计信息
                rowcount = MemberExtendInfoDAL.Update(model.MemberID, model.Amount);
                if (rowcount < 1)
                {
                    return 0;
                }
                //插入会员的资金变动纪录
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = model.MemberID;
                logmodel.MemberName = model.MemberName;
                logmodel.MemberPhone = model.MemberPhone;
                logmodel.OrderCode = model.OrderCode;
                logmodel.OrderID = orderid;
                logmodel.ProduceMoney = model.Amount;
                logmodel.Remark = "会员:" + model.MemberPhone + " 申请提供帮助 " + model.Amount.ToString() + "元";
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<HelpeOrderModel> GetAllHelpeOrderList()
        {
            return dal.GetAllHelpeOrderList();
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<HelpeOrderModel> GetAllHelpeOrderListForPage(HelpeOrderModel model, out int totalrowcount)
        {
            return dal.GetAllHelpeOrderListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 添加提供帮助订单
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public int InsertHelperOrder(HelpeOrderModel model)
        {
            return dal.InsertHelperOrder(model);
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(int oid, int status)
        {
            return HelpeOrderDAL.UpdateStatus(oid, status);
        }
        /// <summary>
        /// 更改置顶
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSortindex(int oid)
        {
            return dal.UpdateSortindex(oid);
        }
        /// <summary>
        /// 查询所有的待匹配的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<HelpeOrderModel> GetWaitHelpeOrderListForPage(HelpeOrderModel model, out int totalrowcount)
        {
            return dal.GetWaitHelpeOrderListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 更新提供帮助订单为已打款
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public int UpdateToPlayMoney(int hid)
        {
            int result = 0;
            string value = SysAdminConfigDAL.GetConfigsByID(6);//得到打款后返还金额
            string inteist = SysAdminConfigDAL.GetConfigsByID(7);//得到打款后利率
            HelpeOrderModel order = HelpeOrderDAL.GetHelpOrderInfo(hid);
            List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(hid,0);
            using (TransactionScope scope = new TransactionScope())
            {
                //更改状态为已打款
                int rowcount = HelpeOrderDAL.UpdateStatus(hid, 3);
                if (rowcount < 1)
                {
                    return 0;
                }
                //返还排单币更改会员利率为打款后利率
                if (!string.IsNullOrWhiteSpace(value))
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(order.MemberID, decimal.Parse(value),decimal.Parse(inteist));
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    AmountChangeLogModel logmodel = new AmountChangeLogModel();
                    logmodel.MemberID = order.MemberID;
                    logmodel.MemberName = order.MemberName;
                    logmodel.MemberPhone = order.MemberPhone;
                    logmodel.OrderCode = order.OrderCode;
                    logmodel.OrderID = hid;
                    logmodel.ProduceMoney = order.Amount;
                    logmodel.Remark = "会员:" + order.MemberPhone + " 打款完成，返还" + order.Amount.ToString() + "元";
                    rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                }
                else
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberInterest(order.MemberID, decimal.Parse(inteist));
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                }
                //更新匹配接受订单的状态为对方已打款
                if (matchorders.Count > 0)
                {
                    foreach (var item in matchorders)
                    {
                         rowcount = AcceptHelpOrderDAL.UpdateStatus(item.AcceptOrderID,4);
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
        /// 取消提供帮助的订单
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public int UpdateToCancle(int hid)
        {
            int result = 0;
            using(TransactionScope scope=new TransactionScope())
            {
                //更改订单状态

                //返还会员扣除的资金

                scope.Complete();
                result = 1;
            }
            return result;
        }
    }
}
