using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class SysAdminConfigBLL
    {
        private SysAdminConfigDAL dal = new SysAdminConfigDAL();
        /// <summary>
        /// 得到所有的系统配置
        /// </summary>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetAllConfigs()
        {
            return dal.GetAllConfigs();
        }
        /// <summary>
        /// 插入配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddConfigInfo(SysAdminConfigsModel model)
        { return dal.AddConfigInfo(model); }
        /// <summary>
        /// 根据ID得到配置信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetConfigsByIDs(string ids)
        { return dal.GetConfigsByIDs(ids); }
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConfigs(SysAdminConfigsModel model)
        {
            return dal.UpdateConfigs(model);
        }
        /// <summary>
        /// 得到顶级配置项目
        /// </summary>
        /// <returns></returns>
        public List<SysAdminConfigsModel> GetFirstConfigs()
        {
            return dal.GetFirstConfigs();
        }
        /// <summary>
        /// 禁用配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelConfig(int id)
        { return dal.DelConfig(id); }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SysAdminConfigsModel GetSingleSysAdminConfigsModel(int ID)
        {
            return dal.GetSingleSysAdminConfigsModel(ID);
        }
         /// <summary>
        /// 得到所有生效配置的字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllConfigsDic()
        {
            return dal.GetAllConfigsDic();
        }
    }
}
