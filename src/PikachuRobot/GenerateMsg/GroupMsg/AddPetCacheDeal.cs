using GenerateMsg.CusConst;
using IServiceSupply;
using Services.PetSystem;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.PetSystem.Menu;
using Data.Pikachu.Menu;
using Services.PikachuSystem;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/8 11:00:22
    /// @source : 
    /// @des : 
    /// </summary>
    public class AddPetCacheDeal : IGenerateGroupMsgDeal
    {
        private readonly IDatabase _database;

        public AddPetCacheDeal(IDatabase database, UserPetService userPetService, MemberInfoService memberInfoService,
            PetService petService, BillFlowService billFlowService)
        {
            this._database = database;
            UserPetService = userPetService;
            MemberInfoService = memberInfoService;
            PetService = petService;
            BillFlowService = billFlowService;
        }

        private UserPetService UserPetService { get; }
        private MemberInfoService MemberInfoService { get; }
        private PetService PetService { get; }
        private BillFlowService BillFlowService { get; }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            var key = CacheConst.GetMemberOptKey(account, groupNo, CacheConst.AddPet);

            var cache = await _database.StringGetAsync(key);

            if (int.TryParse(cache, out var petId))
            {
                await _database.KeyDeleteAsync(key);

                var arr = msg.Split('@');

                // 格式错误直接跳过
                if (arr.Length != 2 || arr[1].Length != 1 || string.IsNullOrWhiteSpace(arr[0])) return null;

                var pet = await PetService.GetAsync(petId);

                Gender sex;

                if ("男".Equals(arr[1])) sex = Gender.MALE;
                else if ("女".Equals(arr[1])) sex = Gender.FAMALE;
                else return "性别错误！";

                if (pet == null)
                    return "宠物已下架!";

                // 记录流水
                await BillFlowService.AddBillAsync(groupNo, account, pet.Price, pet.Price, BillTypes.Consume, "领养宠物");

                // 修正账户
                await MemberInfoService.ChangeAmountAsync(groupNo, account, -pet.Price);

                // 添加宠物
                var userPet = await UserPetService.AddPetAsync(pet, groupNo, account, sex, arr[0]);

                return GroupRes.GetSuccess(new GroupItemRes()
                    {AtTa = true, Msg = $"恭喜您获得一只{userPet.Quality}品质的{pet.Name}"});
            }

            return null;
        }
    }
}