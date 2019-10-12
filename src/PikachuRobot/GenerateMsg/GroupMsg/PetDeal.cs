using System;
using System.Data.Entity;
using IServiceSupply;
using Services.PetSystem;
using Services.PikachuSystem;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Command.CusConst;
using StackExchange.Redis;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:21:08
    /// @source : 
    /// @des : 宠物系统
    /// </summary>
    public class PetDeal : IGenerateGroupMsgDeal
    {
        private readonly IDatabase _database;

        public PetDeal(PetService petService, MemberInfoService memberInfoService, UserPetService userPetService,
            IDatabase database)
        {
            _database = database;
            PetService = petService;
            MemberInfoService = memberInfoService;
            UserPetService = userPetService;
        }

        private PetService PetService { get; }
        private MemberInfoService MemberInfoService { get; }
        private UserPetService UserPetService { get; }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            if ("宠物系统".Equals(msg))
            {
                return @"当前宠物系统支持:
    [宠物商店] [宠物道具商店] [查看宠物] [放养宠物]
";
            }

            if ("宠物商店".Equals(msg))
            {
                var list = await PetService.GetAll().OrderByDescending(u => u.Id).ToListAsync();

                if (list.Count == 0)
                {
                    return "宠物商店还没有入驻的宠物呢,请联系管理员添加吧!";
                }

                StringBuilder builder = new StringBuilder();

                builder.AppendLine(" [宠物介绍图] [宠物名称] [宠物描述] [宠物价格]");

                for (int i = 0; i < list.Count; i++)
                {
                    builder.AppendLine(
                        $"{(i + 1).ToString()}. {list[i].Face}  {list[i].Name}  [{list[i].Description}] {list[i].Price}$");
                }

                builder.AppendLine();
                builder.AppendLine(" 试试对我说 [领养宠物] [宠物名称] 吧~");
                builder.AppendLine(" 示例: 领养宠物 小白");

                return builder.ToString();
            }

            Match match;

            if ((match = Regex.Match(msg, @"^领养宠物([\s|\S]*)")).Success)
            {
                if (UserPetService.IsExists(groupNo, account))
                {
                    return GroupRes.GetSingleSuccess(new GroupItemRes() {AtTa = true, Msg = "你已经拥有宠物了，请勿重复领养!"});
                }

                var petName = match.Groups[1].Value.Trim();

                if (string.IsNullOrWhiteSpace(petName))
                {
                    return "宠物名称不能为空!"; 
                }

                var pet = await PetService.GetInfoByNameAsync(petName);

                if (pet == null)
                {
                    return $"宠物-{petName} 不存在!";
                }

                var memberInfo = await MemberInfoService.GetInfoAsync(groupNo, account);

                if (pet.Price > memberInfo.Amount)
                {
                    return $"宠物-{petName}的价格为{pet.Price} , 而您的余额为{memberInfo.Amount} , 不足以支付!";
                }

                // 添加添加宠物标识
                await _database.StringSetAsync(CacheConst.GetMemberOptKey(account, groupNo, CacheConst.AddPet),
                    pet.Id, TimeSpan.FromSeconds(30));

                return GroupRes.GetSingleSuccess(new GroupItemRes()
                {
                    AtTa = true,
                    Msg = @"请输入[宠物姓名]@[宠物性别{男/女}]
示例: 小白@女"
                });
            }

            return null;
        }
    }
}