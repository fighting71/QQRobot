using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.PetSystem;
using Data.PetSystem.Menu;
using Data.PetSystem.Models;

namespace Services.PetSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/8 10:25:06
    /// @source : 
    /// @des : 
    /// </summary>
    public class UserPetService : BaseService
    {
        private Random _random = new Random();

        public UserPetService(PetContext petContext) : base(petContext)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsExists(string group, string account)
        {
            return PetContext.UserPets.Any(u => u.Enable && u.Group == group && u.Account == account);
        }

        /// <summary>
        /// 随机宠物品质
        /// </summary>
        /// <returns></returns>
        private int RandQuality()
        {
            return _random.Next(5) + 1;
        }

        /// <summary>
        /// 根据品质增长属性
        /// </summary>
        /// <param name="userPet"></param>
        private UserPet IncrementPropByQuality(UserPet userPet)
        {
            userPet.Attack = userPet.Quality + _random.Next(userPet.Quality * 10);
            userPet.Intellect = userPet.Quality + _random.Next(userPet.Quality * 5);
            return userPet;
        }
        
        /// <summary>
        /// 添加宠物
        /// </summary>
        /// <param name="info"></param>
        /// <param name="group"></param>
        /// <param name="account"></param>
        /// <param name="sex"></param>
        /// <param name="nickName"></param>
        public async Task<UserPet> AddPetAsync(PetInfo info, string group, string account, Gender sex,string nickName)
        {
            
            // 初始化宠物信息
            var pet = new UserPet()
            {
                Account = account,
                Age = 1,
                Attack = info.Attack,
                Enable = true,
                Face = info.Face,
                Healthy = 100,
                Group = group,
                Intellect = info.Intellect,
                Mood = 100,
                Quality = RandQuality(),
                Sex = sex,
                PetId = info.Id,
                Name = info.Name,
                NickName = nickName
            };
            
            // 根据品质增长属性值
            IncrementPropByQuality(pet);

            // 添加信息
            PetContext.UserPets.Add(pet);

            // 保存信息
            await PetContext.SaveChangesAsync();
            
            return pet;

        }
    }
}