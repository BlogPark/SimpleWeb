using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleWeb.DataDAL;
using SimpleWeb.DataModels;

namespace SimpleWeb.DataBLL
{
    public class ActiveCodeBLL
    {
        private ActiveCodeDAL dal = new ActiveCodeDAL();
        /// <summary>
        /// 产生新的激活码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ProduceActiveCode(List<ActiveCodeModel> list)
        {
            return dal.ProduceActiveCode(list);
        }
        /// <summary>
        /// 产生新的激活码
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ProduceActiveCode(int count, int type)
        {
            List<ActiveCodeModel> list = new List<ActiveCodeModel>();
            for (int i = 0; i < count; i++)
            {
                ActiveCodeModel model = new ActiveCodeModel();
                model.ActivationCode = Guid.NewGuid().ToString("N").ToUpper();
                model.AType = type;
                list.Add(model);
            }
            return dal.ProduceActiveCode(list);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeModel> GetActiveCodeListForPage(ActiveCodeModel model, out int totalrowcount)
        {
            return dal.GetActiveCodeListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int UpdateStatus(List<string> codes)
        {
            return dal.UpdateStatus(codes);
        }
        /// <summary>
        /// 得到状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetStatus(string code)
        {
            return dal.GetStatus(code);
        }
        /// <summary>
        /// 分配激活码
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="memberphone"></param>
        /// <returns></returns>
        public int AssignedCode(List<string> codes, string memberphone)
        {
            return dal.AssignedCode(codes, memberphone);
        }

        /// <summary>
        /// 分配激活码
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="memberphone"></param>
        /// <returns></returns>
        public int AssignedMoreCode(int count, int type, string memberphone)
        {
            List<string> codes = ActiveCodeDAL.GetTypeCountActiveCode(type,count);
            return dal.AssignedCode(codes, memberphone);
        }

        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberActiveCodeModel> GetMemberActiveCodeListForPage(MemberActiveCodeModel model, out int totalrowcount)
        {
            return dal.GetMemberActiveCodeListForPage(model, out totalrowcount);
        }
        /// <summary>
        /// 修改会员激活码状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateMemberActive(int id)
        {
            return dal.UpdateMemberActive(id);
        }
        /// <summary>
        /// 得到会员的激活码使用信息(分页)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeLogModel> GetActiveCodeLogListForPage(int memberid, int pageindex, int pagesize, out int totalrowcount)
        {
            return dal.GetActiveCodeLogListForPage(memberid, pageindex, pagesize, out totalrowcount);
        }
        /// <summary>
        /// 得到会员的分类激活码信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<MemberActiveCodeModel> GetMemberActiveCodeListForPage(int memberid, int typeid, int pageindex, int pagesize, out int totalrowcount)
        {
            return dal.GetMemberActiveCodeListForPage(memberid, typeid, pageindex, pagesize, out totalrowcount);
        }
        /// <summary>
        /// 会员间赠送激活码
        /// </summary>
        /// <param name="soucememberID">原始会员ID</param>
        /// <param name="type">赠送激活码类型</param>
        /// <param name="acceptMemberPhone">接受会员电话</param>
        /// <param name="count">赠送数量</param>
        /// <returns>返回值（-1 接受会员不存在 -2 没有足够的激活币赠送 0 数据库操作失败 1 成功）</returns>
        public int GiveActiveCodeFromMember(int soucememberID, int type, string acceptMemberPhone, int count)
        {
            return dal.GiveActiveCodeFromMember(soucememberID, type, acceptMemberPhone, count);
        }
        /// <summary>
        /// 根据会员的ID读取特定数量的特定类型激活码
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string> GetMemberCodeByCount(int type, int memberid, int count)
        {
            return ActiveCodeDAL.GetMemberCodeByCount(type,memberid,count);
        }
        /// <summary>
        /// 查询激活码的记录
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalrowcount"></param>
        /// <returns></returns>
        public List<ActiveCodeLogModel> GetActiveCodeLogForPage(int memberid,int pageindex,int pagesize,out int totalrowcount)
        {
            return ActiveCodeDAL.GetActiveCodeLogForPage(memberid,pageindex,pagesize,out totalrowcount);
        }
    }
}
