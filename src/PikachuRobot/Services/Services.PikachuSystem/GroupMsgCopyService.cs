using Data.Pikachu;
using Data.Pikachu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public Task<List<GroupMsgCopy>> GetList(string account,string fromGroup)
        {
            return PikachuDataContext.GroupMsgCopys.Where(u =>
                u.FromGroup.Equals(fromGroup) && u.Person.Equals(account)).ToListAsync();
        }

        public void RemoveGroupCopy(string fromGroup, string targetGroup, string dealPerson,out string msg)
        {
            var old = PikachuDataContext.GroupMsgCopys.FirstOrDefault(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            PikachuDataContext.Entry(old).State = System.Data.Entity.EntityState.Deleted;

            PikachuDataContext.SaveChanges();

            msg = "删除转载成功!";
        }

        public void AddGroupCopy(string fromGroup, string targetGroup, string dealPerson, out string msg)
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

        /// <summary>
        /// 删除群转载
        /// </summary>
        /// <param name="fromGroup"></param>
        /// <param name="targetGroup"></param>
        /// <param name="dealPerson"></param>
        /// <returns></returns>
        public async Task RemoveGroupCopyAsync(string fromGroup, string targetGroup, string dealPerson)
        {
            var old = await PikachuDataContext.GroupMsgCopys.FirstOrDefaultAsync(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            if (old != null)
            {
                PikachuDataContext.Entry(old).State = EntityState.Deleted;

                await PikachuDataContext.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// 添加群转载
        /// </summary>
        /// <param name="fromGroup"></param>
        /// <param name="targetGroup"></param>
        /// <param name="dealPerson"></param>
        /// <returns></returns>
        public async Task AddGroupCopyAsync(string fromGroup, string targetGroup, string dealPerson)
        {
            var old = await PikachuDataContext.GroupMsgCopys.FirstOrDefaultAsync(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            if (old == null)
            {
                PikachuDataContext.GroupMsgCopys.Add(new GroupMsgCopy()
                {
                    FromGroup = fromGroup,
                    TargetGroup = targetGroup,
                    Person = dealPerson
                });
                await PikachuDataContext.SaveChangesAsync();
            }

        }
        
    }
}
