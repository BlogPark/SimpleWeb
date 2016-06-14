using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public  class HelpeOrderBLL
    {
        private HelpeOrderDAL dal = new HelpeOrderDAL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddHelpeOrder(HelpeOrderModel model)
        {
            return dal.AddHelpeOrder(model);
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
            return dal.GetAllHelpeOrderListForPage(model,out totalrowcount);
        }
        /// <summary>
        /// 添加提供帮助订单
        /// </summary>
        /// <returns></returns>
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
            return dal.UpdateStatus(oid,status);
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
    }
}
