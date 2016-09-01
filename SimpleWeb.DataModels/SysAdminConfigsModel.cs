using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWeb.DataModels
{
    [Serializable]
    [DataContract]
    public class SysAdminConfigsModel
    {
        #region 原生字段
        /// <summary>
        /// ID
        /// </summary>       
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 配置项名称
        /// </summary>       
        [DataMember]
        public string ConfigName { get; set; }
        /// <summary>
        /// 配置项父级
        /// </summary>       
        [DataMember]
        public int ConfigFID { get; set; }
        /// <summary>
        /// 配置项值
        /// </summary>       
        [DataMember]
        public string ConfigValue { get; set; }
        /// <summary>
        /// 配置项备注
        /// </summary>       
        [DataMember]
        public string ConfigRemark { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>       
        [DataMember]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 配置项状态(1 启用 0禁用)
        /// </summary>       
        [DataMember]
        public int ConfigStatus { get; set; }
        /// <summary>
        /// 是否指定用户可见
        /// </summary>
        [DataMember]
        public int IsAdmin { get; set; }
        #endregion
        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember]
        public string ConfigStatusName { get; set; }
    }
}
