using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class AdminSiteNewsBll
    {
        AdminSiteNewsDal dal = new AdminSiteNewsDal();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddAdminSiteNew(AdminSiteNewsModel model)
        {
            return dal.AddAdminSiteNew(model);
        }
        /// <summary>
        /// 设置紧急条目
        /// </summary>
        public bool SetUrgent(int id, int isurgent)
        {
            return dal.SetUrgent(id, isurgent);
        }
        /// <summary>
        /// 设置置顶条目
        /// </summary>
        public bool SetIsTop(int id, int istop)
        {
            return dal.SetIsTop(id, istop);
        }
        /// <summary>
        /// 更新条目状态
        /// </summary>
        public bool UpdateStatus(int id, int status)
        {
            return dal.UpdateStatus(id, status);
        }
        /// <summary>
        /// 根据ID得到一个对象实体
        /// </summary>
        public AdminSiteNewsModel GetModel(int ID)
        {
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 根据ID得到一个对象实体
        /// </summary>
        public List<AdminSiteNewsModel> GetModelListByUserID(int userid)
        {
            return dal.GetModelListByUserID(userid);
        }
        /// <summary>
        /// 根据ID得到一个对象实体
        /// </summary>
        public List<AdminSiteNewsModel> GetTop3ModelListByUserID(int userid)
        {
            return dal.GetTop3ModelListByUserID(userid);
        }
    }
}
