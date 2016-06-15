using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class ActiveCodeBLL
    {
        private ActiveCodeDAL dal = new ActiveCodeDAL();
        /// <summary>
        /// 产生新的激活码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ProduceActiveCode(List<ActiveCodeModel> list)
        {
            return dal.ProduceActiveCode(list);
        }
         /// <summary>
        /// 产生新的激活码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ProduceActiveCode(int count, int type)
        {
            List<ActiveCodeModel> list = new List<ActiveCodeModel>();
            for (int i = 0; i < count; i++)
            {
                ActiveCodeModel model = new ActiveCodeModel();
                model.ActivationCode = Guid.NewGuid().ToString("N").ToUpper().Substring(0,16);
                model.AType = type;
                list.Add(model);
            }
            return dal.ProduceActiveCode(list);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeModel> GetActiveCodeListForPage(ActiveCodeModel model, out int totalrowcount)
        {
            return dal.GetActiveCodeListForPage(model,out totalrowcount);
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStatus(List<string> codes)
        {
            return dal.UpdateStatus(codes);
        }
        /// <summary>
        /// 得到状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetStatus(string code)
        {
            return dal.GetStatus(code);
        }
        /// <summary>
        /// 分配激活码
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="memberphone"></param>
        /// <returns></returns>
        public int AssignedCode(List<string> codes, string memberphone)
        {
            return dal.AssignedCode(codes,memberphone);
        }
    }
}
