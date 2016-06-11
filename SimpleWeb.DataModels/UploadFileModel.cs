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
    public class UploadFileModel
    {
        /// <summary>
        /// 上传状态
        /// </summary>
        [DataMember]
        public string status { get; set; }
        /// <summary>
        /// 上传信息
        /// </summary>
        [DataMember]
        public string message { get; set; }
        /// <summary>
        /// 文件宽度
        /// </summary>
        [DataMember]
        public int width { get; set; }
        /// <summary>
        /// 文件高度
        /// </summary>
        [DataMember]
        public int height { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [DataMember]
        public string filename { get; set; }
        /// <summary>
        /// 文件的物理地址
        /// </summary>
        [DataMember]
        public string filepath { get; set; }
        /// <summary>
        /// 文件的网络地址
        /// </summary>
        [DataMember]
        public string fileurlpath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public float filesize { get; set; }
    }
}
