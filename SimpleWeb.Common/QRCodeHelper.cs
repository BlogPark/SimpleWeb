using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace SimpleWeb.Common
{
    public static class QRCodeHelper
    {
        /// <summary>
        /// 创建二维码图片
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] CreateBitMap(string text)
        {
            return QRCodeHelper.CreateBitMap(text);
        }
    }
}
