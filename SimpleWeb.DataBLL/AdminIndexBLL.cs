using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class AdminIndexBLL
    {
        public AdminIndexModels GetDefaultData()
        {
            //decimal staticnum = MemberCapitalDetailDAL.GetTotalAmontForPlant(out dynamicnum);
            AdminIndexModels model = new AdminIndexModels();
            model.ActiveCodeCount = ActiveCodeDAL.GetTotalCount(1);//全部激活码数量
            model.ActiveMemberCount = MemberInfoDAL.GetTotalMemberCount(2);
            model.PaidanCodeCount = ActiveCodeDAL.GetTotalCount(2);//全部排单币数量
            model.TotalAcceptAmont = AcceptHelpOrderDAL.GetTotalAcceptMoney();
            model.TotalHelpAmont = HelpeOrderDAL.GetTotalHelpMoney();
            model.TotalMemberCount = MemberInfoDAL.GetTotalMemberCount(1);
            return model;
        }
    }
}
