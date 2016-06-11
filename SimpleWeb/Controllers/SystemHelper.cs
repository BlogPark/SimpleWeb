using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class SystemHelper
    {
        /// <summary> 
        /// 个人扩展分页
        /// </summary>
        /// <param name="html"></param>
        /// <param name="Totalpage"></param>
        /// <param name="adtion"></param>
        /// <param name="controller"></param>
        /// <param name="pagecurrent"></param>
        /// <returns></returns>
        public static HtmlString PageExtend(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            Regex re1 = new Regex("_[0-9]{1,16}.html", RegexOptions.IgnoreCase);
            Match mc = re1.Match(redirectTo);
            if (string.IsNullOrWhiteSpace(mc.Value))
            {
                redirectTo = redirectTo.Replace(".html", "_1.html");
            }
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            output.Append(@"<ul class=""pagination"">");
            if (totalPages > 1)
            {
                if (currentPage > 1)
                {//处理上一页的连接
                    string str = "_" + (currentPage - 1).ToString() + ".html";
                    Regex re = new Regex("_[0-9]{1,16}.html", RegexOptions.IgnoreCase);
                    string url = re.Replace(redirectTo, str);
                    output.AppendFormat("<li><a href='{0}'>上一页</a> ", url.Replace("_1.html", ".html"));
                }
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {//一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {//当前页处理                     
                            string str = "_" + currentPage.ToString() + ".html";
                            Regex re = new Regex("_[0-9]{1,16}.html", RegexOptions.IgnoreCase);
                            string url = re.Replace(redirectTo, str);
                            output.AppendFormat("<li class='active'><a >{0}</a></li> ", currentPage);
                        }
                        else
                        {//一般页处理
                            string str = "_" + (currentPage + i - currint).ToString() + ".html";
                            Regex re = new Regex("_[0-9]{1,16}.html", RegexOptions.IgnoreCase);
                            string url = re.Replace(redirectTo, str);
                            output.AppendFormat("<li><a href='{0}'>{1}</a></li> ", url.Replace("_1.html", ".html"), currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接
                    string str = "_" + (currentPage + 1).ToString() + ".html";
                    Regex re = new Regex("_[0-9]{1,16}.html", RegexOptions.IgnoreCase);
                    string url = re.Replace(redirectTo, str);
                    output.AppendFormat("<li><a href='{0}'>下一页</a></li> ", url.Replace("_1.html", ".html"));
                }

                output.Append(" ");
                //if (currentPage != totalPages)
                //{
                //    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a> ", redirectTo, totalPages, pageSize);
                //}
                output.Append(" ");
                output.Append("</ul></div>");
            }
            else
            {
                output.AppendFormat("<li class='active'><a >{0}</a></li> ", 1);
                output.Append(" ");
                output.Append("</ul>");
            }
            //output.AppendFormat("<label>第{0}页 / 共{1}页</label>", currentPage, totalPages);//这个统计加不加都行
            return new HtmlString(output.ToString());

        }
    }
}