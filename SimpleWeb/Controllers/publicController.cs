using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleWeb.DataBLL;
using SimpleWeb.DataModels;

namespace SimpleWeb.Controllers
{
    public class publicController : Controller
    {
        //
        // GET: /public/
        private MemberInfoBLL bll = new MemberInfoBLL();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult obtainreagin(int id)
        {
            List<ReginTableModel> list = bll.GetReginTableListModel(id);
            return Json(list);
        }
    }
}
