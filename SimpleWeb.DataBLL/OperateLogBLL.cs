using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class OperateLogBLL
    {
        /// <summary>
        /// 查询会员的资金变动信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="type"></param>
        /// <param name="memberid"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<AmountChangeLogModel> GetAmountChangeLogByPage(int pageindex,int pagesize,int type,int memberid,out int totalrowcount)
        {
            return OperateLogDAL.GetAmountChangeLogByTypeForPage(pageindex,pagesize,type,memberid,out totalrowcount);
        }
        /// <summary>
        /// 查询系统的操作变动信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<UserBehaviorLogModel> GetUserBehaviorLogByPage(UserBehaviorLogModel model,out int totalrowcount)
        {
            return UserBehaviorLogDAL.GetUserBehaviorLogByPage(model,out totalrowcount);
        }
        /// <summary>
        /// 得到最新的资金变动日志
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<AmountChangeLogModel> GetAmountChangeLogByTop(int memberid, int top)
        {
            return OperateLogDAL.GetAmontChangeLogByMemberID(memberid, top);//我的资金变动日志
        }
    }
}
