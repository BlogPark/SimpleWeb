using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class WebSettingsBLL
    {
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int UpdateWebSetting(WebSettingsModel model)
        {
            return WebSettingsDAL.UpdateWebSetting(model);
        }
        /// <summary>
        /// 得到网站的信息
        /// </summary>
        /// <returns></returns>
        public WebSettingsModel GetWebSiteModel()
        {
            return WebSettingsDAL.GetWebSiteModel();
        }
        /// <summary>
        /// 添加新的网站信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddWebSite(WebSettingsModel model)
        {
            return WebSettingsDAL.AddWebSite(model);
        }
        /// <summary>
        /// 关闭网站
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateWebSetting(int id)
        {
            return WebSettingsDAL.CloseWebSite(id);
        }
    }
}
