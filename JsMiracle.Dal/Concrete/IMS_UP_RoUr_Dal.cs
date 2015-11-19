using JsMiracle.Dal.Abstract;
using JsMiracle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsMiracle.Dal.Concrete
{
    public class IMS_UP_RoUr_Dal : DataLayer<IMS_UP_RoUr>, IRoleUser
    {

        public IList<IMS_UP_User> GetUserList(string roleid)
        {

   
            var data = from r in this.DbContext.IMS_UP_RoUrT
                       join u in this.DbContext.IMS_UP_UserT
                       on r.UserID equals u.UserID
                       where r.RoleID == roleid
                       select u;

            // linq 选择数据时候 不能new 已知的对象，只能匿名的。 
            // 但是如果从一个 List 列表 就可以new 已知的类。
            var lst = data.ToList().Select(u=> new IMS_UP_User
                       {                           
                           ID = u.ID,
                           UserID = u.UserID,
                           UserName = string.Format("{0}({1})", u.UserName, u.UserID),
                           //UserName = u.UserName,
                           CreateDate = u.CreateDate,
                           Email = u.Email,
                           LastModDate = u.LastModDate,
                           Password = u.Password
                       }).ToList();

            
            return lst;
        }

        public int SaveRoleUser(string roleid, string userid)
        {
            var ents =
                this.DbContext.IMS_UP_RoUrT.Where(n => n.RoleID == roleid
                                && n.UserID == userid);


            if (ents != null && ents.Count() > 0)
                return 0;


            var ent = new IMS_UP_RoUr()
            {
                RoleID = roleid,
                UserID = userid
            };

            Insert(ent);
            return 0;
        }

        public int RemoveRoleUser(string roleid, string userid)
        {
            var ents =
                this.DbContext.IMS_UP_RoUrT.Where(n => n.RoleID == roleid
                     && n.UserID == userid).ToList();

            if (ents == null || ents.Count == 0)
                return 0;

            int effectRowCount = 0;
            foreach (var u in ents)
            {
                Delete(u.ID);
                effectRowCount++;
            }
            return effectRowCount;
        }
    }
}
