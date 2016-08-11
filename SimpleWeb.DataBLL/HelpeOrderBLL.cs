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
            decimal reinteist = SysAdminConfigDAL.GetConfigsByID(16).ParseToDecimal(10);//得到首次推荐的利率
            decimal maxcount = SysAdminConfigDAL.GetConfigsByID(17).ParseToInt(1);//得到每日最大排单数
            MemberExtendInfoModel meinfo = MemberExtendInfoDAL.GetMemberExtendInfo(model.MemberID);
            //if (meinfo != null)
            //{
            //    TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            //    TimeSpan ts2 = new TimeSpan(meinfo.LastHelperTime.Ticks);
            //    TimeSpan ts = ts1.Subtract(ts2).Duration();
            //    if (ts.Days < 1 && ts.Days > -1)
            //    {
            //        return "0今天已经提供过帮助";
            //    }
            //}
            int helpcount = HelpeOrderDAL.GetTodayHelpCount(model.MemberID);
            if (helpcount >= maxcount)
            {
                return "0今天已经提供过帮助";
            }
            bool isfirst = false;//默认不是第一次
            if (meinfo == null)
            {
                isfirst = true;
            }
            else
            {
                isfirst = meinfo.MemberHelpCount == 0;
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
            ReMemberRelationModel remember = ReMemberRelationDAL.GetReMemberRelation(model.MemberID);
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
                //rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(model.MemberID, model.Amount, dinterest,model.MemberName,model.MemberPhone);
                rowcount = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoneyAndinster(model.MemberID, model.Amount, dinterest, model.MemberName, model.MemberPhone);
                if (rowcount < 1)
                {
                    return "0操作失败";
                }
                //插入表记录
                model.IsFristOrder = isfirst ? 1 : 0;
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
                //若此会员第一次排单，则给推荐人奖励
                if (isfirst)
                {
                    rowcount = MemberCapitalDetailDAL.UpdateMemberDynamicFreezeMoney(remember.MemberID, (model.Amount * reinteist / 100), remember.MemberTruthName, remember.MemberPhone);
                    if (rowcount < 1)
                    {
                        return "0操作失败";
                    }
                    LeaderAmountModel leadermodel = new LeaderAmountModel();//插入领导奖记录
                    leadermodel.OrderID = orderid;
                    leadermodel.OrderCode = model.OrderCode;
                    leadermodel.MemberPhone = model.MemberPhone;
                    leadermodel.MemberName = model.MemberName;
                    leadermodel.MemberID = model.MemberID;
                    leadermodel.Ltype = 1;
                    leadermodel.Amount = (model.Amount * reinteist / 100);
                    rowcount = MemberCapitalDetailDAL.AddLeaderAmount(leadermodel);
                    if (rowcount < 1)
                    {
                        return "0操作失败";
                    }
                    AmountChangeLogModel logmodel1 = new AmountChangeLogModel();
                    logmodel1.MemberID = remember.MemberID;
                    logmodel1.MemberName = remember.MemberTruthName;
                    logmodel1.MemberPhone = remember.MemberPhone;
                    logmodel1.OrderCode = model.OrderCode;
                    logmodel1.OrderID = orderid;
                    logmodel1.ProduceMoney = (model.Amount * reinteist / 100);
                    logmodel1.Remark = "会员:" + remember.MemberPhone + " 得到来自 " + model.MemberPhone + "的首单推荐奖" + (model.Amount * reinteist / 100).ToString() + "元";
                    logmodel1.Type = 3;
                    rowcount = OperateLogDAL.AddAmountChangeLog(logmodel1);
                    if (rowcount < 1)
                    {
                        return "0操作失败";
                    }
                }
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = "";
                    log.BehaviorSource = 1;
                    log.BehaviorType = 2;
                    log.HOrderCode = model.OrderCode;
                    log.MemberID = model.MemberID;
                    log.MemberName = model.MemberName;
                    log.MemberPhone = model.MemberPhone;
                    log.ProcAmount = model.Amount;
                    log.Remark = "会员：" + model.MemberPhone + "提供帮助单号为：" + model.OrderCode;
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
                //更改状态为已打款和更改利率为已打款后的利率
                int rowcount = HelpeOrderDAL.UpdateStatusandinster(hid, 4, decimal.Parse(inteist));
                if (rowcount < 1)
                {
                    return 0;
                }
                //返还排单币更改会员利率为打款后利率
                //if (!string.IsNullOrWhiteSpace(value))
                //{
                //    rowcount = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoney(order.MemberID, decimal.Parse(value), order.MemberName, order.MemberPhone);
                //    if (rowcount < 1)
                //    {
                //        return 0;
                //    }
                //AmountChangeLogModel logmodel = new AmountChangeLogModel();
                //logmodel.MemberID = order.MemberID;
                //logmodel.MemberName = order.MemberName;
                //logmodel.MemberPhone = order.MemberPhone;
                //logmodel.OrderCode = order.OrderCode;
                //logmodel.OrderID = hid;
                //logmodel.ProduceMoney = order.Amount;
                //logmodel.Remark = "会员:" + order.MemberPhone + " 打款完成，返还" + order.Amount.ToString() + "元";
                //logmodel.Type = 5;
                //rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                //}
                //else
                //{
                //    rowcount = MemberCapitalDetailDAL.UpdateMemberInterest(order.MemberID, decimal.Parse(inteist));
                //    if (rowcount < 1)
                //    {
                //        return 0;
                //    }
                //}
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
                scope.Complete();
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 更新提供帮助订单为已打款
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public int UpdateToPlayMoneyV1(int hid, int aid)
        {
            int result = 0;
            string value = SysAdminConfigDAL.GetConfigsByID(6);//得到打款后返还金额
            string inteist = SysAdminConfigDAL.GetConfigsByID(7);//得到打款后利率
            string inteistlist = SysAdminConfigDAL.GetConfigsByID(11);//得到领导奖利率
            HelpeOrderModel order = HelpeOrderDAL.GetHelpOrderInfo(hid);
            List<MatchOrderModel> matchorders = MatchOrderDAL.GetMatchOrderInfo(hid, aid);
            using (TransactionScope scope = new TransactionScope())
            {
                //更改状态为已打款（此处不在变更利率为打款后利率）
                //int rowcount = HelpeOrderDAL.UpdateStatusandinster(hid, 4, decimal.Parse(inteist));
                int rowcount = HelpeOrderDAL.UpdateStatus(hid, 4);
                if (rowcount < 1)
                {
                    return 0;
                }
                rowcount = MatchOrderDAL.UpdateStatusToPayed(hid, aid);
                if (rowcount < 1)
                {
                    return 0;
                }
                //更新匹配接受订单的状态为对方已打款
                rowcount = AcceptHelpOrderDAL.UpdateStatus(aid, 4);
                if (rowcount < 1)
                {
                    return 0;
                }
                try
                {
                    UserBehaviorLogModel log = new UserBehaviorLogModel();
                    log.AOrderCode = matchorders[0].AcceptOrderCode;
                    log.BehaviorSource = 1;
                    log.BehaviorType = 4;
                    log.HOrderCode = matchorders[0].HelperOrderCode;
                    log.MemberID = order.MemberID;
                    log.MemberName = order.MemberName;
                    log.MemberPhone = order.MemberPhone;
                    log.ProcAmount = matchorders[0].MatchedMoney;
                    log.Remark = "会员：" + order.MemberPhone + " 变更单据为打款";
                    rowcount = UserBehaviorLogDAL.AddUserBehaviorLog(log);
                }
                catch { }
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
        public int UpdateToCancle(int hid, int ispipei = 0)
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
                rowcount = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoneyAndinster(model.MemberID, (0 - model.Amount), 1, model.MemberName, model.MemberPhone);
                //rowcount = MemberCapitalDetailDAL.UpdateMemberStaticCapital(model.MemberID, (0 - model.Amount), model.MemberName, model.MemberPhone);
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
                logmodel.Remark = "会员:" + model.MemberPhone + " 取消提供帮助，扣减静态冻结资金 " + model.Amount.ToString() + "元";
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
                rowcount = MemberExtendInfoDAL.CancleHelperOrder(model.MemberID, hid);
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
        public HelpeOrderModel GetHelpOrderInfo(int id, int memberid)
        {
            return HelpeOrderDAL.GetHelpOrderInfo(id, memberid);
        }
        /// <summary>
        /// 返回系统排单总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalHelpMoney()
        {
            return HelpeOrderDAL.GetTotalHelpMoney();
        }
        /// <summary>
        /// 得到当天的提供帮助金额
        /// </summary>
        /// <returns></returns>
        public decimal GetTodayMoney()
        {
            string datastart = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string dataend = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            return HelpeOrderDAL.GetTodayHelpMoney(datastart, dataend);
        }
    }
}
