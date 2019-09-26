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
    /// @since : 2019/9/25 11:46:19
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupManageService:BaseService
    {
        public GroupManageService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        public IQueryable<GroupAuth> GetAll()
        {
             return PikachuDataContext.GroupAuths.Where(u => u.Enable);
        }

        public void RemoveGroupAuth(string groupNo, out string msg)
        {

            var old = PikachuDataContext.GroupAuths.FirstOrDefault(u => u.GroupNo.Equals(groupNo));

            if (old != null)
            {
                old.Enable = false;
                old.UpdateTime = DateTime.Now;
                PikachuDataContext.SaveChanges();
            }

            msg = "取消授权成功!";
        }

        public void AddGroupAuth(string groupNo, out string msg)
        {

            var old = PikachuDataContext.GroupAuths.FirstOrDefault(u => u.GroupNo.Equals(groupNo));

            if (old != null)
            {
                old.Enable = true;
                old.UpdateTime = DateTime.Now;
            }
            else
            {
                PikachuDataContext.GroupAuths.Add(new GroupAuth()
                {
                    GroupNo = groupNo,
                    UpdateTime = DateTime.Now,
                    Enable = true
                });
                ;
            }

            PikachuDataContext.SaveChanges();

            msg = "添加授权成功!";
        }

    }
}
