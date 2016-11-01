using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public  class OrderReportingBLL
    {
        /// <summary>
        /// 添加举报信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReportForHelperDetail(OrderReportingModel model)
        {
            AcceptHelpOrderModel accept = AcceptHelpOrderDAL.GetAcceptOrderInfo(model.OrderID);
            model.OrderCode = accept.OrderCode;
            model.RStatus = 1;
            model.OrderType = 2;            
            return OrderReportingDAL.AddOrderReporting(model);
        }
        /// <summary>
        /// 添加举报信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReportForAcceptDetail(OrderReportingModel model)
        {
            HelpeOrderModel help = HelpeOrderDAL.GetHelpOrderInfo(model.OrderID);
            model.OrderCode = help.OrderCode;
            model.RStatus = 1;
            model.OrderType = 1;
            return OrderReportingDAL.AddOrderReporting(model);
        }
        /// <summary>
        /// 按页查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<OrderReportingModel> GetOrderReportListByPage(OrderReportingModel model,out int totalrowcount)
        {
            return OrderReportingDAL.GetAllOrderReportingForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 取消举报
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateToCancle(int id)
        {
            return OrderReportingDAL.UpdateToCancle(id);
        }
        /// <summary>
        /// 处理完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool UpdateToHandle(int id,string result)
        {
            return OrderReportingDAL.UpdateHandleResult(id,result);
        }
    }
}
