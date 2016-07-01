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
    public class WebSettingsModel
    {

        private int _id;
        /// <summary>
        /// ID
        /// </summary>		
        [DataMember]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _webname;
        /// <summary>
        /// 网站标题
        /// </summary>		
        [DataMember]
        public string WebName
        {
            get { return _webname; }
            set { _webname = value; }
        }
        private string _webdescription;
        /// <summary>
        /// 网站默认描述
        /// </summary>		
        [DataMember]
        public string WebDescription
        {
            get { return _webdescription; }
            set { _webdescription = value; }
        }
        private string _webtype;
        /// <summary>
        /// 网站类型描述
        /// </summary>		
        [DataMember]
        public string WebType
        {
            get { return _webtype; }
            set { _webtype = value; }
        }
        private string _weblogoalt;
        /// <summary>
        /// 网站图标备注
        /// </summary>		
        [DataMember]
        public string WebLogoAlt
        {
            get { return _weblogoalt; }
            set { _weblogoalt = value; }
        }
        private string _weblogo;
        /// <summary>
        /// 网站Logo图片
        /// </summary>		
        [DataMember]
        public string WebLogo
        {
            get { return _weblogo; }
            set { _weblogo = value; }
        }
        private string _webputonrecord;
        /// <summary>
        /// 网站备案号
        /// </summary>		
        [DataMember]
        public string WebPutonrecord
        {
            get { return _webputonrecord; }
            set { _webputonrecord = value; }
        }
        private string _webdefaultkey;
        /// <summary>
        /// 网站默认关键词
        /// </summary>		
        [DataMember]
        public string WebDefaultKey
        {
            get { return _webdefaultkey; }
            set { _webdefaultkey = value; }
        }
        private string _webaddress;
        /// <summary>
        /// 网站联系地址
        /// </summary>		
        [DataMember]
        public string WebAddress
        {
            get { return _webaddress; }
            set { _webaddress = value; }
        }
        private string _webfax;
        /// <summary>
        /// 网站联系传真
        /// </summary>		
        [DataMember]
        public string WebFax
        {
            get { return _webfax; }
            set { _webfax = value; }
        }
        private string _webmobile;
        /// <summary>
        /// 网站联系电话
        /// </summary>		
        [DataMember]
        public string WebMobile
        {
            get { return _webmobile; }
            set { _webmobile = value; }
        }
        private string _webphone;
        /// <summary>
        /// 网站联系固定电话
        /// </summary>		
        [DataMember]
        public string WebPhone
        {
            get { return _webphone; }
            set { _webphone = value; }
        }
        private string _webemail;
        /// <summary>
        /// 网站联系邮箱
        /// </summary>		
        [DataMember]
        public string WebEmail
        {
            get { return _webemail; }
            set { _webemail = value; }
        }
        private string _webaboutus;
        /// <summary>
        /// 网站关于我们描述
        /// </summary>		
        [DataMember]
        public string WebAboutUs
        {
            get { return _webaboutus; }
            set { _webaboutus = value; }
        }
        private int _isopen;
        /// <summary>
        /// 网站是否开启
        /// </summary>		
        [DataMember]
        public int IsOpen
        {
            get { return _isopen; }
            set { _isopen = value; }
        }
        private int _isused;
        /// <summary>
        /// 是否再用
        /// </summary>		
        [DataMember]
        public int IsUsed
        {
            get { return _isused; }
            set { _isused = value; }
        }
        private string _domainname;
        /// <summary>
        /// 网站域名
        /// </summary>		
        [DataMember]
        public string DomainName
        {
            get { return _domainname; }
            set { _domainname = value; }
        }

    }
}
