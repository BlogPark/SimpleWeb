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
        public string AddAcceptHelpOrder(AcceptHelpOrderModel model)
        {
            string result = "0";
            MemberCapitalDetailModel moneydetail = MemberCapitalDetailDAL.GetMemberStaticCapital(model.MemberID);
            int rows = AcceptHelpOrderDAL.GetNotFinishOrderByMember(model.MemberID);
            if (rows > 0)
            {
                return "0还有未完成的接受帮助单据";
            }
            string maxacceptamont = SystemConfigs.GetmaxAcceptAmont();//得到最大的接受帮助限制
            string minacceptamont = SystemConfigs.GetminAcceptAmont();//得到最小的接受帮助限制
            if (model.Amount > maxacceptamont.ParseToDecimal(0))
            {
                return "0超出了平台规定的最大接受值";
            }
            if (model.Amount < minacceptamont.ParseToDecimal(0))
            {
                return "0超出了平台规定的最小接受值";
            }
            using (TransactionScope scope = new TransactionScope())
            {
                //插入表
                int orderid = dal.AddAcceptHelpOrder(model);
                if (orderid < 1)
                {
                    return "0操作失败";
                }
                //扣减会员的相应金额记录      
                int rowcount = 0;
                if (model.SourceType == 1)
                {
                    if (moneydetail.StaticCapital < model.Amount)
                    {
                        return "0操作失败";
                    }
                    rowcount = MemberCapitalDetailDAL.DeductionMemberStaticCapital(model.MemberID, 0 - model.Amount, 0);
                    //清空会员的利率(不判断结果，不排除没有这样单据的可能，因为存在特殊账户)
                    int rowcounts = HelpeOrderDAL.UpdateCurrentInterestToClear(model.MemberID);
                }
                else
                {
                    if (moneydetail.DynamicFunds < model.Amount)
                    {
                        return "0操作失败";
                    }
                    rowcount = MemberCapitalDetailDAL.DeductionMemberDynamicFunds(model.MemberID, 0 - model.Amount, 0);
                }
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                //点击接受帮助后不再为会员计算利息                
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
                    return "0操作失败";
                }
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = model.OrderCode;
                    log.BehaviorSource = 1;
                    log.BehaviorType = 3;
                    log.HOrderCode = "";
                    log.MemberID = model.MemberID;
                    log.MemberName = model.MemberName;
                    log.MemberPhone = model.MemberPhone;
                    log.ProcAmount = model.Amount;
                    log.Remark = "会员：" + model.MemberPhone + "接受帮助单号为：" + model.OrderCode;
                    rowcount = UserBehaviorLogDAL.AddUserBehaviorLog(log);
                }
                catch { }
                scope.Complete();
                result = "1";
            }
            return result;
        }
        /// <summary>
        /// 查询所有的帮助订单
        /// </summary>
        /// <returns></returns>
        public List<AcceptHelpOrderModel> GetAllAcceptOrderList()
        {
            return dal.GetAllAcceptOrderList();
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
        /// 更新状态为已完成（遗弃）
        /// </summary>
        /// <returns></returns>
        public int UpdateStatusToComplete(int aid)
        {
            int result = 0;
            //List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(0, aid);
            List<HelpeOrderModel> matchorderlist = MatchOrderDAL.GetMatchHelpOrderByAid(aid);
            string value = SysAdminConfigDAL.GetConfigsByID(4);//得到注册返还金额 
            string inteistlist = SysAdminConfigDAL.GetConfigsByID(11);//得到领导奖利率
            using (TransactionScope scope = new TransactionScope())
            {
                //更改当前的状态
                int rowcount = AcceptHelpOrderDAL.UpdateStatusToComplete(aid);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更改匹配的提供帮助订单的状态
                #region 注释
                //foreach (var item in matchorders)
                //{
                //    rowcount = HelpeOrderDAL.UpdateStatusForComplete(item.HelperOrderID);
                //    if (rowcount < 1)
                //    {
                //        return 0;
                //    }
                //    HelpeOrderModel order = HelpeOrderDAL.GetHelpOrderInfo(item.HelperOrderID);
                //    //解冻会员的静态冻结资金
                //    rowcount = MemberCapitalDetailDAL.UpdateStaticThawDetail(order.MemberID);
                //    if (rowcount < 1)
                //    {
                //        return 0;
                //    }
                //    //插入会员的资金变动记录
                //    AmountChangeLogModel logmodel = new AmountChangeLogModel();
                //    logmodel.MemberID = order.MemberID;
                //    logmodel.MemberName = order.MemberName;
                //    logmodel.MemberPhone = order.MemberPhone;
                //    logmodel.OrderCode = order.OrderCode;
                //    logmodel.OrderID = item.HelperOrderID;
                //    logmodel.ProduceMoney = 0;
                //    logmodel.Remark = "会员:" + order.MemberPhone + " 所有冻结资金解冻";
                //    logmodel.Type = 5;
                //    rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                //    if (rowcount < 1)
                //    {
                //        return 0;
                //    }
                //} 
                #endregion
                foreach (var item in matchorderlist)
                {
                    rowcount = HelpeOrderDAL.UpdateStatusForComplete(item.ID);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    if (item.DiffAmount == 0)
                    {
                        //返还匹配会员的静态冻结资金和利息
                        rowcount = MemberCapitalDetailDAL.UpdateStaticInterestAndStaticFreezeMoney(item.MemberID, item.Amount, item.Interest);
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                        //返还推荐奖
                        if (item.IsFristOrder == 1)
                        {
                            decimal inster = SysAdminConfigDAL.GetConfigsByID(16).ParseToInt(10);//得到首次推荐的利率
                            decimal money = item.Amount * inster / 100;
                            ReMemberRelationModel model = ReMemberRelationDAL.GetReMemberRelation(item.ID);
                            rowcount = MemberCapitalDetailDAL.UpdateDynamicInterestForComplete(model.MemberID, money);
                            if (rowcount < 1)
                            {
                                return 0;
                            }
                            rowcount = MemberCapitalDetailDAL.UpdateStaticFreezeMoneyForReiger(item.MemberID, value.ParseToDecimal(0));
                        }
                        //返还领导奖
                        rowcount = MemberCapitalDetailDAL.PaymentLeaderPrizeForComplete(item.MemberID, inteistlist, item.OrderCode, item.ID);
                    }
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 更新状态为已完成
        /// </summary>
        /// <returns></returns>
        public int UpdateSingleOrderToComplete(int aid, int hid)
        {
            int result = 0;
            //List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(0, aid);
            HelpeOrderModel helporder = HelpeOrderDAL.GetHelpOrderInfo(hid);//查询提供帮助的单据
            AcceptHelpOrderModel accorder = AcceptHelpOrderDAL.GetAcceptOrderInfo(aid);//查询接受单据的信息
            List<MatchOrderModel> matchinfo = MatchOrderDAL.GetMatchOrderInfo(hid, aid);//单据的匹配信息
            string value = SysAdminConfigDAL.GetConfigsByID(4);//得到注册返还金额 
            string scvalue = SysAdminConfigDAL.GetConfigsByID(6);//得到注册返还金额 
            string inteistlist = SysAdminConfigDAL.GetConfigsByID(11);//得到领导奖利率
            int day = SysAdminConfigDAL.GetConfigsByID(22).ParseToInt(20);//得到领导奖推荐奖冻结天数
            string inteist = SysAdminConfigDAL.GetConfigsByID(7);//得到打款后利率
            using (TransactionScope scope = new TransactionScope())
            {
                //更改当前的接受单据状态和完成金额
                int rowcount = AcceptHelpOrderDAL.UpdateStatusAndMoneyToComplete(aid, matchinfo[0].MatchedMoney);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更改匹配信息
                rowcount = MatchOrderDAL.UpdateStatusToComplete(hid, aid);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更改匹配的提供帮助订单的状态  
                rowcount = HelpeOrderDAL.UpdateStatusForCompleteV1(helporder.ID, matchinfo[0].MatchedMoney);
                if (rowcount < 1)
                {
                    return 0;
                }
                decimal diffamount = (helporder.Amount - helporder.PayedAmount - matchinfo[0].MatchedMoney);
                if (diffamount == 0)
                {
                    //返还匹配会员的静态冻结资金和利息
                    rowcount = MemberCapitalDetailDAL.UpdateStaticInterestAndStaticFreezeMoney(helporder.MemberID, helporder.Amount, helporder.Interest);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    //更改单据利率为高利率
                    rowcount = HelpeOrderDAL.UpdateCurrentInterest(hid, inteist.ParseToInt(2));
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    //更改该单据的利息为0
                    rowcount = HelpeOrderDAL.UpdateHelperOrderClearInterest(helporder.ID);
                    if (rowcount < 1)
                    {
                        return 0;
                    }
                    //返还推荐奖
                    if (helporder.IsFristOrder == 1 && helporder.IsRecommendBack == 0)
                    {
                        decimal inster = SysAdminConfigDAL.GetConfigsByID(16).ParseToInt(10);//得到首次推荐的利率                       
                        decimal money = helporder.Amount * inster / 100;
                        ReMemberRelationModel model = ReMemberRelationDAL.GetReMemberRelation(helporder.MemberID);
                        if (day == 0)//若为0，则当时返还
                        {
                            //返还推荐奖
                            rowcount = MemberCapitalDetailDAL.UpdateDynamicInterestForComplete(model.MemberID, money);
                            if (rowcount < 1)
                            {
                                return 0;
                            }
                            AmountChangeLogModel logmodel = new AmountChangeLogModel();
                            logmodel.MemberID = model.MemberID;
                            logmodel.MemberName = model.MemberTruthName;
                            logmodel.MemberPhone = model.MemberPhone;
                            logmodel.OrderCode = helporder.OrderCode;
                            logmodel.OrderID = helporder.ID;
                            logmodel.ProduceMoney = money;
                            logmodel.Remark = "会员:" + model.MemberPhone + " 得到来自单据：" + helporder.OrderCode + "的推荐奖";
                            logmodel.Type = 3;
                            rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                            if (rowcount < 1)
                            {
                                return result;
                            }
                        }
                        else
                        {
                            //返还推荐奖到待返还列表
                            WaitFreeLeaderAmountModel waitfreemodel = new WaitFreeLeaderAmountModel();
                            waitfreemodel.MemberID = model.MemberID;
                            waitfreemodel.MemberName = model.MemberTruthName;
                            waitfreemodel.MemberPhone = model.MemberPhone;
                            waitfreemodel.Amount = money;
                            waitfreemodel.AStatus = 1;
                            waitfreemodel.TheoryFreeTime = DateTime.Now.AddDays(day);
                            waitfreemodel.Type = 2;
                            waitfreemodel.OrderCode = helporder.OrderCode;
                            waitfreemodel.OrderID = helporder.ID;
                            rowcount = MemberCapitalDetailDAL.AddWaitFreeMoney(waitfreemodel);
                            if (rowcount < 1)
                            {
                                return 0;
                            }
                        }
                        //返还激活码的钱
                        rowcount = MemberCapitalDetailDAL.UpdateStaticFreezeMoneyForReiger(helporder.MemberID, value.ParseToDecimal(0));
                        //更改单据为已经派发推荐奖
                        rowcount = HelpeOrderDAL.UpdateHelperOrderIsRecommendBack(helporder.ID);
                    }
                    if (helporder.IsLeaderBack == 0)
                    {
                        if (day == 0)
                        {
                            //返还领导奖
                            rowcount = MemberCapitalDetailDAL.PaymentLeaderPrizeForComplete(helporder.MemberID, inteistlist, helporder.OrderCode, helporder.ID);
                        }
                        else
                        {
                            //返还领导奖到待返还列表
                            rowcount = MemberCapitalDetailDAL.PaymentLeaderPrizeForComplete(helporder.MemberID, inteistlist, helporder.OrderCode, helporder.ID, day);
                        }
                        //更新单据状态
                        rowcount = HelpeOrderDAL.UpdateHelperOrderIsLeaderBack(helporder.ID);
                    }
                    //返还该单据排单币的金额
                    if (helporder.SchedulingAmount == 0)
                    {
                        rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(helporder.MemberID, scvalue.ParseToDecimal(0), helporder.MemberName, helporder.MemberPhone);
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                        rowcount = HelpeOrderDAL.UpdateHelperOrderIsRecommendBack(helporder.ID, scvalue.ParseToDecimal(0));//更新单据的排单币金额
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                        AmountChangeLogModel logmodel = new AmountChangeLogModel();
                        logmodel.MemberID = helporder.MemberID;
                        logmodel.MemberName = helporder.MemberName;
                        logmodel.MemberPhone = helporder.MemberPhone;
                        logmodel.OrderCode = helporder.OrderCode;
                        logmodel.OrderID = helporder.ID;
                        logmodel.ProduceMoney = scvalue.ParseToDecimal(0);
                        logmodel.Remark = "会员:" + helporder.MemberPhone + " 打款完成，返还排单币" + scvalue + "元";
                        logmodel.Type = 5;
                        rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                        if (rowcount < 1)
                        {
                            return 0;
                        }
                    }

                }
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = accorder.OrderCode;
                    log.BehaviorSource = 1;
                    log.BehaviorType = 5;
                    log.HOrderCode = helporder.OrderCode;
                    log.MemberID = accorder.MemberID;
                    log.MemberName = accorder.MemberName;
                    log.MemberPhone = accorder.MemberPhone;
                    log.ProcAmount = matchinfo[0].MatchedMoney;
                    log.Remark = "会员：" + accorder.MemberPhone + "为单据" + accorder.OrderCode + "确认收款";
                    rowcount = UserBehaviorLogDAL.AddUserBehaviorLog(log);
                }
                catch { }
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
        public int UpdateToCancle(int aid, int ispipei)
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
                    rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(order.MemberID, order.Amount, order.MemberName, order.MemberPhone);
                }
                else if (order.SourceType == 2)//动态资金
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberDynamicFunds(order.MemberID, order.Amount, order.MemberName, order.MemberPhone);
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
                if (ispipei > 0)//若已经匹配则取消对应单据的信息
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
        /// <summary>
        /// 根据接受帮助订单号得到匹配的信息
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public List<HelpOrderExtendInfoModel> GetHelpextendmodels(int hid)
        {
            return AcceptHelpOrderDAL.GetHelpextendmodels(hid);
        }
        /// <summary>
        /// 根据ID查询提供帮助订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AcceptHelpOrderModel GetAcceptOrderInfo(int id, int memberid)
        {
            return AcceptHelpOrderDAL.GetAcceptOrderInfo(id, memberid);
        }

        /// <summary>
        /// 返回系统排单总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalAcceptMoney()
        {
            return AcceptHelpOrderDAL.GetTotalAcceptMoney();
        }
        /// <summary>
        /// 得到当天的提供帮助金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTodayMoney()
        {
            string datastart = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string dataend = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            return AcceptHelpOrderDAL.GetTodayAcceptMoney(datastart, dataend);
        }
    }
}
