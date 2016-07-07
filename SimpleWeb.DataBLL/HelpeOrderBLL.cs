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
        public string AddHelpeOrder(HelpeOrderModel model)
        {
            string result = "0";
            string interest = SysAdminConfigDAL.GetConfigsByID(5);//得到排单后的利率
            string inteistlist = SysAdminConfigDAL.GetConfigsByID(11);//得到领导奖利率
            MemberExtendInfoModel meinfo = MemberExtendInfoDAL.GetMemberExtendInfo(model.MemberID);
            TimeSpan ts1=new TimeSpan(DateTime.Now.Ticks);  
            TimeSpan ts2=new TimeSpan(meinfo.LastHelperTime.Ticks);  
            TimeSpan ts=ts1.Subtract(ts2).Duration();
            if (ts.Days < 1 && ts.Days > -1)
            {
                return "0今天已经提供过帮助";
            }
            ActiveCodeModel activemodel = ActiveCodeDAL.GetActiveCodeExtendModel(model.ActiveCode);
            if (activemodel == null)//没有该激活码信息为失败
            {
                return "0排单币无效";
            }
            if (activemodel.AStatus == 10)//改激活码已使用为失败
            {
                return "0排单币已经被使用";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                //更改激活码的使用状态
                int rowcount = ActiveCodeDAL.UpdateStatus(activemodel.ID, 10);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                rowcount = ActiveCodeDAL.UpdateMemberActiveToUse(model.MemberID, model.OrderCode, model.ActiveCode);
                if (rowcount < 1)
                {
                    return "0操作失败";
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
                    return "0操作失败";
                }
                //更改会员的资金信息和利率
                decimal dinterest = 1;
                if (!string.IsNullOrWhiteSpace(interest))
                {
                    if (!decimal.TryParse(interest, out dinterest))
                    {
                        return "0操作失败";
                    }
                }
                rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(model.MemberID, model.Amount, dinterest,model.MemberName,model.MemberPhone);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                //插入表记录
                int orderid = dal.AddHelpeOrder(model);
                if (orderid < 1)
                {
                    return "0操作失败";
                }
                //更新会员的统计信息
                rowcount = MemberExtendInfoDAL.Update(model.MemberID, model.Amount);
                if (rowcount < 1)
                {
                    return "0操作失败";
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
                logmodel.Type = 1;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                //为推荐人添加领导奖
                rowcount = MemberCapitalDetailDAL.PaymentLeaderPrize(model.MemberID, model.Amount, inteistlist, model.OrderCode, orderid);
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
            string inteistlist = SysAdminConfigDAL.GetConfigsByID(11);//得到领导奖利率
            HelpeOrderModel order = HelpeOrderDAL.GetHelpOrderInfo(hid);
            List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(hid, 0);
            using (TransactionScope scope = new TransactionScope())
            {
                //更改状态为已打款
                int rowcount = HelpeOrderDAL.UpdateStatusandinster(hid, 4, decimal.Parse(inteist));
                if (rowcount < 1)
                {
                    return 0;
                }
                //返还排单币更改会员利率为打款后利率
                if (!string.IsNullOrWhiteSpace(value))
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(order.MemberID, decimal.Parse(value), decimal.Parse(inteist),order.MemberName,order.MemberPhone);
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
                    logmodel.Type = 5;
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
                        rowcount = AcceptHelpOrderDAL.UpdateStatus(item.AcceptOrderID, 4);
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                    }
                }
                //为推荐人增加领导奖（此功能迁移到排单的时候）
                //rowcount = MemberCapitalDetailDAL.PaymentLeaderPrize(order.MemberID, order.Amount, inteistlist, order.OrderCode, order.ID);
                //if (rowcount < 1)
                //{
                //    return 0;
                //}
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 取消提供帮助的订单
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="ispipei">单据是否已经匹配</param>
        /// <returns></returns>
        public int UpdateToCancle(int hid,int ispipei=0)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                //更改订单状态
                int rowcount = HelpeOrderDAL.UpdateStatus(hid, 3);
                if (rowcount < 1)
                {
                    return 0;
                }
                //返还会员追加的资金
                HelpeOrderModel model = HelpeOrderDAL.GetHelpOrderInfo(hid);
                rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(model.MemberID, (0 - model.Amount),model.MemberName,model.MemberPhone);
                if (rowcount < 1)
                {
                    return 0;
                }
                //插入会员资金变动日志
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = model.MemberID;
                logmodel.MemberName = model.MemberName;
                logmodel.MemberPhone = model.MemberPhone;
                logmodel.OrderCode = model.OrderCode;
                logmodel.OrderID = hid;
                logmodel.ProduceMoney = (0 - model.Amount);
                logmodel.Remark = "会员:" + model.MemberPhone + " 取消提供帮助，扣减静态资金 " + model.Amount.ToString() + "元";
                logmodel.Type = 1;
                rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                if (model.Interest > 0)
                {
                    //从账户扣减本次产生的利息
                    rowcount = MemberCapitalDetailDAL.UpdateStaticInterest(model.MemberID, (0 - model.Interest));
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    //插入日志
                    AmountChangeLogModel logmodel1 = new AmountChangeLogModel();
                    logmodel1.MemberID = model.MemberID;
                    logmodel1.MemberName = model.MemberName;
                    logmodel1.MemberPhone = model.MemberPhone;
                    logmodel1.OrderCode = model.OrderCode;
                    logmodel1.OrderID = hid;
                    logmodel1.ProduceMoney = (0 - model.Amount);
                    logmodel1.Remark = "会员:" + model.MemberPhone + " 取消提供帮助，扣减利息 " + model.Interest.ToString() + "元";
                    logmodel.Type = 4;
                    rowcount = OperateLogDAL.AddAmountChangeLog(logmodel1);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                }
                //更新会员统计信息
                rowcount = MemberExtendInfoDAL.CancleHelperOrder(model.MemberID,hid);
                if (rowcount < 1)
                {
                    return 0;
                }
                if (ispipei == 1)//该单已经匹配
                {
                    List<MatchOrderModel> matchs = MatchOrderDAL.GetMatchOrderInfo(hid, 0);
                    rowcount = MatchOrderDAL.UpdateStatus(hid, 0);//更改状态为取消
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    foreach (var item in matchs)//更改接受帮助订单的状态
                    {
                        rowcount = AcceptHelpOrderDAL.CancleOrderForHelp(item.AcceptOrderID, item.MatchedMoney);
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
        /// 根据ID得到单据的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HelpeOrderModel GetHelpordermodel(int id)
        {
            return HelpeOrderDAL.GetHelpOrderInfo(id);
        }
        /// <summary>
        /// 根据提供帮助订单号得到匹配的信息
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public List<AcceptExtendInfoModel> GetAcceptextendmodels(int hid)
        {
            return HelpeOrderDAL.GetAcceptextendmodels(hid);
        }
         /// <summary>
        /// 根据ID和会员查询提供帮助订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  HelpeOrderModel GetHelpOrderInfo(int id, int memberid)
        {
            return HelpeOrderDAL.GetHelpOrderInfo(id,memberid);
        }
    }
}
