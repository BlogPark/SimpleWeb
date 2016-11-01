using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.Common;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Controllers
{
    public class CustomOperateController : Controller
    {
        //
        // GET: /CustomOperate/
        private MemberInfoBLL bll = new MemberInfoBLL();
        private OperateLogBLL logbll = new OperateLogBLL();
        private ActiveCodeBLL activecodebll = new ActiveCodeBLL();
        private HelpeOrderBLL hobll = new HelpeOrderBLL();
        private AcceptHelpOrderBLL aobll = new AcceptHelpOrderBLL();
        private AdminSiteNewsBll newsbll = new AdminSiteNewsBll();
        private MemberCapitalDetailBLL cbll = new MemberCapitalDetailBLL();
        private WebSettingsBLL webbll = new WebSettingsBLL();
        private OrderReportingBLL reportbll = new OrderReportingBLL();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 会员间转增激活码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberGife(string phone, int count, int type)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            int result = activecodebll.GiveActiveCodeFromMember(logmember.ID, type, phone, count);
            if (result > 0)
                return Json("1");
            else
                return Json("0");
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult updatepwd(string pwd)
        {
            if (string.IsNullOrWhiteSpace(pwd))
            {
                return Json("0");
            }
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            string newpwd = DESEncrypt.Encrypt(pwd, AppContent.SecrectStr);
            int row = bll.UpdateUserPwd(logmember.ID, newpwd);
            return Json(row);
        }
        /// <summary>
        /// 添加提供帮助
        /// </summary>
        /// <param name="helpactivecode"></param>
        /// <param name="helpamount"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult addhelporder(string helpactivecode, decimal helpamount, string paytype)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            HelpeOrderModel help = new HelpeOrderModel();
            help.ActiveCode = helpactivecode;
            help.Amount = helpamount;
            help.HStatus = 0;
            help.MemberID = logmember.ID;
            help.MemberName = logmember.TruethName;
            help.MemberPhone = logmember.MobileNum;
            help.OrderCode = "H" + getcodenum();
            help.PayType = paytype;
            string result = hobll.AddHelpeOrder(help);
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        /// <summary>
        /// 添加接受帮助
        /// </summary>
        /// <param name="soucetype"></param>
        /// <param name="money"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult addacceptorder(int soucetype, decimal money, string paytype)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            AcceptHelpOrderModel accept = new AcceptHelpOrderModel();
            accept.SourceType = soucetype;
            accept.Amount = money;
            accept.AStatus = 0;
            accept.MemberID = logmember.ID;
            accept.MemberName = logmember.TruethName;
            accept.MemberPhone = logmember.MobileNum;
            accept.OrderCode = "A" + getcodenum();
            accept.PayType = paytype;
            string result = aobll.AddAcceptHelpOrder(accept);
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        /// <summary>
        /// 自动填充排单币
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPromptCode()
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            List<string> codes = activecodebll.GetMemberCodeByCount(2, logmember.ID, 1);
            if (codes.Count == 0)
            {
                return Json(0);
            }
            else
            {
                return Json(codes[0]);
            }
        }
        /// <summary>
        /// 协助激活会员
        /// </summary>
        /// <param name="memberphone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult activemember(string memberphone, string code)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            string result = bll.ActiveMember(0, memberphone, code, false, logmember.ID);
            if (result == "1")
            {
                return Json("1");
            }
            else
            {
                return Json(result.Substring(1));
            }
        }
        /// <summary>
        /// 变更单据为已打款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult paymoney(int id)
        {
            int result = hobll.UpdateToPlayMoney(id);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 变更单据为已打款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult paymoneysplit(int id, int aid)
        {
            int result = hobll.UpdateToPlayMoneyV1(id, aid);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 完成接受帮助订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult finishorder(int id)
        {
            int result = aobll.UpdateStatusToComplete(id);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 完成接受帮助订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult singlefinishorder(int aid, int hid)
        {
            int result = aobll.UpdateSingleOrderToComplete(aid, hid);
            if (result > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 查询子节点
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult getchildnote(int id)
        {
            List<RecommendMap> list = bll.GetRecommendMap(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getmemberinfo(int id)
        {
            MemberInfoModel model = bll.GetModel(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加举报信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reason"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddhelpReporting(int orderid, string reason, string title, string text)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            OrderReportingModel model = new OrderReportingModel();
            model.OrderID = orderid;
            model.MemberID = logmember.ID;
            model.MemberName = logmember.TruethName;
            model.MemberPhone = logmember.MobileNum;
            model.Title = title;
            model.ReportingText = text;
            model.ReasonType = reason;
            int row = reportbll.AddReportForHelperDetail(model);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 添加举报信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reason"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddacceptReporting(int orderid, string reason, string title, string text)
        {
            MemberInfoModel logmember = Session[AppContent.SESSION_WEB_LOGIN] as MemberInfoModel;
            if (logmember == null)
            {
                return RedirectToAction("Index", "Login", new { area = "WebFrontArea" });
            }
            OrderReportingModel model = new OrderReportingModel();
            model.OrderID = orderid;
            model.MemberID = logmember.ID;
            model.MemberName = logmember.TruethName;
            model.MemberPhone = logmember.MobileNum;
            model.Title = title;
            model.ReportingText = text;
            model.ReasonType = reason;
            int row = reportbll.AddReportForAcceptDetail(model);
            if (row > 0)
            {
                return Json("1");
            }
            else
            {
                return Json("0");
            }
        }
        /// <summary>
        /// 组装新的单据编号
        /// </summary>
        /// <returns></returns>
        private string getcodenum()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int re = new Random().Next(1111, 1899);
            string code = DateTime.Now.ToString("HHmmss") + (year + re).ToString() + (month + 87).ToString() + (day + 66).ToString();
            return code;
        }
    }
}
