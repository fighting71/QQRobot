using Data.Pikachu;
using Data.Pikachu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 11:54:51
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgCopyService : BaseService
    {
        public GroupMsgCopyService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        public IQueryable<GroupMsgCopy> GetByAccount(string loginQq)
        {
            return PikachuDataContext.GroupMsgCopys.Where(u => u.Person.Equals(loginQq));
        }

        public void RemoveGroupAuth(string fromGroup, string targetGroup, string dealPerson,out string msg)
        {
            var old = PikachuDataContext.GroupMsgCopys.FirstOrDefault(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            PikachuDataContext.Entry(old).State = System.Data.Entity.EntityState.Deleted;

            PikachuDataContext.SaveChanges();

            msg = "删除转载成功!";
        }

        public void AddGroupAuth(string fromGroup, string targetGroup, string dealPerson, out string msg)
        {
            var old = PikachuDataContext.GroupMsgCopys.FirstOrDefault(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            if (old != null)
            {
                msg = "已存在转载设置";
            }
            else
            {
                PikachuDataContext.GroupMsgCopys.Add(new GroupMsgCopy()
                {
                    FromGroup = fromGroup,
                    TargetGroup = targetGroup,
                    Person = dealPerson
                });
                ;
            }

            PikachuDataContext.SaveChanges();

            msg = "添加转载设置成功!";
        }

    }
}
