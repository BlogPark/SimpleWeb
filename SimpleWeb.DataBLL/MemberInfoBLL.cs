﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class MemberInfoBLL
    {
        private MemberInfoDAL dal = new MemberInfoDAL();
        /// <summary>
        ///注册会员（已追加注册返还金额功能）
        /// </summary>
        public int AddMemberInfo(MemberInfoModel model)
        {
            int result = 0;
            string value = SysAdminConfigDAL.GetConfigsByID(4);//得到注册返还金额            
            using (TransactionScope scope = new TransactionScope())
            {
                int memberid = dal.AddMemberInfo(model);
                if (memberid < 1)
                {
                    return 0;
                }
                decimal amont = 0;
                if (!string.IsNullOrWhiteSpace(value))
                {

                    if (!decimal.TryParse(value, out amont))
                    {
                        return 0;
                    }
                    int row = MemberCapitalDetailDAL.UpdateMemberStaticFreezeMoney(memberid, amont);
                    if (row < 1)
                    {
                        return 0;
                    }
                }
                AmountChangeLogModel logmodel = new AmountChangeLogModel();
                logmodel.MemberID = memberid;
                logmodel.MemberName = model.TruethName;
                logmodel.MemberPhone = model.MobileNum;
                logmodel.ProduceMoney = amont;
                logmodel.Remark = "会员注册赠送" + amont.ToString() + "元";
                int rowcount = OperateLogDAL.AddAmountChangeLog(logmodel);
                if (rowcount < 1)
                {
                    return 0;
                }
                scope.Complete();
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateMemberInfo(MemberInfoModel model)
        {
            return dal.UpdateMemberInfo(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int ID)
        {
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateStatus(int mid, int status)
        {
            return dal.UpdateStatus(mid, status);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetMemberInfoListForPage(MemberInfoModel model, out int totalrowcount)
        {
            return dal.GetMemberInfoListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 得到行政区域列表
        /// </summary>
        /// <param name="parentid">父级ID</param>
        /// <returns></returns>
        public List<ReginTableModel> GetReginTableListModel(int parentid)
        {
            return dal.GetReginTableListModel(parentid);
        }
        /// <summary>
        /// 得到会员的直荐名单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<ReMemberRelationModel> GetRecommdMemberModel(int rmid)
        {
            return dal.GetRecommdMemberModel(rmid);
        }
    }
}
